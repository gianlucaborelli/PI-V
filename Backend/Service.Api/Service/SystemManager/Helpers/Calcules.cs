﻿namespace Service.Api.Service.SystemManager.Helpers
{
    public class Calcules
    {        
        public static double WetBulbTemperatureEstimation(double temperatura, double umidadeRelativa)
        {
            double t = temperatura;
            double rh = umidadeRelativa;

            double resultado = t * Math.Atan(0.151977 * Math.Sqrt(rh + 8.313659))
                             + Math.Atan(t + rh)
                             - Math.Atan(rh - 1.676331)
                             + 0.00391838 * Math.Pow(rh, 1.5) * Math.Atan(0.023101 * rh)
                             - 4.686035;

            return Math.Round(resultado, 2); 
        }

        public static double IUBTGEstimation(double wetBulbTemperature, double darkBulbAverage, double dryBulbAverage)
        {
            return Math.Round(
                (0.7 * wetBulbTemperature) +
                (0.2 * darkBulbAverage) +
                (0.1 * dryBulbAverage), 2);
        }

        public static double IUBTGEstimation(double wetBulbTemperature, double dryBulbAverage)
        {
            return Math.Round(
                (0.7 * wetBulbTemperature) +
                (0.3 * dryBulbAverage), 2);
        }
    }
}
