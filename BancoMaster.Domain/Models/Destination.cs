using System.ComponentModel.DataAnnotations.Schema;

namespace BancoMaster.Domain.Models
{
    [Table("TB_DESTINATION")]
    public class Destination : Entity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("abbreviated")]
        public string Abbreviated { get; set; }
    }
}
