using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Compensation
{
    class Compensation
    {
        private double[,] Compensate(double _Wind_Speed, double _Wind_Dir)
        {
            double ws = 0.0;
            double wd = 0.0;
            double angle = Math.PI * wd / 180; // เรียก weather_info.windspeed เข้ามาเป็น input.
            double x = ws * Math.Cos(angle);
            double y = ws * Math.Sin(angle);
            double[,] cor = new double[2, 1];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        cor[i, j] = x;
                    }
                    else
                    {
                        cor[i, j] = y;
                    }
                }
            }
            return cor;

        }

        private double Torque(double v)
        {
            double z = 10.0;
            double z0 = 0.03;
            double k = 2.5 * Math.Pow(Math.Log(z / z0), -2);
            double alpha = Math.Sqrt(6 * k);
            double kt = 2 * k * alpha * Math.Pow(v, 2);

            return kt;

        }
    }
}
