using System;
using System.Collections.Generic;

namespace Red_Black_Trees
{
    class Program
    {
        static List<int> generatedNums = new List<int>();

        static Random gen = new Random();

        static int GenerateUniqueNum()
        {
            int rand;// = gen.Next(1, 101);

            do
            {
                rand = gen.Next(1, 101);
            } while (generatedNums.Contains(rand));

            generatedNums.Add(rand);

            return rand;

        }


        static void Main(string[] args)
        {
          /*  List<int> temp = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                temp.Add(GenerateUniqueNum());
                //Console.WriteLine(GenerateUniqueNum());
            }

            temp.Sort();
            */

            Tree<int> tree = new Tree<int>();
            int[] value = new int[10];
            for(int i = 0; i < 10; i++)
            {
                value[i] = GenerateUniqueNum();
                tree.Add(value[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                tree.Remove(value[i]);
            }

            Console.ReadKey();
        }
    }
}
