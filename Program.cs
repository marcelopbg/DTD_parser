using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace DTDValidator
{
    class Program
    {
        private static bool DocumentHasErrors = false;

        private static List<string> errorMessages = new List<string>();]

        public static void Main()
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.XmlResolver = new XmlUrlResolver();
                settings.ValidationType = ValidationType.DTD;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                XmlReader reader = XmlReader.Create("./assets/vw.xml", settings);

                while (reader.Read()) ;
            }
            catch (Exception e)
            {
                errorMessages.Add(e.Message);
                DocumentHasErrors = true;
            }
            finally
            {
                if (DocumentHasErrors)
                {
                    Console.WriteLine("XML Document is Badly Formatted");
                    Console.WriteLine("Errors: \n");
                    foreach (var item in errorMessages)
                    {
                        Console.WriteLine($"{item} \n");
                    }
                }
                else
                {
                    Console.WriteLine("XML Document is Well Formatted");
                }
                Console.ReadKey();
            }
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            errorMessages.Add(e.Message);
            DocumentHasErrors = true;
        }
    }
}
