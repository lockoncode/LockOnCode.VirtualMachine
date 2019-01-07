using System;
using System.Collections.Generic;
using System.Text;
using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Assembler.Operations.NoOperands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.TrinaryOperands
{
    public abstract class TrinaryOperand<DestinationOperandType, SourceOperandType1, SourceOperandType2> : Operation where DestinationOperandType : InstructionOperand where SourceOperandType1 : InstructionOperand where SourceOperandType2 : InstructionOperand
    {
        public DestinationOperandType destinationOperand { get; }
        public SourceOperandType1 sourceOperand1 { get; }
        public SourceOperandType2 sourceOperand2 { get; }
        public DataType DataType { get; }

        protected TrinaryOperand(DataType dataType, DestinationOperandType destinationOperand, SourceOperandType1 sourceOperand1, SourceOperandType2 sourceOperand2)
        {
            this.destinationOperand = destinationOperand;
            this.sourceOperand1 = sourceOperand1;
            this.sourceOperand2 = sourceOperand2;
            this.DataType = dataType;
        }

        public override byte[] AsBytes()
        {
            // Instruction format for trinary operations
            // opcode: first word
            // data type next byte
            // destination operand type: next 4 bits
            // source operand 1 type: next 4 bits
            // source operand 2 type: next 4 bits
            //
            // Value of destination operator
            // Value of source1 operator
            // Value of source2 operator
            var instructionBytes = new List<byte>(BitConverter.GetBytes((short)this.OpCode)) { (byte)this.DataType };
            var operandTypes = (byte)(this.destinationOperand.OperandType | this.sourceOperand1.OperandType << 4);
            instructionBytes.Add(operandTypes);
            instructionBytes.Add(this.sourceOperand2.OperandType);
            instructionBytes.AddRange(this.destinationOperand.AsBytes());
            instructionBytes.AddRange(this.sourceOperand1.AsBytes());
            instructionBytes.AddRange(this.sourceOperand2.AsBytes());
            return instructionBytes.ToArray();
        }
    }
}