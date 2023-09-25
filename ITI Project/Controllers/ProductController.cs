using ITI_Project.Data;
using ITI_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITI_Project.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
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
                return View("Index", _context.products.ToList());
            }
            else
            {
                return View("Index", _context.products.Where(d => d.Name.Contains(search) ||
                                                                   d.Description.Contains(search)).ToList());
            }
        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Product prod = _context.products.Include(d => d.Category).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = prod;

            if (prod == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", prod);
            }
        }

        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.AllCategories = _context.categories.ToList();
            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Product prod, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    prod.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    prod.ImagePath = "\\images\\No_Image.png";
                }

                _context.products.Add(prod);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Create", prod);
            }
        }


        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Product prod = _context.products.FirstOrDefault(d => d.Id == id);

            if (prod == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Edit", prod);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Product prod, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (prod.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + prod.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    prod.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                _context.products.Update(prod);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllCategories = _context.categories.ToList();
                return View("Edit", prod);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Product prod = _context.products.Include(d => d.Category).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentDept = prod;

            if (prod == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", prod);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Product prod = _context.products.Find(id);

            if (prod != null && prod.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + prod.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.products.Remove(prod);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }
    }
}
