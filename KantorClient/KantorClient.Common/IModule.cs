using SimpleInjector;

namespace KantorClient.Common
{
    public interface IModule
    {
        void SetDependencies(Container container);
    }
}
