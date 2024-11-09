using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class InterviewSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int InterviewId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime InterviewDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("ApplicationId")]
        public virtual JobApplication JobApplication { get; set; }
    }
}
