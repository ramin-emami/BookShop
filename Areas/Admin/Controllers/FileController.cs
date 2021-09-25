using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _env;
        public FileController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            foreach(var item in files)
            {
                var UploadsRootFolder = Path.Combine(_env.WebRootPath, "GalleryFiles");
                if (!Directory.Exists(UploadsRootFolder))
                    Directory.CreateDirectory(UploadsRootFolder);

                if(item!=null)
                {
                    string FileExtension = Path.GetExtension(item.FileName);
                    string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension);
                    var path = Path.Combine(UploadsRootFolder, NewFileName);

                    using (var strem = new FileStream(path, FileMode.Create))
                    {
                        await item.CopyToAsync(strem);
                    }
                }
            }

            //ViewBag.Alert = "آپلود فایل ها با موفقیت انجام شد.";
            //return View();
            return new JsonResult("success");
        }
    }
}