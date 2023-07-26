using Microsoft.AspNetCore.Mvc;
using ML;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Http;
using PagedList;
using NuGet.Protocol;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using NuGet.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace PL.Controllers
{
    public class PokemonController : Controller
    {
        int numero;
        public ActionResult GetPokemon(string next, string previous)
        {
            ML.Pokemon pokemon = new ML.Pokemon();
            if (next != null)
            {
                string limite = next;
                int length = limite.Length;
                if (limite == null)
                {
                    pokemon.numero = 0;
                }
                else
                {
                    if (length == 52)
                    {
                        string substr1 = limite.Substring(41, 2);
                        pokemon.numero = int.Parse(substr1);
                    }
                    else if (length == 53)
                    {
                        string substr1 = limite.Substring(41, 3);
                        pokemon.numero = int.Parse(substr1);
                    }
                    else if (length == 54)
                    {
                        string substr1 = limite.Substring(41, 4);
                        pokemon.numero = int.Parse(substr1);
                    }
                }
            }
            if (previous != null)
            {
                string limite = previous;
                int length = limite.Length;
                if (limite == null)
                {
                    pokemon.numero = 0;
                }
                else
                {
                    if (length == 52)
                    {
                        string substr1 = limite.Substring(41, 2);
                        pokemon.numero = int.Parse(substr1);
                    }
                    else if (length == 53)
                    {
                        string substr1 = limite.Substring(41, 3);
                        pokemon.numero = int.Parse(substr1);
                    }
                    else if (length == 54)
                    {
                        string substr1 = limite.Substring(41, 4);
                        pokemon.numero = int.Parse(substr1);
                    }

                }
            }

            ML.Result resultPokemon = new ML.Result();
            resultPokemon.Objects = new List<Object>();



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/");

                var responseTask = client.GetAsync("v2/pokemon?limit=20&offset=" + pokemon.numero);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Pokemon>();
                    readTask.Wait();

                    int i = pokemon.numero + 1;

                    pokemon.next = readTask.Result.next;
                    pokemon.previous = readTask.Result.previous;

                    foreach (dynamic resultItem in readTask.Result.results)
                    {
                        ML.Pokemon resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pokemon>(resultItem.ToString());
                        resultPokemon.Objects.Add(resultItemList);
                        resultItemList.img = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + i++ + ".png";

                    }

                }
                pokemon.results = resultPokemon.Objects;


                ML.Tipo tipo = new ML.Tipo();

                ML.Result resultTipos = new ML.Result();
                resultTipos.Objects = new List<Object>();


                using (var client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri("https://pokeapi.co/api/");

                    var responseTask1 = client1.GetAsync("v2/type");
                    responseTask.Wait();

                    var result1 = responseTask1.Result;
                    if (result1.IsSuccessStatusCode)
                    {
                        var readTask1 = result1.Content.ReadAsAsync<ML.Tipo>();
                        readTask1.Wait();

                        foreach (var resultItem1 in readTask1.Result.results)
                        {
                            ML.Tipo resultItemList1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Tipo>(resultItem1.ToString());
                            resultTipos.Objects.Add(resultItemList1);

                        }

                    }
                    pokemon.Tipos = resultTipos.Objects;
                }
                return View(pokemon);
            }
        }

        [HttpPost]
        public ActionResult GetTipo(ML.Pokemon pokemon)
        {

            ML.Result resultPokemon = new ML.Result();
            resultPokemon.Objects = new List<Object>();

            ML.Tipo tipo1 = new ML.Tipo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/");

                var responseTask = client.GetAsync("v2/type/" + pokemon.tipo + "/");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.pokemon)
                    {
                        dynamic resultItemList = JsonConvert.DeserializeObject(resultItem.ToString());

                        dynamic pokemones = resultItemList.pokemon;

                        ML.Tipo tipo = new ML.Tipo();
                        tipo.name = pokemones.name;
                        tipo.url = pokemones.url;

                        string limite = tipo.url;
                        int length = limite.Length;
                        
                            if (length == 37)
                            {
                                string substr1 = limite.Substring(34, 2);
                                pokemon.numero = int.Parse(substr1);
                            }
                            else if (length ==38)
                            {
                                string substr1 = limite.Substring(34, 3);
                                pokemon.numero = int.Parse(substr1);
                            }
                            else if(length == 39)
                        {
                            string substr1 = limite.Substring(34, 4);
                            pokemon.numero = int.Parse(substr1);
                        }
                        tipo.img = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + pokemon.numero + ".png";
                       
                        resultPokemon.Objects.Add(tipo);
                   
                    }
                }
                tipo1.tipo = pokemon.tipo;
                tipo1.results= resultPokemon.Objects;
                
                return View(tipo1);
            }
        }

        public ActionResult GetDetalle (string url,int numero)
        {
            ML.Detalle detalle = new ML.Detalle();
            int length = url.Length;

            if (length == 36) {
                string substr1 = url.Substring(34, 1);
                numero = int.Parse(substr1);
            }
            if (length == 37)
            {
                string substr1 = url.Substring(34, 2);
                numero = int.Parse(substr1);
            }
            if (length == 38)
            {
                string substr1 = url.Substring(34, 3);
                numero = int.Parse(substr1);
            }
            if (length == 39)
            {
                string substr1 = url.Substring(34, 4);
                numero = int.Parse(substr1);
            }




            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

                var responseTask = client.GetAsync("pokemon/"+numero+"/");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    dynamic resultItem = readTask.Result;

                    var resultItemList = JsonConvert.DeserializeObject(resultItem.ToString());

                    //Nombre,numero pokemon,sprit frontal, tipos de pokemon,stats del pokemon
                    detalle.name = resultItemList.name;
                    detalle.id = resultItemList.id;
                    detalle.forms = resultItemList.forms;
                    detalle.stats = resultItemList.stats;
                    detalle.Frontal = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" +numero+ ".png";
                    detalle.Shiny = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/shiny/"+numero+".png";
                    detalle.types = resultItemList.types;
                }

                return View(detalle);
            }

        }

    }
}
