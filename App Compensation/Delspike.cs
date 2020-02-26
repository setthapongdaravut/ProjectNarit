using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace App_Compensation
{
    class Delspike
    {
        public double Del_WD(double wd)
        {
            double x;
            double signalold1 = 0.0;
            double signalold2 = 0.0;
           
            
            signalold2 = wd;
            double slope = signalold2 - signalold1;
            
            if (slope > 360 || slope < -360)  //Range of wind direction sensor.
            {
                x = signalold1;
            }
            else 
            {
                x = signalold2;
            }
            return x;
            
        }
        public double Del_WS(double ws)
        {
            double x;
            double signalold1 = 0.0;
            double signalold2 = 0.0;


            signalold2 = ws;
            double slope = signalold2 - signalold1;

            if (slope > 15 || slope < -15)   //Range of wind speed sensor.
            {
                x = signalold1; 
            }
            else
            {
                signalold1 = signalold2;
                x = signalold2;
            }
            return x;
        }
    }
}
