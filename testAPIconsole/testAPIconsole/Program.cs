using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Providers.Entities;


namespace testAPIconsole
{
    class Program
    {
        public static string webPostMethod(string URL)
        {
            string name = "mahadi Hassan", email = "mahadi@gmail.com";
            string postData = "name=" + name + "&email=" + email;


            WebRequest request = WebRequest.Create(URL);
            request.Method = "POST";

            Post user = new Post
            {
                name = "Mahadi Hassan",
                email = "mh@gmail.com"
            };
            string json = JsonSerializer.Serialize(user);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using Stream reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse) response).StatusDescription);

            using Stream respStream = response.GetResponseStream();

            using StreamReader reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();
            //Console.WriteLine(data);
            return data;
            // record User(string Name, string Occupation);
        }

        public static string webGetMethod(string URL)
        {
            var request_obj = WebRequest.Create(URL);
            request_obj.Method = "GET";

            HttpWebResponse response_obj = (HttpWebResponse) request_obj.GetResponse();

            string streamResult = null;

            using (Stream stream = response_obj.GetResponseStream())
            {
                StreamReader st = new StreamReader(stream);
                streamResult = st.ReadToEnd();
                st.Close();
            }


            // using var webResponse = request.GetResponse();
            // using var webStream = webResponse.GetResponseStream();
            //
            // using var reader = new StreamReader(webStream);
            // var data = reader.ReadToEnd();

            //Console.WriteLine(data);
            return streamResult;
        }


        static void Main(string[] args)
        {
            string URL = "http://127.0.0.1:8000/test/";
            var data = webPostMethod(URL);
            Console.WriteLine(data);

            var getData = webGetMethod(URL);
            Console.WriteLine(getData);
        }
    }

    public class Post
    {
        public string name { get; set; }
        public string email { get; set; }
        }
}