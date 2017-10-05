using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.NoOperands
{
    public class HaltOperation : Operation
    {
        public override OpCode OpCode => OpCode.Halt;
    }
}