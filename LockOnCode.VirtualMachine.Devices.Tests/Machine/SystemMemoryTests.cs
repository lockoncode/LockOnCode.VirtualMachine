using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using LockOnCode.VirtualMachine.Devices.Machine;
using Shouldly;
using Xunit;

namespace LockOnCode.VirtualMachine.Devices.Tests.Machine
{
    public class SystemMemoryTests
    {
        [Theory]
        [InlineData(256)]
        [InlineData(512)]
        [InlineData(768)]
        [InlineData(1024)]
        [InlineData(1024 * 1024 * 10)]
        public void ShouldBeAbleToCreateASystemMemoryThatIsAMultipleOf32Bytes(ulong ramSize)
        {
            var systemMemory = new SystemMemory(ramSize);
            systemMemory.Size.ShouldBe(ramSize);
        }

        [Theory]
        [InlineData(0)]
        public void ShouldBeAbleToRetrieveA32ByteBlockOfMemoryAsASpan(ulong address)
        {
            var systemMemory = new SystemMemory(512);
            var register = Vector<byte>.One;
            Vector<double>.Count.ShouldBe(4);
            Vector<byte>.Count.ShouldBe(32);
            Vector.IsHardwareAccelerated.ShouldBe(true);
            systemMemory.SetValueAtAddress(0, register);
            var retrievedMemory = systemMemory.RetrieveAddress(address);
            retrievedMemory.ShouldNotBeNull();
            retrievedMemory.Length.ShouldBe(32);
            var newRegister = retrievedMemory.NonPortableCast<byte, Vector<byte>>()[0];
            (newRegister == register).ShouldBeTrue();
            newRegister[31].ShouldBe((byte)1);
        }
    }
}