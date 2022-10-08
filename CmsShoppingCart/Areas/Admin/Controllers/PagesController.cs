using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {        
        private readonly CmsShoppingCartContext _context;
        public PagesController(CmsShoppingCartContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            var pages = _context.Pages.OrderBy(p => p.Sorting);            
            var pageList = await pages.ToListAsync();                         
            return View(pageList);
        }
        public async Task<IActionResult> Details (int id)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await _context.Pages.FirstOrDefaultAsync(p => p.Slug == page.Slug);
                // problem. if slug already exists
                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exists");
                    return View(page);
                }
                // good. if slug does not exist
                _context.Add(page);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(page);
        }
    }
}
