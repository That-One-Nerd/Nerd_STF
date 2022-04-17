using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF
{
    public interface IContainer<T> where T : IEquatable<T>
    {
        public bool Contains(T item);
    }
}
