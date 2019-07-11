using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ZIP.DataModel.Dto;
using ZIP.DataRepository.Files;
using ZIP.FileRecieverApi.Filters;
using ZIP.Utility;

namespace ZIP.FileRecieverApi.Controllers {
    [ApiExceptionFilter]
    [Authorize]
    [Route("api/values")]
    [ApiController]
    public class ZipFileRecieverController: ControllerBase {
        private readonly IConfiguration configuration;

        public ZipFileRecieverController(IConfiguration iConfig) {
            configuration = iConfig;
        }

        [HttpPost]
        [Route("savezipfiles")]
        public IActionResult SaveZipFiles(IEnumerable<ZipFileViewModel> model) {
            var repo = new ZipFileRepo();
            var fileList = new List<ZipFileDetailDto>();
            var key = configuration.GetSection("AppSettings").GetSection("EncKey").Value;

            foreach (var item in model) {
                fileList.Add(new ZipFileDetailDto {
                    FileName = EncryptDecryptString.Decrypt(item.Name, key),
                    IsDerectory = item.IsFolder
                });
            }

            var fileHeader = new ZipFileHeaderDto() {
                FileName = fileList[0].FileName,
                ZipFileDetails = fileList
            };

            repo.SaveFile(fileHeader);
            return Ok();
        }
    }

    public class ZipFileViewModel {
        public string Name { get; set; }
        public bool IsFolder { get; set; }
    }
}
