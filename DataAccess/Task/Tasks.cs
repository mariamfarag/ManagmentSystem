﻿using DataAccess.Enums;
using DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Task
{
    public class Tasks
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? PriorityLevel { get; set; }
        public Status StatusTask { get; set; }
        //public Guid userId { get; set; }
        public virtual Users? user { get; set; }
    }
}
