using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuesAndReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStruct struct1 = new MyStruct();
            MyStruct struct2 = struct1;
            struct2.Contents = 100;

            MyClass class1 = new MyClass();
            MyClass class2 = class1;
            class2.Contents = 100;

            Console.WriteLine("Value types: {0}, {1}", struct1.Contents, struct2.Contents);
            Console.WriteLine("Reference types: {0}, {1}", class1.Contents, class2.Contents);
        }
    }

    struct MyStruct
    {
        public int Contents;
    }

    class MyClass
    {
        public int Contents = 0;
    }
}
