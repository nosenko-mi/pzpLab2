using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pzpLab2.utils
{
    class StudentParser
    {

        public static string Parse(List<Student> list)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Student student in list)
            {
                builder.Append(student.ToString());
            }

            return builder.ToString();
        }
    }
}
