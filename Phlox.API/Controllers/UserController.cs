//TODDO: COMMENTS

using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(PhloxContext context) : BaseController<UserController>(context)
    {

        /// <summary>
        /// Gets all the users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAll()
        {
            Logger.LogInformation("UserController.GetAll called");

            return await Context.Users.ToListAsync();
        }

        /// <summary>
        /// Gets a user based on ID.
        /// </summary>
        /// <param name="id">The user ID</param>
        /// <returns>User with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id)
        {
            Users? user = await (
                from u in Context.Users
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (user is null)
            {
                Logger.LogInformation($"UserController.Get did not find user with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"UserController.Get found user {user}");
            return user;
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>User that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Users user)
        {
            Context.Add(user);
            await Context.SaveChangesAsync();

            Logger.LogInformation("User {user.first_name} {user.last_name} created", user.FirstName, user.LastName);

            return CreatedAtAction(nameof(Get), new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Users user)
        {
            user.Id = id;
            var result = await Get(id);
            var existingUser = result.Value;

            if (existingUser is null) return NotFound();

            Context.Entry(existingUser).CurrentValues.SetValues(user);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var user = result.Value;

            if (user is null) return NotFound("User not found");

            Context.Remove(user);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}