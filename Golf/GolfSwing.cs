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
        private double angle;
        private double angleInRadians;
        private double velocity;

        public double Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        public double AngleInRadians
        {
            get
            {
                return angleInRadians;
            }

            set
            {
                angleInRadians = (Math.PI/180)*value;
            }
        }

        public double Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public double swing(double velo, double ang)
        {
            Angle = ang;
            Velocity = velo;
            AngleInRadians = Angle;

            double hitDistance = Math.Pow(Velocity,2)/GRAVITY*Math.Sin(2*AngleInRadians);
            
            return hitDistance;
        }
    }
}
