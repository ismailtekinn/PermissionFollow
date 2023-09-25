using Premisson.Northwind.Entities.Concreate;


namespace Premisson.Northwind.Core.Utils.Token
{
   public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

    }
}
