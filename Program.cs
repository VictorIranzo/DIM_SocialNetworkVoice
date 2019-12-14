namespace REcoSample
{
    using Microsoft.Extensions.DependencyInjection;
    using REcoSample.Login;
    using System;
    using System.Windows.Forms;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IServiceProvider serviceProvider = BuildServiceProvider();

            try
            {
                Application.Run(new LoginForm(serviceProvider));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AppContext>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}