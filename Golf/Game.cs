using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    #region Game Class
    class Game
    {
        #region/*-----------Constant Variables-------------*/
        private const int MAX_ANGLE = 90;
        private const int MIN_ANGLE = 1;
        private const int MAX_VELOCITY = 200;
        private const int MIN_VELOCITY = 1;
        private const int START_TRIES = 20;
        private const int MAX_COURSES = 1;
        #endregion

        #region/*-----------Get/Set Methods----------------*/
        private int Club { get; set; }
        private int TriesLeft { get; set; }
        private int HitID { get; set; }
        private int StartDistance { get; set; }
        private int CourseID { get; set; }
        private int FinalScore { get; set; }
        private double DistanceLeft { get; set; }
        private double Angle { get; set; }
        private double Velocity { get; set; }
        private double DistanceHit { get; set; }
        private bool GameLoop { get; set; }
        private bool InsideGameLoop { get; set; }
        #endregion



        #region/*-----------Constructors-------------------*/
        List<string> resultList = new List<string>();
        List<string> finalList = new List<string>();
        GolfSwing golfSwing = new GolfSwing();
        Random rnd = new Random();
        #endregion


        #region/*-----------Void methods-------------------*/
        /// <summary>
        /// Starts the game loop.
        /// </summary>
        public void StartGame()
        {
            Course.CreateCourses(MAX_COURSES + 1);
            GameLoop = true;
            Console.WriteLine("Welcome to the golf simulator!");
            while (GameLoop == true)
            {
                if (CourseID == MAX_COURSES)
                {
                    Console.Clear();
                    GetFinalResult();
                    GameLoop = false;
                    Console.ReadKey();
                    break;
                }

                else
                {
                    SetCourse();
                    while (InsideGameLoop == true)
                    {
                        Console.WriteLine(GetStartMessage());

                        CheckTries();

                        SetClub();

                        SetUserVelocity();

                        SetUserAngle();

                        DoSwing();
                    }
                }
            }
        }

        ///<summary>
        ///Create a course and set start values
        /// </summary>
        private void SetCourse()
        {
            if(CourseID < MAX_COURSES)
                CourseID++;
            InsideGameLoop = true;
            TriesLeft = START_TRIES;
            HitID = 0;
            StartDistance = Course.GetCurrentCourseDistance(CourseID);
            DistanceLeft = StartDistance;
        }

        /// <summary>
        /// Checks if the user has any tries left. Throws exception if you don't.
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when TriesLeft is less or equal to 0.</exception>
        /// <summary>
        private void CheckTries()
        {
            if (TriesLeft <= 0)
            {
                ArgumentException argEx = new ArgumentException("You used all your tries.");
                throw argEx;
            }
        }

        /// Wait for userinput (1-3) to set Club and then gives feedback on which club he picked.
        /// </summary>
        private void SetClub()
        {
            Console.WriteLine("\nPlease choose a golf club:\n1. Putter\n2. Iron\n3. Driver");
            bool clubLoop = true;
            while (clubLoop == true)
            {
                ConsoleKey KeyPressed = Console.ReadKey().Key;
                switch (KeyPressed)
                {
                    case ConsoleKey.D1:
                        Club = 1;
                        clubLoop = false;
                        break;
                    case ConsoleKey.D2:
                        Club = 2;
                        clubLoop = false;
                        break;
                    case ConsoleKey.D3:
                        Club = 3;
                        clubLoop = false;
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("You choosed the {0}", GetClub(Club));
        }

        /// <summary>
        /// Waits for user to input a value between MIN_VELOCITY and MAX_VELOCITY.
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when Input is null, char or whitespace.</exception>
        private void SetUserVelocity()
        {

            while (true)
            {
                Console.Write("\nPlease enter the amount of force({0}-{1}): ", MIN_VELOCITY, MAX_VELOCITY);

                // Check if input is valid

                try
                {
                    try
                    {
                        Velocity = double.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException("Input can't be char or whitespace", "velocity");
                    }
                }
                catch (Exception a)
                {
                    throw new ArgumentException("","");
                }
                


                if (Velocity < MIN_VELOCITY || Velocity > MAX_VELOCITY)
                {
                    Console.WriteLine("\nPlease choose a velocity between {0} and {1}", MIN_VELOCITY, MAX_VELOCITY);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Wait for user to input a value between MIN_ANGLE and MAX_ANGLE.
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when Input is null, char or whitespace.</exception>
        private void SetUserAngle()
        {
            while (true)
            {
                Console.Write("Please enter the angle({0}-{1}): ", MIN_ANGLE, MAX_ANGLE);

                // Check if input is valid
                try
                {
                    Angle = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ArgumentException argEx = new ArgumentException("Input can't be null, char or whitespace", "angle");
                    throw argEx;
                }
                if (Angle < MIN_ANGLE || Angle > MAX_ANGLE)
                {
                    Console.WriteLine("\nPlease choose an angle between {0} and {1}", MIN_ANGLE, MAX_ANGLE);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Takes the Angle and Velocity then calculates a swing. Gives feedback about distance traveled, golf club and number of tries left.
        /// </summary>
        private void DoSwing()
        {
            Console.Clear();
            DistanceHit = Math.Round(golfSwing.CalculateSwing(Velocity * GetClubModifier(Club), Angle));
            TriesLeft--;
            Console.WriteLine("The ball traveled {0}m using the {1}. Number of tries: {2}/{3}", DistanceHit, GetClub(Club), START_TRIES - TriesLeft, START_TRIES);
            DistanceLeft -= DistanceHit;

            AddStatsToList();

            CheckTheBall();

        }

        /// <summary>
        /// Adds Velocity, Angle, DistanceHit and Club name to the resultlist.
        /// </summary>
        private void AddStatsToList()
        {
            HitID++;
            resultList.Add(string.Format("\nHit #{0} Velocity: {1} Angle: {2} Distance traveled: {3} Club used: {4}  \n", HitID, Velocity, Angle, DistanceHit, GetClub(Club)));
        }

        private void AddFinalStatsToList()
        {
            finalList.Add(string.Format("\nCourse #{0}: Number of tries: {1} Distance: {2}", CourseID, HitID, StartDistance));
        }

        /// <summary>
        /// Checks if ball went into the hole or went beyond the hole. 
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when ball gets too far away from target.</exception>
        private void CheckTheBall()
        {
            // Check if you hit the hole
            if (DistanceLeft == 0)
            {
                InsideGameLoop = false;
                Console.WriteLine("You completed course #{0}!", CourseID);
                Console.WriteLine(GetCourseResult());
                HitID += FinalScore;
                Console.ReadKey(true);
                resultList.Clear();
            }

            // Set the distance left to a positive value if you went past the hole
            DistanceLeft = GetPositiveDistance(DistanceLeft);

            // Throw exceptions if you went too far away from the hole
            if (DistanceLeft > StartDistance + 1000)
            {
                ArgumentException argEx = new ArgumentException(string.Format("You went too far away from the target. Value: " + DistanceLeft));
                throw argEx;
            }



            // Display distance left
            if (DistanceLeft > 0 || TriesLeft > 0)
            {
                Console.WriteLine("You got {0}m left to the hole", DistanceLeft);
            }
        }
        #endregion

        #region /*-----------Return methods-----------------*/
        /// <summary>
        /// Gets a welcome message and the course distance
        /// </summary>
        /// <returns>Returns a welcome message the the course distance.</returns>
        private string GetStartMessage()
        {
            return string.Format("Distance of course #{0} is {1}m\n", CourseID, StartDistance);
        }

        /// <summary>
        ///     Get name of the Club. Throws ArgumentException if Index is out of range.
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when Input out of range.</exception>
        /// <returns>Returns golfClub[index - 1]</returns>
        private string GetClub(int index)
        {
            string[] golfClub;
            golfClub = new string[3];
            golfClub[0] = "Putter";
            golfClub[1] = "Iron";
            golfClub[2] = "Driver";
            try
            {
                return golfClub[index - 1];
            }
            catch (IndexOutOfRangeException ex)
            {
                ArgumentException argEx = new System.ArgumentException(string.Format("Index is out of range. Value: " + index), "index", ex);
                throw argEx;
            }
        }

        /// <summary>
        /// Get Club Modifier
        /// </summary>
        /// <exception cref="ArgumentException">Throw ArgumentException when Input out of range.</exception>
        /// <returns>Returns golfClub[index - 1]</returns>
        private double GetClubModifier(int index)
        {
            double[] golfClub;
            golfClub = new double[3];
            golfClub[0] = 0.5;
            golfClub[1] = 1;
            golfClub[2] = 1.5;
            try
            {
                return golfClub[index - 1];
            }
            catch (IndexOutOfRangeException ex)
            {
                ArgumentException argEx = new System.ArgumentException(string.Format("Index is out of range. Value: " + index), "index", ex);
                throw argEx;
            }
        }

        /// <summary>
        /// Takes the DistanceLeft value and returns it as positive if it is negative.
        /// </summary>
        /// <param name="distanceLeft"></param>
        /// <returns></returns>
        private double GetPositiveDistance(double distanceLeft)
        {
            double trueDistance;
            if (DistanceLeft < 0)
            {
                trueDistance = distanceLeft - distanceLeft * 2;
                return trueDistance;
            }
            else
            {
                return distanceLeft;
            }
        }

        /// <summary>
        /// Get course results in a string.
        /// </summary>
        /// <returns>Returns course results in a string</returns>
        private string GetCourseResult()
        {
            string result = "";

            var query = from hit in resultList
                        orderby hit.Length descending
                        select hit;
            foreach (var hit in query)
            {
                result += hit;
            }
            return string.Format("\nStatistics for course #{0}: {1}", CourseID, result);
        }

        private string GetFinalResult()
        {
            string result = "";
            var query = from course in resultList
                        orderby course.Length descending
                        select course;
            foreach (var course in query)
            {
                result += course;
            }
            return string.Format("Statistics: \n{0} Final Score: {1} (Lower is better)", result, FinalScore);
        }
        #endregion
    }
    #endregion
}
