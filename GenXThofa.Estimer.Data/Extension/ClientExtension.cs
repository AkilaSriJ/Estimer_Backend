using GenXThofa.Technologies.Estimer.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Extension
{
    public static class ClientExtension
    {
        public static async Task<bool> ExistsByEmailAsync(this DbSet<Client> clients, string email)
        {
            return await clients.AnyAsync(c=>c.Email == email); 
        }
        public static async Task<bool> ExistsByPhoneAsync(this DbSet<Client> clients, string phone)
        {
            return await clients.AnyAsync(c=>c.Phone == phone);
        }
    }
}
