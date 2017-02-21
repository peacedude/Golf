using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    #region GolfSwing Class
    class GolfSwing
    {
        #region/*-----------Constant Variables-------------*/
        private const double GRAVITY = 9.8;
        #endregion

        #region/*-----------Variables----------------------*/
        private double angleInRadians;
        #endregion

        #region/*-----------Get/Set Methods----------------*/
        private double Angle { get; set; }

        /// <summary>
        /// Returns (Math.PI / 180) * value.
        /// </summary>
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
        #endregion

        #region/*-----------Return methods-----------------*/
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
        #endregion
    }
    #endregion
}
