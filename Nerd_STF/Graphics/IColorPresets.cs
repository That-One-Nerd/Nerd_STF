#if CS11_OR_GREATER
using System;
using System.Collections.Generic;
using System.Text;

namespace Nerd_STF.Graphics
{
    public interface IColorPresets<TSelf> where TSelf : struct, IColor<TSelf>, IColorPresets<TSelf>
    {
        static abstract TSelf Black { get; }
        static abstract TSelf Blue { get; }
        static abstract TSelf Clear { get; }
        static abstract TSelf Cyan { get; }
        static abstract TSelf Gray { get; }
        static abstract TSelf Green { get; }
        static abstract TSelf Magenta { get; }
        static abstract TSelf Orange { get; }
        static abstract TSelf Purple { get; }
        static abstract TSelf Red { get; }
        static abstract TSelf White { get; }
        static abstract TSelf Yellow { get; }
    }
}
#endif
