using System;

using VM.Instruction;

namespace VM.Components {
    using Inst = Instructions.Instruction;

    internal class InterpreterComponent {
        RegisterComponent Registers;
        StackComponent Stack;
        MemoryComponent Memory;
        InstructionComponent Instruction;

        public InterpreterComponent(
            RegisterComponent registers,
            StackComponent stack,
            MemoryComponent memory,
            InstructionComponent instruction)
        {
            Registers = registers;
            Stack = stack;
            Memory = memory;
            Instruction = instruction;
        }

        public int Execute(int instruction, int pointer) {
            (string name, Inst inst) = Instructions.InstructionList[instruction];

            int newPointer = inst(pointer, Registers, Stack, Memory, Instruction);
#if DEBUG
            Console.WriteLine($"INSTRUCTION: {name}\nPROGRAM COUNTER: {pointer}");
            foreach (var reg in Registers.RegisterList){
                Console.WriteLine($"{reg.Key.Item2}: {reg.Value}");
            }
#endif
            return newPointer;
        }
    }
}
