using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace LockOnCode.VirtualMachine.Devices.IntegrationTests
{
    public class Test1
    {
        [Fact]
        public void TestSomeThing()
        {
            var result = true;
            result.ShouldBe(true);
        }
    }
}