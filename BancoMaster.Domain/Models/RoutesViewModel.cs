using System.Collections.Generic;

namespace BancoMaster.Domain.Models
{
    public class RoutesViewModel
    {
        public int Id { get; set; }
        public string OriginName { get; set; }
        public string OriginAbbr { get; set; }
        public string DestinyName { get; set; }
        public string DestinyAbbr { get; set; }
        public decimal Price { get; set; }
        public List<ConnectionsViewModel> Connections { get; set; }

        public RoutesViewModel() { }

        public RoutesViewModel(string originName, string originAbbr, string destinyName, string destinyAbbr, decimal price)
        {
            OriginName = originName;
            OriginAbbr = originAbbr;
            DestinyName = destinyName;
            DestinyAbbr = destinyAbbr;
            Price = price;
        }
    }
}
