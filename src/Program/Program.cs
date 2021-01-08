using System;

using VM.Machine;

namespace Program {
    class Program {
        static void Main(string[] args) {
            VirtualMachine vm = new();

            vm.Registers.AddRegister(0x00, "A");
            vm.Registers.AddRegister(0x01, "X");
            vm.Registers.AddRegister(0x02, "Y");

            vm.LoadBytecodeFromFile("test.bc");

            while (vm.Instruction.Active) {
                vm.Update();
            }
        }
    }
}
