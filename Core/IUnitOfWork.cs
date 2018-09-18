using System.Threading.Tasks;

namespace SystemSupportingMSE.Core
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}