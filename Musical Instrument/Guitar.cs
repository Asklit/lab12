using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musical_Instrument
{
    public class Guitar : MusicalInstrument
    {
        /// <summary>
        /// Количество струн
        /// </summary>
        private int _CountString;
        public int CountString
        {
            get => _CountString;
            set
            {
                if (value < 0)
                    _CountString = 0;
                else
                    _CountString = value;
            }
        }

        /// <summary>
        /// Конструкторы без параметров
        /// </summary>
        public Guitar() : base() => CountString = 0;


        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public Guitar(string name, int countString, int number) : base(name, number) => this.CountString = countString;

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Виртуальный метод вывода информации и объекте
        /// </summary>
        public override void ShowVirtualMethod()
        {
            base.ShowVirtualMethod();
            Console.WriteLine($"Количество струн гитары: {CountString}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Обычный метод вывода информации и объекте
        /// </summary>
        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Количество струн гитары: {CountString}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров с кавиатуры
        /// </summary>
        public override void Init()
        {
            base.Init();

            Console.WriteLine("Введите количество струн:");
            try
            {
                CountString = int.Parse(Console.ReadLine());
            }
            catch
            {
                CountString = -1;
            }
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров рандомно
        /// </summary>
        public override void RandomInit()
        {
            base.RandomInit();
            CountString = rand.Next(0, 10);
        }

        /// <summary>
        /// Переопределение equals
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Guitar && base.Equals(obj))
                return this.CountString == ((Guitar)obj).CountString;
            return false;
        }

        /// <summary>
        /// Переопределение tostring
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Количество струн {CountString}";
        }

        /// <summary>
        /// Определение метода клонирования элементов класса
        /// </summary>
        public override object Clone()
        {
            return new Guitar(Name, CountString, Id.Number);
        }


        /// <summary>
        /// Определение метода копирования элементов класса
        /// </summary>
        public override object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }
}
