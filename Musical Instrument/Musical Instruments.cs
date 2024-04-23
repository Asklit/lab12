using System.Diagnostics.CodeAnalysis;

namespace Musical_Instrument
{
    public class IdNumber
    {
        /// <summary>
        /// Номер ID
        /// </summary>
        private int _Number;
        public int Number
        {
            get => _Number;
            set
            {
                if (value < 0)
                    _Number = 0;
                else
                    _Number = value;
            }
        }

        /// <summary>
        /// Конструкторы без параметров
        /// </summary>
        public IdNumber() => Number = 0;

        /// <summary>
        /// Конструкторы с параметрами
        /// </summary>
        public IdNumber(int number) => Number = number;

        /// <summary>
        /// Переопределение метода equals
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is IdNumber)
                return this.Number == ((IdNumber)obj).Number;
            return false;
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Переопределение метода tostring
        /// </summary>
        public override string ToString()
        {
            return $"id: {Number}";
        }

        public override int GetHashCode()
        {
            int hash = 84664589;
            return 84664589 ^ Number;
        }
    }

    public class MusicalInstrument : IInit, IComparable, ICloneable
    {
        /// <summary>
        /// Имена для рандомного заполнения класса
        /// </summary>
        protected string[] arrNames = { "Guitar", "Pianino", "ElectricGuitar" };

        /// <summary>
        /// Название музыкального инструмента
        /// </summary>
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    _Name = "NoName"; 
                else
                    _Name = value;
            }
        }

        /// <summary>
        /// id Музыкального инструмента
        /// </summary>
        public IdNumber Id;

        protected Random rand = new Random();

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MusicalInstrument()
        {
            Name = "NoName";
            Id = new IdNumber(1);
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        public MusicalInstrument(string name, int number)
        {
            this.Name = name;
            Id = new IdNumber(number);
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Виртуальный метод вывода информации и объекте
        /// </summary>
        public virtual void ShowVirtualMethod()
        {
            Console.WriteLine($"{Id}. Название музыкального инструмента: {this.Name}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Обычный метод вывода информации и объекте
        /// </summary>
        public void Show()
        {
            Console.WriteLine($"{Id}. Название музыкального инструмента: {this.Name}");
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров с кавиатуры
        /// </summary>
        public virtual void Init()
        {
            Console.WriteLine("Введите название инструмента:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите id инструмента:");
            try
            {
                Id.Number = int.Parse(Console.ReadLine());
            }
            catch
            {
                Id.Number = -1;
            }
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Метод заполения параметров рандомно
        /// </summary>
        public virtual void RandomInit()
        {
            Name = arrNames[rand.Next(0, arrNames.Length)];
            Id.Number = rand.Next(1, 10000);
        }

        /// <summary>
        /// Переопределение equals
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is MusicalInstrument)
                return this.Name == ((MusicalInstrument)obj).Name && this.Id.Number == ((MusicalInstrument)obj).Id.Number;
            return false;
        }

        /// <summary>
        /// Переопределение tostring
        /// </summary>
        public override string ToString()
        {
            return $"{Id}. {Name}";
        }

        /// <summary>
        /// Определение метода сравнения элементов класса
        /// </summary>
        public virtual int CompareTo(object? obj)
        {
            if (obj == null) return -2;
            if (obj is not MusicalInstrument) return -2;
            if (this.Id.Number == ((MusicalInstrument)obj).Id.Number)
                return 0;
            return this.Id.Number > ((MusicalInstrument)obj).Id.Number ? 1 : -1;
        }

        /// <summary>
        /// Определение метода клонирования элементов класса
        /// </summary>
        public virtual object Clone()
        {
            return new MusicalInstrument(Name, Id.Number);
        }

        /// <summary>
        /// Определение метода копирования элементов класса
        /// </summary>
        public virtual object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public override int GetHashCode()
        {
            int hash = 12345678;
            return Math.Abs(hash ^ Id.GetHashCode() ^ Name.GetHashCode());
        }
    }
}
