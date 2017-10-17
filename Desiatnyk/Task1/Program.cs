using System;
using MatrixNamespace;
using PolynomialNamespace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>
            {
                { 5, 0 },
                { 4, 0 },
                { 3, 9 }
            };
            Polynomial dicpol = new Polynomial(dic);

            Polynomial pol = new Polynomial(new List<int>() { 0, 0, 9, 0, 0, 0 });
            Polynomial pol2 = new Polynomial(new List<int>() { 10, 0, 9, 0, 0, 0 });
            //pol2[50] = 1;

            Polynomial pol3 = pol2 - pol2 + pol;

            //Matrix mat = new Matrix(4, 4, 0, 4);
            //mat.SaveMatrixToBinaryFile("file");
            //Matrix mat2 = new Matrix();
            //mat2 = Matrix.ReadMatrixFromBinaryFile("file");

            Console.ReadKey();
        }
    }
}