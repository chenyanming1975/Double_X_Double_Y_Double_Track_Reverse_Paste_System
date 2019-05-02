using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GeneralMachine.PLC
{
    class Delta
    {
       public System.IntPtr hDMTDll; // handle of a loaded DLL

       public delegate void DelegateClose(int conn_num); // function pointer for disconnection

        // About .Net P/Invoke:

        // [DllImport("XXX.dll", CharSet = CharSet.Auto)] 
        // static extern int ABC(int a , int b);

        // indicates that "ABC" function is imported from XXX.dll
        // XXX.dll exports a function of the same name with "ABC"
        // the return type and the parameter's data type of "ABC" 
        // must be identical with the function exported from XXX.dll
        // and the CharSet = CharSet.Auto causes the CLR 
        // to use the appropriate character set based on the host OS   
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
       public static extern IntPtr LoadLibrary(string dllPath);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FreeLibrary(IntPtr hDll);

        // Data Access
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int RequestData(int comm_type, int conn_num, int slave_addr, int func_code, byte[] sendbuf, int sendlen);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ResponseData(int comm_type, int conn_num, ref int slave_addr, ref int func_code, byte[] recvbuf);

        // Serial Communication
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int OpenModbusSerial(int conn_num, int baud_rate, int data_len, char parity, int stop_bits, int modbus_mode);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern void CloseSerial(int conn_num);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int GetLastSerialErr();
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern void ResetSerialErr();

        // Socket Communication
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int OpenModbusTCPSocket(int conn_num, int ipaddr, int port);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern void CloseSocket(int conn_num);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int GetLastSocketErr();
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern void ResetSocketErr();
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadSelect(int conn_num, int millisecs);

        // MODBUS Address Calculation
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int DevToAddrW(string series, string device, int qty);

        // Wrapped MODBUS Funcion : 0x01
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadCoilsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_r, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x02
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadInputsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_r, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x03
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadHoldRegsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_r, StringBuilder req, StringBuilder res);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadHoldRegs32W(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_r, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x04
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int ReadInputRegsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_r, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x05		   
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteSingleCoilW(int comm_type, int conn_num, int slave_addr, int dev_addr, UInt32 data_w, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x06
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteSingleRegW(int comm_type, int conn_num, int slave_addr, int dev_addr, UInt32 data_w, StringBuilder req, StringBuilder res);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteSingleReg32W(int comm_type, int conn_num, int slave_addr, int dev_addr, UInt32 data_w, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x0F
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteMultiCoilsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_w, StringBuilder req, StringBuilder res);

        // Wrapped MODBUS Funcion : 0x10
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteMultiRegsW(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_w, StringBuilder req, StringBuilder res);
        [DllImport("DMT.dll", CharSet = CharSet.Auto)]
        public static extern int WriteMultiRegs32W(int comm_type, int conn_num, int slave_addr, int dev_addr, int qty, UInt32[] data_w, StringBuilder req, StringBuilder res);
         
    }
}
