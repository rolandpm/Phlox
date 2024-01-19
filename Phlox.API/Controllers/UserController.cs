using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<UserController>
    {

        /// <summary>
        /// Gets all the Users
        /// </summary>
        /// <returns>Returns a list of all Userss</returns>
        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            Logger.LogInformation("UsersController.GetAll called");

            return Context.GetAllUsers();
        }

        /// <summary>
        /// Gets a user based on ID.
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>Returns the user matching the ID specified. Returns 404 if the user does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<Users> GetById(int id)
        {
            Users? user = Context.GetUserById(id);

            if (user is null)
            {
                Logger.LogInformation($"UsersController.Get did not find user with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"UsersController.Get found user {user}");
            return user;
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Returns the result of the create action.</returns>
        [HttpPost]
        public IActionResult Create(UsersDTO userDTO)
        {
            var newUser = userDTO.ToModel();
            Context.CreateUser(newUser);

            Logger.LogInformation($"UsersController.Create created user {newUser}");
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        /// <summary>
        /// Updates the specified External Account.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the user does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, UsersDTO userDTO)
        {
            Users user = userDTO.ToModel();
            user.Id = id;

            if (!Context.UpdateUser(user))
            {
                Logger.LogInformation($"UsersController.Update did not find user with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"UsersController.Update updated user with an ID of {id}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The identifier of the user to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the user does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Context.DeleteUser(id))
            {
                Logger.LogInformation($"UsersController.Delete did not find user with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"UsersController.Update updated user with an ID of {id}");
            return NoContent();
        }
    }
}