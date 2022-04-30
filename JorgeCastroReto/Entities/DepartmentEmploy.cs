using System;
using System.Collections.Generic;

namespace JorgeCastroReto.Entities
{
    public class DepartmentEmploy
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte Status { get; set; }
        public Employee Employee { get; set; } 
        public Department Department { get; set; } 

    }
}
