using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace pzpLab2
{
    class Serializer
    {

        public static bool Serialize(ISerializable obj, string file)
        {
            Stream stream = File.Open(file, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, obj);
            stream.Close();

            return true;
        }

        public static bool Serialize(List<Student> objects, string file)
        {
            Stream stream = File.Open(file, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, objects);

            stream.Close();

            return true;
        }

        public static Student Deserialize(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Student student = (Student)bf.Deserialize(stream);

                    return student;
                }
            }
            catch (IOException)
            {
                return null;
            }

            //Clear mp for further usage.
            //Open the file written above and read values from it.
            //Stream stream = File.Open(file, FileMode.Open);
            //BinaryFormatter bformatter = new BinaryFormatter();
            //Console.WriteLine("Reading Employee Information");
            //Student student = (Student)bformatter.Deserialize(stream);
            //stream.Flush();
            //stream.Close();
            //return student;
        }

        public static List<Student> DeserializeMultipleStudents(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    stream.Position = 0;
                    List<Student> students = (List<Student>)bf.Deserialize(stream);

                    return students;
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

    }
}
