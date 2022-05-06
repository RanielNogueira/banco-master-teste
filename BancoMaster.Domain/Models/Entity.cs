using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BancoMaster.Domain.Models
{
    public class Entity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } 
    }
}
