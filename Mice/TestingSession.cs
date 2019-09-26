using System.Collections.Generic;

namespace Mice
{
    internal class TestingSession
    {
        public IMouse Mouse { get; }

        public IList<IMedicine> Samples { get; }

        public TestingSession(IMouse mouse)
        {
            Mouse = mouse;
            Samples = new List<IMedicine>();
        }
    }
}