using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
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
using CefSharp;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Net;
using Cookie = WebSocketSharp.Net.Cookie;
using WebSocket = WebSocketSharp.WebSocket;

namespace CefSharp2.Pages
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();

            //ChromiumWebBrowser.ExecuteScriptAsyncWhenPageLoaded("document.getElementsByName('btnK')[1].value='test';");
            //ChromiumWebBrowser.Load("http://www.google.com");
            
        }
    }
}
