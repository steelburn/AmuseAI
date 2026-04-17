using Amuse.App.Views;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using TensorStack.Common;
using TensorStack.Common.Common;
using TensorStack.Python.Common;
using TensorStack.WPF;

namespace Amuse.App.Common
{
    public class DiffusionModel : BaseModel
    {
        private ModelStatusType _status;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public BackendType Backend { get; set; }
        public string Name { get; set; }
        public string Pipeline { get; set; }
        public string Path { get; set; }
        public string Variant { get; set; }
        public ModelSourceType Source { get; set; }
        public bool IsDefault { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public View[] ViewFilter { get; set; }
        public bool IsGated { get; set; }
        public VendorType[] Vendor { get; set; }
        public ModelStatusType Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        public string Link { get; set; }
        public MemoryProfile[] MemoryProfile { get; set; }
        public DataType BaseType { get; set; }
        public ProcessType[] ProcessTypes { get; set; }
        public List<SizeOption> Resolutions { get; set; }
        public DiffusionDefaultOptions DefaultOptions { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DiffusionCheckpointModel Checkpoint { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MemoryMode? UserMemoryMode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public QualityMode? UserQualityMode { get; set; }


        public void Initialize(string modelDirectory)
        {
            var isValid = false;
            if (Source == ModelSourceType.Folder)
                isValid = Directory.Exists(Path);
            else if (Source == ModelSourceType.HuggingFace)
                isValid = Directory.Exists(System.IO.Path.Combine(modelDirectory, Utils.GetHuggingFaceCacheId(Path)));
            else if (Source == ModelSourceType.SingleFile)
            {
                isValid = Checkpoint is not null && Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.SingleFile);
            }
            else if (Source == ModelSourceType.Checkpoint)
            {
                isValid = Checkpoint is not null
                    && Utils.TryParseHuggingFaceRepo(Path, out _)
                    && (string.IsNullOrEmpty(Checkpoint.TextEncoder) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.TextEncoder))
                    && (string.IsNullOrEmpty(Checkpoint.TextEncoder2) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.TextEncoder2))
                    && (string.IsNullOrEmpty(Checkpoint.TextEncoder3) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.TextEncoder3))
                    && (string.IsNullOrEmpty(Checkpoint.Transformer) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.Transformer))
                    && (string.IsNullOrEmpty(Checkpoint.Transformer2) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.Transformer2))
                    && (string.IsNullOrEmpty(Checkpoint.Vae) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.Vae))
                    && (string.IsNullOrEmpty(Checkpoint.AudioVae) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.AudioVae))
                    && (string.IsNullOrEmpty(Checkpoint.Vocoder) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.Vocoder))
                    && (string.IsNullOrEmpty(Checkpoint.Connectors) || Utils.IsCheckpointInstalled(modelDirectory, Checkpoint.Connectors));
            }

            if (!isValid)
            {
                // Files not found
                Status = ModelStatusType.Pending;
            }
            else
            {
                // Files found
                if (Status == ModelStatusType.Pending || Status == ModelStatusType.Verifying)
                    Status = ModelStatusType.Unknown;
                else if (Status == ModelStatusType.Downloading || Status == ModelStatusType.DownloadQueue || Status == ModelStatusType.DownloadFailed)
                    Status = ModelStatusType.Pending;
            }
        }


        public DiffusionModel DeepClone(int id)
        {
            return new DiffusionModel
            {
                Id = id,
                Backend = Backend,
                Name = Name,
                Path = Path,
                Variant = Variant,
                Pipeline = Pipeline,
                IsDefault = IsDefault,
                ViewFilter = ViewFilter?.ToArray(),
                BaseType = BaseType,
                IsGated = IsGated,
                Link = Link,
                MemoryProfile = MemoryProfile.Select(x => new MemoryProfile
                {
                    QualityMode = x.QualityMode,
                    MemoryModes = x.MemoryModes.ToArray(),
                }).ToArray(),
                ProcessTypes = [.. ProcessTypes],
                Vendor = Vendor.IsNullOrEmpty() ? null : [.. Vendor],
                Source = Source,
                Resolutions = [.. Resolutions.Select(x => new SizeOption
                {
                    Height = x.Height,
                    Width = x.Width,
                    IsDefault = x.IsDefault
                })],
                DefaultOptions = new DiffusionDefaultOptions
                {
                    Width = DefaultOptions.Width,
                    Height = DefaultOptions.Height,
                    Steps = DefaultOptions.Steps,
                    Steps2 = DefaultOptions.Steps2,
                    GuidanceScale = DefaultOptions.GuidanceScale,
                    GuidanceScale2 = DefaultOptions.GuidanceScale2,
                    Frames = DefaultOptions.Frames,
                    FrameRate = DefaultOptions.FrameRate,
                    SampleRate = DefaultOptions.SampleRate,
                    FrameChunk = DefaultOptions.FrameChunk,
                    FrameChunkOverlap = DefaultOptions.FrameChunkOverlap,
                    FrameOptions = DefaultOptions.FrameOptions?.ToArray(),
                    NoiseCondition = DefaultOptions.NoiseCondition,
                    Scheduler = DefaultOptions.Scheduler,
                    Schedulers = DefaultOptions.Schedulers with { },
                    Strength = DefaultOptions.Strength,
                    IsVaeSlicingEnabled = DefaultOptions.IsVaeSlicingEnabled,
                    IsVaeTilingEnabled = DefaultOptions.IsVaeTilingEnabled,
                },
                Checkpoint = Checkpoint is null ? null : new DiffusionCheckpointModel
                {
                    SingleFile = Checkpoint.SingleFile,
                    TextEncoder = Checkpoint.TextEncoder,
                    TextEncoder2 = Checkpoint.TextEncoder2,
                    TextEncoder3 = Checkpoint.TextEncoder3,
                    Transformer = Checkpoint.Transformer,
                    Transformer2 = Checkpoint.Transformer2,
                    Vae = Checkpoint.Vae,
                    AudioVae = Checkpoint.AudioVae,
                    Vocoder = Checkpoint.Vocoder,
                    Connectors = Checkpoint.Connectors
                }
            };
        }
    }
}
