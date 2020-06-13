using Microsoft.AspNetCore.Mvc;
using SportsGoods.Models;
namespace SportsGoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        private readonly ICategoryRepository catRepository;
        public HomeController(IRepository repo, ICategoryRepository catRepo)
        {
            repository = repo;
            catRepository = catRepo;
        }
        public IActionResult Index()
        {
            return View(repository.Products);
        }
        public IActionResult UpdateProduct(long key)
        {
            ViewBag.Categories = catRepository.Categories;
            return View(key == 0 ? new Product() : repository.GetProduct(key));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                repository.AddProduct(product);
            }
            else
            {
                repository.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            repository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}

