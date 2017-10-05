using System;
using LockOnCode.VirtualMachine.Devices.Machine;

namespace FrontEndTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var memory = new SystemMemory(1024);
            var span = memory.RetrieveAddress(0);
            Console.WriteLine("Hello World!");
        }
    }
}