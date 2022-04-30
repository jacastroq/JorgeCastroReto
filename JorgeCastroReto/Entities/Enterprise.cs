using System;
using System.Collections.Generic;

namespace JorgeCastroReto.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte Status { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
