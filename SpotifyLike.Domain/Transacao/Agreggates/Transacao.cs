using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Transacao.ValueObject;

namespace SpotifyLike.Domain.Transacao.Agreggates
{
    public class Transacao
    {
        public Guid Id { get; set; } 
        public DateTime DtTransacao { get; set; }
        public Decimal Valor { get; set; }
        public Merchant Merchant { get; set; }
        public String Descricao { get; set; }
    }
}
