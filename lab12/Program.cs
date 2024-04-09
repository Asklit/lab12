using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace lab12
{
    internal class Program
    {
        const int min = 1;
        const int max = 9;

        static void Main()
        {
            // Изначальный и склонированный список
            MyList<MusicalInstrument>? list = new MyList<MusicalInstrument>();
            MyList<MusicalInstrument>? cloneList = new MyList<MusicalInstrument>();
            bool exit = false;
            do
            { 
                PrintMenu();
                int number = GetInt(min, max);
                
                // Варианты выбора разных пунктов меню
                switch (number)
                {
                    case 1:
                        list = CreateList(); // Создание списка
                        break;
                    case 2:
                        list.PrintList(); // Вывод элементов списка
                        break;
                    case 3:
                        list = AddPoints(list); // Добаление элементов в список в позициями 1, 3, 5, и тд
                        break;
                    case 4:
                        DeletePoints(list); // Удаление элемента списка
                        break;
                    case 5:
                        cloneList = CloneList(list); // Клонирование списка
                        break;
                    case 6:
                        TestCloneList(list); // Изменение информации в изначальном списке для проверки клонирования
                        break;
                    case 7:
                        cloneList.PrintList(); // Вывод элементов склонированного спика
                        break;
                    case 8:
                        list = DeleteList(list); // Удаление списка из памяти
                        break;
                    case 9:
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
            Console.WriteLine("1. Сформировать двунаправленный список и заполнить его рандомными значениями.");
            Console.WriteLine("2. Распечатать полученный список.");
            Console.WriteLine("3. Добавить в список элементы с номерами 1, 3, 5 и т. д.");
            Console.WriteLine("4. Удалить из списка все элементы, начиная с элемента с заданным информационным полем.");
            Console.WriteLine("5. Выполнить глубокой копирование.");
            Console.WriteLine("6. Проверить глубокой копирование изменив данные в изначальном списке.");
            Console.WriteLine("7. Распечатать склонированный список.");
            Console.WriteLine("8. Удалить список из памяти.");
            Console.WriteLine("9. Завершние работы.");
        }

        /// <summary>
        /// Создание списка
        /// </summary>
        /// <returns>Созданный список</returns>
        static MyList<MusicalInstrument> CreateList()
        {
            Console.Clear();
            Console.WriteLine("Введите длину списка от 1 до 100.");
            int len = GetInt(1, 100);
            MyList<MusicalInstrument> newList = new MyList<MusicalInstrument>(len);
            return newList;
        }

        /// <summary>
        /// Добавление элементов 1, 3, 5 и тд
        /// </summary>
        /// <param name="list">Список</param>
        /// <returns>Новый сформированный список</returns>
        static MyList<MusicalInstrument>? AddPoints(MyList<MusicalInstrument> list)
        {
            Console.Clear();
            if (list.count == 0)
            {
                Console.WriteLine("Список пуст");
                return list;
            }
            MyList<MusicalInstrument> newList = new MyList<MusicalInstrument>();
            Point<MusicalInstrument> current = list.begin;
            for (int i = 0; i < list.count; i++)
            {
                MusicalInstrument random = new();
                random.RandomInit();
                newList.AddToEnd(random);
                newList.AddToEnd(current.Data);
                current = current.Next;
            }
            Console.WriteLine("Элементы добавлены успешно.");
            return newList;
        }

        /// <summary>
        /// Удаление элементов из списка начиная с выбранного
        /// </summary>
        /// <param name="list">Список</param>
        static void DeletePoints(MyList<MusicalInstrument> list)
        {
            Console.Clear();
            if (list.count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            Console.WriteLine("Введите индекс значения для удаления");
            int number = GetInt(int.MinValue, int.MaxValue);
            MusicalInstrument point = new MusicalInstrument();
            if (number >= 0 & number < list.count)
            {
                Point<MusicalInstrument> head = list.begin;
                for (int i = 0; i < number; i++)
                    head = head.Next;
                point = head.Data;
            }
            else
                point.RandomInit();
            Point<MusicalInstrument>? current = list.FindItem(point);
            MyList<MusicalInstrument> newList = new MyList<MusicalInstrument>();
            if (current == null)
            {
                Console.WriteLine("Элемент не найден.");
                return;
            }
            else
            {
                while (current != null) 
                {
                    list.RemoveItem(current.Data);
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// Клонирование списка
        /// </summary>
        /// <param name="list">список</param>
        /// <returns>склонированный список</returns>
        static MyList<MusicalInstrument> CloneList(MyList<MusicalInstrument> list)
        {
            Console.Clear();
            try
            {
                MyList<MusicalInstrument> cloneList = new MyList<MusicalInstrument>(list);
                Console.WriteLine("Список склонирован");
                return cloneList;
            }
            catch (Exception)
            {
                Console.WriteLine("Список пуст");
                return new MyList<MusicalInstrument>();
            }
        }

        /// <summary>
        /// Изменение информации в изначальном списке для проверки клонирования
        /// </summary>
        /// <param name="list">Список</param>
        static void TestCloneList(MyList<MusicalInstrument> list)
        {
            Console.Clear();
            if (list.count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            list.begin.Data.Name = "Something here";
            Guitar guitar = new Guitar();
            guitar.RandomInit();
            list.begin.Next.Data = guitar;
            Console.WriteLine("Данные в изначальном списке изменены.");
        }

        /// <summary>
        /// Удаление списка из памяти
        /// </summary>
        /// <param name="list">Список для удаления</param>
        /// <returns>Новый пустой список</returns>
        static MyList<MusicalInstrument>? DeleteList(MyList<MusicalInstrument> list)
        {
            Console.Clear();
            Point<MusicalInstrument>? current = list.begin;
            while (current != null)
            {
                list.RemoveItem(current.Data);
                current = current.Next;
            }
            Console.WriteLine("Выполнено удаление списка и отчистка памяти.");
            return new MyList<MusicalInstrument>();
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
