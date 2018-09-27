using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialNamespace
{
    public class Polynomial : ICloneable
    {
        private Dictionary<int, int> _coefficients = new Dictionary<int, int>();
        private int _degree = 0;
        public int Degree => _degree;
        Dictionary<int, int> Coefficients => _coefficients;
        public Polynomial()
        {

        }
        public Polynomial(List<int> coefficientList)
        {
            if (coefficientList != null)
            {
                this._degree = coefficientList.All(x => x == 0) ? 0 : coefficientList.Count - coefficientList.IndexOf(coefficientList.First(x => x != 0)) - 1;
                for (int i = 0; i < coefficientList.Count; i++)
                {
                    if (coefficientList[i] != 0)
                    {
                        _coefficients.Add(coefficientList.Count - i - 1, coefficientList[i]);
                    }
                }
            }
            else throw new NullPolynomialInitializationListException("Input List is not initialized");
        }
        public Polynomial(Dictionary<int, int> coefficients)
        {
            if (coefficients != null)
            {
                this._degree = coefficients.Keys.All(x => x == 0) ? 0 : coefficients.Keys.Max();
                foreach (var item in coefficients)
                {
                    if (item.Value != 0)
                    {
                        this._coefficients.Add(item.Key, item.Value);
                    }
                }
            }
            else throw new NullPolynomialInitializationListException("Input Dictionary is not initialized");
        }
        public int this[int index]
        {
            get
            {
                if (!_coefficients.ContainsKey(index))
                {
                    throw new IndexOutOfRangeException("Polynomial doesnt contains " + nameof(index) + " " + index.ToString() + " key");
                }
                _coefficients.TryGetValue(index, out int coefficient);
                return coefficient;
            }
            set
            {
                if (!_coefficients.ContainsKey(index))
                {
                    throw new IndexOutOfRangeException("Polynomial doesnt contains " + nameof(index) + " " + index.ToString() + " key");
                }
                _coefficients.Remove(index);
                _coefficients.Add(index, value);
                _coefficients = _coefficients.OrderByDescending(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            }
        }
        public override string ToString()
        {
            string result = string.Empty;
            foreach (var item in _coefficients)
            {
                result += string.Format((item.Value > 0 ? "+" + item.Value.ToString() : item.Value.ToString()) + "x^" + item.Key.ToString());
            }
            return result;
        }

        public object Clone()
        {
            return new Polynomial(this.Coefficients);
        }

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }
            Dictionary<int, int> leftCoefficients = left.Coefficients;
            Dictionary<int, int> rightCoefficients = right.Coefficients;
            Dictionary<int, int> resultCoefficients = new Dictionary<int, int>();
            foreach (var item in leftCoefficients)
            {
                resultCoefficients.Add(item.Key, item.Value + rightCoefficients.First(x => x.Key == item.Key).Value);
            }
            var val = rightCoefficients.Keys.Except(leftCoefficients.Keys);
            foreach (var key in val)
            {
                rightCoefficients.TryGetValue(key, out int value);
                resultCoefficients.Add(key, value);
            }
            resultCoefficients = resultCoefficients.OrderByDescending(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            return new Polynomial(resultCoefficients);
        }
        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }
            Dictionary<int, int> leftCoefficients = left.Coefficients;
            Dictionary<int, int> rightCoefficients = right.Coefficients;
            Dictionary<int, int> resultCoefficients = new Dictionary<int, int>();
            foreach (var item in leftCoefficients)
            {
                resultCoefficients.Add(item.Key, item.Value - rightCoefficients.First(x => x.Key == item.Key).Value);
            }
            var val = rightCoefficients.Keys.Except(leftCoefficients.Keys);
            foreach (var key in val)
            {
                rightCoefficients.TryGetValue(key, out int value);
                resultCoefficients.Add(key, -value);
            }
            resultCoefficients = resultCoefficients.OrderByDescending(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            return new Polynomial(resultCoefficients);
        }
        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }
            Dictionary<int, int> leftCoefficients = left.Coefficients;
            Dictionary<int, int> rightCoefficients = right.Coefficients;
            Dictionary<int, int> resultCoefficients = new Dictionary<int, int>();
            foreach (var leftItem in leftCoefficients)
            {
                foreach (var rightItem in rightCoefficients)
                {
                    int tempKey = leftItem.Key + rightItem.Key;
                    int tempValue = leftItem.Value * rightItem.Value;
                    if (resultCoefficients.Keys.Contains(tempKey))
                    {
                        resultCoefficients.TryGetValue(tempKey, out int oldValue);
                        resultCoefficients.Remove(tempKey);
                        resultCoefficients.Add(tempKey, oldValue + tempValue);
                    }
                    else
                    {
                        resultCoefficients.Add(tempKey, tempValue);
                    }
                }
            }
            resultCoefficients = resultCoefficients.OrderByDescending(key => key.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            return new Polynomial(resultCoefficients);
        }
    }
}