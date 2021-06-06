
namespace BillingSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.menuStripTop = new System.Windows.Forms.MenuStrip();
            this.Users = new System.Windows.Forms.ToolStripMenuItem();
            this.Category = new System.Windows.Forms.ToolStripMenuItem();
            this.Products = new System.Windows.Forms.ToolStripMenuItem();
            this.DealerCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.Inventory = new System.Windows.Forms.ToolStripMenuItem();
            this.Transcriptions = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblLoggedInUser = new System.Windows.Forms.Label();
            this.lblAppFName = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.lblSHead = new System.Windows.Forms.Label();
            this.pnlFooter.SuspendLayout();
            this.menuStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.Teal;
            this.pnlFooter.Controls.Add(this.lblFooter);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 415);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1186, 35);
            this.pnlFooter.TabIndex = 0;
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFooter.ForeColor = System.Drawing.Color.White;
            this.lblFooter.Location = new System.Drawing.Point(600, 9);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(135, 17);
            this.lblFooter.TabIndex = 0;
            this.lblFooter.Text = "Developed By: RronM";
            // 
            // menuStripTop
            // 
            this.menuStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Users,
            this.Category,
            this.Products,
            this.DealerCustomer,
            this.Inventory,
            this.Transcriptions});
            this.menuStripTop.Location = new System.Drawing.Point(0, 0);
            this.menuStripTop.Name = "menuStripTop";
            this.menuStripTop.Size = new System.Drawing.Size(1186, 24);
            this.menuStripTop.TabIndex = 1;
            this.menuStripTop.Text = "menuStrip1";
            this.menuStripTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStripTop_ItemClicked);
            // 
            // Users
            // 
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(47, 20);
            this.Users.Text = "Users";
            this.Users.ToolTipText = "users";
            this.Users.Click += new System.EventHandler(this.Users_Click);
            // 
            // Category
            // 
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(67, 20);
            this.Category.Text = "Category";
            this.Category.Click += new System.EventHandler(this.Category_Click);
            // 
            // Products
            // 
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(66, 20);
            this.Products.Text = "Products";
            this.Products.Click += new System.EventHandler(this.Products_Click);
            // 
            // DealerCustomer
            // 
            this.DealerCustomer.Name = "DealerCustomer";
            this.DealerCustomer.Size = new System.Drawing.Size(133, 20);
            this.DealerCustomer.Text = " Dealer and Customer";
            this.DealerCustomer.Click += new System.EventHandler(this.DealerCustomer_Click);
            // 
            // Inventory
            // 
            this.Inventory.Name = "Inventory";
            this.Inventory.Size = new System.Drawing.Size(69, 20);
            this.Inventory.Text = "Inventory";
            this.Inventory.Click += new System.EventHandler(this.Inventory_Click);
            // 
            // Transcriptions
            // 
            this.Transcriptions.Name = "Transcriptions";
            this.Transcriptions.Size = new System.Drawing.Size(79, 20);
            this.Transcriptions.Text = "Transaction";
            this.Transcriptions.Click += new System.EventHandler(this.Transcriptions_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblUser.Location = new System.Drawing.Point(15, 35);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(38, 17);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "User:";
            // 
            // lblLoggedInUser
            // 
            this.lblLoggedInUser.AccessibleName = "users";
            this.lblLoggedInUser.AutoSize = true;
            this.lblLoggedInUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLoggedInUser.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblLoggedInUser.Location = new System.Drawing.Point(49, 35);
            this.lblLoggedInUser.Name = "lblLoggedInUser";
            this.lblLoggedInUser.Size = new System.Drawing.Size(0, 17);
            this.lblLoggedInUser.TabIndex = 3;
            // 
            // lblAppFName
            // 
            this.lblAppFName.AutoSize = true;
            this.lblAppFName.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAppFName.Location = new System.Drawing.Point(562, 272);
            this.lblAppFName.Name = "lblAppFName";
            this.lblAppFName.Size = new System.Drawing.Size(111, 37);
            this.lblAppFName.TabIndex = 4;
            this.lblAppFName.Text = "BILLING";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLName.Location = new System.Drawing.Point(667, 272);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(119, 37);
            this.lblLName.TabIndex = 5;
            this.lblLName.Text = "SYSTEM";
            // 
            // lblSHead
            // 
            this.lblSHead.AutoSize = true;
            this.lblSHead.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSHead.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblSHead.Location = new System.Drawing.Point(526, 309);
            this.lblSHead.Name = "lblSHead";
            this.lblSHead.Size = new System.Drawing.Size(301, 25);
            this.lblSHead.TabIndex = 6;
            this.lblSHead.Text = "Billing and Inventory Managment";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 450);
            this.Controls.Add(this.lblSHead);
            this.Controls.Add(this.lblLName);
            this.Controls.Add(this.lblAppFName);
            this.Controls.Add(this.lblLoggedInUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.menuStripTop);
            this.MainMenuStrip = this.menuStripTop;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.menuStripTop.ResumeLayout(false);
            this.menuStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.MenuStrip menuStripTop;
        private System.Windows.Forms.ToolStripMenuItem Users;
        private System.Windows.Forms.ToolStripMenuItem Category;
        private System.Windows.Forms.ToolStripMenuItem Products;
        private System.Windows.Forms.ToolStripMenuItem Inventory;
        private System.Windows.Forms.ToolStripMenuItem Transcriptions;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblLoggedInUser;
        private System.Windows.Forms.Label lblAppFName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.Label lblSHead;
        private System.Windows.Forms.ToolStripMenuItem DealerCustomer;
    }
}

