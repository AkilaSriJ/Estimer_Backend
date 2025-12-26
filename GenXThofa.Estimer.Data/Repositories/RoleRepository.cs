using GenXThofa.Technologies.Estimer.Data.Context;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Repositories
{
    public class RoleRepository(AppDbContext dbContext,ILogger<RoleRepository> logger):IRoleRepository
    {
        private readonly AppDbContext _dbContext= dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<RoleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public IQueryable<Role> GetAll()
        {
            return _dbContext.Roles.AsNoTracking();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role> GetByNameAsync(string roleName)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task<Role> CreateAsync(Role role)
        {
            if(role== null)
                throw new ArgumentNullException(nameof(role));
            var entry= await _dbContext.Roles.AddAsync(role);
            return entry.Entity;
        }

        public Task UpdateAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            _dbContext.Roles.Update(role);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Role role)
        {
            if(role==null)
                throw new ArgumentNullException(nameof(role));
            _dbContext.Roles.Remove(role);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
