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
}
