using lab12._3;
using lab12._4;
using Microsoft.VisualBasic;
using Musical_Instrument;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Xml;

namespace lab12_4
{
    class Program
    {
        const int min = 1;
        const int max = 8;

        static void Main(string[] args)
        {
            MyCollection<MusicalInstrument>? findTree = null;
            bool exit = false;
            do
            {
                PrintMenu();
                int number = GetInt(min, max);

                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        findTree = CreateTree(); // Создание дерева
                        break;
                    case 2:
                        PrintTree(findTree);
                        break;
                    case 3:
                        PrintTreeByIEnumerable(findTree); // Вывод элементов дерева поиска
                        break;
                    case 4:
                        findTree = AddRandomData(findTree); // Добавление рандомного элемента
                        break;
                    case 5:
                        SearchData(findTree); // Проверитт наличие элемента в дереве
                        break;
                    case 6:
                        RemoveData(findTree); // Удалить элемент из дерева поиска
                        break;
                    case 7:
                        findTree = ClearTree(findTree); // Удаление дерева поиска из памяти
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
            Console.WriteLine("1. Сформировать дерево поиска и заполнить ее рандомными значениями.");
            Console.WriteLine("3. Распечатать дерево поиска в красивом виде.");
            Console.WriteLine("3. Распечатать дерево поиска с помощью итерфейса IEnumerable.");
            Console.WriteLine("4. Добавить рандомный элемент методом Add.");
            Console.WriteLine("5. Проверить наличие элемента в дереве поиска.");
            Console.WriteLine("6. Удалить элемент из дерева поиска (Remove).");
            Console.WriteLine("7. Удалить дерево поиска из памяти методом Clear.");
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
        static MyCollection<MusicalInstrument> CreateTree()
        {
            Console.Clear();
            Console.WriteLine("Введите длину дерева от 1 до 100.");
            int len = GetInt(1, 100);
            MyCollection<MusicalInstrument> tree = new MyCollection<MusicalInstrument>(len);
            MyCollection<MusicalInstrument> findTree = tree.ConvertToFindTree();
            return findTree;
        }

        /// <summary>
        /// Перчать дерева с помощью IEnumerable
        /// </summary>
        static void PrintTreeByIEnumerable(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
            }
            else
            {
                foreach (MusicalInstrument item in tree)
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Печать элементов дерева в консоль
        /// </summary>
        static void PrintTree(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
                Console.WriteLine("Дерево пустое");
            else
                tree.Print();
        }

        /// <summary>
        /// Добавить рандомное значение в список
        /// </summary>
        /// <param name="tree"></param>
        static MyCollection<MusicalInstrument> AddRandomData(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                tree = new MyCollection<MusicalInstrument>(0);
            }
            MusicalInstrument mi = new MusicalInstrument();
            mi.RandomInit();
            tree.Add(mi);
            Console.WriteLine("Элемент успешно добавлен");
            return tree;
        }

        /// <summary>
        /// Проверить наличие элемента в дереве поиска
        /// </summary>
        /// <param name="tree"></param>
        static void SearchData(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            Console.WriteLine("Введите id элемента ключа для поиска");
            int id = GetInt(0, int.MaxValue);
            Console.WriteLine("Введите имя элемента ключа для поиска");
            string name = Console.ReadLine();
            if (tree.Contains(new MusicalInstrument(name, id)))
                Console.WriteLine("Элемент находится в дереве.");
            else
                Console.WriteLine("Элемента в дереве нет.");
        }

        /// <summary>
        /// Удаление информаци из дерева по инф полю
        /// </summary>
        static void RemoveData(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            Console.WriteLine("Введите id элемента ключа для удаления");
            int id = GetInt(0, int.MaxValue);
            Console.WriteLine("Введите имя элемента ключа для удаления");
            string name = Console.ReadLine();
            if (tree.Remove(new MusicalInstrument(name, id)))
                Console.WriteLine("Элемент успешно удален.");
            else
                Console.WriteLine("Элемента в дереве нет.");
        }

        /// <summary>
        /// Очищение дерева
        /// </summary>
        static MyCollection<MusicalInstrument> ClearTree(MyCollection<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
            }
            else
            {
                tree.Clear();
                Console.WriteLine("Дерево удалено из памяти");
            }
            return null;
        }
    }
}