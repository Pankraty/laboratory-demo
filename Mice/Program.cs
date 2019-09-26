using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Mice
{
    class Program
    {
        static void Main()
        {
            var laboratory = new Laboratory(100);
            var samples = laboratory.GimmeSamples(1000);
            IMouse[] mice = laboratory.GimmeMice(10);

            IMedicine poisonedSample = null;
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = laboratory.NumberOfLaborants
            };

            var sessions = DistibuteSamplesByMice(samples, mice);

            Parallel.ForEach(sessions.Sessions, options, session =>
            {
                foreach (var sample in session.Samples)
                {
                    session.Mouse.GetMedicine(sample);
                }
            });

            var bits = ToBitArray(0);
            for (int i = 0; i < mice.Length; i++)
            {
                bits[i] = !mice[i].IsAlive;
            }

            var index = ToInt(bits);

            Console.WriteLine($"Sample number {index} is poisoned");
            Console.ReadKey();
        }

        private static TestingSessionSet DistibuteSamplesByMice(IMedicine[] samples, IMouse[] mice)
        {
            var res = new TestingSessionSet(mice.Select(m => new TestingSession(m)).ToArray());
            for (int i = 0; i < samples.Length; i++)
            {
                var bits = ToBitArray(i);
                for (int j = 0; j < res.Sessions.Length; j++)
                {
                    if (bits[j])
                        res.Sessions[j].Samples.Add(samples[i]);
                }
            }

            return res;
        }

        private static BitArray ToBitArray(int number)
        {
            return new BitArray(BitConverter.GetBytes(number));
        }

        private static int ToInt(BitArray bits)
        {
            int[] array = new int[1];
            bits.CopyTo(array, 0);
            return array[0];
        }
    }
}