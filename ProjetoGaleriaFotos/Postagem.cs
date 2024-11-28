using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGaleriaFotos
{
    public class Postagem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DtPostagem { get; set; }

        public string? Comentario { get; set; }

        public string? Foto { get; set; }
    }
}
