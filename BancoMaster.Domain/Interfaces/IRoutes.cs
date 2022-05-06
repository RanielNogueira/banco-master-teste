using BancoMaster.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoMaster.Domain.Interfaces
{
    public interface IRoutes
    {
        public Task Add(Routes Route);
        public Task Update(Routes Route);
        public Task Delete(int id);
        public Routes Listing(int Id);
        public Task<List<RoutesViewModel>> ListingValues(string Origin, string Destiny);
        public Task<RoutesViewModel> BestPrice(string Origin, string Destiny);
    }
}
