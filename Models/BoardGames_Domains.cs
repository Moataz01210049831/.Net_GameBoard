using System.ComponentModel.DataAnnotations;

namespace MyBGList.Models
{
    public class BoardGames_Domains
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Required]
        public int DomainId { get; set; }

        public DateTime CreatedDate { get; set; }

        public BoardGame? BoardGame { get; set; }
        public Domain? Domain { get; set; }




    }
}
