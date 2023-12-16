using Microsoft.AspNetCore.Mvc;
using Session4.Models;
using Session4.Repository;

namespace Session4.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct iproduct;

        public ProductController(IProduct iproduct)
        {
            this.iproduct = iproduct;
        }
        public IActionResult Index()
        {
            var products= iproduct.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                iproduct.Add(product);
                return RedirectToAction("index");
            }
            return View(product);
        }
    }
}
