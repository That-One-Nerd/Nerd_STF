using System.Text;

namespace Nerd_STF.Helpers
{
    internal static class ToStringHelper
    {
#if CS8_OR_GREATER
        public static string PolynomialToString(double[] terms, bool showC, string? format)
#else
        public static string PolynomialToString(double[] terms, bool showC, string format)
#endif
        {
            StringBuilder builder = new StringBuilder();
            bool first = true;
            for (int i = terms.Length - 1; i >= 0; i--)
            {
                double term = terms[i];
                if (term != 0)
                {
                    if (term > 0 && (term != 1 || i == 0))
                    {
                        if (first) builder.Append(term.ToString(format));
                        else builder.Append("+ ").Append(term.ToString(format));
                    }
                    else if (term < 0)
                    {
                        if (first)
                        {
                            if (term != -1 || i == 0) builder.Append(term.ToString(format));
                            else builder.Append('-');
                        }
                        else
                        {
                            if (term != -1 || i == 0) builder.Append("- ").Append((-term).ToString(format));
                            else builder.Append("- ");
                        }
                    }
                    first = false;

                    if (i > 0)
                    {
                        builder.Append('x');
                        if (i > 1) builder.Append('^').Append(i);
                    }
                    builder.Append(' ');
                }
                else if (showC && i == 0) builder.Append("+ C ");
            }
            return builder.Remove(builder.Length - 1, 1).ToString();
        }
    }
}
