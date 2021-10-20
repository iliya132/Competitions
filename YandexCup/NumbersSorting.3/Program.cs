using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
/*
 * Разработчики бэкенда часто взаимодействуют с многочисленными API и дополнительно обрабатывают результаты. Сейчас вам придется сделать именно это!
Во входном файле четыре строчки. В первой находится адрес сервера, во второй — номер порта. В следующих двух строках записаны два целых 32-разрядных числа: 
a и b
. Необходимо осуществить GET-запрос к серверу по указанному номеру порта, передав значения чисел 
a и b
 в значениях одноименных CGI-параметров. Сервер ответит JSON-массивом из целых чисел. Необходимо отсортировать числа в порядке невозрастания и распечатать в выходной файл положительные — по одному числу в строке.
Гарантируется, что общее количество чисел в ответе не превосходит 100, при этом каждое из них — 32-разрядное знаковое целое число.
Это разминочная задача, к которой мы размещаем готовое решения, чтобы вы могли познакомиться с нашей автоматической системой проверки решений. Ввод и вывод осуществляется через файлы, либо через стандартные потоки ввода-вывода, как вам удобнее.
*/
namespace NumbersSorting._3
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = Console.ReadLine(); //адрес сервера
            string port = Console.ReadLine(); //номер порта
            int a = int.Parse(Console.ReadLine()); //число a
            int b = int.Parse(Console.ReadLine()); //число b

            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"{server}:{port}/?a={a}&b={b}");
            
            var response = client.SendAsync(msg).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            int[] array = JsonSerializer.Deserialize<int[]>(content);
            array = array.OrderByDescending(i => i).ToArray();
            foreach (var item in array)
            {
                if (item > 0)
                    Console.WriteLine(item);
            }
        }
    }
}
