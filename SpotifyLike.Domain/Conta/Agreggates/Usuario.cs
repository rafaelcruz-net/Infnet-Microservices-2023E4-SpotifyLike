using SpotifyLike.Domain.Conta.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.Agreggates
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public CPF CPF { get; set; }
        public List<Cartao> Cartoes { get; set;}
        public List<Playlist> Playlists { get; set;}
        public List<Banda> BandasFavoritas { get; set; }
    }
}
