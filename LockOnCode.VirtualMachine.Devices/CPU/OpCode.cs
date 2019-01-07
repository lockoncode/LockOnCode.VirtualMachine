using System;
using System.Collections.Generic;
using System.Text;

namespace LockOnCode.VirtualMachine.Devices.CPU
{
    public enum OpCode : Int16
    {
        Bad = 0, //Catch errors around executing uninitialized memory
        Halt = 1, // Signal the rest of the machine to shutdown
        NOP = 2, // perform a null operation. Only increments ProgramCounter register
        Move = 3,
        Add = 4
    }
}