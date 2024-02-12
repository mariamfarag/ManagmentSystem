using AutoMapper;
using BusinessLogic.ModelRepositories.Task;
using BusinessLogic.ModelRepositories.User;
using BusinessLogic.ViewModels;
using DataAccess.Task;
using DataAccess.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application_UI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ManagingTasksController : Controller
    {
        private readonly ITaskRepository _task;
        private readonly IUserRepository _user;
        private readonly UserManager<Users> _userManager;
        private IMapper _map;

        public ManagingTasksController(ITaskRepository task, IMapper map, IUserRepository user, UserManager<Users> userManager)
        {
            _task = task;
            _map = map;
            _user = user;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            IEnumerable<TaskViewModel> tasksList = _map.Map<IEnumerable<TaskViewModel>>(_task.GetByAll());
            return View(tasksList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskViewModel task)
        {
            Guid userId1 = (Guid)TempData["uUserID"];
            Users usr =null;
            if (ModelState.IsValid)
            {
                if (userId1 != null)
                {
                    string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                    usr = await _userManager.GetUserAsync(HttpContext.User);
                    var usercontodo = _user.GetByAll().Where(x => x.Id == usr.Id)
                        //.
                        //Include(x => x.InquilinoActual)
                        .FirstOrDefault();
                }
                Tasks taskmapper = _map.Map<Tasks>(task);

               // taskmapper.user = usr;
                _task.Add(taskmapper);
                _task.Save();
                return RedirectToAction("Index");
            }
          
           
            return View(task);
        }
    }
}
