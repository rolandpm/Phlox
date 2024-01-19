
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private ILogger<T>? _logger;
        private PhloxContext? _context;

        protected ILogger<T> Logger
            => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        protected PhloxContext Context 
            => _context ??= HttpContext.RequestServices.GetRequiredService<PhloxContext>();
    }
}
