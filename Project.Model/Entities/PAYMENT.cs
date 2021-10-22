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
    public class PAYMENT
    {
        public PAYMENT()
        {
            this.DATE = DateTime.UtcNow;
        }
        [Key]
        public int PAYMENT_ID { get; set; }
        public string ACCOUNT_NO { get; set; }
        public DateTime DATE { get; set; }
        public Decimal AMOUNT { get; set; }
        public string REMARKS { get; set; }
        [NotMapped]
        public int USER_ID { get; set; }
        [NotMapped]
        public string TOKEN { get; set; }
        [NotMapped]
        public string DATE_FORMAT1 { get { return DATE.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"); } }
    }
}

