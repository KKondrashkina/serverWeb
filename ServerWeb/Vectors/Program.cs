using System;

namespace Vectors
{
    class Program
    {
        static void Main(string[] args)
        {
            var vector1 = new Vector(6);
            var vector2 = new Vector(vector1);
            var vector3 = new Vector(new[] { 56.77, 7.5, 86.8, 6.7, 4.6, 66.8, 90.6, 12.4 });
            var vector4 = new Vector(6, new[] { 5.6, 7.7, 6.8 });

            Console.Write("Сумма векторов {0} и {1} = ", vector3, vector4);
            vector3.AddVector(vector4);
            Console.WriteLine(vector3);
            Console.WriteLine();

            Console.Write("Разность векторов {0} и {1} = ", vector3, vector4);
            vector3.SubtractVector(vector4);
            Console.WriteLine(vector3);
            Console.WriteLine();

            var scalar = 10;

            Console.Write("Результат умножения вектора {0} на число {1} = ", vector4, scalar);
            vector4.MultiplyByScalar(scalar);
            Console.WriteLine(vector4);
            Console.WriteLine();

            Console.Write("Результат разворота вектора {0} = ", vector3);
            vector3.RotateVector();
            Console.WriteLine(vector3);
            Console.WriteLine();

            Console.WriteLine("Длина вектора {0} = {1}", vector3, vector3.GetLength());
            Console.WriteLine();

            Console.WriteLine("Третья компонента вектора {0} = {1}", vector3, vector3.GetComponent(3));
            Console.WriteLine();

            vector3.SetComponent(3, 45.3);

            Console.WriteLine("Третья компонента вектора теперь равна {0} = {1}", vector3, vector3.GetComponent(3));
            Console.WriteLine();

            Console.WriteLine("Сумма векторов {0} и {1} = {2}", vector3, vector4, Vector.AddVectors(vector3, vector4));
            Console.WriteLine();

            Console.WriteLine("Разность векторов {0} и {1} = {2}", vector3, vector4, Vector.SubtractVectors(vector3, vector4));
            Console.WriteLine();

            Console.WriteLine("Произведение векторов {0} и {1} = {2}", vector3, vector4, Vector.MultiplyVectors(vector3, vector4));
            Console.WriteLine();

            if (vector1.Equals(vector2))
            {
                Console.WriteLine("Векторы {0} и {1} равны", vector1, vector2);
            }
            else
            {
                Console.WriteLine("Векторы {0} и {1} не равны", vector1, vector2);
            }

            Console.WriteLine();

            if (vector1.Equals(vector3))
            {
                Console.WriteLine("Векторы {0} и {1} равны", vector1, vector3);
            }
            else
            {
                Console.WriteLine("Векторы {0} и {1} не равны", vector1, vector3);
            }

            try
            {
                var vector5 = new Vector(-2);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Размерность меньше 1. Вектор не создан.");
            }

            try
            {
                var vector6 = new Vector(-3, new double[] { 2.3 });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Размерность меньше 1. Вектор не создан.");
            }

            try
            {
                var vector7 = new Vector(2);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Размерность меньше 1. Вектор не создан.");
            }

            Console.ReadKey();
        }
    }
}
