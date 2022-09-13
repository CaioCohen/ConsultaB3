using ConsultaB3.Classes;
using ConsultaB3.models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

Console.WriteLine($"{args?[0]}");

string configText = File.ReadAllText("../../../local.settings.json");
ComunicacaoConfig comunicacao = System.Text.Json.JsonSerializer.Deserialize<ComunicacaoConfig>(configText);
Email email = new Email();
email.subject = $"Preço ação {args?[0]}";
EmailClient emailClient = new EmailClient();
bool enviadoComprar = false;
bool enviadoVender = false;


string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={args?[0]}&apikey=VGOCSRFQL4DHEZBK";
//Estou usando uma api gratuita de consulta a bolsa de valores. Porém, essa foi a única API pública gratuita
//que eu encontrei, as outras tinham que pagar, por isso que estou usando ela, mas ela não permite fazer certas consultas
//Na verdade, as únicas consultas que ela permite, de acordo com a documentação, são :
//IBM,TSCO.LON, SHOP.TRT, GPV.TRV, DAI.DEX, RELIANCE.BSE , 600104.SHH, 000002.SHZ
try
{
    
    HttpClient http = HttpClientFactory.Create();
    while (true)//Por ser uma aplicação console, coloquei pra se manter sempre rodando com while infinito. Mas em uma API, seria possível colocar um time trigger para chamar a cada 10 segundos, por exemplo.
    {
        bool enviar = false;
        var retorno = http.GetAsync(url);
        APIReturn dados = retorno.Result.Content.ReadAsAsync<APIReturn>().Result;
        if(dados != null)
        {
            float preco = float.Parse(dados.GlobalQuote._05Price, System.Globalization.CultureInfo.InvariantCulture);
            float teto = float.Parse(args?[1], System.Globalization.CultureInfo.InvariantCulture);
            float chao = float.Parse(args?[2], System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine(dados.GlobalQuote._05Price);
            if (preco > teto && !enviar)
            {
                enviadoVender = false;
                if (!enviadoComprar)
                {//Impede que depois que um email alertando seja enviado, outro não irá ser enviado enquanto o preço não cair pra baixo so teto
                    enviadoComprar = true;
                    enviar = true;
                    email.body = $"O preço da {args?[0]} acabou de crescer e atingiu {dados.GlobalQuote._05Price}. Recomenda-se vender!";
                }

            }
            else if (preco < chao && !enviar)
            {
                enviadoComprar = false;
                if (!enviadoVender)//Impede que depois que um email alertando seja enviado, outro não irá ser enviado enquanto o preço não subir pra cima o chão
                {
                    enviadoVender = true;
                    enviar = true;
                    email.body = $"O preço da {args?[0]} acabou de descer e atingiu {dados.GlobalQuote._05Price}. Recomenda-se comprar!";
                }

            }
            else //Caso o valor esteja entre o teto e o chão, enviadoComprar e enviadoVender passam a ser falso para o programa poder enviar de novo
            {
                enviadoVender = false;
                enviadoComprar = false;
                enviar = false;
            }
            if (enviar)
            {
                emailClient.sendEmail(comunicacao, email);
            }
        }
        System.Threading.Thread.Sleep(15000);//o AlphaVantage, que é a API que eu estou chamando, apenas possibilita 5 chamadas por minuto e 500 por dia, por isso eu coloquei um tempo de espera tão grande.
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

