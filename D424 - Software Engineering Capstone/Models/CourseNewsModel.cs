using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Models
{
    public class CourseNewsModel
    {
        public DateTime PostedDate { get; set; }
        public string Title { get; set; }
        public string NewsDetails { get; set; }
        public bool IsClosed { get; set; }
        public string ClosureReason { get; set; }
        public DateTime ClosureDate { get; set; }
    }
}
