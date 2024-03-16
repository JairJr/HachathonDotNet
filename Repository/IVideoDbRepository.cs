using Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IVideoDbRepository
    {
        public Task Cadastrar(Video video);
        public Task Alterar(Video video);
    }
}
