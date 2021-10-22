using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class USER
    {
        [Key]
        public int USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        [NotMapped]
        public string CONFIRM_PASSWORD { get; set; }
        public bool ISACTIVE { get; set; }
        public string TOKEN { get; set; }
    }
}

