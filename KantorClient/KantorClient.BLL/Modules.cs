using KantorClient.Common;
using KantorClient.DAL;
using SimpleInjector;

namespace KantorClient.BLL
{
    public class Modules
    {
        private readonly Container _container;
        public Modules()
        {
            _container = new Container();
            SetDependencies(new DALModule());
            SetDependencies(new BLLModule());
        }

        public void SetDependencies(IModule module)
        {
            module.SetDependencies(_container);
        }
        public void Verify() => _container.Verify();
        public Container Container => _container;
    }
}
