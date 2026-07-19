using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList.Models
{
    [Table("BoardGames")]
    public class BoardGame
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Year { get; set; }
        public int Minplayers { get; set; }
        public int Maxplayers { get; set; }
        public int PlayTime  { get; set; }
        public int MinAge { get; set; }
        public int UsersRelated { get; set; }
        public decimal RatingAverage { get; set; }
        public int BGGRank { get; set; }
        public decimal complixtyAverage { get; set; }

        public int OwendUsers { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

       

        public ICollection<BoardGames_Domains>? BoardGames_Domains { get; set; }
        public ICollection<BoardGames_Mechanics>? BoardGames_Mechanics { get; set; }
        public ICollection<BoardGames_Categories>? BoardGames_Categories { get; set; }


    }
}
