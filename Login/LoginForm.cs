namespace REcoSample.Login
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using REcoSample.CreateUser;
    using REcoSample.Profile;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class LoginForm : Form
    {
        private readonly IServiceProvider serviceProvider;

        public LoginForm(IServiceProvider serviceProvider)
        {
            this.InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        private void Login()
        {
            User user;

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                user = context.Users.Include(u => u.OwnerPosts).FirstOrDefault(u => u.Mail == userTextBox.Text && u.Password == passwordTextBox.Text);
            }

            if (user == null)
            {
                MessageBox.Show("Usuario no encontrado.", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            this.Hide();

            ProfileForm profileForm = new ProfileForm(this.serviceProvider, user);
            profileForm.ShowDialog();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            this.Login();
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            CreateUserForm createUserForm = new CreateUserForm(this.serviceProvider);

            createUserForm.ShowDialog();
        }
    }
}