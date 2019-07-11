using Microsoft.EntityFrameworkCore;
using ZIP.DataModel.Files;
using ZIP.Utility;

namespace ZIP.DataStore {
    public class EFDataContext: DbContext {
        public DbSet<ZipFileHeader> ZipFileHeader { get; set; }
        public DbSet<ZipFileDetail> ZipFileDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(WebConfigSetting.ConnectionString);
        }
    }
}
