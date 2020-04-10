using System;
using System.Collections.Generic;

namespace Pea.Geometry.General
{
	public class Matrix
	{
		public int Dimensions { get; protected set; }
		public double[,] Values { get; protected set; }

		public Matrix(int dimensions)
		{
			Dimensions = dimensions;
			Values = new double[dimensions, dimensions];
		}

		public static Matrix Identity(int dimensions)
		{
			var matrix = new Matrix(dimensions);
			for(int i=0; i< dimensions; i++)
			{
				matrix.Values[i, i] = 1;
			}

			return matrix;
		}

		public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.Dimensions != matrix2.Dimensions) throw new ArgumentException("The dimensions of matrices must be the same!");

			var result = new Matrix(matrix1.Dimensions);
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

		public static Matrix Compose(params Matrix[] matrices)
		{
			if (matrices.Length == 0) return null;

			var dimensions = matrices[0].Dimensions;
			var result = matrices[0];

			for(int m=1; m< matrices.Length; m++)
			{
				result = result * matrices[m];
			}

			return result;
		}

		public Vector Apply(Vector vector)
		{
			if (vector.Dimensions != this.Dimensions) throw new ArgumentException("The dimension of the matrix and the vector must be the same!");

			var result = new Vector(vector.Dimensions);
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
