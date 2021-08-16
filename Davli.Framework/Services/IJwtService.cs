using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(List<Claim> claims, DateTime expireTime);
        List<Claim> ValidateJwtToken(string token);
    }
}
