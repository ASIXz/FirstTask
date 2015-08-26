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
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR\n"+message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void serializeAll(string inputFile, string outputFile)
        {
            StreamReader input;
            try
            {
                input = new StreamReader(inputFile);
            }
            catch(ArgumentException)
            {
                Error("Uncorect path");
                return;
            }
            catch(FileNotFoundException)
            {
                Error("Input file not found");
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
                        Error("Uncorrect Data");
                    }
                    data.Add(newObject);
                }
                else
                {
                    Error("Uncorrect input data!");
                }
            }
            StreamWriter output;
            try
            {
                output = new StreamWriter(outputFile);
            }
            catch (ArgumentException)
            {
                Error("Uncorect output path");
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
                Error("Serialization error!");
            }
            input.Close();
            output.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Convertation XML to TXT!!!");
            Console.WriteLine("input:\n\t'path'\t-\tTo change in-out files\n\t'exit'\t-\tTo exit\n\t other\t-\tTo continue whith default settings");
            string userChoise = Console.ReadLine();
            switch (userChoise)
            {
                case "exit":
                    return;
                case "path":
                    Console.WriteLine("input please:\n<Input path>\n<Output path>");
                    SomeTxtToXMLSerializer.serializeAll(Console.ReadLine(), Console.ReadLine());
                    break;
                default:
                    SomeTxtToXMLSerializer.serializeAll("../../file.txt", "file.xml");
                    break;
            }
            Console.WriteLine("Work is done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
