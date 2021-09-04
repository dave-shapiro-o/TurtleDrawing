using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace TurtleDrawing
{
    class Serialiser
    {
        static List<string> patterns = new();

        public static List<string> DeserializePatterns()
        {
            try
            {
                if (File.Exists("patterns.xml"))
                {
                    XmlSerializer serializer = new(patterns.GetType());
                    using StreamReader streamReader = new("patterns.xml");
                    patterns = (List<string>)serializer.Deserialize(streamReader);
                }
                else throw new FileNotFoundException("File not found");
            }
            catch (Exception ex)
            {
                Controller.ErrorOccurred(ex);
            }
            return patterns;
        }
        public static void SerializePatterns()
        {
            try
            {
                XmlSerializer serializer = new(patterns.GetType()); // or(typeof(List<string>))
                using StreamWriter streamWriter = new("patterns.xml");
                serializer.Serialize(streamWriter, patterns);
            }
            catch (Exception ex)
            {
                Controller.ErrorOccurred(ex);
            }
        }
    }
}
