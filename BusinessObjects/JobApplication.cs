using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class JobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public int MemberId { get; set; }
        public string CvFilePath { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status {  get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public virtual InterviewSchedule InterviewSchedule { get; set; }
    }
}
