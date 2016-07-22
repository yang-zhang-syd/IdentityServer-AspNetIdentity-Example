using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer.Data;
using IdentityServer.Data.Models;
using IdentityServer.Data.Services;

namespace IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbContext _dbContext = new DbContext();
        private readonly UserManager _userManager;

        public AccountController()
        {
            _userManager = new UserManager(new UserStore(_dbContext));            
        }

        [HttpGet]
        [Route("account/register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("account/register")]
        public async Task<ActionResult> Register(string signin, UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                return Redirect("~/account/details");
            }

            return View(model);
        }

        [HttpGet]
        [Route("account/details")]
        public ActionResult Details()
        {
            return View();
        }
    }
}