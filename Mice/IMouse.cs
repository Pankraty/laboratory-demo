namespace Mice
{
    public interface IMouse
    {
        bool IsAlive { get; }

        void GetMedicine(IMedicine drop);
    }
}