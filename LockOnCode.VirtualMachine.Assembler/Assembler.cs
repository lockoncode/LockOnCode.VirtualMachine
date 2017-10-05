using System.Collections.Generic;
using LockOnCode.VirtualMachine.Assembler.Directives;
using LockOnCode.VirtualMachine.Assembler.Operations.NoOperands;
using LockOnCode.VirtualMachine.Devices.CPU;

namespace LockOnCode.VirtualMachine.Assembler
{
    public class Assembler
    {
        public byte[] Assemble(System.Collections.Generic.List<IOperation> list)
        {
            var program = new List<byte>();
            list.ForEach(operation =>
            {
                switch (operation)
                {
                    case Operation cpuOperation:
                        program.AddRange(cpuOperation.AsBytes());
                        break;

                    case AssemblerDirective directive:
                        HandleDirective(directive, program);
                        break;
                }
            });

            return program.ToArray();
        }

        private void HandleDirective(AssemblerDirective directive, List<byte> program)
        {
            var type = directive.GetType();
            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(DeclareDataDirective<>))
            {
                program.AddRange(directive.AsBytes());
            }
        }

        public static DeclareDataDirective<DataType> DeclareData<DataType>(DataType data) => new DeclareDataDirective<DataType>(data);

        public static DeclareSpaceDirective<DataType> DeclareSPace<DataType>(ulong size) =>
            new DeclareSpaceDirective<DataType>(size);
    }
}