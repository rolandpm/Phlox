using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Phlox.Models;
using System.Data.Entity;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private ILogger<T>? _logger;

        protected ILogger<T> Logger
            => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
    }
}
