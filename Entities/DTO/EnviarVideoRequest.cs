using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record EnviarVideoRequest
    {
        public Guid Guid { get; set; }

        public string NomeVideo { get; set; }

        public EnviarVideoRequest(Guid guid, string nomeVideo)
        {
            Guid = guid;
            NomeVideo = nomeVideo;
        }

        public EnviarVideoRequest(string nomeVideo)
        {
            NomeVideo = nomeVideo;
            Guid = Guid.NewGuid();
        }
    }
}
