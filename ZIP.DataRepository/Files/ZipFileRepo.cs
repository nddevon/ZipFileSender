using System;
using System.Collections.Generic;
using System.Linq;
using ZIP.DataModel.Dto;
using ZIP.DataModel.Files;
using ZIP.DataRepository.Common;
using ZIP.DataStore;

namespace ZIP.DataRepository.Files {
    public class ZipFileRepo: GenericRepository {
        public ZipFileRepo() : base(new EFDataContext()) {
        }

        public int SaveFile(ZipFileHeaderDto zipFileDto) {
            var objFileHeader = Query<ZipFileHeader>().Where(t => t.FileName == zipFileDto.FileName).FirstOrDefault();

            if (objFileHeader == null) {
                var newProduct = new ZipFileHeader {
                    FileName = zipFileDto.FileName,
                    CreatedDate = DateTime.Now,
                    CreatedUser = 1,
                    IsActive = true
                };
                SaveFileDetails(zipFileDto.ZipFileDetails, newProduct);
                Insert(newProduct);
            }
            else {
                objFileHeader.FileName = zipFileDto.FileName;
                objFileHeader.UpdatedDate = DateTime.Now;
                objFileHeader.UpdatedUser = 2;
            }
            SaveChenge();
            return -1;
        }

        private void SaveFileDetails(List<ZipFileDetailDto> zipFileDetails, ZipFileHeader objFileHeader) {
            foreach (var item in zipFileDetails) {
                objFileHeader.ZipFileDetails.Add(new ZipFileDetail {
                    FileHeaderId = item.FileHeaderId,
                    FileName = item.FileName,
                    IsDerectory = item.IsDerectory
                });
            }
        }
    }
}
