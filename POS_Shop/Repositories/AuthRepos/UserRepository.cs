using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Repositories.AuthRepos
{
    public class UserRepository : IDisposable
    {
        private readonly POSDbContext _context;

        public UserRepository()
        {
            _context = new POSDbContext();
        }

        // Get all users with optional filtering
        public List<AuthUser> GetAllUsers(
            bool includeInactive = false,
            AuthUserRole? roleFilter = null,
            string searchTerm = "")
        {
            IQueryable<AuthUser> query = _context.Users;

            // Filter by active status
            if (!includeInactive)
            {
                query = query.Where(u => u.IsActive);
            }

            // Filter by role
            if (roleFilter.HasValue)
            {
                query = query.Where(u => u.Role == roleFilter.Value);
            }

            // Search by term
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(u =>
                    u.Username.ToLower().Contains(searchTerm) ||
                    u.Email.ToLower().Contains(searchTerm));
            }

            return query.OrderBy(u => u.Id).ToList();
        }

        // Get user by ID
        public AuthUser GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Create new user
        public bool CreateUser(AuthUser user)
        {
            try
            {
                // Check if user already exists
                if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
                {
                    return false;
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Update existing user
        public bool UpdateUser(AuthUser user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser == null) return false;

                // Check if username or email already exists for another user
                if (_context.Users.Any(u =>
                    u.Id != user.Id &&
                    (u.Username == user.Username || u.Email == user.Email)))
                {
                    return false;
                }

                // Update properties
                _context.Entry(existingUser).CurrentValues.SetValues(user);

                // Handle properties that might not be included in SetValues
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.IsActive = user.IsActive;
               
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Delete user (soft delete by setting IsActive to false)
        public bool DeleteUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null) return false;

                // Prevent deletion of SuperAdmin
                if (user.Role == AuthUserRole.SuperAdmin)
                {
                    throw new InvalidOperationException("Cannot delete SuperAdmin user");
                }

                user.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Authenticate user
        public AuthUser Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.IsActive);
            if (user == null) return null;

            if (PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                // Update last login
                user.LastLogin = DateTime.Now;
                _context.SaveChanges();
                return user;
            }

            return null;
        }

        // Get user count by role
        public Dictionary<AuthUserRole, int> GetUserCountByRole()
        {
            return _context.Users
                .Where(u => u.IsActive)
                .GroupBy(u => u.Role)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Role, x => x.Count);
        }

        // Toggle user active status
        public bool ToggleUserStatus(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null) return false;

                // Prevent deactivating SuperAdmin
                if (user.Role == AuthUserRole.SuperAdmin && !user.IsActive)
                {
                    throw new InvalidOperationException("Cannot deactivate SuperAdmin");
                }

                user.IsActive = !user.IsActive;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
