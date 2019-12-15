﻿namespace REcoSample.Profile
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ProfileForm : Form
    {
        private readonly IServiceProvider serviceProvider;
        private readonly User loggedUser;
        private readonly Stack<User> visitedProfiles;
        private User currentUser;

        public ProfileForm(IServiceProvider serviceProvider, User user)
        {
            this.InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.loggedUser = user;

            this.visitedProfiles = new Stack<User>();
            this.visitedProfiles.Push(this.loggedUser);

            this.LoadFriends();

            this.SetupForm(this.loggedUser);
        }

        private void SetupForm(User user)
        {
            this.currentUser = user;
            this.nameLabel.Text = user.Name;

            this.usersListBox.DisplayMember = "Name";

            this.backButton.Enabled = false;
            this.setPhotoButton.Enabled = true;

            if (user.Id != loggedUser.Id)
            {
                this.setPhotoButton.Enabled = false;
                this.backButton.Enabled = true;
            }

            this.photoBox.Image = null;
            if (user.Photo != null)
            {
                Bitmap bitMap = GetBitMapOfUserPhoto(user);

                this.SetImage(bitMap);
            }

            this.SetUserPosts();
        }

        private static Bitmap GetBitMapOfUserPhoto(User user)
        {
            Bitmap bitMap;

            using (MemoryStream memoryStream = new MemoryStream(user.Photo))
            {
                bitMap = new Bitmap(memoryStream, false);
            }

            return bitMap;
        }

        private void SetUserPosts()
        {
            this.postsListView.Clear();
            this.postsListView.View = View.Details;
            this.postsListView.SmallImageList = new ImageList();
            this.postsListView.SmallImageList.ImageSize = new Size(50,50);
            this.postsListView.Columns.Add(text: "Comentarios", width: 500);

            if (this.currentUser.OwnerPosts == null || !this.currentUser.OwnerPosts.Any())
            {
                return;
            }

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                foreach (Post post in context.Posts.Include(p => p.WriterUser).Where(p => p.OwnerUserId == this.currentUser.Id).OrderByDescending(p => p.DateTime))
                {
                    ListViewGroup group = new ListViewGroup(header: $"{post.DateTime.ToShortDateString()} {post.DateTime.ToShortTimeString()}: {post.WriterUser.Name} escribió:");

                    // Aquí es la foto de quién lo escribe, no del user.
                    if (post.WriterUser.Photo != null)
                    {
                        Bitmap userPhoto = GetBitMapOfUserPhoto(post.WriterUser);
                        Image userImage = userPhoto.GetThumbnailImage(50, 50, new Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);

                        this.postsListView.SmallImageList.Images.Add(key: post.WriterUserId.ToString(), image: userImage);
                        this.postsListView.Items.Add(new ListViewItem(text: post.Text, imageKey: post.WriterUserId.ToString(), group: group));
                    }
                    else
                    {
                        this.postsListView.Items.Add(new ListViewItem(text: post.Text, group: group));
                    }

                    this.postsListView.Groups.Add(group);
                }
            }
        }

        private void LoadFriends()
        {
            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                foreach (User user in context.Users.Include(u => u.OwnerPosts))
                {
                    if (user.Id == loggedUser.Id)
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
                User user = context.Users.Find(loggedUser.Id);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, imageFormat);
                    byte[] photoAsByteArray = memoryStream.ToArray();

                    user.Photo = photoAsByteArray;
                    this.loggedUser.Photo = photoAsByteArray;

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
            if (string.IsNullOrEmpty(this.commentTextBox.Text))
            {
                return;
            }

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                Post post = new Post()
                {
                    DateTime = DateTime.Now,
                    Text = this.commentTextBox.Text,
                    WriterUserId = this.loggedUser.Id,
                    OwnerUserId = this.currentUser.Id
                };

                context.Posts.Add(post);

                this.currentUser.OwnerPosts.Add(post);

                context.SaveChanges();
            }

            this.commentTextBox.Text = string.Empty;

            this.SetUserPosts();
        }

        private void usersListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            User selectedUser = this.usersListBox.SelectedItem as User;
            this.visitedProfiles.Push(selectedUser);

            this.SetupForm(selectedUser);
        }
    }
}