using System;

namespace LockOnCode.VirtualMachine.Assembler.Directives
{
    public class DeclareDataDirective<DataType> : AssemblerDirective
    {
        public DataType Data { get; }

        public DeclareDataDirective(DataType data)
        {
            this.Data = data;
        }

        public override byte[] AsBytes()
        {
            return BitConverter.GetBytes((dynamic)this.Data);
        }
    }
}