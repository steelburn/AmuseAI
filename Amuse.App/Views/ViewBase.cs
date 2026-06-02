using Amuse.App.Services;
using Microsoft.Extensions.Logging;
using TensorStack.WPF.Controls;
using TensorStack.WPF.Services;

namespace Amuse.App.Views
{
    public abstract class ViewBase : ViewControl
    {
        private bool _isViewBusy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBase"/> class.
        /// </summary>
        public ViewBase(Settings settings, NavigationService navigationService, IModelDownloadService downloadService, IHistoryService historyService, ILogger logger)
            : base(navigationService)
        {
            Logger = logger;
            Settings = settings;
            HistoryService = historyService;
            DownloadService = downloadService;
            Progress = new ProgressInfo();
            ViewName = View.ToString();
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        public abstract View View { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public override int Id => (int)View;

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        public string ViewName { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public Settings Settings { get; }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        public ProgressInfo Progress { get; }

        /// <summary>
        /// Gets the history service.
        /// </summary>
        public IHistoryService HistoryService { get; }

        /// <summary>
        /// Gets the download service.
        /// </summary>
        public IModelDownloadService DownloadService { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this view busy.
        /// </summary>
        /// <value><c>true</c> if this i view busy; otherwise, <c>false</c>.</value>
        public bool IsViewBusy
        {
            get { return _isViewBusy; }
            set { SetProperty(ref _isViewBusy, value); }
        }

    }
}
