using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZIP.DataModel.Files {
    public class ZipFileDetail {
        [Key]
        public int ZipFileDetailId { get; set; }

        [ForeignKey("ZipFileHeader")]
        public int FileHeaderId { get; set; }

        public bool IsDerectory { get; set; }
        public string FileName { get; set; }

        public virtual ZipFileHeader ZipFileHeader { get; set; }
    }
}
