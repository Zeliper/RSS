using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;
using System.Windows.Markup;
using CefSharp;
using System.IO;

namespace RSS_Subscriber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void RSS()
        {
            SyndicationFeed feed = null;
            try
            {
                using(var reader = XmlReader.Create("https://dragonkid.net/bbs/rss.php?bo_table=t1"))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch { }

            if (feed != null)
            {
                foreach(var element in feed.Items)
                {
                   
                }
                Browser.NavigateToString(CreateContent(feed.Items.ToArray()[7].Summary.Text));
            }
        }

        public string CreateContent(string _text)
        {
            string css = File.ReadAllText("WebStyle.css");
            string inst = 
                "<html lang=\"ko\"" +
                    "<HEAD>" +
                        "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>" +
                        "<style type=\"text/css\">" +
                        css +
                        "</style>" +
                    "</HEAD>" +
                    "<body>" +
                    _text +
                    "</BODY>" +
                "</html>";
            return inst;
        }
        public MainWindow()
        {
            InitializeComponent();
            RSS();
        }
    }
}
