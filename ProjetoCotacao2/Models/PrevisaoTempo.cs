using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    [Table("PrevisaoTempos")]

    public class PrevisaoTempo
    {
        [Key]

        [Display(Name = "Id")]
        public string id { get; set; }


        [Display(Name = "Cidade")]
        public string name { get; set; }

        [Display(Name = "Estado")]
        public string state { get; set; }

        [Display(Name = "País")]
        public string country { get; set; }
        public Data data { get; set; }

        public class Data
        {
            [Display(Name = "Temperatura")]
            public int temperature { get; set; }
            public string wind_direction { get; set; }
            public float humidity { get; set; }

            [Display(Name = "Condição")]
            public string condition { get; set; }
            public float pressure { get; set; }
            public string icon { get; set; }
            public string date { get; set; }
        }
    }
}

