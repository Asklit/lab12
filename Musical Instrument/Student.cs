using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musical_Instrument
{
    [ExcludeFromCodeCoverage]
    public class Student : IInit
    {
        private string[] Names = { "Sasha", "Masha", "Polina", "Stepan", "Svyatoslav", "Roman", "James" };
        private Random rand = new Random();
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (value == null || value == "")
                    _Name = "NoName";
                else
                    _Name = value;
            }
        }

        private int _Age;
        public int Age
        {
            get => _Age;
            set
            {
                if (value < 10 || value > 100)
                    _Age = -1;
                else
                    _Age = value;
            }
        }
        private double _Gpa;
        public double Gpa
        {
            get => _Gpa;
            set
            {
                if (value < 0 || value > 10)
                    _Gpa = -1;
                else
                    _Gpa = value;
            }
        }
        static int count = 0;

        /// <summary>
        /// Конструктор создания объекта без параметров
        /// </summary>
        public Student()
        {
            _Name = "NoName";
            _Age = -1;
            _Gpa = -1;
            count++;
        }

        /// <summary>
        /// Конструктор создания объекта по переданным параметрам
        /// </summary>
        public Student(string name, int age, double gpa)
        {
            this.Name = name;
            this.Age = age;
            this.Gpa = gpa;
            count++;
        }

        /// <summary>
        /// Конструктор голубокого копирования объекта
        /// </summary>
        public Student(Student s)
        {
            this.Name = s.Name;
            this.Age = s.Age;
            this.Gpa = s.Gpa;
            count++;
        }

        /// <summary>
        /// Сравнение студентов по возрасту и GPA
        /// </summary>
        /// <param name="Student">Студент для сравнения с текущим объектом класса</param>
        public void Compare(Student Student)
        {
            if (this.Age > Student.Age)
                Console.WriteLine($"{this.Name} старше {Student.Name}");
            else if (this.Age < Student.Age)
                Console.WriteLine($"GPA {this.Name} младше {Student.Name}");
            else
                Console.WriteLine($"GPA {this.Name} ровесник {Student.Name}");

            if (this.Gpa > Student.Gpa)
                Console.WriteLine($"GPA {this.Name} выше GPA {Student.Name}");
            else if (this.Gpa < Student._Gpa)
                Console.WriteLine($"GPA {this.Name} ниже GPA {Student.Name}");
            else
                Console.WriteLine($"GPA {this.Name} равен GPA {Student.Name}");
        }

        /// <summary>
        /// Сравнение студентов по возрасту и GPA
        /// </summary>
        /// <param name="firstStudent">Студент для сравнения 1</param>
        /// <param name="secoundStudent">Студент для сравнения 2</param>
        public static void CompareStudents(Student firstStudent, Student secoundStudent)
        {
            if (firstStudent.Age > secoundStudent.Age)
                Console.WriteLine($"{firstStudent.Name} старше {secoundStudent.Name}");
            else if (firstStudent.Age < secoundStudent.Age)
                Console.WriteLine($"GPA {firstStudent.Name} младше {secoundStudent.Name}");
            else
                Console.WriteLine($"GPA {firstStudent.Name} ровесник {secoundStudent.Name}");

            if (firstStudent.Gpa > secoundStudent.Gpa)
                Console.WriteLine($"GPA {firstStudent.Name} выше GPA {secoundStudent.Name}");
            else if (firstStudent._Gpa < secoundStudent._Gpa)
                Console.WriteLine($"GPA {firstStudent.Name} ниже GPA {secoundStudent.Name}");
            else
                Console.WriteLine($"GPA {firstStudent.Name} равен GPA {secoundStudent.Name}");
        }

        /// <summary>
        /// Получение информации о студенте
        /// </summary>
        public void GetInfo()
        {
            Console.WriteLine($"Имя студента: {Name}. Возраст студента: {Age}. Средняя оценка студента: {Gpa}.");
        }

        /// <summary>
        /// Получение общего количества студентов
        /// </summary>
        /// <returns>Количество студентов</returns>
        public static int GetCountStudents()
        {
            return count;
        }

        /// <summary>
        /// Форматирование имени
        /// </summary>
        /// <param name="s">Студент</param>
        /// <returns>Студент с отформатированным именем</returns>
        public static Student operator ~(Student s)
        {
            Student newStudent = s;
            newStudent.Name = newStudent.Name.ToLower();
            newStudent.Name = char.ToUpper(newStudent.Name[0]) + newStudent.Name.Substring(1);
            return newStudent;
        }

        /// <summary>
        /// Увеличение возраста студента
        /// </summary>
        /// <param name="s">Студент</param>
        /// <returns>Студент с отформатированным именем</returns>
        public static Student operator ++(Student s)
        {
            Student newStudent = s;
            newStudent.Age++;
            return newStudent;
        }

        /// <summary>
        /// Создание нового студента с новым именем
        /// </summary>
        /// <param name="s">Студент для копирования</param>
        /// <param name="newName">Новое имя студента</param>
        /// <returns>Новый студент с такими же параметрами кроме имени</returns>
        public static Student operator %(Student s, string newName)
        {
            return new Student(newName, s.Age, s.Gpa);
        }

        /// <summary>
        /// Уменьшение GPA
        /// </summary>
        /// <param name="s">Студент</param>
        /// <param name="d">Дельта уменьшения</param>
        /// <returns></returns>
        public static Student operator -(Student s, double d)
        {
            Student newStudent = s;
            if (newStudent.Gpa - d < 0)
                newStudent.Gpa = 0;
            else if (newStudent.Gpa - d > 10)
                newStudent.Gpa = 10;
            else
                newStudent.Gpa -= d;
            return newStudent;
        }

        /// <summary>
        /// Проверка на положительную успеваемость студента
        /// </summary>
        /// <param name="s">Студент</param>
        public static implicit operator bool(Student s)
        {
            return s.Gpa > 6;
        }

        /// <summary>
        /// Получение номера курса студента
        /// </summary>
        /// <param name="s">Студент</param>
        public static explicit operator int(Student s)
        {
            return s.Age < 18 || s.Age > 22 ? -1 : s.Age - 17;
        }

        /// <summary>
        /// equals metod
        /// </summary>
        /// <param name="obj">class object</param>
        public override bool Equals(Object? obj)
        {
            if (obj == null || !(obj is Student))
                return false;
            return this.Age == ((Student)obj).Age && this.Name == ((Student)obj).Name && this.Gpa == ((Student)obj).Gpa;
        }

        /// <summary>
        /// Метод заполения параметров с кавиатуры
        /// </summary>
        public virtual void Init()
        {
            Console.WriteLine("Введите имя студента:");
            Name = Console.ReadLine();

            Console.WriteLine("Введите возраст студента:");
            try
            {
                Age = int.Parse(Console.ReadLine());
            }
            catch
            {
                Age = -1;
            }

            Console.WriteLine("Введите GPA студента:");
            try
            {
                Gpa = int.Parse(Console.ReadLine());
            }
            catch
            {
                Gpa = -1;
            }
        }

        /// <summary>
        /// Метод заполения параметров рандомно
        /// </summary>
        public virtual void RandomInit()
        {
            Name = Names[rand.Next(0, Names.Length)];
            Age = rand.Next(18, 22);
            Gpa = rand.Next(0, 10);
        }

        /// <summary>
        /// Переопределение tostring
        /// </summary>
        public override string ToString()
        {
            return $"Имя студента: {Name}. Возраст студента: {Age}. Средняя оценка студента: {Gpa}.";
        }
    }
}
