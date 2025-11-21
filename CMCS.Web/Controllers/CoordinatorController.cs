using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Web.Data;
using CMCS.Web.Models;

namespace CMCS.Web.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly CMCSContext _context;

        public CoordinatorController(CMCSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderByDescending(c => c.Year)
                .ThenByDescending(c => c.Month)
                .ToListAsync();

            return View(claims);
        }

        public async Task<IActionResult> Approve(int id, string? comment)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.ApprovedByCoordinator;
            claim.CoordinatorComments = comment;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id, string? comment)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.RejectedByCoordinator;
            claim.CoordinatorComments = comment;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
