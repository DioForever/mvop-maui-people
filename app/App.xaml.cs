using app.Backend;

namespace app
{
    public partial class App : Application
    {
        readonly IDatabaseController Database;
        public App(IDatabaseController databaseController)
        {
            InitializeComponent();
            Database = databaseController;
            MainPage = new AppShell();
        }
    }
}
