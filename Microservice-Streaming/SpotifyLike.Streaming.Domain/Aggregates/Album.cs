using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Streaming.Domain.Streaming.Aggregates
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Banda Banda { get; set; }

        public List<Musica> Musicas { get; set; }

        public Album()
        {
            this.Musicas = new List<Musica>(); 
        }

        public void AdicionarMusicas(List<Musica> musicas)
        {
            this.Musicas.AddRange(musicas);
        }

        public void AdicionarMusicas(Musica musicas)
        {
            this.Musicas.Add(musicas);
        }


    }
}
