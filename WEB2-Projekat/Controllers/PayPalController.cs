using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WEB2_Projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayPalController : Controller
    {
        private readonly IConfiguration _configuration;

        public PayPalController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("getOrderId")]
        public async Task<IActionResult> GetOrderId(double amount)
        {
            const SecurityProtocolType tls13 = (SecurityProtocolType)12288;
            ServicePointManager.SecurityProtocol = tls13 | SecurityProtocolType.Tls12;

            string payPalUrl = _configuration.GetSection("PayPalSettings").GetSection("PayPalSandboxUrl").Value;
            string payPalOrderUrl = _configuration.GetSection("PayPalSettings").GetSection("PayPalOrderUrl").Value;
            string clientId = _configuration.GetSection("PayPalSettings").GetSection("ClientId").Value;
            string secretId = _configuration.GetSection("PayPalSettings").GetSection("SecretId").Value;
            var bytes = Encoding.UTF8.GetBytes($"{clientId}:{secretId}");

            PayPalTokenModel token = new PayPalTokenModel();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

                var keyValues = new List<KeyValuePair<string, string>>();
                keyValues.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                var responseMsg = await client.PostAsync(payPalUrl, new FormUrlEncodedContent(keyValues));
                var response = await responseMsg.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<PayPalTokenModel>(response);
            }

            var order = new PayPalOrderModel()
            {
                purchase_units = new List<PayPalAmountModel>() { new PayPalAmountModel() { currency_code = "USD", value = amount } },
                intent = "CAPTURE"
            };

            var jsonOrder = JsonConvert.SerializeObject(order);
            string orderId = "";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

                var requestMsg = new HttpRequestMessage(HttpMethod.Post, payPalOrderUrl);
                requestMsg.Content = new StringContent(jsonOrder, null, "application/json");

                var response = await client.SendAsync(requestMsg);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var orderResponse = JsonConvert.DeserializeObject<PayPalOrderResponseModel>(strResponse);
                    orderId = orderResponse.id;
                    
                    //TODO:
                    //upisati u bazu orderId i
                    //postaviti neki flag u bazi ako je paypal da je npr IsPaid true
                }
            }

            return Ok(orderId);
        }
    }
}
