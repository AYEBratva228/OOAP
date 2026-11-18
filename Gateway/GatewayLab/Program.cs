
using System;
using System.Windows.Forms;
using GatewayLab.Forms;

namespace GatewayLab
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.Run(new MainForm());
        }
    }
}
