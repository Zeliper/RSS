using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows.Controls;
using System.Xml;

namespace RSS_Subscriber
{
    class RSSFeed
    {
        public RSSFeed(string _Title,string _URI)
        {
            Title = _Title;
            URI = _URI;
            GetFeeds(_URI);
        }

        #region Variables,Propertie
        public string Title { get; set; }
        public string URI { get; set; }
        public List<Feed> Feeds { get; set; } = new List<Feed>();
        public class Feed
        {
            public Feed(SyndicationItem _item)
            {
                Title = _item.Title.Text;
                Contents = CreateContent(_item.Summary.Text);
                URI = _item.Links[0].Uri.AbsoluteUri;
            }
            public string Title { get; set; }
            public string Contents { get; set; }
            public string URI { get; set; }
            private string CreateContent(string _text)
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
        }
        #endregion

        #region Function,Method
        public void GetFeeds(string _URI)
        {
            SyndicationFeed feed = null;
            try
            {
                using (var reader = XmlReader.Create(_URI))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch { }

            if (feed != null)
            {
                foreach(SyndicationItem i in feed.Items)
                {
                    Feed inst = new Feed(i);
                    Feeds.Add(inst);
                }
            }
        }
        public static Feed GetFeedFromListBoxItem(object _item)
        {
            return ((Feed)(((ListBoxItem)_item).Tag));
        }
        #endregion
    }
}
