using CAB.Models;
using Microsoft.EntityFrameworkCore;

namespace CAB.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }
        //public DbSet<Cab_model> ProcessData { get; set; }
        public DbSet<CabData> tbl_CabData { get; set; }
        public DbSet<Upload_dtl> tbl_ImportDtls { get; set; }
        public DbSet<UploadBinding> tbl_UploadBinding { get; set; }
        

    }
}
