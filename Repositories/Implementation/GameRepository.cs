using Game_Universe.API.Data;
using Game_Universe.API.Models.Domain;
using Game_Universe.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IEnumerable<Game>> GetAllGamesAsync(string? query = null , string? sortBy=null , string? sortDirection = null , int?pageNumber=1,int?pageSize=100)
        {


            // Query Database

           var games =  dbContext.Game.AsQueryable();

            // Filtering

            if (string.IsNullOrWhiteSpace(query)== false)
            {
                games = games.Where(x => x.Name.Contains(query));
            
            }

            // Sorting
          
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                bool isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Price", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAsc ? games.OrderBy(x => x.Price) : games.OrderByDescending(x => x.Price);
                }
               
                else if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    // Trim the names to ensure correct sorting
                    games = isAsc ? games.OrderBy(x => x.Name.Trim()) : games.OrderByDescending(x => x.Name.Trim());
                }


                else if (string.Equals(sortBy, "Rating", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAsc ? games.OrderBy(x => x.Rating) : games.OrderByDescending(x => x.Rating);
                }


            }
            /////////////////////////////

            //Pagination


             var skipResults = (pageNumber - 1) * pageSize;
             games = games.Skip(skipResults ?? 0).Take(pageSize ?? 100);




            //////////////////

            return await games.ToListAsync();

          // return await dbContext.Game.ToListAsync();





        }

        public async Task<Game?> GetById(Guid id)
        {
           return await dbContext.Game.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount()
        {
           

             return await dbContext.Game.CountAsync();



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
