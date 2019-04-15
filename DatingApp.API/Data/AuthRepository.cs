using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    // This class is responsible of actually querying the database via entity framework
    // and it is here where we inject our data context.
    public class AuthRepository : IAuthRepository
    {
        // Using _ because is a private property
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
             // We Initialize these two arrays
            byte[] passwordHash, passwordSalt;
            // We want to pass the values as refrence instead of by value, so the byte array gets updated
            // to the values. That is why we write out 
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));                
            }
        }
        public async Task<User> Login(string username, string password)
        {
           var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

           if (user == null)
           return null;

           if (!VerifyPasswordsHash(password, user.PasswordHash, user.PasswordSalt))
           return null;

           return user;
        }

        private bool VerifyPasswordsHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));                
               for (int i = 0; i < computedHash.Length; i++)
               {
                   if (computedHash[i] != passwordHash[i]) return false;
               }
            }
             return true;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}