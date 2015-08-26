using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace FirstTask
{
    public class SomeDataClass
    {
        public string firstName;
        public string secondName;
        public string car;
        public int carNumber;
        public double oneMoreField;
        public SomeDataClass() { }
    }
    public static class SomeTxtToXMLSerializer
    {
        public static void serializeAll(string inputFile, string outputFile)
        {
            StreamReader input;
            try
            {
                input = new StreamReader(inputFile);
            }
            catch(ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR\nUncorect path");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            catch(FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR\nInput file not found");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            List<SomeDataClass> data = new List<SomeDataClass>();
            SomeDataClass newObject;
            String[] currentData;
            Char[] separator = {' ', ','};
            while(!input.EndOfStream)
            {
                currentData = input.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (currentData.Length == 5)
                {
                    newObject = new SomeDataClass();

                    newObject.firstName = currentData[0];
                    newObject.secondName = currentData[1];
                    newObject.car = currentData[2];
                    try
                    {
                        newObject.carNumber = Convert.ToInt32(currentData[3]);
                        newObject.oneMoreField = Convert.ToDouble(currentData[4].Replace('.', ','));
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR\nUncorrect Data");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    data.Add(newObject);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR\nUncorrect input data!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            StreamWriter output;
            try
            {
                output = new StreamWriter(outputFile);
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR\nUncorect output path");
                Console.ForegroundColor = ConsoleColor.White;
                input.Close();
                return;
            }
            XmlSerializer writer = new XmlSerializer(typeof(List<SomeDataClass>));
            try
            {
                writer.Serialize(output, data);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR\nSerialization error!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            input.Close();
            output.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SomeTxtToXMLSerializer.serializeAll("../../file.txt", "file.xml");
        }
    }
}
