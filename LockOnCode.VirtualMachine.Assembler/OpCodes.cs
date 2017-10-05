using LockOnCode.VirtualMachine.Assembler.Operands;
using LockOnCode.VirtualMachine.Assembler.Operations.BinaryOperands;
using LockOnCode.VirtualMachine.Assembler.Operations.NoOperands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler
{
    public class OpCodes
    {
        public static readonly IOperation Halt = new HaltOperation();
        public static readonly IOperation NOP = new NopOperation();

        public static IOperation Move<SourceOperand, DestinationOperand>(DataType dataType, SourceOperand source, DestinationOperand destination) where SourceOperand : InstructionOperand where DestinationOperand : InstructionOperand
            => new MoveOperation<SourceOperand, DestinationOperand>(dataType, source, destination);
    }
}