using Microsoft.AspNetCore.Identity;


namespace Game_Universe.API.Repositories.Interface
{
    public interface ITokenRepository
    {


        string CreateTwtToken(IdentityUser user , List<string> roles);


    }
}
