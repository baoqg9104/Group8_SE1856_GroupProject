﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers.DTOs.Member
{
    public class UpdateMemberDTO
    {
        public int UserId {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone {  get; set; }
        public int? Gender { get; set; }
    }
}
