//using Microsoft.AspNetCore.Mvc;
////using NuGet.Protocol.Core.Types;
//using BookStore.Models;
//namespace BookStore.Controllers
//{
//    public class DeleteCategoryController : Controller
//    {
//        public ViewResult DeleteCategory()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult DeleteCategory(int ID)
//        {
//            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CheckDB;Trusted_Connection=True;MultipleActiveResultSets=true";
//            GenericRepository<Category> repo = new GenericRepository<Category>(connectionString);
//            repo.Delete(ID);
//            return View();
//        }
//    }
//}
