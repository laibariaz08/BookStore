//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using BookStore.Models;
//namespace BookStore.Controllers
//{
//    public class UpdateCategoryController : Controller
//    {
//        public ActionResult UpdateCategory()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult UpdateCategory(int id, string Name)
//        {
//            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CheckDB;Trusted_Connection=True;MultipleActiveResultSets=true";
//            Category category = new Category();
//            category.name = Name;
//            GenericRepository<Category> repo = new GenericRepository<Category>(connectionString);
//            repo.Update(category);
//            //Productrepository repository = new Productrepository();
//            //repository.Add(product);
//            return View();
//        }
//    }
//}
