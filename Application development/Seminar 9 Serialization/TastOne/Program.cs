using System.Xml.Linq;
using System.Xml.Serialization;

namespace TastOne
{
    internal class Program
    {
        // Задача №1:
        // Напишите класс, который можно десериализовать из XML формат.
        // Задача №2:
        // Сериализовать получившийся объект.
        static void Main(string[] args)
        {
            var xml =
                """
                <?xml version="1.0" encoding="UTF-8"?>
                <Data.Root xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                    <Data.Root.Names>
                        <Name>Name1</Name>
                        <Name>Name2</Name>
                        <Name>Name3</Name>
                    </Data.Root.Names>
                    <Data.Entry LinkedEntry="Name1">
                        <Data.Name>Name2</Data.Name>
                    </Data.Entry>
                    <Data_x0023_ExtendedEntry LinkedEntry="Name2">
                        <Data.Name>Name1</Data.Name>
                        <Data_x0023_Extended>NameOne</Data_x0023_Extended>
                    </Data_x0023_ExtendedEntry>
                </Data.Root>
                """;
            DataRoot root = new DataRoot();
            root.Names = new string[] { "Name1", "Name2", "Name3" };

            root.Entry = new DataEntry[2]; 
            root.Entry[0] = new DataEntry();
            root.Entry[0].LinkedEntry = "Name1";
            root.Entry[0].Name = "Name2";

            root.Entry[1] = new DataX { LinkedEntry = "Name2", Name = "Name1", DataEx = "NameOne"};
            
            // Вторая задача.
            var serialaze = new XmlSerializer(typeof(DataRoot));
            serialaze.Serialize(Console.Out, root); // Console.Out выводит сериализацию сразу на консоль.
        }
    }

    [XmlRoot("Data.Root")]
    public class DataRoot
    {
        [XmlArray("Data.Root.Names")]
        [XmlArrayItem("Name")]
        public string[] Names;

        [XmlElement(typeof(DataEntry))]
        [XmlElement(typeof(DataX))]
        public DataEntry[] Entry;
    }

    [XmlType("Data.Entry")]
    public class DataEntry
    {
        [XmlAttribute]
        public string LinkedEntry;

        [XmlElement("Data.Name")]
        public string Name;
    }

    [XmlType("Data#ExtendedEntry")] // # - имеет код x0023, по этому вывод соответсвующий
    public class DataX : DataEntry
    {
        [XmlElement("Data#Extended")]
        public string DataEx;
    }
}
