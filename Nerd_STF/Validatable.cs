using System;

namespace Nerd_STF
{
    public class Validatable<T>
    {
        public bool Validated => validated;
        public T Value => TryValidate();

        private readonly Func<T> validateFunc;
        private bool validated;
        private T cached;

#pragma warning disable CS8618
        public Validatable(Func<T> validateFunc)
#pragma warning restore CS8618
        {
            this.validateFunc = validateFunc;
            validated = false;
        }

        private T TryValidate()
        {
            if (!validated)
            {
                // Must validate.
                cached = validateFunc();
                validated = true;
            }
            return cached;
        }

        public void Invalidate()
        {
            validated = false;
        }
    }
}
