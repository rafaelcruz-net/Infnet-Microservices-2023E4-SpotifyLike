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
        public List<Cartao> Cartoes { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }
        public List<Assinatura> Assinaturas { get; set; }


        public Usuario()
        {
            this.Playlists = new List<Playlist>();
            this.BandasFavoritas = new List<Banda>();
            this.Assinaturas = new List<Assinatura>();
            this.Cartoes = new List<Cartao>();
        }

        public void Criar(string nome, string cpf, Plano plano, Cartao cartao)
        {
            this.CPF = new CPF(cpf);
            this.Nome = nome;

            //Assinar um plano
            this.AssinarPlano(plano, cartao);

            //Adicionar o cartão na conta do usuário
            this.AdicionarCartao(cartao);

            //Criar Playlist Default
            this.CriarPlayList();

        }

        private void AdicionarCartao(Cartao cartao)
        {
            this.Cartoes.Add(cartao);
        }

        public void AssinarPlano(Plano plano, Cartao cartao)
        {

            //Debitar o valor do plano no cartão
            cartao.CriarTransacao(plano.Nome, plano.Valor, plano.Descricao);

            //Caso tenha uma assinatura ativa, desativa ela
            if (this.Assinaturas.Count > 0 && this.Assinaturas.Any(x => x.Ativo)) 
            {
                var planoAtivo = this.Assinaturas.FirstOrDefault(x => x.Ativo);
                planoAtivo.Ativo = false;
            }

            //Adiciona uma nova assinatura
            this.Assinaturas.Add(new Assinatura()
            {
                Ativo = true,
                DtAssinatura = DateTime.Now,
                Plano = plano,
                Id = Guid.NewGuid()
            });
        }

        public void CriarPlayList(string nome = "Favoritas")
        {
            this.Playlists.Add(new Playlist()
            {
                Id = Guid.NewGuid(),
                Nome = nome,
                Publica = false,
                Usuario = this
            });
        }


    }
}
