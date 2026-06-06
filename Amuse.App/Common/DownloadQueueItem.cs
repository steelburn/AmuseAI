using Amuse.Common;
using System;
using System.Threading;
using TensorStack.Common;
using TensorStack.WPF;
using TensorStack.WPF.Controls;

namespace Amuse.App.Common
{
    public sealed class DownloadQueueItem : BaseModel
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private float _speed;
        private string _component;
        private string _fileName;
        private string _description;
        private string _errorMessage;

        public DownloadQueueItem(int index, IDownloadModel model)
        {
            Index = index;
            DownloadModel = model;
            Progress = new ProgressInfo();
            TotalProgress = new ProgressInfo();
            ProgressCallback = new Progress<PipelineProgress>(OnProgress);
            _description = GetDescription();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public int Index { get; set; }
        public ProgressInfo Progress { get; }
        public ProgressInfo TotalProgress { get; }
        public IProgress<PipelineProgress> ProgressCallback { get; }
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        public ModelStatusType Status => DownloadModel.Status;
        public string Name => DownloadModel?.Name;
        public string Description => _description;
        public IDownloadModel DownloadModel { get; }

        public float Speed
        {
            get { return _speed; }
            set { SetProperty(ref _speed, value); }
        }

        public string Component
        {
            get { return _component; }
            set { SetProperty(ref _component, value); }
        }

        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public void Cancel()
        {
            ErrorMessage = null;
            _cancellationTokenSource.SafeCancel();
        }


        public void UpdateStatus(ModelStatusType status)
        {
            if (DownloadModel != null)
                DownloadModel.Status = status;

            NotifyPropertyChanged(nameof(Status));
        }


        private void OnProgress(PipelineProgress progress)
        {
            if (progress.Key?.Equals("Download") == false)
                return;

            Speed = progress.Elapsed;
            Component = progress.Subkey;
            FileName = progress.Message;
            Progress.Update(progress.Value, progress.Maximum);
            TotalProgress.Update(progress.BatchValue, progress.BatchMaximum);
        }


        private string GetDescription()
        {
            try
            {
                if (DownloadModel is ComponentModel)
                    return "Component";

                return $"{DownloadModel.GetType().GetProperty("Pipeline").GetValue(DownloadModel)}";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
