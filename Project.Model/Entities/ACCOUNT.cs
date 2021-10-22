using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class ACCOUNT
    {
        [Key]
        public int ACCOUNT_ID { get; set; }
        public int USER_ID { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public Decimal ACCOUNT_BALANCE { get; set; }
        public string STATUS { get; set; }
        public string REMARKS { get; set; }
        [NotMapped]
        public string NAME { get { return LASTNAME + "," + FIRSTNAME + (string.IsNullOrEmpty(MIDDLENAME) ? "" : MIDDLENAME); } }
        public virtual List<PAYMENT> PAYMENTS { get; set; }
        [NotMapped]
        public string TOKEN { get; set; }
    }
}
