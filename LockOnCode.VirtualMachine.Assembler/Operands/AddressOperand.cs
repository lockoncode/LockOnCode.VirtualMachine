using System;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operands
{
    public class AddressOperand : InstructionOperand
    {
        public ulong Address { get; }

        public override byte OperandType => (byte)OperandTypes.Address;

        public AddressOperand(ulong address)
        {
            this.Address = address;
        }

        public override byte[] AsBytes()
        {
            return BitConverter.GetBytes(this.Address);
        }
    }
}