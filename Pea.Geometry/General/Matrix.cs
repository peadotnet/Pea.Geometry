using System;

namespace Pea.Geometry.General
{
	public class Matrix<T> where T: Vector, new()
	{
		public int Dimensions { get; protected set; }
		public double[,] Values { get; protected set; }

		public Matrix(int dimensions)
		{
			Dimensions = dimensions;
			Values = new double[dimensions, dimensions];
		}

		public Matrix(double[,] values)
		{
			var dim0 = values.GetLength(0);
			var dim1 = values.GetLength(1);

			if (dim0 != dim1) throw new ArgumentException("The matrix must be quadratic!");

			Dimensions = dim0;
			Values = new double[dim0, dim0];
			for (int i = 0; i < dim0; i++)
			{
				for (int j = 0; j < dim1; j++)
				{
					Values[i, j] = values[i, j];
				}
			}
		}

		public static Matrix<T> Identity(int dimensions)
		{
			var matrix = new Matrix<T>(dimensions);
			for(int i=0; i< dimensions; i++)
			{
				matrix.Values[i, i] = 1;
			}

			return matrix;
		}

		public static Matrix<T> operator *(Matrix<T> matrix1, Matrix<T> matrix2)
		{
			if (matrix1.Dimensions != matrix2.Dimensions) throw new ArgumentException("The dimensions of matrices must be the same!");

			var result = new Matrix<T>(matrix1.Dimensions);
			for (int i = 0; i < matrix1.Dimensions; i++)
			{
				for (int j = 0; j < matrix1.Dimensions; j++)
				{
					for (int k = 0; k < matrix1.Dimensions; k++)
					{
						result.Values[i, j] += matrix1.Values[i, k] * matrix2.Values[k, j];
					}
				}
			}

			return result;
		}

		public static Matrix<T> Compose(params Matrix<T>[] matrices)
		{
			if (matrices.Length == 0) return null;

			var dimensions = matrices[0].Dimensions;
			var result = matrices[0];

			for(int m = 1; m < matrices.Length; m++)
			{
				result = result * matrices[m];
			}

			return result;
		}

		public T Apply(T vector)
		{
			if (vector.Dimensions != this.Dimensions) throw new ArgumentException("The dimension of the matrix and the vector must be the same!");

			var result = new T();
			for(int i=0; i< vector.Dimensions; i++)
			{
				for (int j=0; j< this.Dimensions; j++)
				{
					result.Coordinates[i] += this.Values[i, j] * vector.Coordinates[j];
				}
			}

			return result;
		}
	}
}
