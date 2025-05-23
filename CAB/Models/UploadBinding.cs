
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CAB.Models
{
    public class UploadBinding
    {
        [Key]
       public int Id { get; set; }
       public string Ipaddress { get; set; }
        
    }

}

