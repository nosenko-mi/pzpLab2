using System.Collections.Generic;
using System.Linq;

namespace pzpLab2.utils
{
    static class StudentSortExtension
    {
        public static List<Student> GetYoungestAndHighestGpa(this List<Student> students)
        {
            int maxYob = students[0].Yob;
            foreach (Student student in students.Skip(1)    )
            {
                if (student.Yob > maxYob)
                {
                    maxYob = student.Yob;
                }
            }

            List<Student> youngests = students.FindAll(s => s.Yob == maxYob).ToList();

            float maxGpa = youngests[0].Gpa;
            foreach (Student student in youngests.Skip(1))
            {
                // student.Gpa > maxYob
                if (student.Gpa > maxGpa)
                {
                    maxGpa = student.Gpa;
                }
            }

            List<Student> smartest = youngests.FindAll(s => s.Gpa == maxGpa).ToList();

            return smartest;
        }

        public static List<Student> SortByNameAndGroup(this List<Student> students)
        {
            List<Student> sortedTeams = students.OrderBy(t => t.Name)
                    .ThenBy(t => t.Group).ToList();

            return sortedTeams;
        }

        public static List<Student> SortByGroupAndGpa(this List<Student> students)
        {
            students.Sort(new StudentComparer());
            return students;
        }
    }


}
