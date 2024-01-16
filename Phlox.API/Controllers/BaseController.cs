using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phlox.Models;

namespace Phlox.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T>(PhloxContext context) : ControllerBase where T : BaseController<T>
    {
        private ILogger<T>? _logger;
        protected ILogger<T> Logger
            => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        private readonly PhloxContext _context = context;
        protected PhloxContext Context { get { return _context; } }
    }
}
