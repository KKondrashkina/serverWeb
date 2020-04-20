using System;
using NLog;
using Vectors;

namespace Logger
{
    class Logger
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Приложение запущено");

            try
            {
                var vector1 = new Vector(new[] { 56.77, 7.5, 86.8, 6.7, 4.6, 66.8, 90.6, 12.4 });
                var vector2 = new Vector(6, new[] { 5.6, 7.7, 6.8 });

                Console.Write("Сумма векторов {0} и {1} = ", vector1, vector2);
                vector1.AddVector(vector2);
                Console.WriteLine(vector1);
                Console.WriteLine();

                Console.WriteLine("Сумма векторов {0} и {1} = {2}", vector1, vector2, Vector.AddVectors(vector1, vector2));
                Console.WriteLine();

                var vector3 = new Vector(-2);
            }
            catch (Exception e)
            {
                logger.Error(e, "Ошибка");
            }

            Console.ReadKey();
        }
    }
}
