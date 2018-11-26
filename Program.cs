using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

/*
    C# console application written by Bryce Palmer to read 
    the data sent from the arduino through bluetooth. Used to showcase 
    how a pcb and arduino can be used to map a coordinate plane and 
    activate a tourniquet. Simply shows the values the arduino would interact with.
 */

namespace TourniquetPCB_BT_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] Ports;
                string input;
                Console.WriteLine("Welcome to the Bluetooth COM Port for the Secret Project");
                Console.WriteLine("------------------------------------------------------------------");
                SerialPort serial = new SerialPort();
                Ports = SerialPort.GetPortNames();

                Console.WriteLine("Available Ports");
                Console.WriteLine("------------------------------------------------------------------");

                for (int i = 0; i < Ports.Length; i++)
                {
                    Console.WriteLine(Ports[i]);
                }
                Console.WriteLine("Type which port you want to connect to");
                input = Console.ReadLine();
                Console.WriteLine("Reading Data from {0}........", input);
                Console.WriteLine();

                serial.PortName = input;
                serial.BaudRate = 9600;
                serial.Parity = Parity.None;
                serial.DataBits = 8;
                serial.StopBits = StopBits.One;
                serial.Open();
                serial.Write("R");
                serial.DataReceived += new SerialDataReceivedEventHandler (Serial_DataReceived);


                Console.ReadKey();
                serial.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            
        }

        private static void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string Output = sp.ReadExisting();
            Console.WriteLine(Output);
        }
    }
}
