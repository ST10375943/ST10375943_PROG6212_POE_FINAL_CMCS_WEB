using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Web.Data;

namespace CMCS.Web.Controllers
{
    public class ClaimHistoryController : Controller
    {
        private readonly CMCSContext _context;

        public ClaimHistoryController(CMCSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims
                .OrderByDescending(c => c.Year)
                .ThenByDescending(c => c.Month)
                .ThenByDescending(c => c.Id)
                .ToListAsync();

            return View(claims);
        }
    }
}
