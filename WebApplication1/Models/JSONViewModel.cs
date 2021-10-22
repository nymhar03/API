using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApi.Models
{
    public class JSONViewModel
    {
            public string ResponseMessage { get; set; }
            public string StatusCode { get; set; }
            public object Data { get; set; }
    }
}