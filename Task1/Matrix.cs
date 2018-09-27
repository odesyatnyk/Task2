using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    [Serializable]
    public class Matrix : ICloneable
    {
        private int[,] data;
        private int rows;
        private int columns;

        public Matrix()
        {
            rows = 0;
            columns = 0;
            data = new int[rows, columns];
        }
        public Matrix(int rows, int columns)
        {
            try
            {
                if (rows > 0 && columns > 0)
                {
                    this.rows = rows;
                    this.columns = columns;
                    Random rnd = new Random();
                    data = new int[rows, columns];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            data[i, j] = rnd.Next();
                        }
                    }
                }
                else
                {
                    throw new InitializeIndexOutOfRangeException("Number of rows and columns must be positive");
                }
            }
            catch (InitializeIndexOutOfRangeException ex)
            {

                throw;
            }

        }
        public int this[int index, int index2]
        {
            get
            {
                try
                {
                    return data[index, index2];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw;
                }
            }
            set
            {
                try
                {
                    data[index, index2] = value;
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw;
                }
            }
        }
        public int[,] GetMatrix()
        {
            int[,] resultMatrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
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
        public void ReadMatrixFromBinaryFile(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName + ".dat", FileMode.OpenOrCreate))
            {
                Matrix newPerson = (Matrix)formatter.Deserialize(fs);
            }
        }
        public object Clone()
        {
            Matrix resultMatrix = new Matrix(this.rows, this.columns);
            for (int i = 0; i < resultMatrix.rows; i++)
            {
                for (int j = 0; j < resultMatrix.columns; j++)
                {
                    resultMatrix[i, j] = this[i, j];
                }
            }
            return resultMatrix;
        }
        public override string ToString()
        {
            string outputMatrix = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    outputMatrix += string.Format(data[i, j].ToString() + new string(' ', 3));
                }
                outputMatrix += "\n";
            }
            return outputMatrix;
        }
        public static Matrix operator +(Matrix left, Matrix right)
        {
            try
            {
                Matrix resultMatrix = new Matrix(left.rows, left.columns);
                if (left.rows == right.rows && left.columns == right.columns)
                {
                    for (int i = 0; i < left.rows; i++)
                    {
                        for (int j = 0; j < left.columns; j++)
                        {
                            resultMatrix[i, j] = left[i, j] + right[i, j];
                        }
                    }
                }
                else
                {
                    throw new DifferentMatrixSizeException("Impossible to add matrix with different sizes");
                }
                return resultMatrix;
            }
            catch (DifferentMatrixSizeException ex)
            {
                return new Matrix();
            }
        }
        public static Matrix operator -(Matrix left, Matrix right)
        {
            try
            {
                if (left.rows == right.rows && left.columns == right.columns)
                {
                    Matrix resultMatrix = new Matrix(left.rows, left.columns);
                    for (int i = 0; i < left.rows; i++)
                    {
                        for (int j = 0; j < left.columns; j++)
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
            catch (DifferentMatrixSizeException ex)
            {
                return new Matrix();
            }
        }
        public static Matrix operator *(Matrix left, Matrix right)
        {
            try
            {
                if (left.columns == right.rows)
                {
                    Matrix resultMatrix = new Matrix(left.rows, right.columns);
                    int temp;
                    int j = 0;
                    for (int i = 0; i < resultMatrix.rows; i++)
                    {
                        temp = 0;
                        for (j = 0; j < resultMatrix.columns; j++)
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
            catch (DifferentMatrixSizeException ex)
            {
                return new Matrix();
            }
        }
        public static bool operator ==(Matrix left, Matrix right)
        {
            for (int i = 0; i < left.rows; i++)
            {
                for (int j = 0; j < left.columns; j++)
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
            for (int i = 0; i < left.rows; i++)
            {
                for (int j = 0; j < left.columns; j++)
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
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
