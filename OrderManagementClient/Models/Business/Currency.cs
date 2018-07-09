using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementClient.Models.Business
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Required]
        public string ShortSign { get; set; }
        [Required]
        public int RoundingDecimals { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
