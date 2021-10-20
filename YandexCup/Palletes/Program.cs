using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Palletes
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.ReadData();
            worker.CalculateItems();
            var itemsToRelease = worker.ExtractPossibleToRelease();
            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(itemsToRelease.Count());
                writer.WriteLine(string.Join(' ', itemsToRelease));
            }
        }

        public class Worker
        {
            private int possibleToRelease = 0;
            private List<int> possibleToReleaseIds = new List<int>();
            private int itemsCount;
            private List<int> deliveryIds;
            private List<int> parentIds;
            private int notArrivedCount;
            private List<int> notArrivedIds;
            private List<Item> items = new List<Item>();
            public void ReadData()
            {
                itemsCount = int.Parse(Console.ReadLine()); // Общее количество палет и коробок 1<=n<=10^6
                deliveryIds = Console.ReadLine().Split(" ").Select(i => int.Parse(i)).ToList(); //deliveryId <=10^9
                parentIds = Console.ReadLine().Split(" ").Select(i => int.Parse(i)).ToList(); //0<=parentId <=n
                notArrivedCount = int.Parse(Console.ReadLine()); //количетсво поставок которые еще не приехали 0<= n
                notArrivedIds = new List<int>();
                if (notArrivedCount >= 1)
                {
                    notArrivedIds.AddRange(Console.ReadLine().Split(' ').Select(i => int.Parse(i)));
                }
            }

            internal void CalculateItems()
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    items.Add(new Item
                    {
                        Id = i+1,
                        DeliveryId = deliveryIds[i],
                        IsArrived = !notArrivedIds.Contains(deliveryIds[i]),
                        ParentId = parentIds[i]
                    });
                }
                items = items.Select(i => i.ParentId != 0 ?
                    i.SetParent(items.First(j => j.Id == i.ParentId)) :
                    i).ToList();
                items = items.Select(i => i.InitArrivedStatus()).ToList();
            }

            internal List<int> ExtractPossibleToRelease()
            {
                var possibleToRelease = items.Where(i => i.ParentId == 0 && i.IsArrived);
                if(possibleToRelease.Count() > 0)
                {
                    return possibleToRelease.Select(i=>i.DeliveryId).ToList();
                }
                else
                {
                    return new List<int>();
                }
            }
        }

        public class Item
        {
            public int Id { get; set; }
            public int DeliveryId { get; set; }
            public int ParentId { get; set; }
            public bool IsArrived { get; set; }
            public Item parent { get; set; }
            public List<Item> childs { get; set; } = new List<Item>();
            public Item SetParent(Item item)
            {
                parent = item;
                item.childs.Add(this);
                return this;
            }
            public Item InitArrivedStatus()
            {
                Item parent = this.parent;
                while (parent != null)
                {
                    parent.IsArrived = parent.IsArrived && IsArrived;
                    parent = parent.parent;
                }
                return this;
            }
        }
    }
}
