using System.Linq;
using System.Text.Json.Serialization;
using TensorStack.Common;
using TensorStack.Common.Common;
using TensorStack.WPF;

namespace Amuse.App.Common
{
    public sealed class ComponentModel : BaseModel, IDownloadModel
    {
        private BackendType _backend;
        private string _name;
        private string _folder;
        private string _type;
        private string _key;
        private string[] _downloadFiles;
        private string _link;
        private string _accessToken;
        private ModelStatusType _status;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        public BackendType Backend
        {
            get { return _backend; }
            set { SetProperty(ref _backend, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Folder
        {
            get { return _folder; }
            set { SetProperty(ref _folder, value); }
        }

        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }

        public string[] DownloadFiles
        {
            get { return _downloadFiles; }
            set { SetProperty(ref _downloadFiles, value); }
        }

        public ModelStatusType Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string AccessToken
        {
            get { return _accessToken; }
            set { SetProperty(ref _accessToken, value); }
        }

        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        [JsonIgnore]
        public string Path { get; private set; }


        public void Initialize(Settings settings)
        {
            Path = System.IO.Path.Combine(settings.DirectoryModel, _type, _folder);
            Status = GetModelStatus(settings);
        }


        public void Delete(Settings settings)
        {
            var directory = GetDirectory(settings);
            if (System.IO.Directory.Exists(directory))
                FileHelper.DeleteDirectory(directory);
        }


        public string GetDirectory(Settings settings)
        {
            return Path;
        }


        private ModelStatusType GetModelStatus(Settings settings)
        {
            var isValid = false;
            var path = GetDirectory(settings);
            var modelFiles = FileHelper.GetUrlFileMapping(DownloadFiles, path);
            if (modelFiles.Values.All(System.IO.File.Exists))
            {
                isValid = true;
            }

            if (Status == ModelStatusType.Available && isValid)
                return ModelStatusType.Installed;
            else if (Status == ModelStatusType.Installed && !isValid)
                return ModelStatusType.Available;
            else if (Status == ModelStatusType.Downloading || Status == ModelStatusType.DownloadQueue || Status == ModelStatusType.DownloadFailed)
                return ModelStatusType.Available;

            return Status;
        }


        public ComponentModel DeepClone(int id)
        {
            return new ComponentModel
            {
                Id = id,
                Backend = Backend,
                Name = Name,
                Key = Key,
                Type = Type,
                Folder = Folder,
                Path = Path,
                Link = Link,
                DownloadFiles = DownloadFiles?.ToArray(),
                Status = Status
            };
        }
    }
}
