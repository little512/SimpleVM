using System;
using System.IO;
using VM.Components;

namespace VM.Machine {
    public class VirtualMachine {
        public RegisterComponent Registers { get; init; }
        public StackComponent Stack { get; init; }
        public MemoryComponent Memory { get; init; }
        public InstructionComponent Instruction { get; init; }

        public VirtualMachine() {
            Registers = new();
            Stack = new();
            Memory = new();
            Instruction = new(Registers, Stack, Memory);
        }

        public void Update() {
            Instruction.Update();
        }

        public void LoadBytecode(ReadOnlySpan<byte> bytecode) {
            for (int i = 0; i < bytecode.Length; i++) {
                Memory.Write(i, bytecode[i]);
            }
        }

        public void LoadBytecodeFromFile(string filepath) {
            ReadOnlySpan<byte> bytecode = File.ReadAllBytes(filepath);

            LoadBytecode(bytecode);
        }
    }
}
