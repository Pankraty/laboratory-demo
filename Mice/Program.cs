using System;

namespace Mice
{
    class Program
    {
        static void Main()
        {
            var laboratory = new Laboratory();
            IMedicine[] samples = laboratory.GimmeSamples(1000);
            IMouse[] mice = laboratory.GimmeMice(10);

            var mouse = mice[0];

            int i = -1;
            do
            {
                i++;
                if (i >= samples.Length)
                    throw new InvalidOperationException("Not fair! All samples are clear!");

                mouse.GetMedicine(samples[i]);
            } while (mouse.IsAlive);

            Console.WriteLine($"Sample number {i} (starting from zero) is poisoned");
            Console.ReadKey();
        }
    }
}
