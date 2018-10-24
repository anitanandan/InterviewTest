namespace Interview.Web.Controllers
{
    using System.Web.Mvc;

    using Interview.Domain.Models;
    using Interview.Domain.Service;
    using Interview.Service;
    using Interview.Web.Model;

    public class UserController : Controller
    {
        public IUserService UserService;

        public UserController()
            : this(new UserService())
        {
        }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        // GET
        public ActionResult Index(User failedUser = null)
        {
            var users = UserService.FindAll();
            return View("Index", new UserListModel() { Users = users, CreatingUser = failedUser });
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            var response = UserService.Save(user);
            if (response.Success)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(error.Property ?? "Error", error.ErrorMessage);
            }

            return Index(user);
        }
    }
}