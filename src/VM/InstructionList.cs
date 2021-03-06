using System;
using System.Collections.Generic;

using VM.Components;

namespace VM.Instruction {
    public static class Instructions {
        public delegate int Instruction(int ptr, RegisterComponent r, StackComponent s, MemoryComponent m, InstructionComponent i);

        public static Dictionary<int, (string, Instruction)> InstructionList = new() {
            [0x0] = ("NOP", (ptr, r, s, m, i) =>
                ++ptr
            ),

            [0x1] = ("PUSH", (ptr, r, s, m, i) => {
                s.Push(r.GetRegisterByAddress(m.Read(ptr + 1)));
                return ptr + 2;
            }),

            [0x2] = ("POP", (ptr, r, s, m, i) => {
                r.SetRegisterByAddress(m.Read(ptr + 1), s.Pop());
                return ptr + 2;
            }),

            [0x3] = ("MOV", (ptr, r, s, m, i) => { // TODO: read size of register
                r.SetRegisterByAddress(m.Read(ptr + 1), m.Read(ptr + 2));
                return ptr + 3;
            }),

            [0x4] = ("INC", (ptr, r, s, m, i) => {
                int reg = m.Read(ptr + 1);
                r.SetRegisterByAddress(reg, r.GetRegisterByAddress(reg) + 1);
                return ptr + 2;
            }),

            [0x5] = ("DEC", (ptr, r, s, m, i) => {
                int reg = m.Read(ptr + 1);
                r.SetRegisterByAddress(reg, r.GetRegisterByAddress(reg) - 1);
                return ptr + 2;
            }),

            [0xFF] = ("EXIT", (ptr, r, s, m, i) => {
                i.ErrorCode = m.Read(ptr + 1);
                i.Active = false;
                return ptr + 2;
            })
        };
    }
}
