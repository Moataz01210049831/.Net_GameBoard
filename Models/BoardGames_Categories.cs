using System.ComponentModel.DataAnnotations;

namespace MyBGList.Models
{
    public class BoardGames_Categories
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public BoardGame? BoardGame { get; set; }
        public Category? Category { get; set; }
    }
}
