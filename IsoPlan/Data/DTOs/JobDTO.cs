using System;
using System.Collections.Generic;

namespace IsoPlan.Data.DTOs
{
    public class JobDTO
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
        public float Remaining { get; set; }
        public float TotalFactures { get; set; }
        public DateTime? DevisDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? RGDate { get; set; }
        public bool RGCollected { get; set; }
        public string Status { get; set; }
        public List<JobItemDTO> JobItems { get; set; }
        public List<FactureDTO> Factures { get; set; }
    }
}
