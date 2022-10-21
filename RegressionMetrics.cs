using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AzureML
{
    internal static class MyMath
    {
        public static double Square(this double value) => Math.Pow(value, 2);
        public static double Sqrt(this double value) => Math.Sqrt(value);
        public static double Abs(this double value) => Math.Abs(value);
    }

    public class RegressionMetrics
    {
        /// <summary>
        /// Andy => observedvalue - predictedvalue
        /// </summary>
        public static double Error(double observedvalue, double predictedvalue)
            => observedvalue - predictedvalue;

        /// <summary>
        /// Andy => R2 scroe = 1 - SSres / SStot
        /// </summary>
        public static double R2scroe(double[] observedvalues, double[] predictedvalues)
            => 1 - SSres(observedvalues, predictedvalues) / SStot(observedvalues);

        /// <summary>
        /// Andy => R2 scroe = 1 - SSres / SStot
        /// </summary>
        public static double Mean(double[] values)
            => values.Average();

        /// <summary>
        /// Andy => 求 中位數.
        /// </summary>
        public static double Median(double[] values)
        {
            var sortValues = values.OrderBy(x => x).ToArray();
            if (sortValues.Length % 2 == 1)
                return sortValues[(sortValues.Length) / 2];

            int medianCount = sortValues.Length / 2;
            return (sortValues[medianCount] + sortValues[medianCount - 1]) / 2;
        }

        /// <summary>
        /// Andy => 求 變異數(Variance) - 差異平方總和的平均.
        /// </summary>
        public static double Variance(double[] observedvalues)
        {
            if (observedvalues == null || observedvalues.Length == 0)
                return 0;

            var mean = observedvalues.Average();
            return observedvalues.Select(obs => (obs - mean).Square()).Average();
        }

        /// <summary>
        /// Andy => 求 標準差(SD). = 變異數 開根號
        /// </summary>
        public static double StandardDeviation(double[] observedvalues)
            => Variance(observedvalues).Sqrt();

        /// <summary>
        /// Andy => 求 總變異量 = 離均差(觀察值 - 平均值)平方和
        /// </summary>
        public static double SStot(double[] observedvalues)
        {
            if (observedvalues == null || observedvalues.Length == 0)
                return 0;

            var mean = observedvalues.Average();
            return observedvalues.Select(obs => (obs - mean).Square()).Sum();
        }

        /// <summary>
        /// Andy => 求 不可解釋變異 = 殘差(residual)(觀察值 - 預測值)平方和
        /// </summary>
        public static double SSres(double[] observedvalues, double[] predictedvalues)
            => observedvalues.Zip(predictedvalues, (obs, pred) => Error(obs, pred).Square()).Sum();

        /// <summary>
        /// Andy => 求 可解釋變異 = 廻歸離均差(預測值-平均觀察值)平方和
        /// </summary>
        public static double SSreg(double[] observedvalues, double[] predictedvalues)
        {
            if (observedvalues == null || observedvalues.Length == 0)
                return 0;
 
            var mean = observedvalues.Average();
            return predictedvalues.Select(pred => (pred - mean).Square()).Sum();
        }

        /// <summary>
        /// Andy => 求 Mean Squared Error
        /// </summary>
        public static double MSE(double[] observedvalues, double[] predictedvalues)
            => observedvalues.Zip(predictedvalues, (obs, pred) => Error(obs, pred).Square()).Average();

        /// <summary>
        /// Andy => 求 Mean Absolute Error
        /// </summary>
        public static double MAE(double[] observedvalues, double[] predictedvalues)
            => observedvalues.Zip(predictedvalues, (obs, pred) => Error(obs, pred).Abs()).Average();

        /// <summary>
        /// Andy => 求 Median Absolute Error
        /// </summary>
        public static double MedAE(double[] observedvalues, double[] predictedvalues)
            => Median(observedvalues.Zip(predictedvalues, (obs, pred) => Error(obs, pred).Abs()).ToArray());

        /// <summary>
        /// Andy => 求 Root Mean Squared Error
        /// </summary>
        public static double RMSE(double[] observedvalues, double[] predictedvalues)
            => MSE(observedvalues, predictedvalues).Sqrt();
    }
}
