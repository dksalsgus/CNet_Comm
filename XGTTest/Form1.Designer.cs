
namespace XGTTest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtNationalNo = new System.Windows.Forms.TextBox();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.txtCmdType = new System.Windows.Forms.TextBox();
            this.txtBlock = new System.Windows.Forms.TextBox();
            this.txtDataSize1 = new System.Windows.Forms.TextBox();
            this.txtDataName1 = new System.Windows.Forms.TextBox();
            this.txtTail = new System.Windows.Forms.TextBox();
            this.rbReadMsg = new System.Windows.Forms.RichTextBox();
            this.rbSendMsg = new System.Windows.Forms.RichTextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "헤더";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "국번";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "명령어";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "명령어타입";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "블록수";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "변수 이름";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "변수 크기";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 302);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "테일";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(693, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(95, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(82, 402);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send Message";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(98, 32);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(100, 21);
            this.txtHeader.TabIndex = 11;
            this.txtHeader.Text = "ENQ";
            // 
            // txtNationalNo
            // 
            this.txtNationalNo.Location = new System.Drawing.Point(98, 69);
            this.txtNationalNo.Name = "txtNationalNo";
            this.txtNationalNo.Size = new System.Drawing.Size(100, 21);
            this.txtNationalNo.TabIndex = 12;
            this.txtNationalNo.Text = "01";
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(98, 106);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(100, 21);
            this.txtCmd.TabIndex = 13;
            this.txtCmd.Text = "R";
            // 
            // txtCmdType
            // 
            this.txtCmdType.Location = new System.Drawing.Point(98, 144);
            this.txtCmdType.Name = "txtCmdType";
            this.txtCmdType.Size = new System.Drawing.Size(100, 21);
            this.txtCmdType.TabIndex = 14;
            this.txtCmdType.Text = "SS";
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(98, 184);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(100, 21);
            this.txtBlock.TabIndex = 15;
            this.txtBlock.Text = "01";
            // 
            // txtDataSize1
            // 
            this.txtDataSize1.Location = new System.Drawing.Point(98, 224);
            this.txtDataSize1.Name = "txtDataSize1";
            this.txtDataSize1.Size = new System.Drawing.Size(100, 21);
            this.txtDataSize1.TabIndex = 16;
            this.txtDataSize1.Text = "06";
            // 
            // txtDataName1
            // 
            this.txtDataName1.Location = new System.Drawing.Point(98, 260);
            this.txtDataName1.Name = "txtDataName1";
            this.txtDataName1.Size = new System.Drawing.Size(100, 21);
            this.txtDataName1.TabIndex = 17;
            this.txtDataName1.Text = "%MW020";
            // 
            // txtTail
            // 
            this.txtTail.Location = new System.Drawing.Point(98, 299);
            this.txtTail.Name = "txtTail";
            this.txtTail.Size = new System.Drawing.Size(100, 21);
            this.txtTail.TabIndex = 18;
            this.txtTail.Text = "E0T";
            // 
            // rbReadMsg
            // 
            this.rbReadMsg.Location = new System.Drawing.Point(422, 260);
            this.rbReadMsg.Name = "rbReadMsg";
            this.rbReadMsg.Size = new System.Drawing.Size(366, 176);
            this.rbReadMsg.TabIndex = 23;
            this.rbReadMsg.Text = "";
            // 
            // rbSendMsg
            // 
            this.rbSendMsg.Location = new System.Drawing.Point(422, 72);
            this.rbSendMsg.Name = "rbSendMsg";
            this.rbSendMsg.Size = new System.Drawing.Size(366, 152);
            this.rbSendMsg.TabIndex = 24;
            this.rbSendMsg.Text = "";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(172, 402);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 25;
            this.btnRead.Text = "Read Msg";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(422, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "SendData";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(422, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "Receive Data";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(578, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 28;
            this.txtPort.Text = "4001";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(422, 12);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 21);
            this.txtHost.TabIndex = 29;
            this.txtHost.Text = "192.168.1.204";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(386, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 12);
            this.label11.TabIndex = 30;
            this.label11.Text = "Host";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(545, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 12);
            this.label12.TabIndex = 31;
            this.label12.Text = "Port";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(693, 41);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(95, 23);
            this.btnDisconnect.TabIndex = 32;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.rbSendMsg);
            this.Controls.Add(this.rbReadMsg);
            this.Controls.Add(this.txtTail);
            this.Controls.Add(this.txtDataName1);
            this.Controls.Add(this.txtDataSize1);
            this.Controls.Add(this.txtBlock);
            this.Controls.Add(this.txtCmdType);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.txtNationalNo);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtNationalNo;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.TextBox txtCmdType;
        private System.Windows.Forms.TextBox txtBlock;
        private System.Windows.Forms.TextBox txtDataSize1;
        private System.Windows.Forms.TextBox txtDataName1;
        private System.Windows.Forms.TextBox txtTail;
        private System.Windows.Forms.RichTextBox rbReadMsg;
        private System.Windows.Forms.RichTextBox rbSendMsg;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnDisconnect;
    }
}

