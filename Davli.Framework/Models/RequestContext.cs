using Microsoft.AspNetCore.Http;
using Davli.Framework.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Models
{
    public class RequestContext : ITransientLifetime
    {
        private readonly IHttpContextAccessor _context;
        public RequestContext(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string Username => _context.HttpContext?.Request?.Headers["Username"].FirstOrDefault();
        public string ApiToken=> _context.HttpContext?.Request?.Headers["ApiToken"].FirstOrDefault();
        public string Authorization=> _context.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault();
        public int? UserId
        {
            get
            {
                int.TryParse(
                    _context.HttpContext?.Request?.Headers.FirstOrDefault(c => c.Key == "UserId").Value,
                    out var uun);
                return uun;
            }
        }
    }
}
