using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using AppCep.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppCep.Service
{
    public class DataService
    {

        public static async Task<Endereco> GetEnderecoByCep(string cep)
        {
            Endereco end;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/endereco/by-cep?cep=" + cep);
                
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    end = JsonConvert.DeserializeObject<Endereco>(json);

                }
                else
                    throw new Exception(response.RequestMessage.Content.ToString());
            }

            return end;
        }

        public static async Task<List<Bairro>> GetBairrosByIdCidade(int id_cidade)
        {
            List<Bairro> arr_bairros = new List<Bairro>();  

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/bairro/by-cidade?id_cidade=" + id_cidade);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    arr_bairros = JsonConvert.DeserializeObject<List<Bairro>>(json);
                }
                else
                    throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_bairros;
        }

        public static async Task<List<Cidade>> GetCidadeByEstado(string uf)
        {
            List<Cidade> arr_cidade = new List<Cidade>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://cep.metoda.com.br/cidade/by-uf?uf=" + uf);
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    arr_cidade = JsonConvert.DeserializeObject<List<Cidade>>(json);
                }
                else
                    throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_cidade;
        }

        public static async Task<List<Logradouro>> GetLogradouroByBairroAndIdCidade(string bairro, int id_cidade)
        {
            List<Logradouro> arr_logradouros = new List<Logradouro>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://cep.metoda.com.br/logradouro/by-bairro?id_cidade={id_cidade}");

                if(response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    arr_logradouros = JsonConvert.DeserializeObject<List<Logradouro>>(json);

                }else
                    throw new Exception(response.RequestMessage.Content.ToString());
            }

            return arr_logradouros;
        }

    }

}

