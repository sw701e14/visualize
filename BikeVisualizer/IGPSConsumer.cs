using Shared.DAL;

namespace BikeVisualizer
{
    public interface IGPSConsumer
    {
        void Load(DatabaseSession session);
    }
}
