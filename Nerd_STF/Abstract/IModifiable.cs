using System;

namespace Nerd_STF.Abstract
{
    public interface IModifiable<TSelf>
        where TSelf : IModifiable<TSelf>
    {
        void Modify(Action<TSelf> action);
    }
}
