using SpotifyLike.Domain.Conta.Agreggates;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain.Conta
{

    public class UsuarioTest
    {
        [Fact]
        public void DeveCriarUsuarioComSucesso()
        {
            Plano plano = new Plano()
            {
                Descricao = "Lorem ipsum",
                Id = Guid.NewGuid(),
                Nome = "Plano Dummy",
                Valor = 19.90M
            };

            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            string cpf = "40635121000";
            string nome = "Dummy Usuario";

            //Act
            Usuario usuario = new Usuario();
            usuario.Criar(nome, cpf, plano, cartao);

            //Assert
            Assert.NotNull(usuario.CPF);
            Assert.NotNull(usuario.Nome);
            Assert.True(usuario.CPF.Numero == cpf);
            Assert.True(usuario.Nome == nome);

            Assert.True(usuario.Assinaturas.Count > 0);
            Assert.Same(usuario.Assinaturas[0].Plano, plano);

            Assert.True(usuario.Cartoes.Count > 0);
            Assert.Same(usuario.Cartoes[0], cartao);

            Assert.True(usuario.Playlists.Count > 0);
            Assert.True(usuario.Playlists[0].Nome == "Favoritas");
            Assert.False(usuario.Playlists[0].Publica);
        }
    }
}

