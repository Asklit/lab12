using lab12._2;
using Musical_Instrument;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab12_2
{
    internal class MyHashtable<TKey, TValue> where TValue : IInit, ICloneable, new()
    {
        /// <summary>
        /// Массив значений
        /// </summary>
        TValue[] tableValue;

        HashItem<TKey>[] HashItem;

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

        public int Capacity => tableValue.Length;
        public int Count => count;

        /// <summary>
        /// Создание хештаблицы с заданной длиной
        /// </summary>
        /// <param name="size"></param>
        /// <param name="fillRatio"></param>
        public MyHashtable(int size=10, double fillRatio=0.72) 
        {
            tableValue = new TValue[size];
            HashItem = new HashItem<TKey>[size];
            deletedFlag = new bool[size];
            this.fillRatio = fillRatio;
        }

        /// <summary>
        /// Вывод информации элементов
        /// </summary>
        public void Print()
        {
            if (count == 0) Console.WriteLine("Таблица пустая");
            else
            {
                for (int i = 0; i < tableValue.Length; i++)
                {
                    if (tableValue[i] != null)
                        Console.WriteLine($"{i + 1}. HashCode: {HashItem[i].HashCode} Value: {tableValue[i]}");
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
        int GetIndex(TKey Key) => Math.Abs(Key.GetHashCode()) % Capacity;

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
            if (tableValue[index] != null)
            {
                // Ищем место и идем до конца таблицы
                while (current < tableValue.Length && tableValue[current] != null)
                    current++;
                if (current == tableValue.Length)
                {
                    // Идем с начала таблицы
                    current = 0;
                    while (current < index && tableValue[current] != null)
                        current++;
                    if (current == index) throw new Exception("Нет места в таблице");
                }
            }
            // Нашли место и добавляем элемент
            tableValue[current] = (TValue)value.Clone();
            HashItem<TKey> hash = new HashItem<TKey>(key);
            HashItem[current] = hash;
            deletedFlag[current] = true;
            count++;
        }
        
        /// <summary>
        /// Проверка наличия элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Наличие/отсутствие элемента</returns>
        public bool Contains(TKey key) => tableValue[GetIndex(key)] != null;

        /// <summary>
        /// Поиск ключа по ID
        /// </summary>
        /// <param name="id">id элемента</param>
        /// <returns>ключ/null</returns>
        /// <exception cref="Exception">Исплючение вызванное отсутствие ID у TKey</exception>
        public TKey? FindKeyByHashCode(int hashcode)
        {

            for (int i = 0; i < Capacity; i++)
            {
                if (HashItem[i] != null)
                {
                    if (HashItem[i].HashCode == hashcode)
                        return HashItem[i].Key;
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
            if (tableValue[index] != null)
            {
                count--;
                tableValue[index] = default;
                HashItem[index] = default;
                deletedFlag[index] = false;
            }
            else if (deletedFlag[index] == false)
            {
                int current = index;
                // Ищем место и идем до конца таблицы
                while (current < tableValue.Length && HashItem[current].Key.Equals(key))
                    current++;
                if (current == tableValue.Length)
                {
                    // Идем с начала таблицы
                    current = 0;
                    while (current < index && HashItem[current].Key.Equals(key))
                        current++;
                    if (current == index) return false;
                }
                else
                {
                    count--;
                    tableValue[current] = default;
                    HashItem[current].Key = default;
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
                TValue[] tempValue = tableValue;
                HashItem<TKey>[] tempKey = HashItem;
                tableValue = new TValue[tempValue.Length * 2];
                HashItem = new HashItem<TKey>[tempKey.Length * 2];
                deletedFlag = new bool[tempKey.Length * 2];
                count = 0;
                for (int i = 0; i < tempKey.Length; i++)
                {
                    if (tempKey[i] != null)
                        AddItem(tempKey[i].Key, tempValue[i]);
                }
            }
            AddItem(key, value);
        }
    }
}
