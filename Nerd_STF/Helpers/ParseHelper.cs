using System;

namespace Nerd_STF.Helpers
{
    internal static class ParseHelper
    {
        // TODO: Allow parsing more stuff (hexadecimal).
        public static double ParseDouble(ReadOnlySpan<char> str)
        {
            // Turns out this is less accurate than copying and modifying
            // the code from ParseDoubleWholeDecimals. I think because applying
            // 0.1 to the whole number is worse than 0.1 to a each individual
            // decimal point.
            int raw = ParseDoubleWholeDecimals(str, out int places);
            double value = raw;
            for (int i = 0; i < places; i++) value *= 0.1;
            return value;
        }
        public static int ParseDoubleWholeDecimals(ReadOnlySpan<char> str, out int places)
        {
            str = str.Trim();
            if (str.Length == 0) goto _fail;
            places = 0;

            bool negative = str.StartsWith("-".AsSpan());

            int result = 0;
            ReadOnlySpan<char>.Enumerator stepper = str.GetEnumerator();
            if (negative) stepper.MoveNext();
            bool decFound = false;
            while (stepper.MoveNext())
            {
                char c = stepper.Current;
                if (c == ',') continue;
                else if (c == '.')
                {
                    decFound = true;
                    continue;
                }

                if (c < '0' || c > '9') goto _fail;
                int value = c - '0';

                result = result * 10 + value;
                if (decFound) places++;
            }

            return negative ? -result : result;

        _fail:
            throw new FormatException("Cannot parse double from span.");
        }
    }
}
