using lab12._3;
using lab12._4;
using Microsoft.VisualBasic;
using Musical_Instrument;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab13
{
    class Program
    {
        const int min = 1;
        const int max = 8;

        public static MyObservableCollection<MusicalInstrument> firstObsCollection = new(0, "Первая коллекция");
        public static MyObservableCollection<MusicalInstrument> secondObsCollection = new(0, "Вторая коллекция");

        public static Journal firstJournal = new Journal();
        public static Journal secondJournal = new Journal();

        static void FirstJournalHandler()
        {
            firstObsCollection.RegisterCountChangedHandler(firstJournal.Add);
            firstObsCollection.RegisterReferenceChangedHandler(firstJournal.Add);
        }

        static void SecondJournalHandler()
        {
            firstObsCollection.RegisterReferenceChangedHandler(secondJournal.Add);
            secondObsCollection.RegisterReferenceChangedHandler(secondJournal.Add);
        }

        static void Main(string[] args)
        {
            FirstJournalHandler();
            SecondJournalHandler();

            bool exit = false;
            do
            {
                PrintMenu();
                int number = GetInt(min, max);

                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        CreateCollection(); // Создание коллекции
                        break;
                    case 2:
                        PrintTree(); // Вывод элементов коллекции
                        break;
                    case 3:
                        AddRandomData(); // Добавление рандомного значения
                        break;
                    case 4:
                        RemoveData(); // Удалить элемент из дерева поиска
                        break;
                    case 5:
                        ChangeValue();
                        break;
                    case 6:
                        firstJournal.PrintJournal(); 
                        break;
                    case 7:
                        secondJournal.PrintJournal();
                        break;
                    case 8:
                        exit = true; // Выход из программы
                        break;
                }
            } while (!exit);
        }

        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню из списка:");
            Console.WriteLine("1. Сформировать коллекцию (дерево поиска) и заполнить ее рандомными значениями.");
            Console.WriteLine("2. Распечатать коллекцию.");
            Console.WriteLine("3. Добавить рандомный элемент методом Add.");
            Console.WriteLine("4. Удалить элемент из дерева поиска (Remove).");
            Console.WriteLine("5. Присвоить новые данные выбранному элементу.");
            Console.WriteLine("6. Вывести первый журнал в консоль.");
            Console.WriteLine("7. Вывести второй журнал в консоль.");
            Console.WriteLine("8. Завершние работы.");
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
        /// Создание дерева
        /// </summary>
        /// <returns>Созданное дерево</returns>
        static void CreateCollection()
        {
            Console.Clear();
            Console.WriteLine("Введите номер коллллекции (1 или 2)");
            int num = GetInt(1, 2);
            Console.Clear();
            Console.WriteLine("Введите длину дерева от 1 до 100.");
            int len = GetInt(1, 100);
            for (int i = 0; i < len; i++)
            {
                MusicalInstrument mi = new();
                mi.RandomInit();

                if (num == 1)
                {
                    while (firstObsCollection.Contains(mi)) // Проверка на добавление существующего элемента
                    {
                        mi.RandomInit();
                    }
                    firstObsCollection.AddPointToFindTree(mi, firstObsCollection.root);
                }   
                else
                {
                    while (secondObsCollection.Contains(mi)) // Проверка на добавление существующего элемента
                    {
                        mi.RandomInit();
                    }
                    secondObsCollection.AddPointToFindTree(mi, secondObsCollection.root);
                }
            }
            if (num == 1)
                firstObsCollection.Count = len;
            else
                secondObsCollection.Count = len;
        }

        /// <summary>
        /// Печать элементов дерева в консоль
        /// </summary>
        static void PrintTree()
        {
            Console.Clear();
            Console.WriteLine("Введите номер коллллекции (1 или 2)");
            int num = GetInt(1, 2);
            if (num == 1)
                if (firstObsCollection.Count == 0)
                    Console.WriteLine("Дерево пустое");
                else
                    firstObsCollection.Print();
            else
                if (secondObsCollection.Count == 0)
                    Console.WriteLine("Дерево пустое");
                else
                    secondObsCollection.Print();

        }

        /// <summary>
        /// Добавить рандомное значение в список
        /// </summary>
        static void AddRandomData()
        {
            Console.Clear();
            Console.WriteLine("Введите номер коллллекции (1 или 2)");
            int num = GetInt(1, 2);
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            
            if (num == 1)
            {
                while (firstObsCollection.Contains(mi)) // Проверка на добавление существующего элемента
                {
                    mi.RandomInit();
                }
                firstObsCollection.Add(mi);
            }
            else
            {
                while (secondObsCollection.Contains(mi)) // Проверка на добавление существующего элемента
                {
                    mi.RandomInit();
                }
                secondObsCollection.Add(mi);
            }
            Console.WriteLine("Элемент успешно добавлен");
        }

        /// <summary>
        /// Удаление информаци из дерева по инф полю
        /// </summary>
        static void RemoveData()
        {
            Console.WriteLine("Введите номер коллллекции (1 или 2)");
            int num = GetInt(1, 2);
            if (num == 1)
            {
                if (firstObsCollection.Count == 0)
                {
                    Console.WriteLine("Дерево пустое");
                    return;
                }
                Console.WriteLine("Введите id элемента ключа для удаления");
                int id = GetInt(0, int.MaxValue);
                Console.WriteLine("Введите имя элемента ключа для удаления");
                string name = Console.ReadLine();
                if (firstObsCollection.Remove(new MusicalInstrument(name, id)))
                    Console.WriteLine("Элемент успешно удален.");
                else
                    Console.WriteLine("Элемента в дереве нет.");
            }
            else
            {
                if (secondObsCollection.Count == 0)
                {
                    Console.WriteLine("Дерево пустое");
                    return;
                }
                Console.WriteLine("Введите id элемента ключа для удаления");
                int id = GetInt(0, int.MaxValue);
                Console.WriteLine("Введите имя элемента ключа для удаления");
                string name = Console.ReadLine();
                if (secondObsCollection.Remove(new MusicalInstrument(name, id)))
                    Console.WriteLine("Элемент успешно удален.");
                else
                    Console.WriteLine("Элемента в дереве нет.");
            }
            
        }

        static void ChangeValue()
        {
            Console.WriteLine("Введите номер коллллекции (1 или 2)");
            int num = GetInt(1, 2);
            if (num == 1)
            {
                if (firstObsCollection.Count == 0)
                {
                    Console.WriteLine("Дерево пустое");
                }
                else
                {
                    Console.WriteLine("Введите id элемента ключа для удаления");
                    int id = GetInt(0, int.MaxValue);
                    Console.WriteLine("Введите имя элемента ключа для удаления");
                    string name = Console.ReadLine();
                    MusicalInstrument mi = new MusicalInstrument();
                    mi.RandomInit();
                    firstObsCollection[new MusicalInstrument(name, id)] = new Point<MusicalInstrument>(mi);
                }
            }
            else
            {
                if (secondObsCollection.Count == 0)
                {
                    Console.WriteLine("Дерево пустое");
                }
                else
                {
                    Console.WriteLine("Введите id элемента ключа для удаления");
                    int id = GetInt(0, int.MaxValue);
                    Console.WriteLine("Введите имя элемента ключа для удаления");
                    string name = Console.ReadLine();
                    MusicalInstrument mi = new MusicalInstrument();
                    mi.RandomInit();
                    secondObsCollection[new MusicalInstrument(name, id)] = new Point<MusicalInstrument>(mi);
                }
            }
        }
    }
}