using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Models {

    [Table("country")]
        

    public class Country {
        [Key]
        [Column("country_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Column("country")]
        public string Name {get;set;}
    }
}