using GenXThofa.Technologies.Estimer.Data.Context;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Repositories
{
    public class ClientRepository: IClientRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ClientRepository> _logger;
        public ClientRepository(AppDbContext dbContext, ILogger<ClientRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<Client> GetAll()
        {
            return _dbContext.Clients.AsNoTracking();

        }
        public async Task<Client> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
        }

        public async Task<Client> CreateAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            var entry = await _dbContext.Clients.AddAsync(client);
            return entry.Entity;
        }

        public Task UpdateAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _dbContext.Clients.Update(client);
            return Task.CompletedTask;

        }
        public Task DeleteAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _dbContext.Clients.Remove(client);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
