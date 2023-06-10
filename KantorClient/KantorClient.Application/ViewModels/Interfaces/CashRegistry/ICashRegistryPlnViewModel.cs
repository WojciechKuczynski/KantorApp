using System.Threading.Tasks;

namespace KantorClient.Application.ViewModels.Interfaces.CashRegistry
{
    public interface ICashRegistryPlnViewModel
    {
        ICashRegistryMainViewParent Parent { get; set; }
        Task Load(bool loaded);

        void LoadForm();
    }
}
