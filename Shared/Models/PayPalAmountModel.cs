﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PayPalAmountModel
    {
        public string currency_code { get; set; }
        public double value { get; set; }
    }
}
