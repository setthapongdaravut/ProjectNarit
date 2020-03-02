using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarMathLib;

namespace App_Compensation
{
    class Compensation
    {
         //งูกินหาง ควยยยยย!!!!
        Delspike dotPD = new Delspike();

        public double Compensate(double ws, double wd)
        {
            //เหลือเอา coordinate กล้องมาใส่
            double angle = Math.PI * wd / 180;
            double x = ws * Math.Cos(angle);
            double y = ws * Math.Sin(angle);
            double[,] cor_wind = new double[3, 1];
            double[,] cor_tele = new double[3, 1] { { 3 }, { 2 }, { 1 } }; //ตำเเหน่งสมมติของกล้อง


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        cor_wind[i, j] = x;
                    }
                    else if (i == 1)
                    {
                        cor_wind[i, j] = y;
                    }
                    else
                    {
                        cor_wind[i, j] = 0;
                    }
                }
            }
        
            return dotPD.DotProduct(cor_wind, cor_tele); //ต้องหารด้วย magnitude ของหน้ากล้อง


        }

        public double Torque(double v, double scaling)
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
