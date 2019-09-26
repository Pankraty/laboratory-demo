namespace Mice
{
    class TestingSessionSet
    {
        public TestingSession[] Sessions { get; }

        public IMedicine AdditionalSample { get; }

        public TestingSessionSet(TestingSession[] sessions, IMedicine additionalSample)
        {
            Sessions = sessions;
            AdditionalSample = additionalSample;
        }
    }
}