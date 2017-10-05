using System.Collections.Generic;
using LockOnCode.VirtualMachine.Devices.CPU;
using Shouldly;
using Xunit;
using System.Linq;
using System.Numerics;
using LockOnCode.VirtualMachine.Devices.Machine;
using System;
using System.Collections;
using LockOnCode.VirtualMachine.Assembler;
using LockOnCode.VirtualMachine.Assembler.Operands;

namespace LockOnCode.VirtualMachine.Devices.Tests.CPU
{
    public class CpuTests
    {
        [Fact]
        public void WhenICreateACpuIPassItMachineCode()
        {
            var cpu = BuildCpu(new List<IOperation>());
            cpu.ShouldNotBeNull();
        }

        [Fact]
        public void WhenTheCpuExecutesAHaltInstruction_ThenTheStopMessageIsSentToTheMachine()
        {
            var source = new List<IOperation> { OpCodes.Halt };
            var cpu = BuildCpu(source);
            var halted = false;
            cpu.Halt += (s, d) => halted = true;
            cpu.Tick();
            halted.ShouldBeTrue();
        }

        [Fact]
        public void WhenTheCpuExecutesAnInstruction_TheProgramCounterPointsToTheNextInstruction()
        {
            var source = new List<IOperation> { OpCodes.NOP, OpCodes.NOP, OpCodes.Halt };
            var cpu = BuildCpu(source);
            RunProgram(cpu, source);
            cpu.PC.ShouldBe(2UL * ((ulong)source.Count));
        }

        private static void RunProgram(Cpu cpu, ICollection program)
        {
            Enumerable.Range(0, program.Count).ToList().ForEach(x => cpu.Tick());
        }

        [Fact]
        public void WhenTheCpuExecutesAnUnRecognisedInstruction_ThenACPUExceptinIsRaised_AndControlTransferedToTheSpecifiedLocationInTheInteruptTable()
        {
            var source = new List<IOperation> { Assembler.Assembler.DeclareData(10UL), OpCodes.Halt };
            var cpu = BuildCpu(source, 2);
            var halted = false;
            cpu.Halt += (s, d) => halted = true;
            cpu.Tick();
            cpu.Tick();
            cpu.PC.ShouldBe(12UL);

            halted.ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToStoreAConstantValueInARegister()
        {
            var source = new List<IOperation> { OpCodes.Move(DataType.VectorOfByte, new VectorConstantOperand<int>(Vector<int>.One), new RegisterOperand(1)) };
            var cpu = BuildCpu(source);
            cpu.Tick();
            Vector.AsVectorInt32(cpu.Registers[1]).ShouldBe(Vector<int>.One);
            cpu.PC.ShouldBe(37UL);
        }

        [Fact]
        public void ShouldBeAbleToMoveAPieceOfDataFromOneMemoryLocationToAnother()
        {
            var source = new List<IOperation>
            {
                OpCodes.Move(DataType.VectorOfByte, new VectorConstantOperand<int>(Vector<int>.One), new AddressOperand(100)),
                OpCodes.Move(DataType.VectorOfByte, new AddressOperand(100), new AddressOperand(150)),
                OpCodes.Move(DataType.VectorOfByte, new AddressOperand(150), new RegisterOperand(1))
            };

            var cpu = BuildCpu(source);
            RunProgram(cpu, source);

            Vector.AsVectorInt32(cpu.Registers[1]).ShouldBe(Vector<int>.One);
        }

        [Fact]
        public void ShouldBeAbleToMoveAScalarPieceOfDataFromOneMemoryLocationToAnother()
        {
            var source = new List<IOperation>
            {
                OpCodes.Move(DataType.UByte, new ScalarConstantOperand<byte>(1), new AddressOperand(190)),
                OpCodes.Move(DataType.Int32, new ScalarConstantOperand<int>(1000), new AddressOperand(192)),
                OpCodes.Move(DataType.UByte, new AddressOperand(190), new RegisterOperand(1)),
                OpCodes.Move(DataType.Int32, new AddressOperand(192), new RegisterOperand(2)),
                OpCodes.Move(DataType.Int32, new RegisterOperand(2), new AddressOperand(196))
            };

            var cpu = BuildCpu(source);
            RunProgram(cpu, source);

            cpu.Registers[1][0].ShouldBe((byte)1);
            Vector.AsVectorInt32(cpu.Registers[2])[0].ShouldBe(1000);
            cpu.Memory.RetrieveAddress(196, 4).NonPortableCast<byte, int>()[0].ShouldBe(1000);
        }

        private static Cpu BuildCpu(List<IOperation> source, ulong programBase = 0)
        {
            var memory = new SystemMemory(200L);

            var machineCode = new Assembler.Assembler().Assemble(source);
            memory.WriteBlock(machineCode, programBase);
            var cpu = new Cpu(memory);
            return cpu;
        }
    }
}