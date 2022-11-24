using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models
{
    public class University
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public List<string> Domains { get; set; }
    }
}
