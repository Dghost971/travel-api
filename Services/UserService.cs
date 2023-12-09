using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class UserService
    {
        private readonly TravelDbContext _dbContext;

        public UserService(TravelDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.User.ToList();
        }

        public User GetUserById(Guid id)
        {
            return _dbContext.User.FirstOrDefault(u => u.Id == id);
        }

        public User CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public ServiceActionResult UpdateUser(Guid id, User updatedUser)
        {
            var existingUser = _dbContext.User.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.UserName = updatedUser.UserName;
                existingUser.Email = updatedUser.Email;
                _dbContext.SaveChanges();
                return ServiceActionResult.FromSuccess();
            }

            return ServiceActionResult.FromError("User not found", ServiceActionErrorReason.NotFound);
        }

        public ServiceActionResult DeleteUser(Guid id)
        {
            var existingUser = _dbContext.User.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                _dbContext.User.Remove(existingUser);
                _dbContext.SaveChanges();
                return ServiceActionResult.FromSuccess();
            }

            return ServiceActionResult.FromError("User not found", ServiceActionErrorReason.NotFound);
        }

        public IEnumerable<Comments> GetCommentsByUserId(Guid id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Id == id);
            return user?.Comments ?? new List<Comments>();
        }

        public IEnumerable<Voyage> GetTripsByUserId(Guid id)
        {
            var user = _dbContext.User.FirstOrDefault(u => u.Id == id);
            return user?.Voyage ?? new List<Voyage>();
        }
    }
}
