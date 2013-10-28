using System;
using Core.Messages;

namespace Services
{
    public class Program
    {
        static ServicesHost _host;

        [STAThread]
        public static void Main(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            _host = new ServicesHost();
        }
    }
}
