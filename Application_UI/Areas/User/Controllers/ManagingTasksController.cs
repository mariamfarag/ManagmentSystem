using AutoMapper;
using BusinessLogic.ModelRepositories.Task;
using BusinessLogic.ViewModels;
using DataAccess.Task;
using Microsoft.AspNetCore.Mvc;

namespace Application_UI.Areas.User.Controllers
{
    [Area("User")]
    public class ManagingTasksController : Controller
    {
        private readonly ITaskRepository _task;
        private IMapper _map;

        public ManagingTasksController(ITaskRepository task, IMapper map)
        {
            _task = task;
            _map = map;
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
        public IActionResult Create(TaskViewModel task)
        {
            //string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                Tasks taskmapper = _map.Map<Tasks>(task);
                _task.Add(taskmapper);
                _task.Save();
                return RedirectToAction("Index");
            }
            return View(task);
        }
    }
}
