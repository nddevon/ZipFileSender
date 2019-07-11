using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ZIP.FileSender.Filters;
using ZIP.FileSender.ViewModel;
using ZIP.Utility;

namespace ZIP.FileSender.Controllers {
    [ControlPanelExceptionFilter]
    [ControlPanelAuth]
    public class ZipFileSendController: BaseController {
        private readonly IConfiguration configuration;

        public ZipFileSendController(IConfiguration iConfig) {
            configuration = iConfig;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Upload(List<ZipFileViewModel> model) {
            return View();
        }

        public IActionResult Save() {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Index");
            }
            var fileList = new List<ZipFileViewModel>();
            ZipInputStream zip = new ZipInputStream(file.OpenReadStream());
            ZipEntry item;

            
            while ((item = zip.GetNextEntry()) != null) {
                fileList.Add(new ZipFileViewModel {
                    Name = item.Name,
                    IsFolder = item.IsDirectory
                }
                );
            }
            zip.Close();
            return View(fileList);
        }

        [HttpPost]
        public IActionResult SaveZipFiles(IEnumerable<ZipFileViewModel> model) {
            try {
                var fileList = new List<ZipFileViewModel>();
                var key = configuration.GetSection("AppSettings").GetSection("EncKey").Value;

                foreach (var item in model) {
                    fileList.Add(new ZipFileViewModel {
                        Name = EncryptDecryptString.Encrypt(item.Name, key),
                        IsFolder = item.IsFolder
                    });
                }
                var fileListJson = JsonConvert.SerializeObject(fileList);
                var response = ApiJsonRequest("/api/values/savezipfiles", fileListJson);

                return RedirectToAction("Save");
            }
            catch (Exception) {

                return RedirectToAction("Error");
            }
        }
    }
}