using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarMathLib;
using System.Diagnostics;

namespace App_Compensation
{
    public class KalmanFilter //For Wind Speed!!!!!//
    {
        Stopwatch stopwatch1 = new Stopwatch();
        Stopwatch stopwatch2 = new Stopwatch();

        private double[,] H, P, K, x; 
        public double[,] Q, R; //Tuning
        private double[,] I = { { 1, 0 }, { 0, 1 } };
        private double b,dt;
        TimeSpan  tsStop1;
        TimeSpan  tsStop2;
        

        public void GetMatrixWS(double H, double P, double x, double b)
        {
            //
            stopwatch1.Start();
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

            stopwatch1.Stop();
            tsStop1 = stopwatch1.Elapsed;
            
            
        }


        public double[,] KalmanFWS(double input)
        {
            stopwatch2.Start();
            double[,] y = new double[2, 1];
            double[,] A = new double[2, 2];
            double[,] w = new double[2, 1];
            this.b = x[1, 0];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 0 && j==1)
                    {
                        A[i, j] = dt;
                    }
                    else if(i==1 && j==0)
                    {
                        A[i, j] = 0;
                    }
                    else { A[i, j] = 1; }

                }
            }


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 1; j++)
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

            stopwatch2.Stop();
            tsStop2 = stopwatch2.Elapsed;
            dt = tsStop2.TotalSeconds + tsStop1.TotalSeconds;
            //Console.WriteLine("Ws = {0}",dt);
            return x;
            
        }
        
    }
}
