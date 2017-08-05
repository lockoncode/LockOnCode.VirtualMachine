using LockOnCode.VirtualMachine.Devices.CPU;
using Shouldly;
using Xunit;

namespace LockOnCode.VirtualMachine.Devices.Tests.CPU
{
    public class CpuTests
    {
        [Fact]
        public void WhenICreateACpuIPassItMachineCode()
        {
            var machineCode = new byte[0];
            var cpu = new Cpu(machineCode);
            cpu.ShouldNotBeNull();
        }
    }
}