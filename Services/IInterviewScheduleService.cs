﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInterviewScheduleService
    {
        List<InterviewSchedule> GetInterviewSchedules();
        InterviewSchedule GetInterviewSchedule(int id);
        void AddInterviewSchedule(InterviewSchedule interviewSchedule);
        void UpdateInterviewSchedule(InterviewSchedule interviewSchedule, string? contition = null);
        void DeleteInterviewSchedule(InterviewSchedule interviewSchedule);
    }
}
