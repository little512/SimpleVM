using System;
using System.Reflection;
using System.Collections.Generic;

namespace VM.Components {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class AddressAttribute : Attribute
    {
        public int Address { get; init; }
        
        public AddressAttribute(int address) {
            Address = address;
        }
    }

    public class RegisterComponent {
        [Address(0x00)]
        public int A { get; set; }

        [Address(0x01)]
        public int X { get; set; }

        [Address(0x02)]
        public int Y { get; set; }

        public void SetRegisterByAddress(int addr, int value) {
            var props = typeof(RegisterComponent).GetProperties();

            foreach (PropertyInfo prop in props) {
                AddressAttribute attr = prop.GetCustomAttribute<AddressAttribute>();

                if (attr is not null) {
                    if (attr.Address == addr) {
                        prop.SetValue(this, value, null);
                        return;
                    }
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public int GetRegisterByAddress(int addr) {
            var props = typeof(RegisterComponent).GetProperties();

            foreach (PropertyInfo prop in props) {
                AddressAttribute attr = prop.GetCustomAttribute<AddressAttribute>();

                if (attr is not null) {
                    if (attr.Address == addr) {
                        return (int)prop.GetValue(this);
                    }
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public int GetRegisterAddressByName(string name) {
            var props = typeof(RegisterComponent).GetProperties();

            foreach (PropertyInfo prop in props) {
                AddressAttribute attr = prop.GetCustomAttribute<AddressAttribute>();

                if (attr is not null) {
                    if (prop.Name == name) {
                        return attr.Address;
                    }
                }
            }

            throw new IndexOutOfRangeException("Invalid register address");
        }

        public RegisterComponent() {
            A = 0;
            X = 0;
            Y = 0;
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
        public byte[] Memory { get; set; }

        public MemoryComponent() {
            Memory = new byte[1000];
        }

        public bool Write(int index, byte value) {
            Memory[index] = value;
            return true;
        }

        public byte Read(int index) {
            return Memory[index];
        }
    }

    public class InstructionComponent {
        private InterpreterComponent Interpreter;
        public int InstructionPointer { get; set; }
        public bool Active { get; private set; }
        private MemoryComponent _memory;

        public InstructionComponent(RegisterComponent registers, StackComponent stack, MemoryComponent memory) {
            Interpreter = new(registers, stack, memory);
            Active = true;
            _memory = memory;
        }

        public void Update() {
            byte Instruction = _memory.Read(InstructionPointer);

            InstructionPointer = Interpreter.Execute(Instruction, InstructionPointer);
        }
    }
}
