namespace Mice
{
    class TestingSessionSet
    {
        public TestingSession[] Sessions { get; }

        public TestingSessionSet(TestingSession[] sessions)
        {
            Sessions = sessions;
        }
    }
}