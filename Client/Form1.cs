using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            udpClient = new UdpClient();
        }

        private UdpClient udpClient;
        private const int serverPort = 8080;

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIP = txtServerIP.Text.Trim();
                string message = txtMessage.Text;

                if (string.IsNullOrEmpty(serverIP) || string.IsNullOrEmpty(message))
                {
                    Log("Please enter a server IP and a message.");
                    return;
                }

                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                byte[] sendData = Encoding.UTF8.GetBytes(message);
                udpClient.Send(sendData, sendData.Length, serverEndPoint);

                Log("Message sent: " + message);

                // Receive response
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedData = udpClient.Receive(ref remoteEP);
                string response = Encoding.UTF8.GetString(receivedData);

                Log("Response from server: " + response);
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }

        private void Log(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
        }
    }
}
