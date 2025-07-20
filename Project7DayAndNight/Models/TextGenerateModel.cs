using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project7DayAndNight.Models
{
    public class TextGenerateModel
    {
        [Newtonsoft.Json.JsonProperty("generated_text")]
        public string generated_text { get; set; }
    }
}