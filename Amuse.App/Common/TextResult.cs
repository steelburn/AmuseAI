using System.Collections.Generic;
using System.Linq;
using TensorStack.Common;

namespace Amuse.App.Common
{
    public record TextResult
    {
        public TextResult() { }
        public TextResult(params TextInput[] inputs)
        {
            Results.AddRange(inputs);
        }

        public List<TextInput> Results { get; set; } = [];
        public TextInput Result => Results.FirstOrDefault();
    }
}
