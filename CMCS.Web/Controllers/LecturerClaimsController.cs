using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMCS.Web.Data;
using CMCS.Web.Models;

namespace CMCS.Web.Controllers
{
    public class LecturerClaimsController : Controller
    {
        private readonly CMCSContext _context;

        public LecturerClaimsController(CMCSContext context)
        {
            _context = context;
        }

        // GET: /LecturerClaims
        public async Task<IActionResult> Index()
        {
            var list = await _context.Claims
                .OrderByDescending(c => c.SubmittedDate)
                .ToListAsync();

            return View(list);
        }

        // GET: /LecturerClaims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LecturerClaims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.SubmittedDate = DateTime.Now;
                claim.Status = ClaimStatus.Pending;

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(claim);
        }

    }
}
