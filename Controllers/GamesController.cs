using Game_Universe.API.Data;
using Game_Universe.API.Models.Domain;
using Game_Universe.API.Models.DTO;
using Game_Universe.API.Repositories.Implementation;
using Game_Universe.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Game_Universe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

         //Post

        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateGames([FromBody]CreateGameRequestDto requestDto)
        {
            // Map DTO to Domain Models
            var game = new Game
            {
                Name = requestDto.Name,
                Type = requestDto.Type,
                Description = requestDto.Description,
                Rating = requestDto.Rating,
                Platform = requestDto.Platform,
                Age = requestDto.Age,
                Price = requestDto.Price,
                Label = requestDto.Label,
                IsVisible = requestDto.IsVisible,
            };

            await gameRepository.CreateAsync(game);

            // Domain model to DTO
            var response = new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Type = game.Type,
                Description = game.Description,
                Rating = game.Rating,
                Platform = game.Platform,
                Age = game.Age,
                Price = game.Price,
                Label = game.Label,
                IsVisible = game.IsVisible,

            };

            return Ok(response);


        }


        // Get

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllGames()
        {
          var games =   await gameRepository.GetAllGamesAsync();

            // Map Domain model to DTO
            var response = new List<GameDto>();
            foreach (var game in games)
            {
                response.Add(new GameDto
                {
                       Id=game.Id,
                       Name = game.Name,
                       Type = game.Type,
                       Description = game.Description,
                       Age= game.Age,
                       Price = game.Price,
                       Label = game.Label,
                       IsVisible = game.IsVisible,
                       Platform = game.Platform,
                       Rating= game.Rating,
                });
            }

             return Ok(response);

        }


        // GET Single By Id:
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetGameById([FromRoute]Guid id)
        {
          var existingGame =   await gameRepository.GetById(id);
            if(existingGame == null)
            {
                return NotFound();
            }

            var response = new GameDto
            {
                Id = existingGame.Id,
                Name = existingGame.Name,
                Type = existingGame.Type,
                Description = existingGame.Description,
                Age = existingGame.Age,
                Price = existingGame.Price,
                Label = existingGame.Label,
                IsVisible = existingGame.IsVisible,
                Platform = existingGame.Platform,
                Rating = existingGame.Rating,

            };

            return Ok(response);

             
        }



        //PUT
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditGame([FromRoute]Guid id ,UpdateGameRequestDto requestDto )
        {
            //Convert to Domain Model
            var game = new Game
            {
                Id = id,
                Name = requestDto.Name,
                Type = requestDto.Type,
                Description = requestDto.Description,
                Age = requestDto.Age,
                Price = requestDto.Price,
                Label = requestDto.Label,
                IsVisible = requestDto.IsVisible,
                Platform = requestDto.Platform,
                Rating = requestDto.Rating,

            };
           
               game =  await  gameRepository.UpdateAsync(game);
            if(game == null) 
            { 
                return NotFound(); 
            }

            // Convert Domain model to DTO
            var response = new GameDto
            {
                 Id = game.Id,
                 Name = game.Name,
                 Type = game.Type,
                 Description = game.Description,
                 Age = game.Age,
                 Price = game.Price,
                 Label = game.Label,
                 IsVisible = game.IsVisible,
                 Platform = game.Platform,
                 Rating = game.Rating,

            };

            return Ok(response);

        }





        //DELETE

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult > DeleteGame([FromRoute] Guid id)
        {
          var game =   await gameRepository.DeleteAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            // Convert Domain Model to DTO
            var response = new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Type = game.Type,
                Description = game.Description,
                Age = game.Age,
                Price = game.Price,
                Label = game.Label,
                IsVisible = game.IsVisible,
                Platform = game.Platform,
                Rating = game.Rating,


            };
            return Ok(response);
        }
        






    }
}
