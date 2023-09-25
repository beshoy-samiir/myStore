using ITI_Project.Data;
using ITI_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;

            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.categories.ToList());
            }
            else
            {
                return View("Index", _context.categories.Where(d => d.Name.Contains(search)).ToList());
            }
        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Category cat = _context.categories.Include(d => d.products).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = cat;

            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", cat);
            }
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Category cat)
        {
            if (ModelState.IsValid == true)
            {
                _context.categories.Add(cat);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Create", cat);
            }
        }


        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Category cat = _context.categories.FirstOrDefault(d => d.Id == id);

            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", cat);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Category cat)
        {
            if (ModelState.IsValid == true)
            {
                _context.categories.Update(cat);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", cat);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Category cat = _context.categories.Include(d => d.products).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = cat;

            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", cat);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Category cat = _context.categories.Find(id);
            _context.categories.Remove(cat);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }

    }
}
