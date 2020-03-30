using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;


namespace TestProgram1
{
    class Program
    {
        static void Main()
        {
            //Функция из пакета System.Text.Encoding.CodePages 4.7.0 для решения проблем с кодировкой кириллицы
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); 
            try
            {
                //Получение XML-файла с указанного адреса:
                string Request = "http://www.cbr.ru/scripts/XML_daily.asp";

                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Request);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(myStreamReader);
                myStreamReader.Close();

                //Создание вспомогательных переменных
                string Currency = "Гонконгский доллар";
                double HKDValue = 0;
                bool IfFound = false;

                //Цикл по узлам xmlDocument, поиск узла, содержащего строку Currency
                XmlElement xRoot = xmlDocument.DocumentElement;
                foreach (XmlNode xnode in xRoot)
                {
                    if (xnode.SelectSingleNode("Name").InnerText == Currency) {
                        //Конвертация найденного значения курса валюты в double и вывод его на экран
                        HKDValue = Convert.ToDouble(xnode.SelectSingleNode("Value").InnerText);
                        Console.WriteLine("Курс гонконгского доллара к рублю составляет " + HKDValue);
                        IfFound = true;
                        }
                        if (IfFound == true) break;                 
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

