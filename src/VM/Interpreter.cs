using System;

using VM.Instruction;

namespace VM.Components {
    using Inst = Instructions.Instruction;

    internal class InterpreterComponent {
        RegisterComponent Registers;
        StackComponent Stack;
        MemoryComponent Memory;

        public InterpreterComponent(
            RegisterComponent registers,
            StackComponent stack,
            MemoryComponent memory)
        {
            Registers = registers;
            Stack = stack;
            Memory = memory;
        }

        public int Execute(int instruction, int pointer) {
            (string name, Inst inst) = Instructions.InstructionList[instruction];

            int newPointer = inst(pointer, Registers, Stack, Memory);
#if DEBUG
            Console.WriteLine($"INSTRUCTION: {name}\nPROGRAM COUNTER: {pointer}");
            // Console.WriteLine($"A: {Registers.A}\nX: {Registers.X}\nY: {Registers.Y}");
#endif
            return newPointer;
        }
    }
}
