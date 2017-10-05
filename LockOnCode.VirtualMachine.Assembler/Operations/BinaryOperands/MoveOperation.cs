using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.BinaryOperands
{
    public class MoveOperation<SourceOperand, DestinationOperand> : BinaryOperation<SourceOperand, DestinationOperand> where SourceOperand : InstructionOperand where DestinationOperand : InstructionOperand
    {
        public override OpCode OpCode => OpCode.Move;

        public MoveOperation(DataType dataType, SourceOperand source, DestinationOperand destination) : base(dataType, source, destination)
        {
        }
    }
}