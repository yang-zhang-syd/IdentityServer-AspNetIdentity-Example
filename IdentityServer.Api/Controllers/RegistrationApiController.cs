using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer.Data;
using IdentityServer.Data.Models;
using IdentityServer.Data.Services;

namespace IdentityServer.Api.Controllers
{
    public class RegistrationApiController : ApiController
    {
        private readonly DbContext _dbContext = new DbContext();
        private readonly UserManager _userManager;

        public RegistrationApiController()
        {
            _userManager = new UserManager(new UserStore(_dbContext));
        }

        [HttpPost]
        [Route("api/registrationapi/register")]
        public async Task<IHttpActionResult> Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data invalid.");

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);
            return Ok(new {result = result.Succeeded, sub = user == null && result.Succeeded ? "" : user.Id});
        }

        [HttpPost]
        [Route("api/registrationapi/UpdatePassword")]
        public async Task<IHttpActionResult> UpdatePassword(UpdateUserPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data invalid.");

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == model.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);
            return Ok(new { result = result.Succeeded, sub = user.Id });
        }
    }
}
