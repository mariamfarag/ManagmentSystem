using BusinessLogic.BaseRepository;
using DataAccess;
using DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelRepositories.User
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        //ManagementSystemDBContext _Context;
        public UserRepository()
        {
          //  _Context = new ManagementSystemDBContext();
        }
    }

    public interface IUserRepository : IBaseRepository<Users>
    {
        #region Custom_Methods
        #endregion
    }
}
