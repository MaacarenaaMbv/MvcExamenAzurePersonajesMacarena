using MvcExamenAzurePersonajesMacarena.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcExamenAzurePersonajesMacarena.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T> (string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);   
            return data;
        }

        /*public async Task<Personaje> FindPersonajeSerie(string serie)
        {
            string request = ""
        }*/

        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            string request = "api/personajes/" + idPersonaje;
            Personaje data = await this.CallApiAsync<Personaje>(request);
            return data;
        }

        public async Task InsertPersonajeAsync(int idPersonaje, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/insertpersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = idPersonaje;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idPersonaje)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/personajes/deletepersonaje/" + idPersonaje;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task UpdatePersonajeAsync(int idPersonaje, string nombre, string imagen, string serie)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/personajes/updatepersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = idPersonaje;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

    }
}
