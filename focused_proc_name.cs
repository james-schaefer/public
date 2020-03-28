// The following code snippit gets the process name (w/ absolute path)
// of the windows process that currently has focus.  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text;
using System.Runtime.InteropServices;

namespace proc_name_cs
{
    class Program
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
             ProcessAccessFlags processAccess,
             bool bInheritHandle,
             int processId
        );

        [DllImport("psapi.dll")]
        static extern uint GetProcessImageFileName(
            IntPtr hProcess,
            [Out] StringBuilder lpImageFileName,
            [In] [MarshalAs(UnmanagedType.U4)] int nSize
        );

        public static string GetActiveWindowFileName()
        {
            const int max_path = 260;
            IntPtr window_handle;
            IntPtr process_handle;
            uint gpid_return_value;
            uint proc_name_return_value;
            uint focused_pid;
            StringBuilder file_name_buffer = new StringBuilder(max_path);
            file_name_buffer.Insert(0, "unknown");

            window_handle = GetForegroundWindow();
            gpid_return_value = GetWindowThreadProcessId(window_handle, out focused_pid);

            process_handle = OpenProcess( 
                ProcessAccessFlags.QueryInformation| ProcessAccessFlags.VirtualMemoryRead 
                ,false
                ,(int)focused_pid);

            proc_name_return_value = GetProcessImageFileName(
                process_handle
                , file_name_buffer
                , max_path);

            return file_name_buffer.ToString();
        }


        static void Main(string[] args)
        {
            string active_window = GetActiveWindowFileName();
            Console.WriteLine(active_window);
        }
    }
}
