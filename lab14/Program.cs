using lab12._4;
using lab14;
using Microsoft.VisualBasic;
using Musical_Instrument;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace lab13
{
    class Program
    {
        public static int min = 1;
        public static int max = 3;

        public static SortedDictionary<string, List<MusicalInstrument>> firstPartCollection = new();
        public static MyCollection<MusicalInstrument> secondPartCollection = new(0);

        static void Main(string[] args) 
        {
            bool exit = false;
            do
            {
                PrintStartMenu();
                int number = GetInt(min, max);

                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        max = 7;
                        do
                        {
                            PrintFirstPartMenu();
                            number = GetInt(min, max);

                            // Варианты выбора разных пунктов меню
                            switch (number)
                            {
                                case 1:
                                    CreateFirstPartCollection(); // Создание коллекции
                                    break;
                                case 2:
                                    PrintCollection(); // Вывод элементов коллекции
                                    break;
                                case 3:
                                    GetGroups();
                                    break;
                                case 4:
                                    GetAgregateData();
                                    break;
                                case 5:
                                    GetGroupedData();
                                    break;
                                case 6:
                                    JoinData();
                                    break;
                                case 7:
                                    exit = true; // Выход из программы
                                    break;
                            }
                        } while (!exit);
                        break;
                    case 2:
                        max = 6;
                        do
                        {
                            PrintSecondPartMenu();
                            number = GetInt(min, max);

                            // Варианты выбора разных пунктов меню
                            switch (number)
                            {
                                case 1:
                                    CreateSecondPartCollection(); // Создание коллекции
                                    break;
                                case 2:
                                    PrintTree(); // Вывод элементов коллекции
                                    break;
                                case 3:
                                    GetCountInstruments();
                                    break;
                                case 4:
                                    GetAgregateDataSecondPart();
                                    break;
                                case 5:
                                    GetGroupsSecondPart();
                                    break;
                                case 6:
                                    exit = true; // Выход из программы
                                    break;
                            }
                        } while (!exit);
                        break;
                    case 3:
                        exit = true; // Выход из программы
                        break;
                }
            } while (!exit);
        }

        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        static void PrintStartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню из списка:");
            Console.WriteLine("1. Первая часть");
            Console.WriteLine("2. Вторая часть");
            Console.WriteLine("3. Завершние работы.");
        }

        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        static void PrintFirstPartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню из списка:");
            Console.WriteLine("1. Сформировать коллекцию SortedDictionary<string, List<MusicalInstrument>>");
            Console.WriteLine("2. Распечатать коллекцию.");
            Console.WriteLine("3. Вывести все группы с количесовом инструментов больше указанного. (Where)");
            Console.WriteLine("4. Вывод агрегирование данных (Sum, Min, Max, Avg)");
            Console.WriteLine("5. Вывести данные в сгрупперованном виде по названию инструмента (GroupBy)");
            Console.WriteLine("6. Создать коллекцию и соединить с изначатьной и вывести в консоль результат (Join + Let)");
            Console.WriteLine("7. Завершние работы.");
        }

        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        static void PrintSecondPartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню из списка:");
            Console.WriteLine("1. Сформировать коллекцию (дерево поиска) и заполнить ее рандомными значениями.");
            Console.WriteLine("2. Распечатать коллекцию.");
            Console.WriteLine("3. Узнать количество определленных инструментов. (Where + Count)");
            Console.WriteLine("4. Вывод агрегирование данных (Sum, Min, Max, Avg)");
            Console.WriteLine("5. Группировка данных (GroupBy)");
            Console.WriteLine("6. Завершние работы.");
        }

        /// <summary>
        /// Ввод числа
        /// </summary>
        /// <param name="number">Вводимое число</param>
        /// <param name="isConvert">Проверка правильности ввода</param>
        /// <returns>Введенное число number</returns>
        static int GetInt(int minInt, int maxInt)
        {
            bool isConvert;
            int number;
            // Проверка корректности ввода числа
            do
            {
                isConvert = int.TryParse(Console.ReadLine(), out number);
                if (!isConvert)
                {
                    Console.WriteLine("Некорректный ввод. Повторите ввод числа.");
                }
                else if (number < minInt)
                {
                    Console.WriteLine($"Число за допустимыми границами ({minInt}, {maxInt}). Введите число еще раз.");
                    isConvert = false;
                }
                else if (number > maxInt)
                {
                    Console.WriteLine($"Число за допустимыми границами ({minInt}, {maxInt}). Введите число еще раз.");
                    isConvert = false;
                };

            } while (!isConvert);

            return number;
        }

        /// <summary>
        /// Создание коллекции
        /// </summary>
        static void CreateFirstPartCollection()
        {
            Console.Clear();
            Console.WriteLine("Введите длину словаря от 1 до 100.");
            int len = GetInt(1, 100);
            Random random = new Random();

            for (int i = 0; i < len; i++)
            {
                int count = random.Next(1, 5);
                List<MusicalInstrument> list = new List<MusicalInstrument>();
                for (int j = 0; j < count; j++)
                {
                    MusicalInstrument mi = new();
                    mi.RandomInit();
                    list.Add(mi);
                }
                firstPartCollection.Add($"Группа {i + 1}", list);
            }
        }

        public static void CreateSecondPartCollection()
        {
            Console.Clear();
            Console.WriteLine("Введите длину дерева от 1 до 100.");
            int len = GetInt(1, 100);
            for (int i = 0; i < len; i++)
            {
                MusicalInstrument mi = new();
                mi.RandomInit();
                while (secondPartCollection.Contains(mi)) // Проверка на добавление существующего элемента
                {
                    mi.RandomInit();
                }
                secondPartCollection.AddPointToFindTree(mi, secondPartCollection.root);
                secondPartCollection.Count++;
            }
        }

        /// <summary>
        /// Печать элементов в консоль
        /// </summary>
        static void PrintCollection()
        {
            Console.Clear();
            if (firstPartCollection.Count == 0)
                Console.WriteLine("Коллекция пустая.");
            else
            {
                foreach (var item in firstPartCollection)
                {
                    Console.WriteLine($"Название группы: {item.Key}");
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Console.WriteLine($"    {item.Value[i]}");
                    }
                }
            }
        }

        /// <summary>
        /// Печать элементов дерева в консоль
        /// </summary>
        static void PrintTree()
        {
            Console.Clear();
            if (secondPartCollection.Count == 0)
                Console.WriteLine("Дерево пустое");
            else
                secondPartCollection.Print();
        }

        static void GetGroups()
        {
            if (firstPartCollection.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);

            Console.Clear();
            Console.WriteLine("Введите количество инструментов для сравнения");
            int num = GetInt(1, 10);
            IEnumerable<KeyValuePair<string, List<MusicalInstrument>>> res;
            if (type == 1) 
                res = firstPart.LinqWhereRequest(firstPartCollection, num);
            else
                res = firstPart.ExtensionWhereRequest(firstPartCollection, num);
            if (res.Count() == 0)
                Console.WriteLine("Ничего не найдено");
            else
            {
                foreach (var item in res)
                {
                    Console.WriteLine($"Название группы: {item.Key}");
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Console.WriteLine($"    {item.Value[i]}");
                    }
                }
            }
        }

        static void GetAgregateData()
        {
            if (firstPartCollection.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);
            if (type == 1)
            {
                Console.WriteLine($"(Sum) Количество инструментов всех групп: {firstPart.LinqSumRequest(firstPartCollection)}");
                Console.WriteLine($"(Max) Максимальное количество инструментов у групп: {firstPart.LinqMaxRequest(firstPartCollection)}");
                Console.WriteLine($"(Min) Минимальное количество инструментов у групп: {firstPart.LinqMinRequest(firstPartCollection)}");
                Console.WriteLine($"(Avg) Среднее количество инструментов у групп: {firstPart.LinqAvgRequest(firstPartCollection)}");
            }
            else
            {
                Console.WriteLine($"(Sum) Количество инструментов всех групп: {firstPart.ExtensionSumRequest(firstPartCollection)}");
                Console.WriteLine($"(Max) Максимальное количество инструментов у групп: {firstPart.ExtensionMaxRequest(firstPartCollection)}");
                Console.WriteLine($"(Min) Минимальное количество инструментов у групп: {firstPart.ExtensionMinRequest(firstPartCollection)}");
                Console.WriteLine($"(Avg) Среднее количество инструментов у групп: {firstPart.ExtensionAvgRequest(firstPartCollection)}");
            }

            Console.WriteLine();
            if (firstPartCollection.Count == 0)
                Console.WriteLine("Коллекция пустая.");
            else
            {
                foreach (var item in firstPartCollection)
                {
                    Console.WriteLine($"Название группы: {item.Key}");
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Console.WriteLine($"    {item.Value[i]}");
                    }
                }
            }
        }

        static void GetGroupedData()
        {
            if (firstPartCollection.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);

            IEnumerable<IGrouping<string, MusicalInstrument>> res;
            if (type == 1)
                res = firstPart.LinqGroupByRequest(firstPartCollection);
            else
                res = firstPart.ExtensionGroupByRequest(firstPartCollection);
            foreach (var item in res)
            {
                Console.WriteLine($"Название инструмента: {item.Key}. Размер: {item.Count()}");
                foreach (var elem in item)
                {
                    Console.WriteLine($"    {elem}");
                }
            }
        }

        static void JoinData()
        {
            if (firstPartCollection.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);

            Console.Clear();
            Console.WriteLine("Введите длину словаря от 1 до 100.");
            int len = GetInt(1, 100);
            Random random = new Random();
            SortedDictionary<string, List<MusicalInstrument>> temp = new();

            for (int i = 0; i < len; i++)
            {
                int count = random.Next(1, 5);
                List<MusicalInstrument> list = new List<MusicalInstrument>();
                for (int j = 0; j < count; j++)
                {
                    MusicalInstrument mi = new();
                    mi.RandomInit();
                    list.Add(mi);
                }
                temp.Add($"Группа {i + 1}", list);
            }

            IEnumerable<KeyValuePair<string, List<MusicalInstrument>>> res;
            if (type == 1)
                res = firstPart.LinqJoinRequest(firstPartCollection, temp);
            else
                res = firstPart.ExtensionJoinRequest(firstPartCollection, temp);
            //firstPartCollection = new();
            foreach (var item in res)
            {
                Console.WriteLine($"Название группы: {item.Key}");
                for (int i = 0; i < item.Value.Count; i++)
                {
                    Console.WriteLine($"    {item.Value[i]}");
                }
                //firstPartCollection.Add(item.Key, item.Value);
            }
        }

        static void GetCountInstruments()
        {
            if (secondPartCollection.Count == 0)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);

            Console.Clear();
            Console.WriteLine("Введите название инструмента");
            string name = Console.ReadLine();

            int res;
            if (type == 1)
                res = secondPart.LinqWhereRequest(secondPartCollection, name);
            else
                res = secondPart.ExtensionWhereRequest(secondPartCollection, name);
            Console.WriteLine($"Музкальных инструментов {name} найдено: {res}");
        }

        static void GetAgregateDataSecondPart()
        {
            if (secondPartCollection.Count == 0)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);
            if (type == 1)
            {
                Console.WriteLine($"(Sum) Сумма всех ID: {secondPart.LinqSumRequest(secondPartCollection)}");
                Console.WriteLine($"(Max) Максимальный ID: {secondPart.LinqMaxRequest(secondPartCollection)}");
                Console.WriteLine($"(Min) Минимальный ID: {secondPart.LinqMinRequest(secondPartCollection)}");
                Console.WriteLine($"(Avg) Среднее ID: {secondPart.LinqAvgRequest(secondPartCollection)}");
            }
            else
            {
                Console.WriteLine($"(Sum) Сумма всех ID: {secondPart.ExtensionSumRequest(secondPartCollection)}");
                Console.WriteLine($"(Max) Максимальный ID: {secondPart.ExtensionMaxRequest(secondPartCollection)}");
                Console.WriteLine($"(Min) Минимальный ID: {secondPart.ExtensionMinRequest(secondPartCollection)}");
                Console.WriteLine($"(Avg) Среднее ID: {secondPart.ExtensionAvgRequest(secondPartCollection)}");
            }

            Console.WriteLine();
            if (secondPartCollection.Count == 0)
                Console.WriteLine("Дерево пустое");
            else
                secondPartCollection.Print();
        }

        static void GetGroupsSecondPart()
        {
            if (secondPartCollection.Count == 0)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            Console.Clear();
            Console.WriteLine("Введите тип запроса (Linq - 1, Extension - 2)");
            int type = GetInt(1, 2);

            IEnumerable<IGrouping<string, MusicalInstrument>> res;
            if (type == 1)
                res = secondPart.LinqGroupByRequest(secondPartCollection);
            else
                res = secondPart.ExtensionGroupByRequest(secondPartCollection);
            if (res.Count() == 0)
                Console.WriteLine("Ничего не найдено");
            else
            {
                foreach (var item in res)
                {
                    Console.WriteLine($"Название инструмента: {item.Key}. Размер: {item.Count()}");
                    foreach (var elem in item)
                    {
                        Console.WriteLine($"    {elem}");
                    }
                }
            }
        }
    }
}