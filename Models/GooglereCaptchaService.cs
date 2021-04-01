using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace GoogleRecap.Models
{
    public class GooglereCaptchaService
    {
        private ReCAPTCHASettings _settings;

        public GooglereCaptchaService(IOptions<ReCAPTCHASettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual async Task<GoogleREspo> VerifyreCaptcha(string _Token)
        {
            GooglereCaptchaData _MyData = new GooglereCaptchaData
            {
                response=_Token,
                secret = _settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();

            var repponse = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?=secret{_MyData.secret}&response={_MyData.response}");

            var capresp = JsonConvert.DeserializeObject<GoogleREspo>(repponse);

            return capresp;

        }
    }

    public class GooglereCaptchaData
    {
        public string response { get; set; } //token

        public string secret { get; set; }
    }
   
    public class GoogleREspo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set;}
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
