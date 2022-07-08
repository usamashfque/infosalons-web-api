using Infosalons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> SignIn(SignIn user);
        Task<bool> CheckIsRegistered(string email);
    }
}
