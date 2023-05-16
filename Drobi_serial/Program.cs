using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//Создайте программу для работы с массивом дробей (числитель и знаменатель). Она должна иметь такую функциональность:
//1.Ввод массива дробей с клавиатуры
//2. Сериализация массива дробей
//3. Сохранение сериализованного массива в файл
//4. Загрузка сериализованного массива из файла. После загрузки нужно произвести десериализацию
//Выбор конкретного формата сериализации необходимо сделать вам. Обращаем ваше внимание, что выбор должен быть обоснованным.
namespace Drobi_serial
{
    public class Drob
    {
        public int num { get; set; }
        public int denum { get; set; }

        public Drob(int n, int d)
        {
            num = n;
            denum = d;          
        }
        public Drob() { }

        public override string ToString()
        {
            return $"{num}/{denum}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Сколько всего дробей? : ");
            int size = int.Parse(Console.ReadLine());
            Drob[] drobi = new Drob[size];
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Введите дробь {i + 1} в формате 'числитель/знаменатель': ");
                string input = Console.ReadLine();
                string[] parts = input.Split('/');
                int n = int.Parse(parts[0]);
                int d = int.Parse(parts[1]);

                drobi[i] = new Drob(n, d);
            }
            Console.WriteLine("Введенный массив дробей:");

            foreach (Drob d in drobi)
            {
                Console.WriteLine(d);
            }
            //записываем данные в файл
            using (StreamWriter writer = new StreamWriter("drobi.txt"))
            {
                foreach (Drob d in drobi)
                    writer.WriteLine(d);
            }
            Console.WriteLine( "Данные загружены в файл" );

            //сериализация 
            FileStream stream = new FileStream("drob.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(Drob[]));
            serializer.Serialize(stream, drobi);
            stream.Close();
            Console.WriteLine("Сериализация успешно выполнена!");

            stream = new FileStream("drob.xml", FileMode.Open);
            serializer = new XmlSerializer(typeof(Drob[]));
            drobi = (Drob[])serializer.Deserialize(stream);
            foreach (Drob d in drobi)
            {
                Console.WriteLine(d.num+"/"+d.denum);
            }
            Console.WriteLine("Десериализация успешно выполнена!");
            stream.Close();

        }
    }
}