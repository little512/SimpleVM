using System;

namespace VM.Components {
    public class InstructionComponent {
        private InterpreterComponent Interpreter;
        public int InstructionPointer { get; set; }
        public bool Active { get; set; }
        public int ErrorCode { get; set; }
        private MemoryComponent memory;

        public InstructionComponent(RegisterComponent registers, StackComponent stack, MemoryComponent memory) {
            Interpreter = new(registers, stack, memory, this);
            Active = true;
            this.memory = memory;
            InstructionPointer = memory.ExecutionStartAddress;
        }

        public void Update() {
            byte Instruction = memory.Read(InstructionPointer);

            InstructionPointer = Interpreter.Execute(Instruction, InstructionPointer);
        }
    }
}
