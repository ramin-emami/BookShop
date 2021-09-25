using BookShop.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class UploadLargeFileViewModel
    {
        [UploadFileSize(1048576)]
        public IFormFile File { get; set; }
    }
}
