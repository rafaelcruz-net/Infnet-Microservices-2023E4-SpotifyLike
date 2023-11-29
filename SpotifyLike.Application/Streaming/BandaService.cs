using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;
using SpotifyLike.Repository.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming
{
    public class BandaService
    {
        private BandaRepository Repository = new BandaRepository(); 
        public BandaService() { }

        public BandaDto Criar(BandaDto dto) 
        {
            Banda banda = new Banda()
            {
                Descricao = dto.Descricao,
                Nome = dto.Nome,
            };

            if (dto.Albums != null) 
            {
                foreach (var item in dto.Albums)
                {
                    Album album = new Album()
                    {
                        Id = Guid.NewGuid(),
                        Nome = item.Nome,
                    };

                    if (item.Musicas != null)
                    {
                        foreach (var musica in item.Musicas)
                        {
                            album.AdicionarMusicas(new Musica()
                            {
                                Duracao = new Domain.Streaming.ValueObject.Duracao(musica.Duracao),
                                Nome = musica.Nome,
                                Album = album,
                                Id = Guid.NewGuid()
                            });
                        }
                    }

                    banda.AdicionarAlbum(album);
                }
            }

            this.Repository.Criar(banda);
            dto.Id = banda.Id;

            return dto;
        
        }

        public BandaDto ObterBanda(Guid id)
        {
            var banda = this.Repository.ObterBanda(id);

            if (banda == null)
                return null;

            BandaDto dto = new BandaDto()
            {
                Id = banda.Id,
                Descricao = banda.Descricao,
                Nome = banda.Nome,
            };

            if (banda.Albums != null)
            {
                dto.Albums = new List<AlbumDto>(); 

                foreach (var album in banda.Albums)
                {
                    AlbumDto albumDto = new AlbumDto()
                    {
                        Id = album.Id,
                        Nome = album.Nome,
                        Musicas = new List<MusicaDto>()
                    };

                    album.Musicas?.ForEach(m =>
                    {
                        albumDto.Musicas.Add(new MusicaDto()
                        {
                            Id = m.Id,
                            Duracao = m.Duracao.Valor,
                            Nome = m.Nome
                        });
                    });

                    dto.Albums.Add(albumDto);
                }
            }

            return dto;

        }
    }
}
