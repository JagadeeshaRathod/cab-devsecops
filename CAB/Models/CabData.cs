using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAB.Models
{
    public class  CabData
    {
        //public int Id { get; set; }
        [Key]

        [Column("Cab_Id")]
        public int Id { get; set; }

       // [Required]
        [Column("ArmyNo")]
        public string ArmyNo { get; set; }

       // [Required]
        [Column("Name")]
        public string  Name { get; set; }

      
        [Column("Complaint_Type")]
        public string Complaint_Type { get; set; }

        [Column("Complaint_No")]
        public string? ComplaintNo { get; set; }

        //[Required]
        [Column("Rank")]
        public string  Rank { get; set; }

    
        [Column("Subject")]
        public string ? Subject { get; set; }

   
        [Column("Regt")]
        public string ? Regt { get; set; }

        [Column("Case_Status")]
        public string ? Case_Status { get; set; }


       // [Required]
        [Column("Dt_Of_Complaint")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dt_Of_Complaint { get; set; }



        [Column("Recd_Date_Cab")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Recd_Date_Cab { get; set; }

        [Column("InfoDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime InfoDate { get; set; }
        //public bool Checked { get; set; }

    }
}
