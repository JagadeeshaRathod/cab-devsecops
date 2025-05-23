using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CAB.Models
{
    public class Cab_model 
    {
        [Key]
        public int Id { get; set; }

        [JsonInclude]
        [JsonPropertyName("ArmyNo")]
        public string? ArmyNo { get; set; }

        [JsonInclude]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonInclude]
        [JsonPropertyName("Complaint_Type")]
        public string? Complaint_Type { get; set; }


        [JsonInclude]
        [JsonPropertyName("ComplaintNo")]
        public string? ComplaintNo { get; set; }

        [JsonInclude]
        [JsonPropertyName("Rank")]
        public string? Rank { get; set; }

        [JsonInclude]
        [JsonPropertyName("Subject")]
        public string? Subject { get; set; }

        [JsonInclude]
        [JsonPropertyName("Regt")]
        public string? Regt { get; set; }

        [JsonInclude]
        [JsonPropertyName("Case_Status")]
        public string? Case_Status { get; set; }

        //[JsonInclude]
        //[DataType(DataType.Date)]
        //[Display
        //(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[JsonPropertyName("Dt_Of_Complaint")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dt_Of_Complaint { get; set; }

        //[JsonInclude]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[JsonPropertyName("Recd_Date_Cab")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Recd_Date_Cab { get; set; }

        //[JsonInclude]
        //[JsonPropertyName("Checked")]
        //public bool Checked { get; set; }
    }
}
