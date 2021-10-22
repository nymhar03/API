using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class USER_TOKEN
    {
        [Key]
        public int TOKEN_ID { get; set; }
        public int USER_ID { get; set; }
        public string TOKEN { get; set; }
        public DateTime GRANTED_ON { get; set; }
        public DateTime EXPIRES_ON { get; set; }
    }
}
