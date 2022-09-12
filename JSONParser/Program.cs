using Newtonsoft.Json;

namespace JSONParser
{
    class Program
    {
        class pain
        {
            public int[] nums { get; set; }

            public pain()
            {
            }
        }
        class LinkedListNode<T>
        {
            public T Value { get; set; }
            public LinkedListNode<T> Next { get; set; }

            public LinkedListNode()
            {
            }
            public LinkedListNode(T value)
            {
                Value = value;
            }
        }
        static void Main(string[] args)
        {
            var node = new LinkedListNode<int>(1)
            {
                Next = new LinkedListNode<int>(2)
                {
                    Next = new LinkedListNode<int>(3)
                    {
                        Next = new LinkedListNode<int>(4)
                    }
                }
            };

            var pain = new pain()
            {
                nums = new int[5] { 1, 2, 3, 4, 5 }
            };

            string jsonString = JsonConvert.SerializeObject(node);

            ;

            var tokens = JSONParser.GetTokens(jsonString);
            var deserializedObj = JSONParser.Parse<LinkedListNode<int>>(jsonString);

            ;

            JSONParser.VisualizeJSONInConsole(jsonString);

            //Object
            //--- Name: string: Brandon
            //--- Cat: Object: 
        }
    }
}
