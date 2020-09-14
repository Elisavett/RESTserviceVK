using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Web;

namespace REST_service
{
    public partial class Form2 : Form
    {
        public class Item
        {

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("screen_name")]
            public string ScreenName { get; set; }

            [JsonProperty("is_closed")]
            public int IsClosed { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("photo_50")]
            public string Photo50 { get; set; }

            [JsonProperty("photo_100")]
            public string Photo100 { get; set; }

            [JsonProperty("photo_200")]
            public string Photo200 { get; set; }
        }
        public class SampleResponse1
        {
            [JsonProperty("response")]
            public Response Response { get; set; }
        }
        public class Response
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("items")]
            public IList<Item> Items { get; set; }
        }
        public List<Item> group = new List<Item>();
        public class ResponseIsMember
        {
            [JsonProperty("response")]
            public string Response { get; set; }
        }
        public string token;
        public string user_id;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string Access_token, string userId)
        {
            InitializeComponent();
            token = Access_token;
            user_id = userId;
        }
        private string GET(string Url, string Method, string Token, NameValueCollection parametrs)
        {
            WebRequest req = WebRequest.Create(String.Format(Url, Method, Token, parametrs));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }
        SampleResponse1 Gr = new SampleResponse1();
        private void button1_Click(object sender, EventArgs e)
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.92&extended=1";
            string method = "groups.get";
            string g = GET(reqStrTemplate, method, token, null);
            Gr = JsonConvert.DeserializeObject<SampleResponse1>(g);
            for (int i = 0; i < Gr.Response.Items.Count; i++)
            {
                listBox1.Items.Add(Gr.Response.Items[i].Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Url = "https://api.vk.com/method/{0}?access_token={1}&{2}&v=5.92";
            string method = "groups.isMember";
            NameValueCollection values = HttpUtility.ParseQueryString(String.Empty);
            values.Add("group_id", Gr.Response.Items[listBox1.SelectedIndex].Id.ToString());
            values.Add("user_id", UserBox.Text);
            string g = GET(Url, method, token, values);
            ResponseIsMember r = new ResponseIsMember();
            string result= JsonConvert.DeserializeObject<ResponseIsMember>(g).Response;
            if (result == "0") ResultBox.Text = "не состоит";
            else ResultBox.Text = "состоит";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            UserBox.Text = user_id;
            label2.Text = "Мой id: " + user_id;
        }
    }
}
