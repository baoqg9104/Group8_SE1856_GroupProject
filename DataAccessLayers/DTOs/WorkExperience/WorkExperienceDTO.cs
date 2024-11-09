using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.DTOs.WorkExperience
{
    public class WorkExperienceDTO
    {
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool IsCurrent { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
    }
}
