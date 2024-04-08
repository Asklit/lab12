using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musical_Instrument
{
    [ExcludeFromCodeCoverage]
    public class StudentsArray
    {
        private Student[] Students;
        private string[] Names = { "Sasha", "Masha", "Polina", "Stepan", "Svyatoslav", "Roman", "James" };
        static Random Rand = new Random();
        public int Length
        {
            get => Students.Length;
        }
        public Student this[int index]
        {
            get
            {
                if (index >= 0 && index < Students.Length)
                    return Students[index];
                throw new ArgumentException();
            }
            set
            {
                if (index >= 0 && index < Students.Length)
                    Students[index] = value;
                else
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Конструктор создания объекта без параметров
        /// </summary>
        public StudentsArray()
        {
            Students = Array.Empty<Student>();
        }

        /// <summary>
        /// Конструктор создания объекта с рандомными данными
        /// </summary>
        public StudentsArray(int len)
        {
            Students = new Student[len];
            for (int i = 0; i < len; i++)
            {
                Student s = new(Names[Rand.Next(0, Names.Length)], Rand.Next(18, 22), Rand.Next(0, 9) + Rand.NextDouble());
                Students[i] = s;
            }
        }

        /// <summary>
        /// Конструктор создания объекта по переданным параметрам
        /// </summary>
        public StudentsArray(int len, string[] namesArray, int[] ageArray, int[] gpaArray)
        {
            Students = new Student[len];
            for (int i = 0; i < len; i++)
            {
                Student s = new(namesArray[i], ageArray[i], gpaArray[i]);
                Students[i] = s;
            }
        }

        /// <summary>
        /// Конструктор глубокого копирования
        /// </summary>
        public StudentsArray(StudentsArray sa)
        {
            this.Students = new Student[sa.Length];
            for (int i = 0; i < sa.Students.Length; i++)
            {
                Student s = new(sa[i]);
                this.Students[i] = s;
            }
        }

        /// <summary>
        /// Получение информации о списке студентов
        /// </summary>
        public void GetInfo()
        {
            for (int i = 0; i < Students.Length; i++)
            {
                Students[i].GetInfo();
            }
        }

        /// <summary>
        /// equals metod
        /// </summary>
        /// <param name="obj">class object</param>
        public override bool Equals(Object? obj)
        {
            if (obj == null || !(obj is StudentsArray))
                return false;
            return this.Students == ((StudentsArray)obj).Students;
        }
    }
}
