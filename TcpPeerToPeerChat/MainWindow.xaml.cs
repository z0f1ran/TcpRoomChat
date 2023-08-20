using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TcpPeerToPeerChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            InputEndpointWindow inputWindow = new InputEndpointWindow();            
            inputWindow.ShowDialog();
            using (ClientService client = new ClientService(inputWindow.ip, inputWindow.port)) 
            {
                
                ChatWindow chatWindow = new ChatWindow(client.GetCommunication());
                chatWindow.ShowDialog();
            }
        }

        private void btnWaitConnection_Click(object sender, RoutedEventArgs e)
        {
            InputEndpointWindow inputWindow = new InputEndpointWindow();
            inputWindow.ShowDialog();
            using (ServerService server = new ServerService(inputWindow.ip, inputWindow.port))
            {
                ChatWindow chatWindow = new ChatWindow(server.GetCommunication());
                Thread serverThread = new Thread(server.StartServer);
                serverThread.Start();
                chatWindow.ShowDialog();
                serverThread.Join();
            }            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
