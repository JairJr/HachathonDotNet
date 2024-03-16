using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Database
{
    public class Video
    {
        public Guid Guid { get; set; }
        public string? NomeVideo { get; set; }
        public string? StausVideo { get; set; }
        public string? PastaImagens { get; set; }
    }
}
