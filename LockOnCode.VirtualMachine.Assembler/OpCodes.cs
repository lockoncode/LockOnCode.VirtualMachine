using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Assembler.Operations.BinaryOperands;
using LockOnCode.VirtualMachine.Assembler.Operations.NoOperands;
using LockOnCode.VirtualMachine.Assembler.Operations.TrinaryOperand;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler
{
    public class OpCodes
    {
        public static readonly IOperation Halt = new HaltOperation();
        public static readonly IOperation NOP = new NopOperation();

        public static IOperation Move<SourceOperand, DestinationOperand>(DataType dataType, SourceOperand source, DestinationOperand destination) where SourceOperand : InstructionOperand where DestinationOperand : InstructionOperand
            => new MoveOperation<SourceOperand, DestinationOperand>(dataType, source, destination);

        public static IOperation Add<SourceOperand1, SourceOperand2, DestinationOperand>(DataType dataType, SourceOperand1 source1, SourceOperand2 source2, DestinationOperand destination) where SourceOperand1 : InstructionOperand where SourceOperand2 : InstructionOperand where DestinationOperand : InstructionOperand
           => new AddOperation<DestinationOperand, SourceOperand1, SourceOperand2>(dataType, destination, source1, source2);
    }
}