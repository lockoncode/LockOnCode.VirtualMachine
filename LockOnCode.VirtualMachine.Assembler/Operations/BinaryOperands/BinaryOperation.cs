using System;
using System.Collections.Generic;
using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Assembler.Operations.NoOperands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.BinaryOperands
{
    public abstract class BinaryOperation<LeftOperandType, RightOperandType> : Operation where LeftOperandType : InstructionOperand where RightOperandType : InstructionOperand
    {
        public LeftOperandType LeftOperand { get; }
        public RightOperandType RightOperand { get; }
        public DataType DataType { get; }

        protected BinaryOperation(DataType dataType, LeftOperandType leftOperand, RightOperandType rightOperand)
        {
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
            this.DataType = dataType;
        }

        public override byte[] AsBytes()
        {
            // Instruction format for binary operations
            // opcode: first word
            // data type next byte
            // left hand operand type: next 4 bits
            // Right hand operand type next 4 bits
            // Value of left hand operator
            // Value of right hand operator
            var instructionBytes = new List<byte>(BitConverter.GetBytes((short)this.OpCode)) { (byte)this.DataType };
            var operandTypes = (byte)(this.LeftOperand.OperandType | (byte)(this.RightOperand.OperandType << 4));
            instructionBytes.Add(operandTypes);
            instructionBytes.AddRange(this.LeftOperand.AsBytes());
            instructionBytes.AddRange(this.RightOperand.AsBytes());
            return instructionBytes.ToArray();
        }
    }
}