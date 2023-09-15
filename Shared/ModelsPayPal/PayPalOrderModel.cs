using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PayPalOrderModel
    {
        public List<PayPalAmountModel> purchase_units { get; set; }
        public string intent { get; set; }
    }
}
