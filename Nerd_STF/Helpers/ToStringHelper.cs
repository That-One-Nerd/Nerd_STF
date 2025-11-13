using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections.Generic;
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

#if CS8_OR_GREATER
        public static string MatrixToString<T>(T matrix, string? format)
#else
        public static string MatrixToString<T>(T matrix, string format)
#endif
            where T : IMatrix<T>
        {
            // First convert all items to their string counterparts,
            // then measure the lengths and do spacing accordingly.
            Int2 size = matrix.Size;
            string[,] items = new string[size.y, size.x];
            for (int x = 0; x < size.x; x++) for (int y = 0; y < size.y; y++)
                    items[y, x] = matrix[x, y].ToString(format);

            // Then write each line separately.
            StringBuilder[] lines = new StringBuilder[size.x + 2];
            for (int i = 0; i < lines.Length; i++)
            {
                StringBuilder builder = new StringBuilder();
                if (i == 0) builder.Append('┌');
                else if (i == lines.Length - 1) builder.Append('└');
                else builder.Append('│');

                lines[i] = builder;
            }
            int totalLen = 0;
            for (int x = 0; x < size.y; x++)
            {
                int maxLen = 0;
                for (int y = 0; y < size.x; y++)
                {
                    string item = items[x, y];
                    if (item.Length > maxLen) maxLen = item.Length;
                }
                totalLen += maxLen + 1;
                for (int y = 0; y < size.x; y++)
                {
                    StringBuilder builder = lines[y + 1];
                    string item = items[x, y];
                    int spacing = maxLen - item.Length;
                    builder.Append(new string(' ', spacing + 1));
                    builder.Append(item);
                }
            }

            // Finish up and merge.
            StringBuilder total = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                StringBuilder builder = lines[i];
                if (i == 0) builder.Append(new string(' ', totalLen)).Append(" ┐");
                else if (i == lines.Length - 1) builder.Append(new string(' ', totalLen)).Append(" ┘");
                else builder.Append(" │");
                total.Append(builder);
                if (i != lines.Length - 1) total.AppendLine();
            }
            return total.ToString();
        }

        private static readonly string dimNumSymbols = " ijk?";
#if CS8_OR_GREATER
        public static string HighDimNumberToString(IEnumerable<double> terms, string? format, IFormatProvider? provider)
#else
        public static string HighDimNumberToString(IEnumerable<double> terms, string format, IFormatProvider provider)
#endif
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            bool first = true;
            foreach (double term in terms)
            {
                if (term == 0)
                {
                    index++;
                    continue;
                }
                if (first) builder.Append(term.ToString(format, provider));
                else
                {
                    if (term > 0)
                    {
                        builder.Append(" + ");
                        builder.Append(term.ToString(format, provider));
                    }
                    else
                    {
                        builder.Append(" - ");
                        builder.Append((-term).ToString(format, provider));
                    }
                }
                if (index > 0) builder.Append(dimNumSymbols[MathE.Min(index, dimNumSymbols.Length)]);
                first = false;
                index++;
            }
            if (first) builder.Append(0.0.ToString(format, provider));
            return builder.ToString();
        }
    }
}
