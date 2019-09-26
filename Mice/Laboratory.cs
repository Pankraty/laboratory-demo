using System;
using System.Linq;

namespace Mice
{
    public class Laboratory
    {
        private readonly Random _rnd;

        public Laboratory() : this(null)
        {
        }

        internal Laboratory(int? seed)
        {
            _rnd = seed.HasValue
                ? new Random(seed.Value)
                : new Random();
        }

        public IMouse[] GimmeMice(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => new Mouse())
                .ToArray();
        }

        public IMedicine[] GimmeSamples(int count)
        {
            var samples = Enumerable.Range(0, count)
                .Select(i => new Medicine(false))
                .ToArray();

            var spoiledIndex = _rnd.Next(count);

            samples[spoiledIndex] = new Medicine(true);
            return samples;
        }

        private class Mouse : IMouse
        {
            public Mouse()
            {
                IsAlive = true;
            }

            public bool IsAlive { get; private set; }

            public void GetMedicine(IMedicine drop)
            {
                if (drop is Medicine medicine && medicine.IsPoisoned)
                    IsAlive = false;
            }
        }

        private class Medicine : IMedicine
        {
            public Medicine(bool isPoisoned)
            {
                IsPoisoned = isPoisoned;
            }

            public bool IsPoisoned { get; }
        }
    }
}