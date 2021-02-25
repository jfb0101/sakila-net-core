using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Models {

    [Table("film")]
    public class Film {
        [Key]
        [Column("film_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}

        [Column("title")]
        public string Title {get;set;}

        [Column("release_year")]
        public int? ReleaseYear {get;set;}
    }
}