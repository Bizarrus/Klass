using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Klass.Helper {
    public class Password {
        private static string URL = "https://www.knuddels.de/logincheck.html";

        public static void Check(string nickname, string password, Action<JObject> callback) {
            var request             = (HttpWebRequest) WebRequest.Create(URL);
            request.ContentType     = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Accept          = "application/json";
            request.Referer         = URL;
            request.Method          = "POST";
            request.UserAgent       = "";

            using(var writer = new StreamWriter(request.GetRequestStream())) {
                writer.Write("nick=" + nickname + "&pwd=" + password);
                writer.Close();
            }

            var response = (HttpWebResponse) request.GetResponse();

            using(var reader = new StreamReader(response.GetResponseStream())) {
                callback(JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd()));
            }
        }
    }
}
