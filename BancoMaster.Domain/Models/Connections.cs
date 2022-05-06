using System.ComponentModel.DataAnnotations.Schema;

namespace BancoMaster.Domain.Models
{
    [Table("TB_CONNECTIONS")]
    public class Connections : Entity
    {
        [Column("route_id")]
        public int RouteId { get; set; }

        [Column("ordination")]
        public int Ordination { get; set; }

        [Column("destination_id")]
        public int DestinationId { get; set; }
    }
}
