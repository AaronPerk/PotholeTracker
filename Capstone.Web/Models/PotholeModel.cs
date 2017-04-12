using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class PotholeModel
    {
        public int PotholeID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int WhoReported { get; set; }
        public int WhoInspected { get; set; }
        public string Picture { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime? InspectDate { get; set; }
        public DateTime? RepairStartDate { get; set; }
        public DateTime? RepairEndDate { get; set; }
        public int Severity { get; set; }
        public string Comment { get; set; }


        public string GetStatus()
        {
            if (RepairEndDate != null)
            {
                return "Repair Complete";
            }
            else if (RepairStartDate != null)
            {
                return "Repair In Progress";
            }
            else if (InspectDate != null)
            {
                return "Awaiting Repair";
            }
            else 
            {
                return "Awaiting Inspection";
            }

        }
    }
}