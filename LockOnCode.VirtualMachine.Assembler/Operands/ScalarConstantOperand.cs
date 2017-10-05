using System;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler.Operands
{
    public class ScalarConstantOperand<ConstantType> : InstructionOperand where ConstantType : struct
    {
        public override byte OperandType => (byte)OperandTypes.ScalarConstant;

        public ConstantType Value { get; }

        public ScalarConstantOperand(ConstantType value)
        {
            this.Value = value;
        }

        public override byte[] AsBytes()
        {
            switch (Value)
            {
                case byte b:
                    return new byte[] { b };

                case sbyte sb:
                    return new byte[] { (byte)sb };

                case short i16:
                    return BitConverter.GetBytes(i16);

                case ushort ui16:
                    return BitConverter.GetBytes(ui16);

                case int i32:
                    return BitConverter.GetBytes(i32);

                case uint ui32:
                    return BitConverter.GetBytes(ui32);

                case long i64:
                    return BitConverter.GetBytes(i64);

                case ulong ui64:
                    return BitConverter.GetBytes(ui64);

                case float f:
                    return BitConverter.GetBytes(f);

                case double d:
                    return BitConverter.GetBytes(d);

                case char c:
                    return BitConverter.GetBytes(c);

                default:
                    throw new ArgumentOutOfRangeException(nameof(Value));
            }
        }
    }
}