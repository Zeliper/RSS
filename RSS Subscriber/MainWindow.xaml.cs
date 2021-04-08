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
using System.Diagnostics;

namespace RSS_Subscriber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {//"https://dragonkid.net/bbs/rss.php?bo_table=t1"

        public MainWindow()
        {
            InitializeComponent();
            RSSFeed rss = new RSSFeed("드래곤 키드", "https://dragonkid.net/bbs/rss.php?bo_table=t1");
            foreach(RSSFeed.Feed i in rss.Feeds)
            {
                ListBoxItem inst = new ListBoxItem();
                inst.Content = i.Title;
                inst.Tag = i;
                List.Items.Add(inst);
            }
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Browser.NavigateToString(RSSFeed.GetFeedFromListBoxItem(List.SelectedItem).Contents);
        }

        private void List_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {//
            Process.Start(new ProcessStartInfo(RSSFeed.GetFeedFromListBoxItem(List.SelectedItem).URI) { UseShellExecute=true });
        }
    }
}
