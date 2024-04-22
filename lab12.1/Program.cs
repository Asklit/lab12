using lab12._2;
using Musical_Instrument;
using System;
using System.Linq.Expressions;
using System.Threading.Channels;

namespace lab12_2
{
    internal class Program
    {
        const int min = 1;
        const int max = 6;

        static void Main()
        {
            // Создание хештаблицы
            MyHashtable<MusicalInstrument, Guitar>? ht = new MyHashtable<MusicalInstrument, Guitar>();
            Item<MusicalInstrument, Guitar> item = null;
            bool exit = false;
            do
            {
                PrintMenu();
                int number = GetInt(min, max);

                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        ht = CreateHashTable(); // Создание хештаблицы
                        break;
                    case 2:
                        ht.Print(); // Вывод элементов хештаблицы
                        break;
                    case 3:
                        item = SearchItem(ht); // Поиск элемента
                        break;
                    case 4:
                        DeletePoints(ht, item); // Поиск и удаление элемента
                        item = null;
                        break;
                    case 5:
                        AddPoints(ht); // Добаление элементов в хештаблицу
                        break;
                    case 6:
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
            Console.WriteLine("1. Сформировать хештаблицу и заполнить ее рандомными значениями.");
            Console.WriteLine("2. Распечатать полученую хештаблицу.");
            Console.WriteLine("3. Выполнить поиск по id и name.");
            Console.WriteLine("4. Удалить найденный элемент.");
            Console.WriteLine("5. Добавить в таблицу рандомные значения.");
            Console.WriteLine("6. Завершние работы.");
        }

        /// <summary>
        /// Создание хештаблицы
        /// </summary>
        /// <returns>Созданная хештаблица</returns>
        static MyHashtable<MusicalInstrument, Guitar> CreateHashTable()
        {
            Console.Clear();
            Console.WriteLine("Введите длину хештаблицы от 1 до 100.");
            int len = GetInt(1, 100);
            Console.WriteLine($"Введите количество элементов хештаблицы от 1 до 100.");
            MyHashtable<MusicalInstrument, Guitar> newHT = new MyHashtable<MusicalInstrument, Guitar>(len);
            int count = GetInt(1, 100);
            for (int i = 0; i < count; i++)
            {
                MusicalInstrument mi = new();
                mi.RandomInit();
                Guitar guitar = new Guitar();
                guitar.RandomInit();
                newHT.AddData(mi, guitar);
            }
            return newHT;
        }

        /// <summary>
        /// Добавление рандомных значений
        /// </summary>
        /// <param name="ht">Хештаблица</param>
        static void AddPoints(MyHashtable<MusicalInstrument, Guitar> ht)
        {
            Console.Clear();
            if (ht.Count == 0)
            {
                Console.WriteLine("Хештаблица пустая");
                return;
            }
            Console.WriteLine("Введите какое количество элементов хотите добавить.");
            int count = GetInt(1, 100);
            bool flag;
            for (int i = 0; i < count; i++)
            { 
                flag = false;
                while (!flag)
                {
                    MusicalInstrument mi = new();
                    mi.RandomInit();
                    Guitar guitar = new Guitar();
                    guitar.RandomInit();
                    flag = ht.AddData(mi, guitar);
                }
            }
            Console.WriteLine("Элементы добавлены успешно");
        }

        /// <summary>
        /// Поиск элемента с ключом введенным ID и именем
        /// </summary>
        /// <param name="ht">Хештаблица</param>
        static Item<MusicalInstrument, Guitar> SearchItem(MyHashtable<MusicalInstrument, Guitar> ht)
        {
            if (ht.Count == 0)
            {
                Console.WriteLine("Хештаблица пустая");
                return default;
            }
            Console.WriteLine("Введите id элемента ключа для удаления");
            int id = GetInt(0, int.MaxValue);
            Console.WriteLine("Введите имя элемента ключа для удаления");
            string name = Console.ReadLine();
            try
            {
                Item<MusicalInstrument, Guitar> item = ht.FindKeyByData(id, name);
                if (item == null)
                {
                    Console.WriteLine("Элемент не найден");
                    return default;
                }
                else
                {
                    Console.WriteLine($"Найден элемент с значением {item.Value}");
                    return item;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Класс не имеет поля id");
                return default;
            }
        }

        /// <summary>
        /// Удаление элемента с ключом введенным ID
        /// </summary>
        /// <param name="ht">Хештаблица</param>
        static void DeletePoints(MyHashtable<MusicalInstrument, Guitar> ht, Item<MusicalInstrument, Guitar> item)
        {
            if (ht.Count == 0)
                Console.WriteLine("Хештаблица пустая");
            else if (item == null)
                Console.WriteLine("Элемент для удаления не определен.");
            else
            {
                ht.RemoveData(item);
                Console.WriteLine($"Элемент с значением {item.Value} успешно удален");
            }
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
    }
}