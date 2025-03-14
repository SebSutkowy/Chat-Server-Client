using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private UdpClient udpListener;
        private Thread listenThread;
        private const int listenPort = 8080;

        private void btnStart_Click(object sender, EventArgs e)
        {
            listenThread = new Thread(new ThreadStart(StartListening));
            listenThread.IsBackground = true;
            listenThread.Start();
            Log("Server started on port " + listenPort);
        }

        private void StartListening()
        {
            try
            {
                udpListener = new UdpClient(listenPort);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, listenPort);

                while (true)
                {
                    byte[] receivedData = udpListener.Receive(ref remoteEP);
                    string receivedMessage = Encoding.UTF8.GetString(receivedData);

                    Log($"Received from {remoteEP.Address}: {receivedMessage}");

                    // Send response
                    string responseMessage = "Message received";
                    byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                    udpListener.Send(responseData, responseData.Length, remoteEP);
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new MethodInvoker(delegate { txtLog.AppendText(message + Environment.NewLine); }));
            }
            else
            {
                txtLog.AppendText(message + Environment.NewLine);
            }
        }
    }
}
