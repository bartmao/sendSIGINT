using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendSIGINT
{
    public partial class SendSIGINTInvoker : Form
    {
        public SendSIGINTInvoker(string[] args)
        {
            //InitializeComponent();
            int processId;
            if (args != null && args.Length >= 1 && int.TryParse(args[0], out processId))
            {
                Do(processId);
            }
            Environment.Exit(0);
        }

        public void Do(int processId) {
            var attachFlag = AttachConsole((uint)processId);
            if (attachFlag) {
                SetConsoleCtrlHandler(IntPtr.Zero, true);
                GenerateConsoleCtrlEvent(0, 0);
                FreeConsole();
                SetConsoleCtrlHandler(IntPtr.Zero, false);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleCtrlHandler(IntPtr process, bool add);
    }
}
