using Microsoft.AspNetCore.Mvc;
using Phlox.Models;
using Phlox.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : BaseController<DealController>
    {

        /// <summary>
        /// Gets all the Deal
        /// </summary>
        /// <returns>Returns a list of all Deals</returns>
        [HttpGet]
        public ActionResult<List<Deal>> GetAll()
        {
            Logger.LogInformation("DealController.GetAll called");

            return Context.GetAllDeals();
        }

        /// <summary>
        /// Gets a deal based on ID.
        /// </summary>
        /// <param name="id">The deal ID</param>
        /// <returns>Returns the deal matching the ID specified. Returns 404 if the deal does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<Deal> GetById(int id)
        {
            Deal? deal = Context.GetDealById(id);

            if (deal is null)
            {
                Logger.LogInformation($"DealController.Get did not find deal with an ID of {id}");
                return NotFound();
            }

            Logger.LogInformation($"DealController.Get found deal {deal}");
            return deal;
        }

        /// <summary>
        /// Creates the specified deal.
        /// </summary>
        /// <param name="deal">The deal.</param>
        /// <returns>Returns the result of the create action.</returns>
        [HttpPost]
        public IActionResult Create(DealDTO dealDTO)
        {
            var newDeal = dealDTO.ToModel();
            Context.CreateDeal(newDeal);

            Logger.LogInformation($"DealController.Create created deal {newDeal}");
            return CreatedAtAction(nameof(GetById), new { id = newDeal.Id }, newDeal);
        }

        /// <summary>
        /// Updates the specified deal.
        /// </summary>
        /// <param name="id">The ID of the deal.</param>
        /// <param name="dealDTO">The deal dto.</param>
        /// <returns>Returns the result of the update action. Returns 404 if the deal does not exist.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, DealDTO dealDTO)
        {
            Deal deal = dealDTO.ToModel();
            deal.Id = id;

            if (!Context.UpdateDeal(deal))
            {
                Logger.LogInformation($"DealController.Update did not find deal with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"DealController.Update updated deal with an ID of {id}");
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified deal.
        /// </summary>
        /// <param name="id">The identifier of the deal to delete</param>
        /// <returns>Returns the result of the delete action. Returns 404 if the deal does not exist.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Context.DeleteDeal(id))
            {
                Logger.LogInformation($"DealController.Delete did not find deal with an ID of {id}");
                return NotFound("External account not found");
            }

            Logger.LogInformation($"DealController.Update updated deal with an ID of {id}");
            return NoContent();
        }
    }
}