using DataAccess.Enums;
using DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModels
{
    public class TaskViewModel
    {
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? PriorityLevel { get; set; }
        public Status StatusTask { get; set; }
        public int userId { get; set; }
        //public Users? user { get; set; }
    }
}
