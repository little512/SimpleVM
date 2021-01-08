using System;

namespace VM.Components {
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
}
