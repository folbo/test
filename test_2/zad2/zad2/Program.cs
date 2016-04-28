using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data file name: ");
            Reader.Filename = Console.ReadLine();
            Reader.ReadFile();
            Reader.Print();
            Console.ReadKey();
        }
    }

    public static class Reader
    {
        public static string Filename = "";
        public static List<Model> data = new List<Model>();


        public static void ReadFile()
        {
            using (FileStream fs = new FileStream(Filename, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (fs.Position != fs.Length)
                {
                    try
                    {
                        byte[] deviceData = new byte[8];
                        deviceData = br.ReadBytes(8);

                        var input = Convert.ToString(deviceData[5], 2).Select(s => s.Equals('1')).ToList();
                        while (input.Count < 8)
                            input.Insert(0, false);

                        var model = new Model()
                        {
                            Type = deviceData[0],
                            SerialNumber = BitConverter.ToString(deviceData, 1, 4).Replace("-", ""),
                            Inputs = input.ToArray(),
                            Checksum = BitConverter.ToInt16(deviceData, 6)
                        };

                        if (deviceData[0] + BitConverter.ToInt32(deviceData, 1) + deviceData[5] != model.Checksum)
                        {
                            //Console.WriteLine("Data error.");
                        }
                        data.Add(model);
                    } catch
                    {
                        Console.WriteLine("Error reading file. Model data is probably corrupted.");
                    }
                }
            }
        }

        public static void Print()
        {
            int i = 1;
            foreach (var model in data)
            {
                Console.WriteLine($"Device {i}");
                Console.WriteLine($"Type: {model.Type}");
                Console.WriteLine($"SerialNumber: {model.SerialNumber}");
                Console.WriteLine($"Inputs: {String.Join(", ", model.Inputs)}");
                Console.WriteLine($"Checksum: {model.Checksum}");
                Console.WriteLine();
                i++;
            }
        }
    }

    public class Model
    {
        public int Type { get; set; }
        public string SerialNumber { get; set; }
        public bool[] Inputs { get; set; }
        public int Checksum { get; set; }
    }
}
