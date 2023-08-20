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
using System.Windows.Threading;

namespace TcpPeerToPeerChat
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        Communication communicator;
        DispatcherTimer timer;
        public ChatWindow(Communication communication)
        {
            InitializeComponent();
            communicator = communication;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            communicator.SendMessage(txtBoxMessage.Text);
            //listBoxMessenger.Items.Add(txtBoxMessage.Text);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            btnReceive_Click(null, null);
        }

        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            btnReceive.Background = Brushes.Red;
            string message = communicator.ReceiveMessage();
            if (message != null) {
                listBoxMessenger.Items.Add(message);
            }
            btnReceive.Background = Brushes.White;
        }
    }
}
