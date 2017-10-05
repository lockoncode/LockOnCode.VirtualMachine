using System;
using System.Collections.Generic;
using System.Text;

namespace LockOnCode.VirtualMachine.Devices.CPU
{
    public enum DataType : byte
    {
        UByte = 1,
        Byte,
        UInt16,
        Int16,
        UInt32,
        Int32,
        UInt64,
        Int64,
        Float,
        Double,
        VectorOfUByte,
        VectorOfByte,
        VectorOfUInt16,
        VectorOfInt16,
        VectorOfUInt32,
        VectorOfInt32,
        VectorOfUInt64,
        VectorOfInt64,
        VectorOfFloat,
        VectorOfDouble,
    }
}