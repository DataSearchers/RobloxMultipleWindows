using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MultiBloX
{

    public partial class Form1 : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_HANDLE_INFORMATION
        {
            public uint ProcessId;
            public byte ObjectTypeNumber;
            public byte Flags;
            public ushort Handle;
            public uint Object;
            public uint GrantedAccess;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OBJECT_NAME_INFORMATION
        {
            public UNICODE_STRING Name;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);

        [DllImport("ntdll.dll")]
        private static extern uint NtQuerySystemInformation(int systemInformationClass, IntPtr systemInformation, uint systemInformationLength, out uint returnLength);

        [DllImport("ntdll.dll")]
        private static extern uint NtQueryObject(IntPtr handle, int objectInformationClass, IntPtr objectInformation, uint objectInformationLength, out uint returnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

        private const int SystemHandleInformation = 16;
        private const int ObjectNameInformation = 1;
        private const uint PROCESS_ALL = 0x001F0FFF;
        private const uint DUPLICATE_CLOSE_SOURCE = 0x0001;
        private const uint DUPLICATE_SAME_ACCESS = 0x0002;


        private static Mutex mutex = null;
        private readonly static string mutexName = "ROBLOX_singletonEvent";


        public Form1()
        {
            InitializeComponent();
        }

        private void vButton1_Click(object sender, EventArgs e)
        {
            StopRobloxInstances();
            CloseHandle();
            OpenMutex();
            vButton1.Enabled = false;
            vButton1.Text = "STARTED";
        }


        private static void StopRobloxInstances()
        {
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                process.Kill();
            }
        }
        private static void CloseHandle()
        {
            Process[] processes = Process.GetProcessesByName("RobloxPlayerBeta");

            foreach (var process in processes)
            {
                uint size = 0x10000;
                IntPtr buffer = Marshal.AllocHGlobal((int)size);

                while (true)
                {
                    uint status = NtQuerySystemInformation(SystemHandleInformation, buffer, size, out uint _);

                    if (status == 0xC0000004)
                    {
                        size *= 2;
                        Marshal.FreeHGlobal(buffer);
                        buffer = Marshal.AllocHGlobal((int)size);
                    }
                    else
                    {
                        break;
                    }
                }

                int handleCount = Marshal.ReadInt32(buffer);
                IntPtr ptr = new IntPtr(buffer.ToInt64() + Marshal.SizeOf(typeof(int)));

                for (int i = 0; i < handleCount; i++)
                {
                    SYSTEM_HANDLE_INFORMATION handleInfo = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ptr, typeof(SYSTEM_HANDLE_INFORMATION));

                    if (handleInfo.ProcessId == process.Id)
                    {
                        IntPtr processHandle = OpenProcess(PROCESS_ALL, false, process.Id);
                        if (processHandle == IntPtr.Zero)
                        {
                            ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(SYSTEM_HANDLE_INFORMATION)));
                            continue;
                        }

                        bool success = DuplicateHandle(processHandle, new IntPtr(handleInfo.Handle), GetCurrentProcess(), out IntPtr dupHandle, 0, false, DUPLICATE_SAME_ACCESS);
                        if (!success)
                        {
                            CloseHandle(processHandle);
                            ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(SYSTEM_HANDLE_INFORMATION)));
                            continue;
                        }

                        uint bufferSize = 0x1000;
                        IntPtr nameBuffer = Marshal.AllocHGlobal((int)bufferSize);

                        uint status = NtQueryObject(dupHandle, ObjectNameInformation, nameBuffer, bufferSize, out uint _);

                        if (status != 0)
                        {
                            Marshal.FreeHGlobal(nameBuffer);
                            CloseHandle(dupHandle);
                            CloseHandle(processHandle);
                            ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(SYSTEM_HANDLE_INFORMATION)));
                            continue;
                        }

                        OBJECT_NAME_INFORMATION objectNameInfo = (OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(nameBuffer, typeof(OBJECT_NAME_INFORMATION));
                        if (objectNameInfo.Name.Length > 0)
                        {
                            string name = Marshal.PtrToStringUni(objectNameInfo.Name.Buffer, objectNameInfo.Name.Length / 2);
                            if (name.Contains("ROBLOX_singletonEvent"))
                            {
                                DuplicateHandle(processHandle, new IntPtr(handleInfo.Handle), IntPtr.Zero, out _, 0, false, DUPLICATE_CLOSE_SOURCE);
                            }
                        }

                        Marshal.FreeHGlobal(nameBuffer);
                        CloseHandle(dupHandle);
                        CloseHandle(processHandle);
                    }
                    ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(SYSTEM_HANDLE_INFORMATION)));
                }

                Marshal.FreeHGlobal(buffer);
            }
        }
        
        private static void OpenMutex()
        {
            try
            {
                mutex = new Mutex(false, mutexName);
            }
            catch
            {
                MessageBox.Show("ERROR : Mutex cannot be created.");
            }
        }

        private void vButton2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
