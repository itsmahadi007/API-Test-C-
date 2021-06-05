using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;


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
            string json = JsonConvert.SerializeObject(user);
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


            List<testdb.testingdb> testsTestdbs = JsonConvert.DeserializeObject<List<testdb.testingdb>>(streamResult);
            if (testsTestdbs == null) return " ";
            foreach (dynamic questions in testsTestdbs)
            {
                Console.WriteLine(questions.id + " " + questions.name + " " + questions.email);
            }

            var testsize = testsTestdbs.Count;

            Console.WriteLine(testsize);

            //dynamic myObject = JValue.Parse(streamResult);

            //Console.WriteLine(myObject[3].id + " " + myObject[3].name + " " + myObject[3].email);

            // foreach (dynamic questions in myObject)
            // {
            //     Console.WriteLine(questions.id + " " + questions.name +" "+questions.email);
            // }


            // using var webResponse = request.GetResponse();
            // using var webStream = webResponse.GetResponseStream();
            //
            // using var reader = new StreamReader(webStream);
            // var data = reader.ReadToEnd();

            //Console.WriteLine(data);
            return " ";
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