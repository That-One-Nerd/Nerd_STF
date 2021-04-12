using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Interfaces
{
    public interface INegatives<T>
    {
        public T Absolute { get; }
        public bool IsNegative { get; }
        public T Negative { get; }
        public T Positive { get; }
    }
}