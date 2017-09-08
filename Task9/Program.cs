using System;

using static System.Console;

namespace Task9
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            WriteLine("Введите длину сипска");
            while (!int.TryParse(ReadLine(),out num) || num <= 0)
                WriteLine("Ошибка, введите положительное число");

            var list = new TwoWayLinkedList(num);
            for (int i = 0; i < list.Length; i++)
                Write(" {0}",list[i]);
            WriteLine();
			
			WriteLine("Какой элемент удалить?");
            while (!int.TryParse(ReadLine(),out num) || num < 0 || num >= list.Length)
                WriteLine("Ошибка, введите положительное число меньше длины списка");
			
			list.Delete(num);

            WriteLine("\nРезультат");
            for (int i = 0; i < list.Length; i++)
                Write(" {0}", list[i]);
            WriteLine();

            ReadKey(true);
        }
    }
}