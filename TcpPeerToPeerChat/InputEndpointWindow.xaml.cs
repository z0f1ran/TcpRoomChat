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
using System.Windows.Shapes;

namespace TcpPeerToPeerChat
{
    /// <summary>
    /// Логика взаимодействия для InputEndpointWindow.xaml
    /// </summary>
    public partial class InputEndpointWindow : Window
    {
        public string ip;
        public int port;
        public InputEndpointWindow()
        {
            InitializeComponent();
            ip = null; port = 0;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ip = txtBoxIp.Text;
            port = Convert.ToInt32(txtBoxPort.Text);
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
