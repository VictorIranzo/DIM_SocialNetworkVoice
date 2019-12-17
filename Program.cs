namespace REcoSample
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using REcoSample.Login;
    using REcoSample.Profile;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IServiceProvider serviceProvider = BuildServiceProvider();

            CreateDatabase(serviceProvider);

            try
            {
                // Uncomment this code to initialize the application from the Login form.
                ////Application.Run(new LoginForm(serviceProvider));

                // Code to intialize the application with the first user registered.
                Application.Run(
                    new ProfileForm(
                        serviceProvider,
                        serviceProvider.CreateScope().ServiceProvider
                            .GetService<PersistenceContext>().Users.Include(u => u.OwnerPosts).FirstOrDefault()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private static void CreateDatabase(IServiceProvider serviceProvider)
        {
            using (PersistenceContext context = serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            string databaseConnection = "Data Source=Database.db";
            serviceCollection.AddDbContext<PersistenceContext>(options => options.UseSqlite(databaseConnection));

            return serviceCollection.BuildServiceProvider();
        }
    }
}