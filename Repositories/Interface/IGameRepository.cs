using Game_Universe.API.Models.Domain;

namespace Game_Universe.API.Repositories.Interface
{
    public interface IGameRepository
    {


        Task<Game> CreateAsync(Game game);

        Task<IEnumerable<Game>>  GetAllGamesAsync(string? query = null , string? sortBy=null,string?sortDirection = null );


        Task<Game?>  GetById(Guid id);


       Task<Game?> UpdateAsync(Game game);




           Task<Game?>  DeleteAsync(Guid id);




    }
}
