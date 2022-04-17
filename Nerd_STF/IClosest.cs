using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF
{
    public interface IClosest<T> where T : IEquatable<T>
    {
        public T ClosestTo(T item);
    }
}
