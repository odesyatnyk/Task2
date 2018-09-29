using NUnit.Framework;
using MathHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MathHelper.PolynomialTests
{
    [TestFixture]
    public class PolynomialTestClass
    {
        [Test]
        public void PolynomialCtor_PassNullList_Exception()
        {
            Assert.Catch<NullPolynomialInitializationListException>(() =>
            {
                List<int> coefficients = null;
                var pol = new Polynomial(coefficients);
            });
            
        }

        [Test]
        public void PolynomialCtor_PassListWithZeroes_PolynomWithZeroDegreeAndNoCoeficients()
        {
            var coefficients = new List<int>() { 0, 0, 0, 0, 0 };

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(0, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(0, pol.Coefficients.Count);
        }

        [Test]
        public void PolynomialCtor_PassEmptyList_PolynomWithZeroDegreeAndNoCoeficients()
        {
            var coefficients = new List<int>();

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(0, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(0, pol.Coefficients.Count);
        }

        [Test]
        public void PolynomialCtor_PassListValidCoefs_ValidPolynom()
        {
            var coefficients = new List<int>() { 10, 5, 0, 1, 0 };

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(4, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(3, pol.Coefficients.Count);
            Assert.IsTrue(pol.Coefficients.ContainsKey(4));
            Assert.IsTrue(pol.Coefficients.ContainsKey(3));
            Assert.IsTrue(pol.Coefficients.ContainsKey(1));
            Assert.AreEqual(pol[4], 10);
            Assert.AreEqual(pol[3], 5);
            Assert.AreEqual(pol[1], 1);
        }

        [Test]
        public void PolynomialCtor_PassNullDictionary_Exception()
        {
            Assert.Catch<NullPolynomialInitializationListException>(() =>
            {
                Dictionary<int, int> coefficients = null;
                var pol = new Polynomial(coefficients);
            });
            
        }

        [Test]
        public void PolynomialCtor_PassDictionaryWithZeroes_PolynomWithZeroDegreeAndNoCoeficients()
        {
            var coefficients = new Dictionary<int, int>()
            {
                {0, 0}
            };

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(0, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(0, pol.Coefficients.Count);
        }

        [Test]
        public void PolynomialCtor_PassEmptyDictionary_PolynomWithZeroDegreeAndNoCoeficients()
        {
            var coefficients = new Dictionary<int, int>();

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(0, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(0, pol.Coefficients.Count);
        }

        [Test]
        public void PolynomialCtor_PassDictionaryValidCoefs_ValidPolynom()
        {
            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 }
            };

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(4, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(3, pol.Coefficients.Count);
            Assert.IsTrue(pol.Coefficients.ContainsKey(4));
            Assert.IsTrue(pol.Coefficients.ContainsKey(3));
            Assert.IsTrue(pol.Coefficients.ContainsKey(1));
            Assert.AreEqual(pol[4], 10);
            Assert.AreEqual(pol[3], 5);
            Assert.AreEqual(pol[1], 1);
        }

        [Test]
        public void PolynomialCtor_PassDictionaryValidCoefs_ValidPolynom1()
        {
            var coefficients = new Dictionary<int, int>()
            {
                { 1, 10 },
                { 0, 1 }
            };

            var pol = new Polynomial(coefficients);

            Assert.IsNotNull(pol);
            Assert.AreEqual(1, pol.Degree);
            Assert.NotNull(pol.Coefficients);
            Assert.AreEqual(2, pol.Coefficients.Count);
            Assert.IsTrue(pol.Coefficients.ContainsKey(1));
            Assert.IsTrue(pol.Coefficients.ContainsKey(0));
            Assert.AreEqual(pol[1], 10);
            Assert.AreEqual(pol[0], 1);
        }


        [Test]
        public void PolynomialIndexer_IndexOutOfRange_Exception()
        {
            Assert.Catch<IndexOutOfRangeException>(() =>
            {
                var coefficients = new Dictionary<int, int>()
                {
                    { 4, 10 },
                    { 3, 5 },
                    { 1, 1 }
                };

                var pol = new Polynomial(coefficients);
                var value = pol[2];
            });


        }

        [Test]
        public void PolynomialIndexer_ValidIndex_ReturnsValue()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 }
            };

            var pol = new Polynomial(coefficients);
            var value = pol[4];

            Assert.AreEqual(10, value);
        }

        [Test]
        public void PolynomialIndexer_ValidIndex_AssignNewValue()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 }
            };

            var pol = new Polynomial(coefficients);
            pol[4] = 99;

            Assert.AreEqual(99, pol[4]);
        }

        [Test]
        public void PolynomialClone_Clone_NewEqualPolynomials()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 }
            };

            var pol = new Polynomial(coefficients);
            var newPol = (Polynomial)pol.Clone();


            Assert.IsInstanceOf(typeof(Polynomial), newPol);
            Assert.AreEqual(pol.Degree, newPol.Degree);
            Assert.AreEqual(pol.Coefficients, newPol.Coefficients);
        }

        [Test]
        public void PolynomialClone_Clone_NewEqualPolynomialsPointsToDifferentObjects()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 }
            };

            var pol = new Polynomial(coefficients);
            var newPol = (Polynomial)pol.Clone();

            pol[4] = 11;

            Assert.AreEqual(11, pol[4]);
            Assert.AreEqual(10, newPol[4]);
            Assert.AreNotEqual(newPol[4], pol[4]);
        }

        [Test]
        public void PolynomialToString_ToString_ValidString()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 4, 10 },
                { 3, 5 },
                { 1, 1 },
                { 0, 5 }
            };
            var pol = new Polynomial(coefficients);
            var stringRepresentation = pol.ToString();

            Assert.IsNotNull(stringRepresentation);
            Assert.IsNotEmpty(stringRepresentation);
            Assert.AreEqual("10x^4 + 5x^3 + 1x + 5", stringRepresentation);
        }

        [Test]
        public void PolynomialToString_ToString_ValidString1()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 0, 5 }
            };
            var pol = new Polynomial(coefficients);
            var stringRepresentation = pol.ToString();

            Assert.IsNotNull(stringRepresentation);
            Assert.IsNotEmpty(stringRepresentation);
            Assert.AreEqual("5", stringRepresentation);
        }

        [Test]
        public void PolynomialToString_ToString_ValidString2()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 1, 5 }
            };
            var pol = new Polynomial(coefficients);
            var stringRepresentation = pol.ToString();

            Assert.IsNotNull(stringRepresentation);
            Assert.IsNotEmpty(stringRepresentation);
            Assert.AreEqual("5x", stringRepresentation);
        }

        [Test]
        public void PolynomialToString_ToString_ValidString3()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 1, 5 },
                { 0, 5 }
            };
            var pol = new Polynomial(coefficients);
            var stringRepresentation = pol.ToString();

            Assert.IsNotNull(stringRepresentation);
            Assert.IsNotEmpty(stringRepresentation);
            Assert.AreEqual("5x + 5", stringRepresentation);
        }

        [Test]
        public void PolynomialToString_ToString_ValidString4()
        {

            var coefficients = new Dictionary<int, int>()
            {
                { 10, -5 },
                { 1, 5 },
                { 0, 5 }
            };
            var pol = new Polynomial(coefficients);
            var stringRepresentation = pol.ToString();

            Assert.IsNotNull(stringRepresentation);
            Assert.IsNotEmpty(stringRepresentation);
            Assert.AreEqual("- 5x^10 + 5x + 5", stringRepresentation);
        }
    }
}