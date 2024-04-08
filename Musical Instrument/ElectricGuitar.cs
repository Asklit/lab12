using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Musical_Instrument
{
    public class ElectricGuitar : Guitar
    {
        protected string[] arrSource = { "USB", "Батарейки", "Аккамуляторы" };
        /// <summary>
        /// Источник питания
        /// </summary>
        private string _EnetgySource;
        public string EnergySource
        {
            get => _EnetgySource;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _EnetgySource = "NoSource";
                else
                    _EnetgySource = value;
            }
        }

        /// <summary>
        /// Конструкторы без параметров
        /// </summary>
        public ElectricGuitar() : base() => EnergySource = "NoSource";

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public ElectricGuitar(string name, int countString, string source, int number) : base(name, countString, number) => EnergySource = source;

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Виртуальный метод вывода информации и объекте
        /// </summary>
        public override void ShowVirtualMethod()
        {
            base.ShowVirtualMethod();
            Console.WriteLine($"Источник питания гитары: {EnergySource}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Обычный метод вывода информации и объекте
        /// </summary>
        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Источник питания гитары: {EnergySource}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров с кавиатуры
        /// </summary>
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите источник питания клавиатуры:");

            EnergySource = Console.ReadLine();
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров рандомно
        /// </summary>
        public override void RandomInit()
        {
            base.RandomInit();
            EnergySource = arrSource[rand.Next(0, arrSource.Length)];
        }
        
        /// <summary>
        /// Переопределение equals
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is ElectricGuitar && base.Equals(obj))
                return this.EnergySource == ((ElectricGuitar)obj).EnergySource;
            return false;
        }

        /// <summary>
        /// Переопределение tostring
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Источник питания {EnergySource}";
        }

        /// <summary>
        /// Колонирование объекта
        /// </summary>
        public override object Clone()
        {
            return new ElectricGuitar(Name, CountString, EnergySource, Id.Number);
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
