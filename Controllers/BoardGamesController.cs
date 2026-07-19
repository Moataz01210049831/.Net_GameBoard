using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGamesController(ILogger<BoardGame> _logger,ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        //[Route("GetBoardGames")]
        public async Task<ActionResult<BoardGame>> Get(int pageNumber=0,int pageSize=0,string? filterQuery=null)
        {
            _logger.LogInformation("test loginig data");
            var query = context.BoardGames.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterQuery))
                query = query.Where(b => b.Name.Contains(filterQuery));

            var totalCount = await query.CountAsync();
            var bordBox = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            var res = new RestDTO
            {
                Data = bordBox,
                totalCount = totalCount
            };
            return Ok(
                res

                );



        }

        [HttpPost]
        [Route("UpdateBoardGame")]
        public async Task<ActionResult<BoardGame>> Post(BoardGameDTO model)
        {
            var boardGame = await context.BoardGames
                .FirstOrDefaultAsync(b => b.Id == model.Id);

            if (boardGame is null)
                return NotFound($"BoardGame with Id {model.Id} was not found.");

            if (!string.IsNullOrWhiteSpace(model.Name))
                boardGame.Name = model.Name;
            if (model.Year.HasValue)
                boardGame.Year = model.Year.Value;
            if (model.MinPlayers.HasValue)
                boardGame.Minplayers = model.MinPlayers.Value;
            if (model.MaxPlayers.HasValue)
                boardGame.Maxplayers = model.MaxPlayers.Value;
            if (model.PlayTime.HasValue)
                boardGame.PlayTime = model.PlayTime.Value;
            if (model.MinAge.HasValue)
                boardGame.MinAge = model.MinAge.Value;

            boardGame.LastModifiedDate = DateTime.UtcNow;

            context.BoardGames.Update(boardGame);
            await context.SaveChangesAsync();

            return Ok(boardGame);
        }

        [HttpDelete]
        [Route("DeleteBoardGame/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var boardGame = await context.BoardGames
                .FirstOrDefaultAsync(b => b.Id == id);

            if (boardGame is null)
                return NotFound($"BoardGame with Id {id} was not found.");

            context.BoardGames.Remove(boardGame);
            await context.SaveChangesAsync();

            return Ok($"BoardGame with Id {id} was deleted.");
        }
    }
}
