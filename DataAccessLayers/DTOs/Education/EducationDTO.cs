using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.DTOs.Education
{
    public class EducationDTO
    {
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public bool IsCurrent { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }

    }
}
