using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operands
{
    public class VectorConstantOperand<ConstantType> : InstructionOperand where ConstantType : struct
    {
        public Vector<ConstantType> Value { get; }

        public override byte OperandType => (byte)OperandTypes.VectorConstant;

        public VectorConstantOperand(Vector<ConstantType> value)
        {
            this.Value = value;
        }

        public override byte[] AsBytes()
        {
            var returnBytes = new byte[Marshal.SizeOf(Value)];
            Vector.AsVectorByte(this.Value).CopyTo(returnBytes);
            return returnBytes;
        }
    }
}