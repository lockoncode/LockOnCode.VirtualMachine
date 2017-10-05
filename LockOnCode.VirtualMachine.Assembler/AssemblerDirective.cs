using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler
{
    public abstract class AssemblerDirective : IOperation
    {
        public abstract byte[] AsBytes();
    }
}