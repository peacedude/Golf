using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class GolfSwing
    {
        private const double GRAVITY = 9.8;

        private double angleInRadians;

        private double Angle { get; set; }

        private double AngleInRadians
        {
            get
            {
                return angleInRadians;
            }

            set
            {
                angleInRadians = (Math.PI / 180) * value;
            }
        }

        /// <summary>
        /// Takes the velocity and angle then calculates how far your ball will go.
        /// </summary>
        /// <param name="velo">Used to indicate Velocity</param>
        /// <param name="ang">Used to indicate Angle</param>
        /// <returns>Returns a calculated hit</returns>
        public double CalculateSwing(double velo, double ang)
        {
            Angle = ang;
            AngleInRadians = Angle;

            double hitDistance = Math.Pow(velo, 2) / GRAVITY * Math.Sin(2 * AngleInRadians);

            return hitDistance;
        }
    }
}
