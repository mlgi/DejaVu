using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejaVu
{
    [System.Serializable]
    class Matrix
    {
        private List<List<double>> _matrix; // the multidimensional array that holds the matrix values
        private int _rows; // number of rows, as determined by the number of elements in the first column added
        private int _columns; // number of columns, changes after every column added

        // properties
        public List<double> this[int index] // indexer
        {
            get
            {
                return _matrix[index];
            }
            set
            {
                if (value.Count == _rows)
                {
                    _matrix[index] = value;
                }
                else
                {
                    throw new Exception("Column replacement size invalid");
                }
            }
        }
        public int Rows  // get number of rows
        {
            get
            {
                return _rows;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int Columns // get number of columns
        {
            get
            {
                return _columns;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        // overrides and operators
        override public string ToString() // for printing
        {
            string output = "\n";

            for (int j = 0; j < _rows; j++)
            {
                output += "| ";
                for (int i = 0; i < _columns; i++)
                {
                    output += _matrix[i][j];
                    output += " ";
                }
                output += "|\n";
            }

            return output;
        }
        public static Matrix operator+ (Matrix left, Matrix right) // matrix addition of two matrices
        {
            if (left.Rows == right.Rows && left.Columns == right.Columns)
            {
                Matrix newMatrix = new Matrix();
                for (int i = 0; i < left.Columns; i++)
                {
                    List<double> newColumn = new List<double>();
                    for (int j = 0; j < right.Rows; j++)
                    {
                        newColumn.Add(left[i][j] + right[i][j]);
                    }
                    newMatrix.AddColumn(newColumn);
                }

                return newMatrix;
            }
            throw new Exception("Matrix addition sizes not equal");
            // return left;
        }
        public static Matrix operator* (Matrix left, Matrix right) // dot product of two matrices
        {
            if (left.Columns == right.Rows)
            {
                Matrix newMatrix = new Matrix();
                // matrix will be of size left.Rows by right.Columns
                for (int i = 0; i < right.Columns; i++)
                {
                    // it will be computed per element per column
                    List<double> newColumn = new List<double>();
                    for (int j = 0; j < left.Rows; j++)
                    {
                        double sum = 0;
                        for (int k = 0; k < left.Columns; k++)
                        {
                            sum += left[k][j] * right[i][k];
                        }
                        newColumn.Add(sum);
                    }
                    newMatrix.AddColumn(newColumn);
                }
                return newMatrix;
            }
            throw new Exception("Matrix multiplication size invalid: " + left + "x" + right + "\n");
        }
        public static Matrix operator* (double scalar, Matrix me) // scalar multiplication
        {
            Matrix newMatrix = new Matrix();
            for (int i = 0; i < me.Columns; i++)
            {
                List<double> newColumn = new List<double>();
                for (int j = 0; j < me.Rows; j++)
                {
                    newColumn.Add(me[i][j] * scalar);
                }
                newMatrix.AddColumn(newColumn);
            }
            return newMatrix;
        }
        public static Matrix operator* (Matrix me, double scalar) // scalar multiplication commutative
        {
            Matrix newMatrix = new Matrix();
            for (int i = 0; i < me.Columns; i++)
            {
                List<double> newColumn = new List<double>();
                for (int j = 0; j < me.Rows; j++)
                {
                    newColumn.Add(me[i][j] * scalar);
                }
                newMatrix.AddColumn(newColumn);
            }
            return newMatrix;
        }
        // constructors
        public Matrix() // constructor
        {
            _matrix = new List<List<double>>();
            _rows = 0;
            _columns = 0;
        }
        public Matrix(List<double> vector) // vector constructor
        {
            _matrix = new List<List<double>>();
            this.AddColumn(vector);
        }
        public Matrix(Matrix other) // copy constructor
        {
            List<List<double>> newMatrix = new List<List<double>>();
            _rows = other.Rows;
            _columns = other.Columns;
            for (int i = 0; i < other.Columns; i++)
            {
                newMatrix.Add(new List<double>(other[i]));
            }
            _matrix = newMatrix;
        }
        // methods
        public void AddColumn(List<double> newList)
        {
            if (newList.Count == Rows || Rows == 0)
            {
                _matrix.Add(newList);
                _columns += 1;
                _rows = newList.Count;
            }
        }// adds a new column into the matrix
        ~Matrix() // destructor
        {
            _matrix.Clear();
            _matrix = null;
            _rows = 0;
            _columns = 0;
        }
    }
}
