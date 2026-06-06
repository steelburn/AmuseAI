using Amuse.App.Services;
using System.Linq;
using System.Runtime.CompilerServices;
using TensorStack.Common;

namespace Amuse.App.Common
{
    public sealed record DeviceModel : Device
    {
        public DeviceModel() { }
        public DeviceModel(Device options, GPUDevice gpuDevice) : base(options)
        {
            PCIBusId = gpuDevice.PCIBusId;
            DeviceType = gpuDevice.DeviceType;
            QualityModes = GetQualityModes(Vendor);
            SupportedBackends = GetSupportedBackends(Vendor, DeviceType);
            DefaultQualityMode = QualityModes.Contains(QualityMode.Standard) ? QualityMode.Standard : QualityMode.Production;
        }

        public int PCIBusId { get; init; }
        public string DeviceType { get; init; }
        public QualityMode[] QualityModes { get; init; }
        public QualityMode DefaultQualityMode { get; init; }
        public BackendType[] SupportedBackends { get; init; }


        private static QualityMode[] GetQualityModes(VendorType vendor)
        {
            return vendor switch
            {
                VendorType.AMD => [QualityMode.Standard, QualityMode.Production],
                VendorType.Nvidia => [QualityMode.Draft, QualityMode.Standard, QualityMode.Production],
                _ => [QualityMode.Production]
            };
        }


        private static BackendType[] GetSupportedBackends(VendorType vendor, string deviceType)
        {
            if (vendor == VendorType.AMD)
            {
                switch (deviceType)
                {
                    case "gfx1200":
                    case "gfx1201":
                        return [BackendType.PyTorch, BackendType.OnnxRuntime]; // "RDNA4";
                    case "gfx1100":
                    case "gfx1101":
                    case "gfx1102":
                    case "gfx1103":
                    case "gfx1150":
                        return [BackendType.PyTorch, BackendType.OnnxRuntime]; // "RDNA3";
                    case "gfx1030":
                    case "gfx1031":
                    case "gfx1032":
                    case "gfx1034":
                    case "gfx1035":
                        return [BackendType.OnnxRuntime];                      // "RDNA2";
                    case "gfx1010":
                    case "gfx1012":
                        return [BackendType.OnnxRuntime];                      // "RDNA1";
                    case "gfx950":
                    case "gfx942":
                    case "gfx90a":
                    case "gfx908":
                        return [BackendType.PyTorch, BackendType.OnnxRuntime]; // "CDNA";
                    default:
                        break;
                }
                return [BackendType.OnnxRuntime];
            }
            return [BackendType.PyTorch, BackendType.OnnxRuntime];
        }


        public bool Equals(DeviceModel other) => ReferenceEquals(this, other);
        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
    }
}
