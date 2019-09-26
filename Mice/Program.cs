using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Mice
{
    class Program
    {
        static void Main()
        {
            var laboratory = new Laboratory();
            var samples = new ConcurrentQueue<IMedicine>(
                laboratory.GimmeSamples(1000));
            IMouse[] mice = laboratory.GimmeMice(10);

            IMedicine poisonedSample = null;

            var options = new ParallelOptions()
                {MaxDegreeOfParallelism = laboratory.NumberOfLaborants};
            Parallel.ForEach(mice, options, mouse =>
            {
                while (poisonedSample == null && samples.TryDequeue(out var sample))
                {
                    mouse.GetMedicine(sample);
                    if (!mouse.IsAlive)
                    {
                        poisonedSample = sample;
                        break;
                    }
                }
            });

            Console.WriteLine($"Sample number {poisonedSample.Index} is poisoned");
            Console.ReadKey();
        }
    }
}
