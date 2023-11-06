using SpotifyLike.Domain.Conta.Agreggates;
using SpotifyLike.Domain.Conta.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Domain.Conta
{
    public class CartaoTest
    {
        [Fact]
        public void DeveCriarTransacaoComSucesso()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            cartao.CriarTransacao("Dummy", 19M, "Dummy Transacao");
            Assert.True(cartao.Transacoes.Count > 0);
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoInativo()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = false,
                Limite = 1000M,
                Numero = "6465465466",
            };

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("Dummy", 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoSemLimite()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 10M,
                Numero = "6465465466",
            };

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("Dummy", 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoValorDuplicado()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Agreggates.Transacao()
            {
                DtTransacao = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("Dummy", 19M, "Dummy Transacao"));
        }

        [Fact]
        public void NaoDeveCriarTransacaoComCartaoAltoFrequencia()
        {
            Cartao cartao = new Cartao()
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                Limite = 1000M,
                Numero = "6465465466",
            };

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Agreggates.Transacao()
            {
                DtTransacao = DateTime.Now.AddMinutes(-1),
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Agreggates.Transacao()
            {
                DtTransacao = DateTime.Now.AddMinutes(-0.5),
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });

            cartao.Transacoes.Add(new SpotifyLike.Domain.Transacao.Agreggates.Transacao()
            {
                DtTransacao = DateTime.Now,
                Id = Guid.NewGuid(),
                Merchant = new SpotifyLike.Domain.Transacao.ValueObject.Merchant()
                {
                    Nome = "Dummy"
                },
                Valor = 19M,
                Descricao = "saljasdlak"
            });


            Assert.Throws<CartaoException>(
                () => cartao.CriarTransacao("Dummy", 19M, "Dummy Transacao"));
        }

    }
}
