using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiProject.Controllers
{
    public class CotizacionController : ApiController
    {
        PriceManager _priceManager = new PriceManager();

        [Route("api/cotizacion/dolar")]
        public IHttpActionResult GetDolar()
        {
            try
            {
                IDivisa divisa = new Dolar();
                this._priceManager.TipoDivisa = divisa;
                var result = this._priceManager.GetPrice();
                return Ok(result);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }
        }

        [Route("api/cotizacion/peso")]
        public IHttpActionResult GetPeso()
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route("api/cotizacion/real")]
        public IHttpActionResult GetReal()
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
