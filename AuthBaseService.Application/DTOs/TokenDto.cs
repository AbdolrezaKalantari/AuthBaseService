using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string Response { get; set; } = string.Empty;
    }
}
