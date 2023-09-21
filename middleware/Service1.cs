using middleware.API;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace middleware
{
    public partial class Service1 : ServiceBase
    {
        private Timer scheduler;
        public Service1()
        {
            InitializeComponent();
        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            new ApiRequests();
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            scheduler = new Timer
            {
                Interval = double.Parse(ConfigurationManager.AppSettings["timer"])
            };

            scheduler.Elapsed += Scheduler_Elapsed;
            scheduler.Enabled = true;
        }

        private void Scheduler_Elapsed(object sender, ElapsedEventArgs e)
        {
            scheduler.Enabled = false;
            new ApiRequests();
            scheduler.Enabled = true;
        }

        protected override void OnStop() { }
    }
}
