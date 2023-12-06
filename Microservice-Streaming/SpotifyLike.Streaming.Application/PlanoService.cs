using SpotifyLike.Streaming.Application.Dto;
using SpotifyLike.Streaming.Domain.Streaming.Aggregates;
using SpotifyLike.Streaming.Repository.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Streaming.Application
{
    public class PlanoService
    {
        private PlanoRepository PlanoRepository { get; set; }   
        public PlanoService() 
        {
            this.PlanoRepository = new PlanoRepository(); 
        }

        public PlanoDto ObterPlano(Guid id)
        {
            var plano = this.PlanoRepository.ObterPlanoPorId(id);

            if (plano == null)
                return null;    

            return new PlanoDto()
            {
                Descricao = plano.Descricao,
                Id = plano.Id,
                Nome = plano.Nome,
                Valor = plano.Valor,
            };

        }
    }
}
