using System;

using VM.Machine;

namespace Program {
    class Program {
        static void Main(string[] args) {
            VirtualMachine vm = new();

            vm.LoadBytecodeFromFile("test.bc");

            while (vm.Instruction.Active) {
                vm.Update();
            }
        }
    }
}
