using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace REcoSample.CreateUser
{
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using REcoSample.Profile;
    using System.Windows.Forms;

    public partial class CreateUserForm : Form
    {
        private readonly IServiceProvider serviceProvider;

        public CreateUserForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.nameTextBox.Text)
                || string.IsNullOrEmpty(this.mailTextBox.Text)
                || string.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                return;
            }

            User newUser;

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                newUser = new User()
                {
                    Name = this.nameTextBox.Text,
                    Mail = this.mailTextBox.Text,
                    Password = this.passwordTextBox.Text,
                };

                context.Users.Add(newUser);

                context.SaveChanges();
            }

            this.Hide();
            ProfileForm profileForm = new ProfileForm(this.serviceProvider, newUser);

            profileForm.ShowDialog();
        }
    }
}