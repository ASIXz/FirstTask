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
            StreamReader input = new StreamReader(inputFile);
            List<SomeDataClass> data = new List<SomeDataClass>();
            SomeDataClass newObject;
            String[] currentData;
            Char[] separator = {' ', ','};
            while(!input.EndOfStream)
            {
                currentData = input.ReadLine().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                newObject = new SomeDataClass();

                newObject.firstName = currentData[0];
                newObject.secondName = currentData[1];
                newObject.car = currentData[2];
                newObject.carNumber = Convert.ToInt32(currentData[3]);
                newObject.oneMoreField = Convert.ToDouble(currentData[4].Replace('.',','));

                data.Add(newObject);
            }
            StreamWriter output = new StreamWriter(outputFile);
            XmlSerializer writer = new XmlSerializer(typeof(List<SomeDataClass>));
            writer.Serialize(output, data);
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
