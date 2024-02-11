using BusinessLogic.BaseRepository;
using DataAccess.Task;
using DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ModelRepositories.Task
{
    public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
    {
        public TaskRepository()
        {
        }
    }
    public interface ITaskRepository : IBaseRepository<Tasks>
    {
        #region Custom_Methods
        #endregion
    }
}