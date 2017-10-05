using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operations.NoOperands
{
    public class NopOperation : Operation
    {
        public override OpCode OpCode => OpCode.NOP;
    }
}