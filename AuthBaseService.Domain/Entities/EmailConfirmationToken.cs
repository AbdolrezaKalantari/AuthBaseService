using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Domain.Entities
{
    public class EmailConfirmationToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; } = false;

        public User? User { get; set; }
    }

}
