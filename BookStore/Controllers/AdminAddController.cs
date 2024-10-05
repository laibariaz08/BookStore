//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using BookStore.Models;

//namespace BookStore.Controllers
//{
//    [Authorize]
//    public class AdminAddController : Controller
//    {
//        private readonly ILogger<AdminAddController> _logger;
//        private readonly IWebHostEnvironment Env;
//        public AdminAddController(ILogger<AdminAddController> logger, IWebHostEnvironment en)
//        {
//            _logger= logger;
//            Env = en;
//        }
//        [HttpGet]
//        public ActionResult Add()
//        {
//            return View();
//        }
//        [HttpPost]
//       public ActionResult AddData(string Name, int Price, int Stock, IFormFile xyz )
//       {
//            Product product = new Product();
//            product.name = Name;
//            product.Price = Price;
//            product.stock = Stock;
//            string wwwFolder = Env.WebRootPath;
//            string path = Path.Combine(wwwFolder, "UploadedImages");
//            if (!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);
//            }
//            string filename = Path.GetFileName(xyz.FileName);
//            var PathwithFileName = Path.Combine(path, filename);
//            FileStream stream = new FileStream(PathwithFileName, FileMode.Create);
//            xyz.CopyTo(stream);
//            stream.Close();
//            int index = PathwithFileName.IndexOf("Uploaded");
//            PathwithFileName = PathwithFileName.Substring(index);
//            PathwithFileName = PathwithFileName.Replace('\\', '/');
//            PathwithFileName = "/" + PathwithFileName;
//            product.ImageUrl = PathwithFileName;
//            Productrepository repository = new Productrepository();
//            repository.Add(product);
//            return View();
//        }

//    }
//}
