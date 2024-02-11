using DataAccess.Task;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.User
{
    public class Users : IdentityUser
    {
        public Users()
        {
            tasks = new HashSet<Tasks>();
        }
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserType { get; set; }
        public virtual ICollection<Tasks> tasks { get; set; }

    }
}
