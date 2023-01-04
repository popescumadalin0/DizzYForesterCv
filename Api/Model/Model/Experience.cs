using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Experience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public DateTime WhenStart { get; set; }
        public DateTime WhenEnd { get; set; }
        public string LeaveReason { get; set; }
        public int UserId { get; set; }
    }
}
