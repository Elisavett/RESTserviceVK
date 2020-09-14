using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;


namespace REST_service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, EventArgs e)
        {
            string appId = "6759394";
            var uriStr = @"https://oauth.vk.com/authorize?client_id=" + appId +
                         @"&scope=offline&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.6&response_type=token";
            Browser.Navigate(new Uri(uriStr));

        }

        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains(@"oauth.vk.com/blank.html"))
            {
                string url = e.Url.Fragment;
                url = url.Trim('#');
                string Access_token = HttpUtility.ParseQueryString(url).Get("access_token");
                string UserID = HttpUtility.ParseQueryString(url).Get("user_id");
                Form2 newform = new Form2(Access_token, UserID);
                newform.Show();
                this.Hide();
            }

        }
    }
}
