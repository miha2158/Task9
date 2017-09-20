using System;

using static System.Console;

namespace Task9
{
    class Program
    {
        #region Menu

        private static void SwapColors()
        {
            var temp = BackgroundColor;
            BackgroundColor = ForegroundColor;
            ForegroundColor = temp;
        }
        static void DoSwapped(Action action)
        {
            SwapColors();
            action.Invoke();
            SwapColors();
        }
        private static Tuple<int, int> SelectorXY(string[][] items)
        {
            var CursorBefore = CursorVisible;
            CursorVisible = false;

            var offset = new Tuple<int, int>(3, 1);
            SetCursorPosition(CursorLeft + offset.Item1, CursorTop + offset.Item2);
            var defaultXY = new Tuple<int, int>(CursorLeft, CursorTop);

            var PreviousPosition = new Tuple<int, int>(0, 0);
            var CurrentPosition = new Tuple<int, int>(0, 0);

            int maxlength = items[0][0].Length;
            {
                foreach (string[] s1 in items)
                    foreach (string s in s1)
                        if (s.Length > maxlength)
                            maxlength = s.Length;
            }

            int spread = maxlength + offset.Item1;

            foreach (string[] item in items)
            {
                foreach (string s in item)
                {
                    Write(s);
                    CursorLeft = CursorLeft - s.Length + spread;
                }

                SetCursorPosition(defaultXY.Item1, CursorTop + 1);
            }

            while (true)
            {
                SetCursorPosition(defaultXY.Item1 + spread * PreviousPosition.Item1,
                    defaultXY.Item2 + PreviousPosition.Item2);
                Write(items[PreviousPosition.Item2][PreviousPosition.Item1]);

                SetCursorPosition(defaultXY.Item1 + spread * CurrentPosition.Item1,
                    defaultXY.Item2 + CurrentPosition.Item2);
                DoSwapped(delegate { Write(items[CurrentPosition.Item2][CurrentPosition.Item1]); });

                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                    case ConsoleKey.NumPad5:
                    {
                        SetCursorPosition(0, defaultXY.Item2 + items.Length + 1);
                        CursorVisible = CursorBefore;
                    }
                        return CurrentPosition;

                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.NumPad6:
                    {
                        PreviousPosition = CurrentPosition;
                        CurrentPosition =
                            new Tuple<int, int>(
                                CurrentPosition.Item1 + 1 < items[CurrentPosition.Item2].Length
                                    ? CurrentPosition.Item1 + 1
                                    : 0,
                                CurrentPosition.Item2);
                    }
                        break;

                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.NumPad4:
                    {
                        PreviousPosition = CurrentPosition;
                        CurrentPosition =
                            new Tuple<int, int>(CurrentPosition.Item1 - 1 >= 0
                                    ? CurrentPosition.Item1 - 1
                                    : items[0].Length - 1,
                                CurrentPosition.Item2);
                    }
                        break;

                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.NumPad8:
                    {
                        PreviousPosition = CurrentPosition;
                        CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1,
                            CurrentPosition.Item2 - 1 >= 0
                                ? CurrentPosition.Item2 - 1
                                : items.Length - 1);
                    }
                        break;

                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.NumPad2:
                    {
                        PreviousPosition = CurrentPosition;
                        CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1,
                            CurrentPosition.Item2 + 1 < items.Length
                                ? CurrentPosition.Item2 + 1
                                : 0);
                    }
                        break;
                }
            }
        }

        #endregion

        static void Main(string[] args)
        {
            int num = 0;
            WriteLine("Введите длину сипска");
            while (!int.TryParse(ReadLine(), out num) || num <= 0)
                WriteLine("Ошибка, введите положительное число");


            var options = new[] { new[] { "Удалить" }, new[] { "Найти" }, new[] { "Выход" } };
            var list = new TwoWayLinkedList(num);

            do
            {
                Clear();
                for (int i = 0; i < list.Length; i++)
                    Write(" {0}", list[i]);
                WriteLine();

                var pos = SelectorXY(options);
                switch (pos.Item2)
                {
                    case 0:
                        WriteLine("Какой элемент удалить?");
                        while (!int.TryParse(ReadLine(), out num) || num < 0 || num >= list.Length)
                            WriteLine("Ошибка, введите положительное число меньше длины списка");
                        list.Delete(num - 1);

                        WriteLine("\nРезультат");
                        for (int i = 0; i < list.Length; i++)
                            Write(" {0}", list[i]);
                        WriteLine();

                        ReadKey(true);
                        break;

                    case 1:
                        WriteLine("Что найти?");
                        while (!int.TryParse(ReadLine(), out num))
                            WriteLine("Введите число");
                        var s = list.Search(num);

                        if (s != -1)
                            WriteLine("Индекс найденного элемента: {0}", s + 1);
                        else WriteLine("Элемент не найден");
                        ReadKey(true);
                        break;

                    case 2:
                        return;
                }
            }
            while (true);
        }
    }
}