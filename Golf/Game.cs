using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class Game
    {
        private const int MAX_ANGLE = 90;
        private const int MIN_ANGLE = 1;
        private const int MAX_VELOCITY = 200;
        private const int MIN_VELOCITY = 1;
        private const int START_TRIES = 20;

        int club;

        public void StartGame()
        {
            Random rnd = new Random();
            int triesLeft = START_TRIES;
            int hitID = 0;
            int startDistance = rnd.Next(1600, 2900);
            List<string> result = new List<string>();
            GolfSwing golfSwing = new GolfSwing();
            double distanceLeft = startDistance;
            bool loop = true;
            Console.WriteLine("Welcome to the golf simulator!");

            while (loop == true)
            {
                if (triesLeft == 0)
                {
                    ArgumentException argEx = new ArgumentException("You used all your tries.");
                    throw argEx;
                }
                double angle;
                double velocity;

                Console.WriteLine("\nPlease choose a golf club:\n1. Putter\n2. Iron\n3. Driver");
                try
                {
                    club = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ArgumentException argEx = new ArgumentException("Input can't be null, char or whitespace", "club");
                    throw argEx;
                }
                Console.Clear();
                Console.WriteLine("You choosed the {0}", GetClub(club));
                Console.Write("\nPlease enter the amount of force(1-200): ");

                // Check if input is valid
                try
                {
                    velocity = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    ArgumentException argEx = new ArgumentException("Input can't be null, char or whitespace", "velocity");
                    throw argEx;
                }


                if (velocity < MIN_VELOCITY || velocity > MAX_VELOCITY)
                {
                    Console.WriteLine("\nPlease choose a velocity between 1 and 200");
                }
                else
                {
                    Console.Write("Please enter the angle(1-90): ");

                    // Check if input is valid
                    try
                    {
                        angle = double.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        ArgumentException argEx = new ArgumentException("Input can't be null, char or whitespace", "angle");
                        throw argEx;
                    }


                    if (angle > MIN_ANGLE || angle < MAX_ANGLE)
                    {
                        Console.Clear();
                        double distanceHit = Math.Round(golfSwing.swing(velocity*GetClubModifier(club), angle));
                        triesLeft--;
                        Console.WriteLine("The ball traveled {0}m using the {1}. Number of tries: {2}/{3}", distanceHit, GetClub(club), START_TRIES - triesLeft, START_TRIES);
                        distanceLeft -= distanceHit;
                        hitID++;

                        // Add swing info to list
                        result.Add("Hit #"+ hitID +" Velocity: " + velocity + " Angle: " + angle + " Distance traveled: " + distanceHit + " Club used: " + GetClub(club) + "\n");
                        
                        // Check if you hit the hole
                        if (distanceLeft == 0)
                        {
                            Console.WriteLine("You won!");
                            Console.WriteLine("\n\nStatistics: ");
                            foreach(string hit in result)
                            {
                                Console.WriteLine(hit);
                            }
                            Console.ReadKey(true);
                            loop = false;
                        }

                        // Set the distance left to a positive value if you went past the hole
                        if (distanceLeft < 0) { distanceLeft = distanceLeft - distanceLeft * 2; }

                        // Throw exceptions if you went too far away from the hole
                        if (distanceLeft > startDistance + 1000)
                        {
                            ArgumentException argEx = new ArgumentException(string.Format("You went too far away from the target. Value: " + distanceLeft));
                            throw argEx;
                        }

                        // Display distance left
                        if (distanceLeft > 0 || triesLeft > 0)
                        {
                            Console.WriteLine("You got {0}m left to the hole", distanceLeft);
                        }

                    }


                }
            }
        }
        public double GetClubModifier(int index)
        {
            double[] golfClub;
            golfClub = new double[3];
            golfClub[0] = 0.5;
            golfClub[1] = 1;
            golfClub[2] = 1.5;
            try
            {
                return golfClub[index-1];
            }
            catch(IndexOutOfRangeException ex)
            {
                ArgumentException argEx = new System.ArgumentException(string.Format("Index is out of range. Value: " + index), "index", ex);
                throw argEx;
            }
        }
        public string GetClub(int index)
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
    }
}
