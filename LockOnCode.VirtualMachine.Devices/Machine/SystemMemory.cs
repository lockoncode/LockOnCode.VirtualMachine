using System;
using System.Numerics;

namespace LockOnCode.VirtualMachine.Devices.Machine
{
    public class SystemMemory
    {
        private readonly ulong ramSize;
        private byte[] ram;

        public SystemMemory(ulong ramSize)
        {
            this.ramSize = ramSize;
            this.ram = new byte[ramSize];
        }

        public ulong Size => this.ramSize;

        public Span<byte> RetrieveAddress(ulong address, int length = 32)
        {
            //cast long to int for now
            return new Span<byte>(ram, (int)address, length);
        }

        public void SetValueAtAddress(ulong address, Span<byte> value)
        {
            value.ToArray().CopyTo(ram, (int)address);
        }

        public void SetValueAtAddress(ulong address, Vector<byte> value)
        {
            value.CopyTo(ram, (int)address);
        }

        public void WriteBlock(byte[] content, ulong address = 0)
        {
            content.CopyTo(this.ram, (int)address);
        }
    }
}