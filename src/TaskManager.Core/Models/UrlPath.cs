using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Models
{
    public class UrlPath
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string Name { get; set; }
        public List<UrlParam> UrlParams { get; set; }
    }
}
