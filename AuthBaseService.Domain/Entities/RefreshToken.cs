using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } = false;

        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
