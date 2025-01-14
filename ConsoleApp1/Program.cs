using System;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*myObj[] Objs = {
                new myObj(2, 8),
                new myObj(3, 12),
                new myObj(3, 4),
                new myObj(6, 15),
                new myObj(7, 6),
                new myObj(9, 11),
                new myObj(10, 9),
                new myObj(12, 6),
                new myObj(13, 12),
                new myObj(14, 5),
                new myObj(14, 9),
                new myObj(10, 2),
            };*/

            
            int elementCount = File.ReadLines(@"myObjectsFile.txt").Count(); // obyektlar soni
            myObj[] Objs = new myObj[elementCount];  // obyektlarni saqlovchi massiv
            using (StreamReader sr = File.OpenText(@"myObjectsFile.txt"))
            {
                string s;
                string[] stringArray;
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    stringArray = s.Split(' ');
                    Objs[i++] = new myObj(int.Parse(stringArray[0]), int.Parse(stringArray[1]), int.Parse(stringArray[2]), double.Parse(stringArray[3]), double.Parse(stringArray[4]), double.Parse(stringArray[5]));
                }
            }

            double[] oneWithOther = new double[elementCount]; // Bitta elementni boshqalarigacha masofalari
            double[,] thirdMins = new double[elementCount, 2];  // Har bir elementga eng yaqin k ta elemnt index va qiymati
            int k = 3; // k - minimum

            int t = 0;
            for (int i = 0; i < elementCount; i++)
            {
                for (int j = 0; j < elementCount; j++)
                {
                    oneWithOther[t++] = EvclidMetric(Objs[i], Objs[j]);
                }
                for (int q = 0; q < elementCount; q++)
                {
                    Console.Write(oneWithOther[q] + "  ");
                }
                Console.WriteLine();

                (int i, double l) relultThird = findThirdMin(oneWithOther, k);
                thirdMins[i, 0] = relultThird.i;
                thirdMins[i, 1] = relultThird.l;

                t = 0;
            }

            // Olingan minimum k qiymatning maksimumini topish
            double densityValue = thirdMins[0, 1];
            int densityIndex = 0;
            int densityMainIndex = 0;
            for (int i = 1; i < thirdMins.GetLength(0); i++)
            {
                if (thirdMins[i, 1] > densityValue)
                {
                    densityValue = thirdMins[i, 1];
                    densityMainIndex = Convert.ToInt32(thirdMins[i, 0]); // zichligi eng yuqori bo'lgan obyektga yaqin bo'lgan 3-element indeksi
                    densityIndex = i; // zichligi eng yuqori bo'lgan obyekt indeksi
                }
            }
            densityIndex++;
            densityMainIndex++;

            for (int q = 0; q < elementCount; q++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Write(thirdMins[q, i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("{0} - obyekt; {1} - yaqin bo'lgan obyekt ----> {2}", densityIndex, densityMainIndex, densityValue);
            
        }
        static double EvclidMetric(myObj Obj1, myObj Obj2)
        {
            double l;
            l = Math.Sqrt(Math.Pow(Obj1.f1 - Obj2.f1, 2) + Math.Pow(Obj1.f2 - Obj2.f2, 2) + Math.Pow(Obj1.f3 - Obj2.f3, 2) + Math.Pow(Obj1.f4 - Obj2.f4, 2) + Math.Pow(Obj1.f5 - Obj2.f5, 2) + Math.Pow(Obj1.f6 - Obj2.f6, 2));
            return (l == 0) ? 0 : Math.Round(l, 5);
        }
        static (int i, double l) findThirdMin(double[] arr, int k)
        {
            (int i, double l) result;
            double[] mins = new double[k + 2];
            mins[0] = 0;
            int findedIndex = -1;
            for (int i = 1; i < mins.Length; i++)
            {
                double deltaMin = double.MaxValue;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (deltaMin > arr[j] && mins[i - 1] <= arr[j])
                    {
                        deltaMin = arr[j];
                        findedIndex = j;
                    }
                }
                mins[i] = deltaMin;
                arr[findedIndex] = double.MaxValue;
            }
            result.i = findedIndex;
            result.l = 1.0 / mins[mins.Length - 1];
            result.l = Math.Round(result.l, 5);
            return result;
        }
    }
    public class myObj
    {
        public int f1;
        public int f2;
        public int f3;
        public double f4;
        public double f5;
        public double f6;

        public myObj(int f1, int f2, int f3, double f4, double f5, double f6)
        {
            this.f1 = f1;
            this.f2 = f2;
            this.f3 = f3;
            this.f4 = f4;
            this.f5 = f5;
            this.f6 = f6;
        }
    }
}