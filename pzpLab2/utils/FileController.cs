using System.Collections.Generic;
using System.IO;

namespace pzpLab2
{
    class FileController
    {

        public static void CreateFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath));
        }

        public static void SaveStudentFile(string fileName, List<Student> students)
        {
            Serializer.Serialize(students, fileName);
        }

        public static List<Student> OpenStudentFile(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            if (fi.Extension != ".st")
            {
                return null;
            }
            return Serializer.DeserializeMultipleStudents(fileName);
        }
    }
}
