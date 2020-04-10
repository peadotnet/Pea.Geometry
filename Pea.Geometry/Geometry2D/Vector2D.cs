using Pea.Geometry.General;
using System;

namespace Pea.Geometry2D
{
	public class Vector2D : Vector
	{
		public static Vector2D XAxis { get; } = new Vector2D(1, 0);
		public static Vector2D YAxis { get; } = new Vector2D(0, 1);

		public double X
		{
			get
			{
				return Coordinates[0];
			}
			set
			{
				Coordinates[0] = value;
			}
		}
		public double Y
		{
			get
			{
				return Coordinates[1];
			}
			set
			{
				Coordinates[1] = value;
			}
		}

		public Vector2D() : base(3) { }

		public Vector2D(double x, double y) : this()
		{
			X = x;
			Y = y;
			Coordinates[2] = 1;
		}

		public Vector2D(double angle) : this()
		{
			X = Math.Cos(angle);
			Y = Math.Sin(angle);
			Coordinates[2] = 1;
		}

		public double GetLength()
		{
			return Math.Sqrt(X * X + Y * Y);
		}

		public Vector2D GetNormal()
		{
			return new Vector2D(Y, -1 * X);
		}

		#region Operator overloading
		public static Vector2D operator -(Vector2D vector2, Vector2D vector1)
		{
			return new Vector2D(vector2.X - vector1.X, vector2.Y - vector1.Y);
		}

		public static Vector2D operator +(Vector2D vector2, Vector2D vector1)
		{
			return new Vector2D(vector2.X + vector1.X, vector2.Y + vector1.Y);
		}

		public static Vector2D operator *(Vector2D vector, double scalar)
		{
			return new Vector2D(vector.X * scalar, vector.Y * scalar);
		}
		#endregion
	}
}
