using System.Collections.Generic;

namespace ZIP.DataModel.Dto {
    public class ZipFileHeaderDto {
        public int FileHeaderId { get; set; }
        public string FileName { get; set; }
        public List<ZipFileDetailDto> ZipFileDetails { get; set; }
    }
}
