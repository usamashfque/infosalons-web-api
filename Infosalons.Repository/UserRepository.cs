using Infosalons.Domain.Interfaces;
using Infosalons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckIsRegistered(string email)
        {
            var result = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (result is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> SignIn(SignIn cred)
        {
            return await _context.Users.Where(x => x.Email == cred.Email && x.Password == cred.Password).FirstOrDefaultAsync();
        }
        //public async Task<IEnumerable<Order>> GetOrdersByOrderName(string orderName)
        //{
        //    return await _context.Orders.Where(c => c.OrderDetails.Contains(orderName)).ToListAsync();
        //}
    }
}
