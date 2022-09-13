using ConsultaB3.models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

Console.WriteLine($"{args?[0]}");

string configText = File.ReadAllText("local.settings.json");
ComunicacaoConfig comunicacao = System.Text.Json.JsonSerializer.Deserialize<ComunicacaoConfig>(configText);

string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={args?[0]}&apikey=VGOCSRFQL4DHEZBK";
//Estou usando uma api gratuita de consulta a bolsa de valores. Porém, essa foi a única API pública gratuita
//que eu encontrei, as outras tinham que pagar, por isso que estou usando ela, mas ela não permite fazer certas consultas
//Na verdade, as únicas consultas que ela permite, de acordo com a documentação, são :
//IBM,Tesco PLC, Shopify Inc, GreenPower Motor Company Inc, Daimler AG, Reliance Industries , SAIC Motor Corporation, China Vanke Company Ltd
try
{
    HttpClient http = HttpClientFactory.Create();
    var retorno = http.GetAsync(url);
    APIReturn dados = retorno.Result.Content.ReadAsAsync<APIReturn>().Result;
    Console.WriteLine(dados.GlobalQuote._05Price);
}catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

