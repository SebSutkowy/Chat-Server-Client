namespace TURN_STUN_Server
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnSwitchClient;
        private System.Windows.Forms.Button btnFindPublicIP;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblPublicIP;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnSwitchClient = new System.Windows.Forms.Button();
            this.btnFindPublicIP = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblPublicIP = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnStartServer
            this.btnStartServer.Location = new System.Drawing.Point(20, 20);
            this.btnStartServer.Size = new System.Drawing.Size(120, 30);
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);

            // btnSwitchClient
            this.btnSwitchClient.Location = new System.Drawing.Point(150, 20);
            this.btnSwitchClient.Size = new System.Drawing.Size(120, 30);
            this.btnSwitchClient.Text = "Switch to Client";
            this.btnSwitchClient.UseVisualStyleBackColor = true;
            this.btnSwitchClient.Click += new System.EventHandler(this.btnSwitchClient_Click);

            // btnFindPublicIP
            this.btnFindPublicIP.Location = new System.Drawing.Point(280, 20);
            this.btnFindPublicIP.Size = new System.Drawing.Size(120, 30);
            this.btnFindPublicIP.Text = "Find Public IP";
            this.btnFindPublicIP.UseVisualStyleBackColor = true;
            this.btnFindPublicIP.Click += new System.EventHandler(this.btnFindPublicIP_Click);

            // lblPublicIP
            this.lblPublicIP.Location = new System.Drawing.Point(20, 60);
            this.lblPublicIP.Size = new System.Drawing.Size(100, 20);
            this.lblPublicIP.Text = "Public IP:";

            // txtPublicIP
            this.txtPublicIP.Location = new System.Drawing.Point(120, 60);
            this.txtPublicIP.Size = new System.Drawing.Size(280, 20);
            this.txtPublicIP.ReadOnly = true;

            // lblServerIP
            this.lblServerIP.Location = new System.Drawing.Point(20, 90);
            this.lblServerIP.Size = new System.Drawing.Size(100, 20);
            this.lblServerIP.Text = "Server IP:";

            // txtServerIP
            this.txtServerIP.Location = new System.Drawing.Point(120, 90);
            this.txtServerIP.Size = new System.Drawing.Size(280, 20);
            this.txtServerIP.Text = "Enter Server IP...";

            // lblMessage
            this.lblMessage.Location = new System.Drawing.Point(20, 120);
            this.lblMessage.Size = new System.Drawing.Size(100, 20);
            this.lblMessage.Text = "Message:";

            // txtMessage
            this.txtMessage.Location = new System.Drawing.Point(120, 120);
            this.txtMessage.Size = new System.Drawing.Size(280, 20);

            // btnSend
            this.btnSend.Location = new System.Drawing.Point(20, 150);
            this.btnSend.Size = new System.Drawing.Size(120, 30);
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // txtLog
            this.txtLog.Location = new System.Drawing.Point(20, 190);
            this.txtLog.Size = new System.Drawing.Size(380, 200);
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // Form1
            this.ClientSize = new System.Drawing.Size(420, 410);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.btnSwitchClient);
            this.Controls.Add(this.btnFindPublicIP);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtPublicIP);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblPublicIP);
            this.Text = "UDP Server & Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
