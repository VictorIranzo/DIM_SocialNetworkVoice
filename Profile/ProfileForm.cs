namespace REcoSample.Profile
{
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    public partial class ProfileForm : Form
    {
        private readonly IServiceProvider serviceProvider;
        private readonly User currentUser;
        private readonly Stack<User> visitedProfiles;

        public ProfileForm(IServiceProvider serviceProvider, User user)
        {
            this.InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.currentUser = user;

            this.visitedProfiles = new Stack<User>();
            this.visitedProfiles.Push(this.currentUser);

            this.LoadFriends();

            this.SetupForm(this.currentUser);
        }

        private void SetupForm(User user)
        {
            this.nameLabel.Text = user.Name;

            this.usersListBox.DisplayMember = "Name";

            this.backButton.Enabled = false;
            this.setPhotoButton.Enabled = true;

            if (user.Id != currentUser.Id)
            {
                this.setPhotoButton.Enabled = false;
                this.backButton.Enabled = true;
            }

            this.photoBox.Image = null;
            if (user.Photo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(user.Photo))
                {
                    Bitmap bitMap = new Bitmap(memoryStream, false);

                    this.SetImage(bitMap);
                }
            }
        }

        private void LoadFriends()
        {
            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                foreach (User user in context.Users)
                {
                    if (user.Id == currentUser.Id)
                    {
                        continue;
                    }

                    usersListBox.Items.Add(user);
                }
            }
        }

        private void setPhotoButton_Click(object sender, EventArgs e)
        {
            this.SetPhoto();
        }

        private void SetPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.png|*.jpg";
            openFileDialog.Multiselect = false;

            DialogResult result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            Bitmap bitMap = new Bitmap(openFileDialog.FileName);
            ImageFormat imageFormat = openFileDialog.FileName.EndsWith(".png") ? ImageFormat.Png : ImageFormat.Jpeg;
            Image image = SetImage(bitMap);

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                User user = context.Users.Find(currentUser.Id);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, imageFormat);
                    user.Photo = memoryStream.ToArray();

                    context.SaveChanges();
                }
            }
        }

        private Image SetImage(Bitmap bitMap)
        {
            Image image = bitMap.GetThumbnailImage(121, 93, new Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);

            photoBox.Image = image;
            return image;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.visitedProfiles.Pop();
            this.SetupForm(this.visitedProfiles.Peek());
        }

        private void addPostButton_Click(object sender, EventArgs e)
        {

        }

        private void usersListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            User selectedUser = this.usersListBox.SelectedItem as User;
            this.visitedProfiles.Push(selectedUser);

            this.SetupForm(selectedUser);
        }
    }
}