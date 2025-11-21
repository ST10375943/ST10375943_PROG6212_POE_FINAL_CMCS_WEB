using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Web.Data;
using CMCS.Web.Models;

namespace CMCS.Web.Controllers
{
    public class HRController : Controller
    {
        private readonly CMCSContext _context;

        public HRController(CMCSContext context)
        {
            _context = context;
        }

        // CLAIMS READY FOR HR PROCESSING
        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims
                .Where(c => c.Status == ClaimStatus.ApprovedByCoordinator)
                .OrderByDescending(c => c.Year)
                .ThenByDescending(c => c.Month)
                .ToListAsync();

            return View(claims);
        }

        // HR approves
        public async Task<IActionResult> Approve(int id, string? comment)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.ProcessedByHR;
            claim.HRComments = comment;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // HR rejects
        public async Task<IActionResult> Reject(int id, string? comment)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.RejectedByHR;
            claim.HRComments = comment;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
