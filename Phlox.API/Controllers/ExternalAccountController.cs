//TODDO: COMMENTS

using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalAccountController(PhloxContext context) : BaseController<ExternalAccountController>(context)
    {

        /// <summary>
        /// Gets all the ExternalAccount
        /// </summary>
        /// <returns>List of all ExternalAccount</returns>
        [HttpGet]
        public async Task<ActionResult<List<ExternalAccount>>> GetAll()
        {
            Logger.LogInformation("ExternalAccountController.GetAll called");

            return await Context.ExternalAccount.ToListAsync();
        }

        /// <summary>
        /// Gets a externalAccount based on ID.
        /// </summary>
        /// <param name="id">The externalAccount ID</param>
        /// <returns>externalAccount with matching ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternalAccount>> Get(int id)
        {
            ExternalAccount? externalAccount = await (
                from u in Context.ExternalAccount
                where u.Id == id
                select u
            ).FirstOrDefaultAsync();

            if (externalAccount is null)
            {
                Logger.LogInformation($"ExternalAccountController.Get did not find externalAccount with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"ExternalAccountController.Get found externalAccount {externalAccount}");
            return externalAccount;
        }

        /// <summary>
        /// Creates the specified externalAccount.
        /// </summary>
        /// <param name="externalAccount">The externalAccount.</param>
        /// <returns>externalAccount that was created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ExternalAccount externalAccount)
        {
            Context.Add(externalAccount);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"ExternalAccount {externalAccount.ServiceName} created");

            return CreatedAtAction(nameof(Get), new {id = externalAccount.Id}, externalAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExternalAccount externalAccount)
        {
            externalAccount.Id = id;
            var result = await Get(id);
            var existingexternalAccount = result.Value;

            if (existingexternalAccount is null) return NotFound();

            Context.Entry(existingexternalAccount).CurrentValues.SetValues(externalAccount);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Get(id);
            var externalAccount = result.Value;

            if (externalAccount is null) return NotFound("externalAccount not found");

            Context.Remove(externalAccount);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}