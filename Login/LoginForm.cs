namespace REcoSample.Login
{
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using System;
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
                user = context.Users.FirstOrDefault(u => u.Mail == userTextBox.Text && u.Password == passwordTextBox.Text);
            }

            if (user == null)
            {
                MessageBox.Show("Usuario no encontrado.", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


        }
    }
}