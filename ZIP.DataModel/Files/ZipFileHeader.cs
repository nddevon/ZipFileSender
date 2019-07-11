using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZIP.DataModel.Common;

namespace ZIP.DataModel.Files {
    [Table("ZipFileHeader", Schema = "dbo")]
    public class ZipFileHeader: ModelBase {
        public ZipFileHeader() {
            ZipFileDetails = new List<ZipFileDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileHeaderId { get; set; }

        public string FileName { get; set; }

        public ICollection<ZipFileDetail> ZipFileDetails { get; set; }
    }
}
