using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class Course
    {
        static private int Distance { get; set; }
        static private int CourseID { get; set; }
        static public Dictionary<int, int> CourseDict = new Dictionary<int, int>();
        static private Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public static void CreateCourses(int amountOfCourses)
        {
            for (int i = 0; i < amountOfCourses; i++)
            {
                Distance = rnd.Next(1600, 2900);
                CourseDict.Add(i, Distance);
            }
        }

        public static int GetCurrentCourseDistance(int idx)
        {
            return CourseDict[idx];
        }

    }
}
