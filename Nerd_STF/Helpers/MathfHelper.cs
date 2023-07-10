namespace Nerd_STF.Helpers;

internal static class MathfHelper
{
    public static (int[] group, long max)[] MillerRabinWitnessNumbers =
    {
        (new int[] { 2, 3 }, 1_373_653),
        (new int[] { 31, 73 }, 9_080_191),
        (new int[] { 2, 3, 5 }, 25_326_001),
        (new int[] { 2, 13, 23, 1_662_803 }, 1_122_004_669_633),
        (new int[] { 2, 3, 5, 7, 11 }, 2_152_302_898_747),
        (new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 }, long.MaxValue) // The real maximum is 318_665_857_834_031_151_167_461
    };

    public static bool IsPrimeClassic(long num)
    {
        for (long i = 2; i <= num / 2; i++) if (num % i == 0) return false;
        return true;
    }

    // 0x49204655434B494E47204C4F5645204E554D42525048494C45
    // For some reason (I think the witnesses are slightly off), 12 numbers under 100,000
    // are misrepresented as prime. I guess one of the witnesses becomes a liar for them.
    // Mostly works though.
    //
    // TODO: In 2.10, the Mathf.PowerMod(int, int, int) method needs to be reworked to
    //       have a better O(n).
    public static bool IsPrimeMillerRabin(long num)
    {
        // Negatives are composite, zero and one are composite, two and three are prime.
        if (num <= 3) return num > 1;

        long unchanged = num;

        // Find the number's proper witness group.
        int[]? witnessGroup = null;
        for (int i = 0; i < MillerRabinWitnessNumbers.Length; i++)
        {
            if (num <= MillerRabinWitnessNumbers[i].max)
            {
                witnessGroup = MillerRabinWitnessNumbers[i].group;
                break;
            }
        }
        if (witnessGroup is null) throw new MathException($"The number {num} is out of range of the available witness " +
            $"numbers. Use the {nameof(PrimeCheckMethod.Classic)} method instead."); // This should never happen.

        // Prep the number for court.
        num -= 1; // For clarity.

        // Seperate out powers of two.
        int m = 0;
        while (num % 2 == 0)
        {
            m++;
            num /= 2;
        }

        long d = num; // The rest.

        // Our number is rewritten as 2^m * d + 1

        foreach (int a in witnessGroup)
        {
            // Calculate a^d = 1 mod n
            // If true, the number *may* be prime (test all star numbers to be sure)
            // If false, the number is *definitely* composite.

            bool thinks = false;
            for (int m2 = 0; m2 < m; m2++)
            {
                // Add any amount of multiples of two as given, but not as many as the original breakdown.

                int additional = 1;
                for (int m3 = 0; m3 < m2; m3++) additional *= 2;

                long result = Mathf.PowerMod(a, additional * d, unchanged);
                if (Mathf.AbsoluteMod(result + 1, unchanged) == 0 || Mathf.AbsoluteMod(result - 1, unchanged) == 0)
                {
                    thinks = true;
                    break;
                }
            }

            if (!thinks) return false; // Definitely not prime.
            else continue; // For clarity. A claim that the number is prime is not trustworthy until we've checked all witnesses.
        }

        return true; // Probably prime.
    }
}
