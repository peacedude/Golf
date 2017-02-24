using System;
using System.Collections.Generic;

namespace Golf
{
    internal class Course
    {
        private static int Distance { get; set; }
        public static Dictionary<int, int> CourseDict = new Dictionary<int, int>();
        private static readonly Random Rnd = new Random(Guid.NewGuid().GetHashCode());

        public static void CreateCourses(int amountOfCourses)
        {
            for (var i = 0; i < amountOfCourses; i++)
            {
                Distance = Rnd.Next(1600, 2900);
                CourseDict.Add(i, Distance);
            }
        }

        public static int GetCurrentCourseDistance(int idx)
        {
            return CourseDict[idx];
        }

    }
}
