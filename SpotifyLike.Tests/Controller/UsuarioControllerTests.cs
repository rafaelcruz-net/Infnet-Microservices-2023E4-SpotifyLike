using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyLike.API.Controllers;
using SpotifyLike.Application.Conta.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Tests.Controller
{
    public class UsuarioControllerTests
    {
        [Fact]
        public void DeveChamarPostCriarUsuaarioComSucesso()
        {
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

            var logger = LoggerFactory.Create(logger => logger.AddConsole())
                                      .CreateLogger<UsuarioController>();

            var controller = new UsuarioController(logger);

            var response = controller.CriarConta(dto);

            Assert.True(response is CreatedResult);

            var responseContent = (response as CreatedResult).Value;
            Assert.True(responseContent is UsuarioDto);
            Assert.True((responseContent as UsuarioDto).Id != Guid.Empty);
        }
    }
}
