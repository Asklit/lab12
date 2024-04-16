﻿using lab12._2;
using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab12_2
{
    public class MyHashtable<TKey, TValue> where TValue : IInit, ICloneable, new()
    {
        public Item<TKey, TValue>[] Items;

        /// <summary>
        /// Массив флагов для удаления элементов
        /// </summary>
        bool[] deletedFlag;

        /// <summary>
        /// Количество элементов
        /// </summary>

        int count = 0;

        /// <summary>
        /// Максимальная заполненность
        /// </summary>
        double fillRatio;

        public int Capacity => Items.Length;
        public int Count => count;

        /// <summary>
        /// Создание хештаблицы с заданной длиной
        /// </summary>
        /// <param name="size"></param>
        /// <param name="fillRatio"></param>
        public MyHashtable(int size=10, double fillRatio=0.72) 
        {
            Items = new Item<TKey, TValue>[size];
            deletedFlag = new bool[size];
            this.fillRatio = fillRatio;
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Вывод информации элементов
        /// </summary>
        public void Print()
        {
            if (count == 0) Console.WriteLine("Таблица пустая");
            else
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                        Console.WriteLine($"{i + 1}. NumberInHashTable: {GetIndex(Items[i].Key) + 1} Key: {Items[i].Key} Value: {Items[i].Value}");
                    else
                        Console.WriteLine($"{i + 1}.");
                }
            }
        }

        /// <summary>
        /// Получение индекса элемента в таблице по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Индекс ключа в хештаблице</returns>
        public int GetIndex(TKey Key) => Math.Abs(Key.GetHashCode()) % Capacity;

        /// <summary>
        /// Добавление значения в хештаблицу с учетом колиззии
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение для добавления</param>
        /// <exception cref="Exception">Исключение: Таблица заполнена</exception>
        void AddItem(TKey key, TValue value)
        {
            if (value == null) return;

            int index = GetIndex(key);
            int current = index;
            if (Items[index] != null)
            {
                // Ищем место и идем до конца таблицы
                while (current < Items.Length && Items[current] != null)
                    current++;
                if (current == Items.Length)
                {
                    // Идем с начала таблицы
                    current = 0;
                    while (current < index && Items[current] != null)
                        current++;
                    if (current == index) throw new Exception("Нет места в таблице");
                }
            }
            // Нашли место и добавляем элемент
            Item<TKey, TValue> item = new Item<TKey, TValue>(key, value);
            Items[current] = item;
            deletedFlag[current] = true;
            count++;
        }
        
        /// <summary>
        /// Проверка наличия элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Наличие/отсутствие элемента</returns>
        public bool Contains(TKey key) => Items[GetIndex(key)] != null;

        /// <summary>
        /// Поиск ключа по ID
        /// </summary>
        /// <param name="id">id элемента</param>
        /// <returns>ключ/null</returns>
        /// <exception cref="Exception">Исплючение вызванное отсутствие ID у TKey</exception>
        public TKey? FindKeyByIDCode(int id)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (Items[i] != null)
                {
                    if (Items[i].Key is MusicalInstrument)
                        if ((Items[i].Key as MusicalInstrument).Id.Number == id)
                            return Items[i].Key;
                }
            }
            return default;
        }

        /// <summary>
        /// Удаление значения по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>true/false, удален элемент или нет</returns>
        public bool RemoveData(TKey key)
        {
            int index = GetIndex(key);
            if (index < 0) return false;
            if (Items[index] != null)
            {
                count--;
                Items[index] = default;
                deletedFlag[index] = false;
            }
            else if (deletedFlag[index] == false)
            {
                int current = index;
                // Ищем место и идем до конца таблицы
                while (current < Items.Length)
                {
                    if (Items[current] != null)
                    {
                        if (GetIndex(key) == GetIndex(Items[current].Key))
                            break;
                    }
                    current++;
                }
                if (current == Items.Length)
                {
                    // Идем с начала таблицы
                    current = 0;
                    while (current < Items.Length)
                    {
                        if (Items[current] != null)
                        {
                            if (GetIndex(key) == GetIndex(Items[current].Key))
                                break;
                        }
                        current++;
                    }
                    if (current == index) return false;
                }
                else
                {
                    count--;
                    Items[current] = default;
                    deletedFlag[current] = false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// Добавление элемента (Уделичение длины хештаблицы при добавлении элемента) и вызов функции добавления
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public void AddData(TKey key, TValue value) 
        {
            if ((double)Count / Capacity > fillRatio)
            {
                Item<TKey, TValue>[] tempItems = Items;
                Items = new Item<TKey, TValue>[tempItems.Length * 2];
                deletedFlag = new bool[Items.Length * 2];
                count = 0;
                for (int i = 0; i < tempItems.Length; i++)
                {
                    if (tempItems[i] != null)
                        AddItem(tempItems[i].Key, tempItems[i].Value);
                }
            }
            AddItem(key, value);
        }
    }
}