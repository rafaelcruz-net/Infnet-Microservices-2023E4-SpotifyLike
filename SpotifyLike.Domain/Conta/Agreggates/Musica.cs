using SpotifyLike.Domain.Conta.Agreggates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Musica
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("duracao")]
        public int Duracao { get; set; }
    }
}
