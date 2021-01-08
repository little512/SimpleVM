using System;
using System.Collections.Generic;

namespace VM.Components {
    public class RegisterComponent {
        public Dictionary<(int, string), int> RegisterList { get; set; }

        public bool AddRegister(int addr, string name) {
            RegisterList.Add((addr, name), 0);
            return true;
        }

        public bool RemoveRegister(int addr, string name) {
            return RegisterList.Remove((addr, name));
        }

        public void SetRegisterByAddress(int addr, int value) {
            foreach (var reg in RegisterList) {
                if (reg.Key.Item1 == addr) {
                    RegisterList[reg.Key] = value;
                    return;
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public int GetRegisterByAddress(int addr) {
            foreach (var reg in RegisterList) {
                if (reg.Key.Item1 == addr) {
                    return RegisterList[reg.Key];
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public int GetRegisterAddressByName(string name) {
            foreach (var reg in RegisterList) {
                if (reg.Key.Item2 == name) {
                    return RegisterList[reg.Key];
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public RegisterComponent() {
            RegisterList = new();
        }
    }

    public class StackComponent {
        public int Size { get; set; }
        Stack<int> Stack { get; set; }

        public const int MaxSize = 255;

        public StackComponent(int maxSize = MaxSize) {
            Size = 0;
            Stack = new(maxSize);
        }

        public void Push(int element) {
            if (Size < MaxSize) {
                Size++;
                Stack.Push(element);
            } else
                throw new IndexOutOfRangeException("Stack overflow");
        }

        public int Pop() {
            if (Size > 0) {
                Size--; 
                return Stack.Pop();
            } else
                throw new IndexOutOfRangeException("Stack underflow");
        }
    }

    public class MemoryComponent {
        byte[] memory { get; set; }
        public int Size { get; init; }
        public int ExecutionStartAddress { get; set; }

        public MemoryComponent(int size, int startAddress = 0) {
            if (startAddress > size)
                throw new IndexOutOfRangeException("The Execution Start Address may not be larger than the Memory Size.");

            memory = new byte[size];
            Size = size;
            ExecutionStartAddress = startAddress;
        }

        public bool Write(int index, byte value) {
            memory[index] = value;
            return true;
        }

        public byte Read(int index) {
            return memory[index];
        }
    }

    public class InstructionComponent {
        private InterpreterComponent Interpreter;
        public int InstructionPointer { get; set; }
        public bool Active { get; set; }
        public int ErrorCode { get; set; }
        private MemoryComponent _memory;

        public InstructionComponent(RegisterComponent registers, StackComponent stack, MemoryComponent memory) {
            Interpreter = new(registers, stack, memory, this);
            Active = true;
            _memory = memory;
            InstructionPointer = _memory.ExecutionStartAddress;
        }

        public void Update() {
            byte Instruction = _memory.Read(InstructionPointer);

            InstructionPointer = Interpreter.Execute(Instruction, InstructionPointer);
        }
    }
}
