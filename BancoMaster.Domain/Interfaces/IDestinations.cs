using BancoMaster.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoMaster.Domain.Interfaces
{
    public interface IDestinations
    {
        Task<List<Destination>> ListingValues();
    }
}
