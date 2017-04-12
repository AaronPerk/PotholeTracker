using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IUserDAL
    {

        bool RegisterUser(RegisterModel newUser);
        bool VerifyUniqueEmail(string email);
        int LogInUser(LoginModel user);
        User GetUser(int userId);
    }
}
