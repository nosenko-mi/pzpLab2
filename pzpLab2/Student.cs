using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Вхідний файл містить інформацію про студентів деякого факультету:
//long nomer; // номер запису в файлі
//char name[20]; // прізвище
//int year; // рік народження
//char group[20]; // група
//float sred; // середній бал
//Потрібно у вихідний файл записати відомості про тих студентів, які серед наймолодших мають максимальний середній бал.


namespace pzpLab2
{

    class StudentComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            int ret = String.Compare(x.Group, y.Group);
            // Compare y to x to sort by descending, x to y otherwise
            return ret != 0 ? ret : y.Gpa.CompareTo(x.Gpa);
        }
    }
    
    [Serializable()]
    class Student : ISerializable
    {
        private static readonly Random randomInstance = new Random();

        public long? Id { get; set; }
        public string Name { get; set; }
        public int Yob { get; set; }
        public string Group { get; set; }
        public float Gpa { get; set; }

        public Student()
        {
            Id = null;
            Name = "";
            Yob = -1;
            Group = "";
            Gpa = -1;
        }

        public Student(string name, int yob, string group, float gpa)
        {
            Id = RandomLong();
            Name = name;
            Yob = yob;
            Group = group;
            Gpa = gpa;
        }

        private long RandomLong()
        {
            lock (randomInstance)
            {
                byte[] bytes = new byte[8];
                randomInstance.NextBytes(bytes);
                return BitConverter.ToInt64(bytes, 0);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Name", Name);
            info.AddValue("Yob", Yob);
            info.AddValue("Group", Group);
            info.AddValue("Gpa", Gpa);
        }

        public Student(SerializationInfo info, StreamingContext context)
        {
            Id = (long?)info.GetValue("Id", typeof(long?));
            Name = (string)info.GetValue("Name", typeof(string));
            Yob = (int)info.GetValue("Yob", typeof(int));
            Group = (string)info.GetValue("Group", typeof(string));
            Gpa = (float)info.GetValue("Gpa", typeof(float));
        }
        public override string ToString()
        {
            return $"Student: \n\tId: {Id};\n\tName: {Name};\n\tYob: {Yob};\n\tGroup: {Group};\n\tGPA: {Gpa};\n";
        }

    }
}
