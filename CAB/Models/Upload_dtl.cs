using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAB.Models
{
    public class Upload_dtl
    {
        [Key]
        [Column("Import_Id")]
        public int Id { get; set; }


        [Column("Uploaded_filename")]
        public string? FileName { get; set; }
        [Column("Uploaded_dt")]
        public DateTime uploaddt { get; set; }

        [Column("Uploaded_By")]
        public string? UploadedBy  { get; set; }

        //public string Status { get; set; }
        //public bool ProcessData { get; set; }
    }

    public class CreateUpload_dtl
    {
        public IFormFile? postedFiles { get; set; }

    }
}

