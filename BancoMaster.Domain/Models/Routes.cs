using System.ComponentModel.DataAnnotations.Schema;

namespace BancoMaster.Domain.Models
{
    [Table("TB_ROUTES")]
    public class Routes : Entity
    {
        [Column("origin_id")]
        public int OriginId { get; set; }
        [Column("destiny_id")]
        public int DestinyId { get; set; }
        [Column("price")]
        public double Price { get; set; }
    }
}
