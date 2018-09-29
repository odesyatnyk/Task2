using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelper
{
    [Serializable]
    public class Matrix : ICloneable
    {
        private int[,] _data;
        private int _rows;
        private int _columns;
        public int Rows => _rows;
        public int Columns => _columns;
        public Matrix()
        {

        }
        public Matrix(int rows, int columns)
        {

            if (rows > 0 && columns > 0)
            {
                this._rows = rows;
                this._columns = columns;
                _data = new int[rows, columns];
            }
            else
            {
                throw new InitializeIndexOutOfRangeException("Number of rows and columns must be positive");
            }
        }
        public Matrix(int rows, int columns, int minVal, int maxVal)
        {

            if (rows > 0 && columns > 0)
            {
                this._rows = rows;
                this._columns = columns;
                Random rnd = new Random();

                _data = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        _data[i, j] = rnd.Next(minVal, maxVal);
                    }
                }
            }
            else
            {
                throw new InitializeIndexOutOfRangeException("Number of rows and columns must be positive");
            }
        }
        public int this[int index, int index2]
        {
            get
            {
                try
                {
                    return _data[index, index2];
                }
                catch (IndexOutOfRangeException ex)
                {
                    IndexOutOfRangeException argEx = new IndexOutOfRangeException("Inaccessible index of matrix", ex);
                    throw argEx;
                }
            }
            set
            {
                try
                {
                    _data[index, index2] = value;
                }
                catch (IndexOutOfRangeException ex)
                {
                    IndexOutOfRangeException argEx = new IndexOutOfRangeException("Inaccessible index of matrix", ex);
                    throw argEx;
                }
            }
        }
        public int[,] GetMatrix()
        {
            int[,] resultMatrix = new int[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    resultMatrix[i, j] = this[i, j];
                }
            }
            return resultMatrix;
        }
        public void SaveMatrixToBinaryFile(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName + ".dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }

        }
        static public Matrix ReadMatrixFromBinaryFile(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Matrix matrix = new Matrix();
            using (FileStream fs = new FileStream(fileName + ".dat", FileMode.OpenOrCreate))
            {
                matrix = (Matrix)formatter.Deserialize(fs);
            }
            return matrix;
        }
        public object Clone()
        {
            Matrix resultMatrix = new Matrix(this._rows, this._columns);
            for (int i = 0; i < resultMatrix._rows; i++)
            {
                for (int j = 0; j < resultMatrix._columns; j++)
                {
                    resultMatrix[i, j] = this[i, j];
                }
            }
            return resultMatrix;
        }
        public override string ToString()
        {
            string outputMatrix = string.Empty;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    outputMatrix += string.Format(_data[i, j].ToString()).PadLeft(6);
                }
                outputMatrix += "\n";
            }
            return outputMatrix;
        }
        public static Matrix operator +(Matrix left, Matrix right)
        {

            Matrix resultMatrix = new Matrix(left._rows, left._columns);
            if (left._rows == right._rows && left._columns == right._columns)
            {
                for (int i = 0; i < left._rows; i++)
                {
                    for (int j = 0; j < left._columns; j++)
                    {
                        resultMatrix[i, j] = left[i, j] + right[i, j];
                    }
                }
                return resultMatrix;
            }
            else
            {
                throw new DifferentMatrixSizeException("Impossible to add matrix with different sizes");
            }
        }
        public static Matrix operator -(Matrix left, Matrix right)
        {

            if (left._rows == right._rows && left._columns == right._columns)
            {
                Matrix resultMatrix = new Matrix(left._rows, left._columns);
                for (int i = 0; i < left._rows; i++)
                {
                    for (int j = 0; j < left._columns; j++)
                    {
                        resultMatrix[i, j] = left[i, j] - right[i, j];
                    }
                }
                return resultMatrix;
            }
            else
            {
                throw new DifferentMatrixSizeException("Impossible to substract matrix with different sizes");
            }
        }
        public static Matrix operator *(Matrix left, Matrix right)
        {

            if (left._columns == right._rows)
            {
                Matrix resultMatrix = new Matrix(left._rows, right._columns);
                int temp;
                int j = 0;
                for (int i = 0; i < resultMatrix._rows; i++)
                {
                    temp = 0;
                    for (j = 0; j < resultMatrix._columns; j++)
                    {
                        temp += left[i, j] * right[j, i];
                    }
                    resultMatrix[i, j] = temp;
                }
                return resultMatrix;
            }
            else
            {
                throw new DifferentMatrixSizeException("Impossible to multiply unconsistent matrixes");
            }

        }
        public static bool operator ==(Matrix left, Matrix right)
        {
            if (left.Rows != right.Rows)
            {
                return false;
            }
            if (left.Columns != right.Columns)
            {
                return false;
            }
            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    if (left[i, j] != right[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool operator !=(Matrix left, Matrix right)
        {
            if (left.Rows != right.Rows)
            {
                return true;
            }
            if (left.Columns != right.Columns)
            {
                return true;
            }
            for (int i = 0; i < left._rows; i++)
            {
                for (int j = 0; j < left._columns; j++)
                {
                    if (left[i, j] != right[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new NullReferenceException("Obj parameter is null.");
            if (!(obj is Matrix))
                throw new InvalidCastException("Obj cannot be casted to Matrix type.");
            return this == (Matrix)obj;
        }
        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }
    }
}
