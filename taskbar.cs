using System;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace BatteryStatusApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Création d'un espace de travail Runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            // Création d'un pipeline pour exécuter le script PowerShell
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(@"[System.Windows.Forms.SystemInformation]::PowerStatus | Select-Object -Property [BatteryChargeStatus],[BatteryFullLifetime],[BatteryLifePercent],[BatteryLifeRemaining],[PowerLineStatus]");

            // Exécution du script et récupération du résultat
            Collection<PSObject> results = pipeline.Invoke();

            // Affichage du résultat dans la barre des tâches
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(5000, "Résultat du script PowerShell", results[0].ToString(), ToolTipIcon.Info);

            runspace.Close();
        }
    }
}
