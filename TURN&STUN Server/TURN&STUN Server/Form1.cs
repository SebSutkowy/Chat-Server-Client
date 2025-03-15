using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TURN_STUN_Server
{
    public partial class Form1 : Form
    {
        private UdpClient udpClient;
        private UdpClient udpServer;
        private Thread listenThread;
        private IPEndPoint serverEndPoint;
        private string publicIP;
        private int publicPort;
        private const int serverPort = 8080; // Server's listening port
        private bool isServerMode = false;

        public Form1()
        {
            InitializeComponent();
            btnSend.Enabled = false; // Disable send button until mode is selected
        }

        // START SERVER MODE
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            isServerMode = true;
            btnStartServer.Enabled = false;
            btnSwitchClient.Enabled = true; // Allow switching back

            listenThread = new Thread(StartListening);
            listenThread.IsBackground = true;
            listenThread.Start();

            Log("✅ Server Mode: Hosting on port " + serverPort);
            btnSend.Enabled = true;
        }

        private void StartListening()
        {
            udpServer = new UdpClient(serverPort);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, serverPort);

            while (true)
            {
                byte[] receivedData = udpServer.Receive(ref remoteEP);
                string receivedMessage = Encoding.UTF8.GetString(receivedData);
                Log($"📩 Received from {remoteEP.Address}:{remoteEP.Port} - {receivedMessage}");

                // Send acknowledgment back to the client
                string response = "Message received!";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                udpServer.Send(responseData, responseData.Length, remoteEP);
            }
        }

        // SWITCH TO CLIENT MODE
        private void btnSwitchClient_Click(object sender, EventArgs e)
        {
            isServerMode = !isServerMode;
            if(isServerMode)
            {
                btnSwitchClient.Text = "Switch to Client";
                btnStartServer.Enabled = true;
                btnSend.Enabled = false;
            }
            else
            {
                btnSwitchClient.Text = "Switch to Server";
                btnStartServer.Enabled = false;
                btnSend.Enabled = true;
            }
        }

        // FIND PUBLIC IP USING STUN

        private void btnFindPublicIP_Click(object sender, EventArgs e)
        {
            try
            {
                string stunServer = "stun.l.google.com"; // Google STUN Server
                int stunPort = 19302;

                UdpClient udpClient = new UdpClient();
                udpClient.Connect(stunServer, stunPort);

                byte[] stunRequest = new byte[20];
                stunRequest[0] = 0x00;
                stunRequest[1] = 0x01; // Binding Request
                Array.Copy(Guid.NewGuid().ToByteArray(), 0, stunRequest, 4, 16); // Transaction ID

                udpClient.Send(stunRequest, stunRequest.Length);

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] stunResponse = udpClient.Receive(ref remoteEP);

                if (stunResponse.Length > 0)
                {
                    // Extract public IP & port
                    int mappedPort = (stunResponse[26] << 8) | stunResponse[27];
                    string publicIP = $"{stunResponse[28]}.{stunResponse[29]}.{stunResponse[30]}.{stunResponse[31]}";

                    txtPublicIP.Text = $"{publicIP}:{mappedPort}";
                    Log($"🌎 Public IP: {publicIP}, Public Port: {mappedPort}");
                }
                else
                {
                    Log("⚠️ STUN failed to retrieve public IP.");
                }

                udpClient.Close();
            }
            catch (Exception ex)
            {
                Log("❌ STUN Error: " + ex.Message);
            }
        }


    // CLIENT SEND MESSAGE
    private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string targetIP = txtServerIP.Text.Trim();
                if (string.IsNullOrEmpty(targetIP))
                {
                    Log("⚠️ Enter a valid target IP.");
                    return;
                }

                serverEndPoint = new IPEndPoint(IPAddress.Parse(targetIP), serverPort);
                udpClient = new UdpClient();

                string message = txtMessage.Text;
                byte[] sendData = Encoding.UTF8.GetBytes(message);

                udpClient.Send(sendData, sendData.Length, serverEndPoint);
                Log("📤 Message sent: " + message);

                // Try TURN Server if UDP fails
                TryTURNServer(message);
            }
            catch (Exception ex)
            {
                Log("⚠️ Error: " + ex.Message);
            }
        }

        // BACKUP: USE TURN SERVER IF DIRECT CONNECTION FAILS
        private void TryTURNServer(string message)
        {
            try
            {
                string turnServerDomain = "relay.backups.cz"; // Free TURN Server
                IPAddress[] addresses = Dns.GetHostAddresses(turnServerDomain); 
                
                if(addresses.Length == 0)
                {
                    Log("❌ Failed to resolve TURN server domain. ");
                    return;
                }

                IPAddress turnServerIP = addresses[0];

                Log($"🌍 Turn Server Resolved: {turnServerIP}");
                int turnPort = 3478;

                Log("Y");
                IPEndPoint turnEndPoint = new IPEndPoint(turnServerIP, turnPort);
                Log("Z");
                UdpClient turnClient = new UdpClient();
                Log("A");

                byte[] sendData = Encoding.UTF8.GetBytes("[TURN] " + message);
                Log("B");
                turnClient.Send(sendData, sendData.Length, turnEndPoint);
                Log("🔄 Sent via TURN server.");
                turnClient.Close();
            }
            catch (Exception ex)
            {
                Log("❌ TURN Server Error: " + ex.Message);
            }
        }

        // LOGGING FUNCTION
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
