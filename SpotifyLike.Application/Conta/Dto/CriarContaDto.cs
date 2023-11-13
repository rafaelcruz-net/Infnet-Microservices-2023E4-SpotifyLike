using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Dto
{
    public class CriarContaDto
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public String CPF { get; set; }
        public Guid PlanoId { get; set; }
        public CartaoDto Cartao { get; set; }
    }

    public class CartaoDto
    {
        public String Numero { get; set; }
        public Decimal Limite { get; set; }
        public Boolean Ativo { get; set; }
    }
}
