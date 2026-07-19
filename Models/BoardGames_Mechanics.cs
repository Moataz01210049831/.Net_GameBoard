using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyBGList.Models
{
   // [PrimaryKey(nameof(BoardGameId),nameof(MechanicId))]
    public class BoardGames_Mechanics
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Required]
        public int MechanicId { get; set; }
        public DateTime CreatedDate { get; set; }
        public BoardGame BoardGame { get; set; }
        public Mechanic mechanic { get; set; }


    }
}
