using System.ComponentModel.DataAnnotations;

namespace CAB.Models
{
    public class SearchRecord
    {
        [Key]
        public string? ArmyNo { get; set; }

        public string? Name { get; set; }
        public string? Complaint_Type { get; set; }

        public string? ComplaintNo { get; set; }
        public string? Rank { get; set; }
        public string? Subject { get; set; }
        public string? Regt { get; set; }
        public string? Case_Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dt_Of_Complaint { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Recd_Date_Cab { get; set; }
        public bool Checked { get; set; }
    }
}
