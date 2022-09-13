using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaB3.models
{
    public class GlobalQuote
    {
        [JsonProperty("01. symbol")]
        public string _01Symbol { get; set; }

        [JsonProperty("02. open")]
        public string _02Open { get; set; }

        [JsonProperty("03. high")]
        public string _03High { get; set; }

        [JsonProperty("04. low")]
        public string _04Low { get; set; }

        [JsonProperty("05. price")]
        public string _05Price { get; set; }

        [JsonProperty("06. volume")]
        public string _06Volume { get; set; }

        [JsonProperty("07. latest trading day")]
        public string _07LatestTradingDay { get; set; }

        [JsonProperty("08. previous close")]
        public string _08PreviousClose { get; set; }

        [JsonProperty("09. change")]
        public string _09Change { get; set; }

        [JsonProperty("10. change percent")]
        public string _10ChangePercent { get; set; }
    }

    public class APIReturn
    {
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}
