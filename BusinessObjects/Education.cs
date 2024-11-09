﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EducationId { get; set; }
        public int MemberId { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public bool IsCurrent { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public string StartDate => $"{FromMonth}/{FromYear}";
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public string FormatEndDate()
        {
            if (ToMonth == null) return "Now";
            return $"{ToMonth}/{ToYear}";
        }
        public string EndDate => FormatEndDate();

        public virtual Member Member { get; set; }
    }
}
