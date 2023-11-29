using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Core.Exception;
using SpotifyLike.Domain.Conta.Agreggates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Conta;
using SpotifyLike.Repository.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta
{
    public class UsuarioService
    {
        private PlanoRepository planoRepository = new PlanoRepository();
        private UsuarioRepository usuarioRepository = new UsuarioRepository();


        public UsuarioDto CriarConta(UsuarioDto conta)
        {
            //Todo: Verificar pegar plano
            Plano plano = this.planoRepository.ObterPlanoPorId(conta.PlanoId);

            if (plano == null)
            {
                new BusinessException(new BusinessValidation()
                {
                    ErrorMessage = "Plano não encontrado",
                    ErrorName = nameof(CriarConta)
                }).ValidateAndThrow();
            }


            Cartao cartao = new Cartao();
            cartao.Ativo = conta.Cartao.Ativo;
            cartao.Numero = conta.Cartao.Numero;
            cartao.Limite = conta.Cartao.Limite;

            //Criar Usuario
            Usuario usuario = new Usuario();
            usuario.Criar(conta.Nome, conta.CPF, plano, cartao);

            //Gravar o usuario na base;
            this.usuarioRepository.SalvarUsuario(usuario);
            conta.Id = usuario.Id;

            // Retornar Conta Criada
            return conta;
        }

        public UsuarioDto ObterUsuario(Guid id)
        {
            var usuario = this.usuarioRepository.ObterUsuario(id);

            if (usuario == null)
                return null;

            UsuarioDto result = new UsuarioDto()
            {
                Id = usuario.Id,
                Cartao = new CartaoDto()
                {
                    Ativo = usuario.Cartoes.FirstOrDefault().Ativo,
                    Limite = usuario.Cartoes.FirstOrDefault().Limite,
                    Numero = "xxxx-xxxx-xxxx-xx"
                },
                CPF = usuario.CPF.NumeroFormatado(),
                Nome = usuario.Nome,
            };

            return result;
        }
    }
}
