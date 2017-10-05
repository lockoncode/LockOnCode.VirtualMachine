using System;
using System.Collections.Generic;
using System.Text;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Devices.Machine
{
    public class Machine
    {
        private Cpu cpu;

        public Machine(Cpu cpu)
        {
            this.cpu = cpu;
        }

        public void Tick()
        {
            if (MachineState == MachineState.Started)
            {
                ++this.Ticks;
            }
        }

        private MachineState MachineState { get; set; }

        public long Ticks { get; private set; }

        public void Start()
        {
            MachineState = MachineState.Started;
        }
    }

    internal enum MachineState
    {
        Stopped,
        Started
    }
}