using GenXThofa.Technologies.Estimer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAll();
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
        Task SaveChangesAsync();
    }
}
