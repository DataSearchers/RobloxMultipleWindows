using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ListAllHandles
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQuerySystemInformation(
            int SystemInformationClass,
            IntPtr SystemInformation,
            uint SystemInformationLength,
            ref uint ReturnLength);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryObject(
            IntPtr Handle,
            int ObjectInformationClass,
            IntPtr ObjectInformation,
            uint ObjectInformationLength,
            ref uint ReturnLength);

        private const uint PROCESS_DUP_HANDLE = 0x00000040;
        private const int SystemHandleInformation = 16;

        static void Main(string[] args)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
                {
                    IntPtr processHandle = OpenProcess(PROCESS_DUP_HANDLE, false, process.Id);
                    if (processHandle == IntPtr.Zero)
                    {
                        Console.WriteLine($"Failed to open handle for process {process.Id}. Error: {Marshal.GetLastWin32Error()}");
                        continue;
                    }

                    Console.WriteLine($"Displaying handles for process: {process.ProcessName} (PID: {process.Id})");

                    uint length = 0;
                    int queryResult = NtQuerySystemInformation(SystemHandleInformation, IntPtr.Zero, 0, ref length);

                    if (queryResult != 0 && length == 0)
                    {
                        Console.WriteLine("Failed to determine buffer size for system handle information.");
                        continue;
                    }

                    IntPtr infoBuffer = Marshal.AllocHGlobal((int)length);
                    try
                    {
                        queryResult = NtQuerySystemInformation(SystemHandleInformation, infoBuffer, length, ref length);
                        if (queryResult != 0)
                        {
                            Console.WriteLine("Failed to query system handle information.");
                            continue;
                        }

                        int handleCount = Marshal.ReadInt32(infoBuffer);
                        IntPtr handleEntryPtr = IntPtr.Add(infoBuffer, 4);

                        Console.WriteLine($"Handle Count: {handleCount}");

                        for (int i = 0; i < handleCount; i++)
                        {
                            IntPtr handle = Marshal.ReadIntPtr(handleEntryPtr);
                            handleEntryPtr = IntPtr.Add(handleEntryPtr, IntPtr.Size);

                            Console.WriteLine($"Handle: {handle}");
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(infoBuffer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
