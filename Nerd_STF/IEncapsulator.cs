using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF
{
    public interface IEncapsulator<T, TE> : IContainer<TE> where T : IEquatable<T> where TE : IEquatable<TE>
    {
        public T Encapsulate(TE val);
    }
}
