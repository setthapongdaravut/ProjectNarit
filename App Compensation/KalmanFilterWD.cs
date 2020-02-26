using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarMathLib;
using System.Diagnostics;

namespace App_Compensation
{
    public class KalmanFilterWD //For Wind Direction!!!!!//
    {
        Stopwatch stopwatch3 = new Stopwatch();
        Stopwatch stopwatch4 = new Stopwatch();

        private double[,] H, P, K, x;
        public double[,] Q, R; //Tuning
        private double[,] I = { { 1, 0 }, { 0, 1 } };
        private double b, dt;
        TimeSpan  tsStop3;
        TimeSpan  tsStop4;


        public void GetMatrixWD(double H, double P, double x, double b)
        {
            //
            stopwatch3.Start();
            //
            double[,] x_new = new double[2, 1];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        x_new[i, j] = x;
                    }
                    else
                    {
                        x_new[i, j] = b;
                    }

                }
            }

            this.H = StarMath.multiply(H, I);
            this.P = StarMath.multiply(P, I);
            this.x = x_new;

            stopwatch3.Stop();
            tsStop3 = stopwatch3.Elapsed;

        }


        public double[,] KalmanFWD(double input)
        {
            stopwatch4.Start();
            double[,] y = new double[2, 1];
            double[,] A = new double[2, 2];
            double[,] w = new double[2, 1];
            this.b = x[1, 0];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        A[i, j] = dt;
                    }
                    else if (i == 1 && j == 0)
                    {
                        A[i, j] = 0;
                    }
                    else { A[i, j] = 1; }

                }
            }

            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        w[i, j] = Math.Pow(dt, 2) / 2;
                    }
                    else
                    {
                        w[i, j] = dt;
                    }
                }
            }


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    if (i == 0)
                    {
                        y[i, j] = input;
                    }
                    else
                    {
                        y[i, j] = b;
                    }
                }
            }


            //Kalman Filter 2x2
            x = A.multiply(x).add(w);
            P = A.multiply(P).multiply(A.transpose()).add(Q);

            K = P.multiply(H).multiply((H.multiply(P).multiply(H.transpose()).add(R)).inverse());
            x = x.add(K.multiply(y.subtract(H.multiply(x))));
            P = (I.subtract(K.multiply(H))).multiply(P);
            P = (I.subtract(K.multiply(H))).multiply(P);

            stopwatch4.Stop();
            tsStop4 = stopwatch4.Elapsed;
            dt = tsStop4.TotalSeconds + tsStop3.TotalSeconds;
            Console.WriteLine(dt);
            return x;

        }
    }
}
