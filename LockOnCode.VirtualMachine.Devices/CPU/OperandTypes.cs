namespace LockOnCode.VirtualMachine.Devices.CPU
{
    public enum OperandTypes : byte
    {
        VectorConstant = 1,
        ScalarConstant,
        Register,
        Address
    }
}