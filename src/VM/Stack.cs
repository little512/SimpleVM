using System;
using System.Collections.Generic;

namespace VM.Components {
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
}
