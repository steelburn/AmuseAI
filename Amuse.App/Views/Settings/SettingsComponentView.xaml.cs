using Amuse.App.Common;
using Amuse.App.Services;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using TensorStack.WPF;
using TensorStack.WPF.Controls;
using TensorStack.WPF.Services;

namespace Amuse.App.Views
{
    /// <summary>
    /// Interaction logic for SettingsComponentView.xaml
    /// </summary>
    public partial class SettingsComponentView : ViewBase
    {
        private ComponentModel _selectedModel;
        private string _filterText;
        private string _filterStatus;

        public SettingsComponentView(Settings settings, NavigationService navigationService, IModelDownloadService downloadService, IHistoryService historyService, ILogger<SettingsComponentView> logger)
            : base(settings, navigationService, downloadService, historyService, logger)
        {
            FilterStatuses = ["Show All", .. Enum.GetNames<ModelStatusType>()];
            FilterStatus = FilterStatuses[0];
            DeleteModelCommand = new AsyncRelayCommand(DeleteModelAsync, () => SelectedModel is not null);
            OpenModelCommand = new AsyncRelayCommand(OpenModelAsync, () => SelectedModel is not null);
            DownloadModelCommand = new AsyncRelayCommand(DownloadModelAsync);
            DownloadModelCancelCommand = new AsyncRelayCommand(DownloadModelCancelAsync);
            FilterClearCommand = new AsyncRelayCommand(FilterClearAsync, CanClearFilter);
            ModelCollection = new ListCollectionView(settings.Components) { Filter = CollectionFilter(), IsLiveSorting = true };
            ModelCollection.SortDescriptions.Add(new SortDescription(nameof(ComponentModel.Backend), ListSortDirection.Ascending));
            ModelCollection.SortDescriptions.Add(new SortDescription(nameof(ComponentModel.Name), ListSortDirection.Ascending));
            ModelCollection.MoveCurrentToFirst();
            SelectedModel = ModelCollection.CurrentItem as ComponentModel;
            InitializeComponent();
        }

        public override View View => View.Component;
        public AsyncRelayCommand DeleteModelCommand { get; }
        public AsyncRelayCommand OpenModelCommand { get; }
        public AsyncRelayCommand DownloadModelCommand { get; }
        public AsyncRelayCommand DownloadModelCancelCommand { get; }
        public AsyncRelayCommand FilterClearCommand { get; }
        public ListCollectionView ModelCollection { get; }
        public string[] FilterStatuses { get; }

        public ComponentModel SelectedModel
        {
            get { return _selectedModel; }
            set { SetProperty(ref _selectedModel, value); }
        }

        public string FilterText
        {
            get { return _filterText; }
            set { SetProperty(ref _filterText, value); ModelCollection?.Refresh(); }
        }

        public string FilterStatus
        {
            get { return _filterStatus; }
            set { SetProperty(ref _filterStatus, value); ModelCollection?.Refresh(); }
        }


        public override Task OpenAsync(OpenViewArgs args = null)
        {
            return base.OpenAsync(args);
        }


        private Predicate<object> CollectionFilter()
        {
            return (obj) =>
            {
                if (obj is not ComponentModel model)
                    return false;

                var isvalid = true;
                if (!string.IsNullOrEmpty(_filterStatus))
                {
                    if (Enum.TryParse<ModelStatusType>(_filterStatus, out var statusType))
                    {
                        isvalid = isvalid && model.Status == statusType;
                    }
                }
                if (!string.IsNullOrEmpty(_filterText))
                {
                    isvalid = isvalid && model.Name.Contains(_filterText, StringComparison.OrdinalIgnoreCase);
                }
                return isvalid;
            };
        }


        private Task FilterClearAsync()
        {
            FilterText = null;
            FilterStatus = FilterStatuses[0];
            return Task.CompletedTask;
        }


        private bool CanClearFilter()
        {
            return !string.IsNullOrWhiteSpace(_filterText)
                || _filterStatus != FilterStatuses[0];
        }


        private Task OpenModelAsync()
        {
            URL.NavigateToUrl(_selectedModel.GetDirectory(Settings));
            return Task.CompletedTask;
        }


        private async Task DeleteModelAsync()
        {
            if (await DialogService.ShowMessageAsync("Delete Model", $"Are you sure you want to delete this model?", TensorStack.WPF.Dialogs.MessageDialogType.YesNo, TensorStack.WPF.Dialogs.MessageBoxIconType.Warning, TensorStack.WPF.Dialogs.MessageBoxStyleType.Danger))
            {
                await Task.Run(() => _selectedModel.Delete(Settings));
                _selectedModel.Status = ModelStatusType.Available;
                await SaveAsync(); 
            }
        }


        private async Task DownloadModelAsync()
        {
            await DownloadService.QueueAsync(_selectedModel);
        }


        private async Task DownloadModelCancelAsync()
        {
            await DownloadService.CancelAsync(_selectedModel);
        }


        private async Task SaveAsync()
        {
            Settings.ScanModels();
            await SettingsManager.SaveAsync(Settings);
        }
    }
}