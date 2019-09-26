using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mice
{
    class Program
    {
        static void Main()
        {
            var laboratory = new Laboratory();
            var samples = laboratory.GimmeSamples(1000);
            IMouse[] mice = laboratory.GimmeMice(10);

            IMouse[] miceAlive = mice;
            IMedicine poisonedSample = null;
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = laboratory.NumberOfLaborants
            };

            var suspiciousSamples = samples;
            do
            {
                var sessions = DistibuteSamplesByMice(suspiciousSamples, miceAlive);
                Parallel.ForEach(sessions.Sessions, options, session =>
                {
                    foreach (var sample in session.Samples)
                    {
                        session.Mouse.GetMedicine(sample);
                    }

                    if (!session.Mouse.IsAlive)
                    {
                        suspiciousSamples = session.Samples.ToArray();
                        if (suspiciousSamples.Length == 1)
                            poisonedSample = suspiciousSamples[0];
                    }
                });

                if (miceAlive.All(m => m.IsAlive))
                    poisonedSample = sessions.AdditionalSample;
                miceAlive = mice.Where(m => m.IsAlive).ToArray();
            } while (poisonedSample == null && miceAlive.Length > 0);

            if (poisonedSample == null && miceAlive.Length == 0)
                throw new InvalidOperationException("We ran out of mice");

            Console.WriteLine($"Sample number {poisonedSample.Index} is poisoned");
            Console.ReadKey();
        }

        private static TestingSessionSet DistibuteSamplesByMice(IMedicine[] suspiciousSamples, IMouse[] miceAlive)
        {
            var res = new TestingSessionSet(
                miceAlive.Select(m => new TestingSession(m)).ToArray(),
                suspiciousSamples[0]);

            for (int i = 1; i < suspiciousSamples.Length; i++)
            {
                var session = res.Sessions[(i - 1) % res.Sessions.Length];
                session.Samples.Add(suspiciousSamples[i]);
            }

            return res;
        }
    }
}
