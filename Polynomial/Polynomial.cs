using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace PolynomialNamespace
{
    public class Polynomial : ICloneable
    {
        public int Degree { get; private set; }
        public Dictionary<int, int> Coefficients { get; private set; }

        public Polynomial()
        {
            Degree = 0;
            Coefficients = new Dictionary<int, int>();
        }

        public Polynomial(List<int> coefficientList)
        {
            if (coefficientList == null)
                throw new NullPolynomialInitializationListException("Input List is not initialized");

            Degree = coefficientList.All(x => x == 0) ? 0 : coefficientList.Count - coefficientList.IndexOf(coefficientList.First(x => x != 0)) - 1;

            for (int i = 0; i < coefficientList.Count; i++)
                if (coefficientList[i] != 0)
                    Coefficients.Add(coefficientList.Count - i - 1, coefficientList[i]);


        }

        public Polynomial(Dictionary<int, int> coefficients)
        {
            if (coefficients == null)
                throw new NullPolynomialInitializationListException("Input Dictionary is not initialized");

            Degree = coefficients.Keys.All(x => x == 0) ? 0 : coefficients.Keys.Max();
            Coefficients = coefficients.Where(x => x.Value != 0).Select(x => new KeyValuePair<int, int>(x.Key, x.Value))
                                                                .OrderByDescending(x => x.Key)
                                                                .ToDictionary(x => x.Key, x => x.Value);
        }

        public int this[int key]
        {
            get
            {
                if (!Coefficients.ContainsKey(key))
                    throw new IndexOutOfRangeException($"Polynomial doesnt contains {key} degree");

                return Coefficients[key];
            }
            set
            {
                if (!Coefficients.ContainsKey(key))
                    throw new IndexOutOfRangeException($"Polynomial doesnt contains {key} degree");

                Coefficients[key] = value;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in Coefficients)
                result.Append(string.Format((item.Value > 0 ? "+" + item.Value.ToString() : item.Value.ToString()) + "x^" + item.Key.ToString()));

            return result.ToString();
        }

        public object Clone()
        {
            return new Polynomial(Coefficients);
        }

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();

            var leftCoefficients = left.Coefficients;
            var rightCoefficients = right.Coefficients;
            var resultCoefficients = new Dictionary<int, int>();

            foreach (var item in leftCoefficients)
                resultCoefficients.Add(item.Key, item.Value + rightCoefficients.First(x => x.Key == item.Key).Value);

            var newKeys = rightCoefficients.Keys.Except(leftCoefficients.Keys);

            foreach (var key in newKeys)
                resultCoefficients.Add(key, rightCoefficients[key]);

            resultCoefficients = resultCoefficients.OrderByDescending(x => x.Key)
                                                   .ToDictionary(x => x.Key, y => y.Value);

            return new Polynomial(resultCoefficients);
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();

            var leftCoefficients = left.Coefficients;
            var rightCoefficients = right.Coefficients;
            var resultCoefficients = new Dictionary<int, int>();

            foreach (var item in leftCoefficients)
                resultCoefficients.Add(item.Key, item.Value - rightCoefficients.First(x => x.Key == item.Key).Value);

            var newKeys = rightCoefficients.Keys.Except(leftCoefficients.Keys);

            foreach (var key in newKeys)
                resultCoefficients.Add(key, -rightCoefficients[key]);

            resultCoefficients = resultCoefficients.OrderByDescending(x => x.Key)
                                                   .ToDictionary(x => x.Key, y => y.Value);
            return new Polynomial(resultCoefficients);
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();

            var leftCoefficients = left.Coefficients;
            var rightCoefficients = right.Coefficients;
            var resultCoefficients = new Dictionary<int, int>();

            foreach (var leftItem in leftCoefficients)
                foreach (var rightItem in rightCoefficients)
                {
                    var tempKey = leftItem.Key + rightItem.Key;
                    var tempValue = leftItem.Value * rightItem.Value;

                    if (resultCoefficients.ContainsKey(tempKey))
                        resultCoefficients[tempKey] += tempValue;
                    else
                        resultCoefficients.Add(tempKey, tempValue);
                }

            resultCoefficients = resultCoefficients.OrderByDescending(x => x.Key)
                                                   .ToDictionary(x => x.Key, y => y.Value);

            return new Polynomial(resultCoefficients);
        }
    }
}