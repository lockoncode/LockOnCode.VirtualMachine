using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace LockOnCode.VirtualMachine.Devices.Tests.Machine
{
    public class MachineTests
    {
        [Fact]
        public void TheMachineDoesNothingWhenStopped()
        {
            var machine = BuildTestMachine();

            machine.Tick();
            machine.Ticks.ShouldBe(0);
        }

        [Fact]
        public void WhenIStartTheMachine_WhenIPerformATick_TheTickCounterIsIncremented()
        {
            var machine = BuildTestMachine();
            machine.Start();
            machine.Tick();
            machine.Ticks.ShouldBe(1);
        }

        private static Devices.Machine.Machine BuildTestMachine()
        {
            return new Devices.Machine.Machine(null);
        }
    }
}