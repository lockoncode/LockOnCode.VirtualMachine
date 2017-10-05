using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operands
{
    public class RegisterOperand : InstructionOperand
    {
        public byte Register { get; }

        public override byte OperandType => (byte)OperandTypes.Register;

        public RegisterOperand(byte register)
        {
            this.Register = register;
        }

        public override byte[] AsBytes()
        {
            return new byte[] { Register };
        }
    }
}