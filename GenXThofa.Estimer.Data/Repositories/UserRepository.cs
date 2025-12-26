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
    public class UserRepository(AppDbContext dbContext, ILogger<UserRepository> logger) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<UserRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users.AsNoTracking();

        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            if(user== null)
                throw new ArgumentNullException(nameof(user));
            var entry=await _dbContext.Users.AddAsync(user);
            return entry.Entity;
        }

        public Task UpdateAsync(User user)
        {
            if(user==null)
                throw new ArgumentNullException(nameof(user));
            _dbContext.Users.Update(user);
            return Task.CompletedTask;

        }
        public Task DeleteAsync(User user)
        {
            if(user==null)
                throw new ArgumentNullException(nameof(user));
            _dbContext.Users.Remove(user);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
}
}
