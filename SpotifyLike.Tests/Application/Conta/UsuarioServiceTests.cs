using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Application.Conta
{
    public class UsuarioServiceTests
    {
        [Fact]
        public void DeveCriarContaComSucesso()
        {
            //Arrange
            UsuarioDto dto = new UsuarioDto()
            {
                Nome = "Lorem Ipsum do teste",
                CPF = "26952278095",
                Cartao = new CartaoDto()
                {
                    Ativo = true,
                    Limite = 100,
                    Numero = "5248581002684983"
                },
                PlanoId = new Guid("8D044595-D4A6-4E1A-9F09-DAB92205C71C")
            };

            UsuarioService service = new UsuarioService();
            service.CriarConta(dto);

            Assert.True(dto.Id != Guid.Empty);
        }

        [Fact]
        public void NaoDeveCriarContaComPlanoInvalido()
        {
            //Arrange
            UsuarioDto dto = new UsuarioDto()
            {
                Nome = "Lorem Ipsum do teste",
                CPF = "26952278095",
                Cartao = new CartaoDto()
                {
                    Ativo = true,
                    Limite = 100,
                    Numero = "5248581002684983"
                },
                PlanoId = Guid.NewGuid()
            };

            UsuarioService service = new UsuarioService();
           
            Assert.Throws<BusinessException>(() => service.CriarConta(dto));
        }
    }
}
