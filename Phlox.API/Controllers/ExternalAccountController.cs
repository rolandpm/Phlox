
using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalAccountController : BaseController<ExternalAccountController>
    {

        /// <summary>
        /// Gets all the ExternalAccount
        /// </summary>
        /// <returns>Returns a list of all ExternalAccounts</returns>
        [HttpGet]
        public ActionResult<List<ExternalAccount>> GetAll()
        {
            Logger.LogInformation("ExternalAccountController.GetAll called");

            return Context.GetAllExternalAccounts();
        }

        /// <summary>
        /// Gets a externalAccount based on ID.
        /// </summary>
        /// <param name="id">The externalAccount ID</param>
        /// <returns>Returns the externalAccount matching the ID specified. Returns 404 if the externalAccount does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<ExternalAccount> GetById(int id)
        {
            ExternalAccount? externalAccount = Context.GetExternalAccountById(id);

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
        /// <returns>Returns the result of the create action. Returns 404 if the userID specified in the externalAccount to create doesn't exist.</returns>
        [HttpPost]
        public IActionResult Create(ExternalAccountDTO externalAccountDTO)
        {
            //If user doesn't exist
            if (Context.GetUserById(externalAccountDTO.UserId) is null)
            {
                Logger.LogInformation($"ExternalAccountController.Create did not find User with an ID of {externalAccountDTO.UserId}");
                return NotFound("User not found");
            }

            var newExternalAccount = externalAccountDTO.ToModel();
            Context.CreateExternalAccount(newExternalAccount);

            Logger.LogInformation($"ExternalAccountController.Create created externalAccount {newExternalAccount}");
            return CreatedAtAction(nameof(GetById), new { id = newExternalAccount.Id }, newExternalAccount);
        }

        /// <summary>
        /// Updates the specified External Account.
        /// </summary>
        /// <param name="id">The ID of the external account.</param>
        /// <param name="externalAccountDTO">The external account dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the externalAccount does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, ExternalAccountDTO externalAccountDTO)
        {
            ExternalAccount externalAccount = externalAccountDTO.ToModel();
            externalAccount.Id = id;

            if (!Context.UpdateExternalAccount(externalAccount))
            {
                Logger.LogInformation($"ExternalAccountController.Update did not find externalAccount with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ExternalAccountController.Update updated externalAccount with an ID of {id}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified externalAccount.
        /// </summary>
        /// <param name="id">The identifier of the externalAccount to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the externalAccount does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Context.DeleteExternalAccount(id))
            {
                Logger.LogInformation($"ExternalAccountController.Delete did not find externalAccount with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"ExternalAccountController.Update updated externalAccount with an ID of {id}");
            return NoContent();
        }
    }
}