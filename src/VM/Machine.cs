using System;
using System.IO;
using VM.Components;

namespace VM.Machine {
    public class VirtualMachine {
        public RegisterComponent Registers { get; init; }
        public StackComponent Stack { get; init; }
        public MemoryComponent Memory { get; init; }
        public InstructionComponent Instruction { get; init; }

        public VirtualMachine(int memorySize) { // TODO: make components optional
            Registers = new();
            Stack = new();
            Memory = new(memorySize);
            Instruction = new(Registers, Stack, Memory);
        }

        public void Update() {
            Instruction.Update();
        }

        public bool LoadBytecode(ReadOnlySpan<byte> bytecode) {
            if (bytecode.Length > Memory.Size - Memory.ExecutionStartAddress)
                throw new IndexOutOfRangeException("Bytecode too long for given Execution Start Address, try allocating more memory.");

            for (int i = Memory.ExecutionStartAddress; i < bytecode.Length; i++) {
                Memory.Write(i, bytecode[i]);
            }

            return true;
        }

        public bool LoadBytecodeFromFile(string filepath) {
            ReadOnlySpan<byte> bytecode = File.ReadAllBytes(filepath);

            return LoadBytecode(bytecode);
        }
    }
}
