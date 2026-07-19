using MyBGList.Models;

namespace MyBGList.DTO
{
    public class RestDTO
    {
        public int totalCount { get; set; }
        public List<BoardGame> Data { get; set; } = new List<BoardGame>();
    }
}
