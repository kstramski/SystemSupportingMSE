using System.Threading.Tasks;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportEventsDbContext context;

        public UnitOfWork(SportEventsDbContext context)
        {
            this.context = context;
        }

        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}