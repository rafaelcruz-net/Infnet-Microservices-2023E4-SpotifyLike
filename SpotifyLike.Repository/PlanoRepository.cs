using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpotifyLike.Repository
{
    public class PlanoRepository
    {
        private HttpClient HttpClient { get; set; }

        public PlanoRepository()
        {
            this.HttpClient = new HttpClient();
        }

        public async Task<Plano> ObterPlano(Guid id)
        {
            var result = await this.HttpClient.GetAsync($"https://localhost:7192/api/Plano/{id}");

            if (result.IsSuccessStatusCode == false)
                return null;

            var content = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Plano>(content);  

        }
    }
}
