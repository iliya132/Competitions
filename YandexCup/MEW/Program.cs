using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MEW
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Read();
            worker.DoTheJob();
            foreach(var item in worker.pairs)
            {
                Console.WriteLine(item.Value);
            }
        }
    }

    public class Worker
    {
        public Dictionary<string, string> pairs = new Dictionary<string, string>();
        public List<string> Names = new List<string>();
        public List<string> Answers = new List<string>();
        string serverName = @"http://127.0.0.1:7777/";
        string methodName = "MEW";
        const string HEADER_NAME = "X-Cat-Variable";
        const string HEADER_VALUE = "X-Cat-Value";

        public void ReadTest()
        {
            Names.AddRange(new string[] { "Window", "Bird", "Food", "Human" });
        }
        public void Read()
        {
            for (int i = 0; i < 4; i++)
            {
                Names.Add(Console.ReadLine());
            }
        }

        public IEnumerable<string> Request(params string[] names)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(methodName), $"{serverName}");
                foreach(var name in names)
                {
                    request.Headers.Add(HEADER_NAME, name);
                }
                var httptask = client.SendAsync(request);
                httptask.Wait();
                return httptask.Result.Headers.GetValues(HEADER_VALUE);
            }
        }

        public void DoTheJob()
        {
            var answers1 = Request(Names[0], Names[1]);
            var answers2 = Request(Names[1], Names[2]);
            var answers3 = Request(Names[2], Names[3]);

            pairs.Add(Names[0], answers1.Except(answers2).Count() > 0 ? answers1.Except(answers2).First() : answers1.First());
            pairs.Add(Names[1], answers2.Intersect(answers1).Count() > 0 ? answers2.Intersect(answers1).First() : answers2.First());
            pairs.Add(Names[2], answers2.Intersect(answers3).Count() > 0 ? answers2.Intersect(answers3).First() : answers2.First());
            pairs.Add(Names[3], answers3.Except(answers2).Count() > 0 ? answers3.Except(answers2).First() : answers3.First());
        }
    }
}
