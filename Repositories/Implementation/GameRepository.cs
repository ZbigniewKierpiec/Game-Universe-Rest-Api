using Game_Universe.API.Data;
using Game_Universe.API.Models.Domain;
using Game_Universe.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Game_Universe.API.Repositories.Implementation
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Game> CreateAsync(Game game)
        {


            await dbContext.Game.AddAsync(game);
            await dbContext.SaveChangesAsync();
            return game;

        }

        public async Task<Game?> DeleteAsync(Guid id)
        {
       var existingGame  =   await dbContext.Game.FirstOrDefaultAsync(x => x.Id == id);
            if (existingGame == null) {
            
                 return null;
            }

            dbContext.Game.Remove(existingGame);
            await dbContext.SaveChangesAsync();
            return existingGame;


        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync(string? query = null)
        {


            // Query Database

           var games =  dbContext.Game.AsQueryable();

            // Filtering

            if (string.IsNullOrWhiteSpace(query)== false)
            {
                games = games.Where(x => x.Name.Contains(query));
            
            }

            // Sorting


            //Pagination

            return await games.ToListAsync();

          // return await dbContext.Game.ToListAsync();





        }

        public async Task<Game?> GetById(Guid id)
        {
           return await dbContext.Game.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Game?> UpdateAsync(Game game)
        {
           var existingGame =  await dbContext.Game.FirstOrDefaultAsync(x=>x.Id == game.Id);
                      if(existingGame != null)
            {
                dbContext.Entry(existingGame).CurrentValues.SetValues(game);
                await dbContext.SaveChangesAsync();
                return game;
            }
            return null;
        }
    }
}
