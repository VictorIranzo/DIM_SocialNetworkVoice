namespace REcoSample.Profile
{
    partial class ProfileForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.photoBox = new System.Windows.Forms.PictureBox();
            this.setPhotoButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.postsListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.commentTextBox = new System.Windows.Forms.RichTextBox();
            this.addPostButton = new System.Windows.Forms.Button();
            this.usersListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.photoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // photoBox
            // 
            this.photoBox.Location = new System.Drawing.Point(12, 12);
            this.photoBox.Name = "photoBox";
            this.photoBox.Size = new System.Drawing.Size(121, 93);
            this.photoBox.TabIndex = 0;
            this.photoBox.TabStop = false;
            // 
            // setPhotoButton
            // 
            this.setPhotoButton.Location = new System.Drawing.Point(12, 111);
            this.setPhotoButton.Name = "setPhotoButton";
            this.setPhotoButton.Size = new System.Drawing.Size(121, 23);
            this.setPhotoButton.TabIndex = 1;
            this.setPhotoButton.Text = "Cambiar foto";
            this.setPhotoButton.UseVisualStyleBackColor = true;
            this.setPhotoButton.Click += new System.EventHandler(this.setPhotoButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(155, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(51, 20);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "label1";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(639, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "Volver";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // postsListView
            // 
            this.postsListView.HideSelection = false;
            this.postsListView.Location = new System.Drawing.Point(159, 168);
            this.postsListView.Name = "postsListView";
            this.postsListView.Size = new System.Drawing.Size(555, 251);
            this.postsListView.TabIndex = 4;
            this.postsListView.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Amigos";
            // 
            // commentTextBox
            // 
            this.commentTextBox.Location = new System.Drawing.Point(159, 54);
            this.commentTextBox.Name = "commentTextBox";
            this.commentTextBox.Size = new System.Drawing.Size(555, 80);
            this.commentTextBox.TabIndex = 6;
            this.commentTextBox.Text = "";
            // 
            // addPostButton
            // 
            this.addPostButton.Location = new System.Drawing.Point(597, 138);
            this.addPostButton.Name = "addPostButton";
            this.addPostButton.Size = new System.Drawing.Size(117, 23);
            this.addPostButton.TabIndex = 7;
            this.addPostButton.Text = "Añadir comentario";
            this.addPostButton.UseVisualStyleBackColor = true;
            this.addPostButton.Click += new System.EventHandler(this.addPostButton_Click);
            // 
            // usersListBox
            // 
            this.usersListBox.FormattingEnabled = true;
            this.usersListBox.Location = new System.Drawing.Point(12, 168);
            this.usersListBox.Name = "usersListBox";
            this.usersListBox.Size = new System.Drawing.Size(120, 251);
            this.usersListBox.TabIndex = 9;
            this.usersListBox.SelectedValueChanged += new System.EventHandler(this.usersListBox_SelectedValueChanged);
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 431);
            this.Controls.Add(this.usersListBox);
            this.Controls.Add(this.addPostButton);
            this.Controls.Add(this.commentTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.postsListView);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.setPhotoButton);
            this.Controls.Add(this.photoBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProfileForm";
            this.Text = "Perfil";
            ((System.ComponentModel.ISupportInitialize)(this.photoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox photoBox;
        private System.Windows.Forms.Button setPhotoButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ListView postsListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox commentTextBox;
        private System.Windows.Forms.Button addPostButton;
        private System.Windows.Forms.ListBox usersListBox;
    }
}