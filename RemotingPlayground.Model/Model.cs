using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingPlayground
{
    public class Person
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Int32 Age { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
    }

    public class Company
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }
}
