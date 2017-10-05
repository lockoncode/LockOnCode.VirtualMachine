using System;
using System.Collections.Generic;
using System.Text;

namespace LockOnCode.VirtualMachine.Assembler.Operands
{
    public abstract class InstructionOperand
    {
        public abstract byte OperandType { get; }

        public abstract byte[] AsBytes();
    }
}