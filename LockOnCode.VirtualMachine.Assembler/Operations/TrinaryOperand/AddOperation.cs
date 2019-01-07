using System;
using System.Collections.Generic;
using System.Text;
using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Assembler.Operations.TrinaryOperands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.TrinaryOperand
{
    public class AddOperation<DestinationOperand, Source1Operand, Source2Operand> : TrinaryOperand<DestinationOperand, Source1Operand, Source2Operand> where Source1Operand : InstructionOperand
        where Source2Operand : InstructionOperand
        where DestinationOperand : InstructionOperand
    {
        public override OpCode OpCode => OpCode.Add;

        public AddOperation(DataType dataType, DestinationOperand destination, Source1Operand source1, Source2Operand source2) : base(dataType, destination, source1, source2)
        {
        }
    }
}