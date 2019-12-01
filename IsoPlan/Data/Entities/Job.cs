﻿using System;
using System.Collections.Generic;

namespace IsoPlan.Data.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public string ClientContact { get; set; }
        public string DevisStatus { get; set; }
        public float TotalBuy { get; set; }
        public float TotalSell { get; set; }
        public float TotalProfit { get; set; }
        public DateTime? DevisDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RGDate { get; set; }
        public bool RGCollected { get; set; }
        public string Status { get; set; }
        public List<JobItem> JobItems { get; set; }
        public List<JobFile> Files { get; set; }
    }
}
