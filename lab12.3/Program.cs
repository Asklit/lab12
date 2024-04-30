using lab12._3;
using Musical_Instrument;

namespace lab12
{
    internal class Program
    {
        const int min = 1;
        const int max = 7;

        static void Main()
        {
            // Создание дерева
            MyTree<MusicalInstrument>? tree = null;
            MyTree<MusicalInstrument>? findTree = null;
            bool exit = false;
            do
            {
                PrintMenu();
                int number = GetInt(min, max);

                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        tree = CreateTree(); // Создание дерева
                        break;
                    case 2:
                        PrintTree(tree); // Вывод элементов ИСД
                        break;
                    case 3:
                        SearchItem(tree); // Поиск элемента
                        break;
                    case 4:
                        findTree = ConvertToFindTree(tree); // Преобразование ИДС в дерево поиска
                        break;
                    case 5:
                        PrintTree(findTree); // Вывод дерева поиска
                        break;
                    case 6:
                        tree = DeleteTree(tree); // Удаление ИСД из памяти
                        break;
                    case 7:
                        findTree = DeleteTree(findTree); // Удаление дерева поиска из памяти
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
            Console.WriteLine("1. Сформировать идеально сбалансированное бинарное дерево и заполнить ее рандомными значениями.");
            Console.WriteLine("2. Распечатать ИСД.");
            Console.WriteLine("3. Найти максимальный элемент в дереве (элемент с максимальным id).");
            Console.WriteLine("4. Преобразовать идеально сбалансированное дерево в дерево поиска.");
            Console.WriteLine("5. Распечатать дерево поиска.");
            Console.WriteLine("6. Удалить ИСД из памяти.");
            Console.WriteLine("7. Удалить дерево поиска из памяти.");
            Console.WriteLine("8. Завершние работы.");
        }

        /// <summary>
        /// Создание дерева
        /// </summary>
        /// <returns>Созданная хештаблица</returns>
        static MyTree<MusicalInstrument> CreateTree()
        {
            Console.Clear();
            Console.WriteLine("Введите длину дерева от 1 до 100.");
            int len = GetInt(1, 100);
            MyTree<MusicalInstrument> newTree = new MyTree<MusicalInstrument>(len);
            return newTree;
        }
        
        /// <summary>
        /// Печать элементов дерева в консоль
        /// </summary>
        static void PrintTree(MyTree<MusicalInstrument> tree)
        {
            if (tree == null)
                Console.WriteLine("Дерево пустое");
            else
                tree.Print();
        }

        /// <summary>
        /// Поиск максимального элемента в дереве
        /// </summary>
        static void SearchItem(MyTree<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            MusicalInstrument point = tree.GetMaxValue();
            Console.WriteLine($"Вывод значения максимального элемента {point}");
        }

        /// <summary>
        /// Конвертация из ИСД в дерево поиска
        /// </summary>
        /// <param name="tree">ИСД</param>
        /// <returns>Новое дерево поиска</returns>
        static MyTree<MusicalInstrument> ConvertToFindTree(MyTree<MusicalInstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Дерево пустое");
                return null;
            }
            return tree.ConvertToFindTree();
        }

        /// <summary>
        /// Удаление дерева из памяти
        /// </summary>
        /// <param name="tree">Дерево для удаления</param>
        static MyTree<MusicalInstrument> DeleteTree(MyTree<MusicalInstrument> tree)
        {
            Console.WriteLine("Дерево успешно удалено из памяти");
            tree.DeleteTree();
            return null;
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
