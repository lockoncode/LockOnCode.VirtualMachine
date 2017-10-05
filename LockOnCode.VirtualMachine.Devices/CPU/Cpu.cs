using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LockOnCode.VirtualMachine.Devices.Machine;

namespace LockOnCode.VirtualMachine.Devices.CPU
{
    public class Cpu
    {
        public SystemMemory Memory { get; }
        private readonly HashSet<OpCode> NoOperandInstructions = new HashSet<OpCode> { OpCode.Halt, OpCode.NOP };
        private readonly HashSet<OpCode> TwoOperandInstructions = new HashSet<OpCode> { OpCode.Move };

        public Cpu(SystemMemory memory)

        {
            ProgramCounter = 0;
            this.Memory = memory;
            this.Registers = new Vector<byte>[255];
        }

        public ulong ProgramCounter { get; private set; }

        public Vector<byte>[] Registers;

        public event EventHandler Halt;

        public void Tick()
        {
            var instructionStart = this.Memory.RetrieveAddress(ProgramCounter, 4);
            var instruction = instructionStart.NonPortableCast<byte, OpCode>()[0];
            if (NoOperandInstructions.Contains(instruction))
            {
                ProgramCounter += HandleZeroOperandInstruction(instruction);
            }
            else if (TwoOperandInstructions.Contains(instruction))
            {
                var instructionSize = HandleTwoOperandInstruction(instructionStart, instruction);
                ProgramCounter += instructionSize;
            }
            else
            {
                Interupt();
            }
        }

        private byte HandleZeroOperandInstruction(OpCode instruction)
        {
            switch (instruction)
            {
                case OpCode.Halt:
                    OnHalt(new EventArgs());
                    break;

                case OpCode.NOP:

                    break;
            }
            return sizeof(OpCode);
        }

        private byte HandleTwoOperandInstruction(Span<byte> instructionStart, OpCode instruction)
        {
            switch (instruction)
            {
                case OpCode.Move:
                    return HandleMove(instructionStart);

                default:
                    return 0;
            }
        }

        private byte HandleMove(Span<byte> instructionStart)
        {
            var instructionDataType = instructionStart[2];
            var firstOperandType = (byte)(instructionStart[3] & 0x0F);
            var secondOperandType = (byte)((instructionStart[3] & 0xF0) >> 4);
            var firstOperand = RetrieveSourceValue(instructionDataType, firstOperandType, 4);
            var destinationAddress = this.Memory.RetrieveAddress(ProgramCounter + firstOperand.Size + 4);
            var secondOperandSize = WriteToDestintion(secondOperandType, destinationAddress, firstOperand.Value);
            return (byte)(4 + firstOperand.Size + secondOperandSize);
        }

        private byte WriteToDestintion(byte operandType, Span<byte> span, Span<byte> value)
        {
            switch ((OperandTypes)operandType)
            {
                case OperandTypes.Register:
                    var zeroArray = new byte[Vector<byte>.Count];
                    Vector<byte>.Zero.CopyTo(zeroArray);
                    value.CopyTo(zeroArray);
                    var registerValue = new Vector<byte>(zeroArray);
                    Registers[span[0]] = registerValue;
                    return 1;

                case OperandTypes.Address:
                    var address = span.NonPortableCast<byte, ulong>()[0];
                    this.Memory.SetValueAtAddress(address, value);
                    return sizeof(ulong);

                default:
                    throw new ArgumentOutOfRangeException(nameof(operandType));
            }
        }

        private (byte Size, Span<byte> Value) RetrieveSourceValue(byte dataType, byte operandType, ulong currentOffset)
        {
            var size = FindSizeForType(dataType);
            switch ((OperandTypes)operandType)
            {
                case OperandTypes.Register:
                    var registerSpan = this.Memory.RetrieveAddress(ProgramCounter + currentOffset);
                    var registerAsArray = new byte[Vector<byte>.Count];
                    this.Registers[registerSpan[0]].CopyTo(registerAsArray);
                    return (1, new Span<byte>(registerAsArray).Slice(0, size));

                case OperandTypes.VectorConstant:
                    Span<byte> sliced = this.Memory.RetrieveAddress(this.ProgramCounter + currentOffset);

                    return (size, sliced);

                case OperandTypes.ScalarConstant:
                    Span<byte> scalarSlice = this.Memory.RetrieveAddress(this.ProgramCounter + currentOffset, size);

                    return (size, scalarSlice);

                case OperandTypes.Address:
                    var value = ValueAtAddress(currentOffset, size);
                    return (sizeof(ulong), value);

                default:
                    throw new ArgumentOutOfRangeException(nameof(operandType));
            }
        }

        private Span<byte> ValueAtAddress(ulong operandInstructionOffset, byte size)
        {
            var address = this.Memory.RetrieveAddress(this.ProgramCounter + operandInstructionOffset).NonPortableCast<byte, ulong>()[0];
            var value = this.Memory.RetrieveAddress(address, size);
            return value;
        }

        private byte FindSizeForType(byte dataType)
        {
            switch ((DataType)dataType)
            {
                case DataType.UByte:
                case DataType.Byte:
                    return 1;

                case DataType.UInt16:
                case DataType.Int16:
                    return 2;

                case DataType.UInt32:
                case DataType.Int32:
                case DataType.Float:
                    return 4;

                case DataType.UInt64:
                case DataType.Int64:
                case DataType.Double:
                    return 8;

                case DataType.VectorOfUByte:
                case DataType.VectorOfByte:
                case DataType.VectorOfUInt16:
                case DataType.VectorOfInt16:
                case DataType.VectorOfUInt32:
                case DataType.VectorOfInt32:
                case DataType.VectorOfUInt64:
                case DataType.VectorOfInt64:
                case DataType.VectorOfFloat:
                case DataType.VectorOfDouble:
                    return 32;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType));
            }
        }

        private void Interupt()
        {
            //Load ProgramCounter from interrupt table
            ProgramCounter = this.Memory.RetrieveAddress(2).NonPortableCast<byte, ushort>()[0];
        }

        protected virtual void OnHalt(EventArgs e)
        {
            Halt?.Invoke(this, e);
        }
    }
}