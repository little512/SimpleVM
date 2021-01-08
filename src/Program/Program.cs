using System;
using System.IO;

using VM.Machine;
using VM.Instruction;

namespace Program {
    class Program {
        static int Main(string[] args) {
            if (args.Length > 0) {
                VirtualMachine vm = new(8); // initialize vm with 64 bytes of memory
                bool Loaded = false;

                vm.Registers.AddRegister(0x00, "A"); // add a register named A with an ID of 0x00
                vm.Registers.AddRegister(0x01, "X");
                vm.Registers.AddRegister(0x02, "Y");

                Instructions.InstructionList.Add(
                    0x06, // the opcode of the instruction
                    ("TEST", (ptr, r, s, m, i) => { // instructions take a reference to the virtual machine components
                        // instructions return the pointer to where the program should continue from
                        return ++ptr; // skips to next byte for evaluation
                    })
                );

                try {
                    Loaded = vm.LoadBytecodeFromFile(args[0]); // load binary file with bytecode
                } catch(FileNotFoundException e) {
                    Console.WriteLine($"{e.Message}");
                } catch (Exception e) {
                    Console.WriteLine($"{e}");
                } finally {
                    if (Loaded) {
                        while (vm.Instruction.Active) {
                            vm.Update(); // execute one instruction and update components accordingly
                        }

                        Console.WriteLine($"The VM exited with code { vm.Instruction.ErrorCode }");
                    }
                }

                return vm.Instruction.ErrorCode;
            } else {
                Console.WriteLine("usage: vm [file]");
            }

            return 0;
        }
    }
}
