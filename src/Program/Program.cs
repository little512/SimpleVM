using System;

using VM.Machine;
using VM.Instruction;

namespace Program {
    class Program {
        static void Main(string[] args) {
            VirtualMachine vm = new();

            vm.Registers.AddRegister(0x00, "A"); // add a register named A with an ID of 0x00
            vm.Registers.AddRegister(0x01, "X");
            vm.Registers.AddRegister(0x02, "Y");

            Instructions.InstructionList.Add(
                0x06,
                ("TEST", (ptr, r, s, m, i) => {
                    return ++ptr; // skips to next byte for evaluation
                })
            );

            vm.LoadBytecodeFromFile("test.bc"); // load binary file with bytecode

            while (vm.Instruction.Active) {
                vm.Update(); // execute one instruction and update components accordingly
            }

            Console.WriteLine($"The VM exited with code { vm.Instruction.ErrorCode }");
        }
    }
}
