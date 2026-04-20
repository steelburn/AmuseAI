using TensorStack.WPF;

namespace Amuse.App.Common
{
    public record InterpolateInputOptions : BaseRecord
    {
        private int _multiplier;

        public int Multiplier
        {
            get { return _multiplier; }
            set { SetProperty(ref _multiplier, value); }
        }
    }
}
