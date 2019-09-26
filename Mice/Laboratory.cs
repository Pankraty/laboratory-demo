using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mice
{
    public class Laboratory
    {
        private const int MedicineTimeToWork = 100;

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
                .Select(i => new Medicine(i, false))
                .ToArray();

            var spoiledIndex = _rnd.Next(count);

            samples[spoiledIndex] = new Medicine(spoiledIndex, true);
            return samples;
        }

        private class Mouse : IMouse
        {
            private bool _isAlive;

            public Mouse()
            {
                _isAlive = true;
            }

            public bool IsAlive => CheckIsAlive();

            private bool CheckIsAlive()
            {
                if (_makeMedicineWork == null)
                    return true;

                _makeMedicineWork.Wait();
                return _isAlive;
            }

            private Task _makeMedicineWork;

            public void GetMedicine(IMedicine drop)
            {
                _makeMedicineWork = Task.Run(async () => 
                {
                    await Task.Delay(MedicineTimeToWork);
                    if (drop is Medicine medicine && medicine.IsPoisoned)
                        _isAlive = false;
                });
            }
        }

        private class Medicine : IMedicine
        {
            public Medicine(int index, bool isPoisoned)
            {
                Index = index;
                IsPoisoned = isPoisoned;
            }

            public bool IsPoisoned { get; }

            public int Index { get; }
        }
    }
}