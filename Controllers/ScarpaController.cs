using Microsoft.AspNetCore.Mvc;
using s17_l3.Models;

namespace s17_l3.Controllers
{
    public class ScarpaController : Controller
    {
        private readonly ScarpeDbContext _context;

        public ScarpaController(ScarpeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var scarpe = _context.GetAll();
            return View(scarpe);
        }


        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            if (id.HasValue)
            {
                var scarpa = _context.GetById(id.Value);
                if (scarpa is null)
                {
                    return View("Error");
                }
                return View(scarpa);
            }
            else
            {
                return RedirectToAction("Index", "Scarpa");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Scarpa scarpa, IFormFile img1, IFormFile img2, IFormFile img3)
        {
            if (img1 != null)
            {
                scarpa.img1 = UploadImage(img1);
            }

            if (img2 != null)
            {
                scarpa.img2 = UploadImage(img2);
            }

            if (img3 != null)
            {
                scarpa.img3 = UploadImage(img3);
            }

            var scarpaAggiunta = _context.Scarpe.Add(scarpa);
            _context.SaveChanges();

            if (scarpaAggiunta is not null)
            {
                TempData["messaggioAggiunta"] = $"Articolo aggiunto correttamente";
                return RedirectToAction("Details", new { id = scarpa.ID });
            }

            TempData["messaggioErroreA"] = $"C'è stato un errore nell'aggiunta di {scarpa.Nome}";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null) return RedirectToAction("Index", "Scarpa");

            var scarpa = _context.GetById(id.Value);
            if (scarpa is null) return View("Error");
            return View(scarpa);
        }

        [HttpPost]
        public IActionResult Edit(Scarpa scarpa, IFormFile newImg1)
        {
            _context.Modify(scarpa);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = scarpa.ID });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var scarpa = _context.GetById(id.Value);
            return View(scarpa);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var scarpaCancellata = _context.hardDelete(id);
            if (scarpaCancellata is not null)
            {
                TempData["messaggioCancellato"] = $"Risorsa {scarpaCancellata.Nome} è stato eliminato";
                return RedirectToAction("Index");
            }
            TempData["messaggioErroreC"] = $"C'è stato un errore nell'eliminazione di {scarpaCancellata}"
; return RedirectToAction("Index", new { id });
        }

        private string UploadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                string fileName = Path.GetFileName(image.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                return fileName;
            }

            return "";
        }
    }


}