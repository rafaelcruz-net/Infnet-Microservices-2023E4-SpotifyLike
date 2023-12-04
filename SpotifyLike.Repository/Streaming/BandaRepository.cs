using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Streaming
{
    public class BandaRepository
    {
        private static List<Banda> Bandas = new List<Banda>();
        
        public void Criar(Banda banda)
        {
            banda.Id = Guid.NewGuid();
            Bandas.Add(banda);
        }

        public Banda ObterBanda(Guid id)
        {
            return Bandas.FirstOrDefault(x => x.Id == id);
        }

        public Musica ObterMusica(Guid idMusica)
        {
            Musica result = null;

            foreach (var banda in Bandas)
            {
                foreach (var album in banda.Albums)
                {
                    result = album.Musicas.FirstOrDefault(x => x.Id == idMusica);
                    
                    if (result != null)
                        break;
                }
            }

            return result;

            /*return Bandas.Select(x =>
            {
                return (from y in x.Albums
                        select y.Musicas.FirstOrDefault(m => m.Id == idMusica))
                       .FirstOrDefault();
            }).FirstOrDefault();*/

            /*
              SELECT M.* FROM BANDAS B
              INNER JOIN ALBUM A ON A.IDBANDA = B.ID
              INNER JOIN MUSICA M ON M.IDLALBUM = A.ID
              WHERE M.ID = @ID  
            */
                                 
                   
        }
    }
}
