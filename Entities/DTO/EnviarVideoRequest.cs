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

        public FileStream Stream { get; set; }

        public EnviarVideoRequest(Guid guid, string nomeVideo, FileStream stream)
        {
            Guid = guid;
            NomeVideo = nomeVideo;
            Stream = stream;
        }

        public EnviarVideoRequest(string nomeVideo, FileStream stream)
        {
            NomeVideo = nomeVideo;
            Stream = stream;
            Guid = Guid.NewGuid();
        }
    }
}
