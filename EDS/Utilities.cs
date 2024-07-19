using System;
using System.Collections.Generic;

namespace EDS
{
    internal class Utilities
    {
        public static bool IsAxisX_Angle(double angle)
        {
            bool flag = false;

            double tlrncAngl = 5;

            List<double> listOfAngle = new List<double>();
            listOfAngle.Add(0);
            listOfAngle.Add(180);
            listOfAngle.Add(360);

            ////listOfAngle.Add(270); 
            ////listOfAngle.Add(90);


            foreach (double straightAngle in listOfAngle)
            {
                double diff = Math.Abs(straightAngle - angle);

                if (diff <= tlrncAngl)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
    }
}