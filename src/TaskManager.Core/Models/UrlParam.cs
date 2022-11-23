using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models
{
    public class UrlParam
    {
        public int Id { get; set; }
        public int PathId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
