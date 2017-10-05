using System;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.NoOperands
{
    public abstract class Operation : IOperation
    {
        public abstract OpCode OpCode { get; }

        public virtual byte[] AsBytes()
        {
            return BitConverter.GetBytes((short)this.OpCode);
        }
    }
}