using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject
{
    public class PriceManager
    {
        private IDivisa _tipoDivisa;

        public IDivisa TipoDivisa
        {
            set { this._tipoDivisa = value; }
        }

        public object GetPrice()
        {
            return this._tipoDivisa.GetPrice();
        }
    }
}