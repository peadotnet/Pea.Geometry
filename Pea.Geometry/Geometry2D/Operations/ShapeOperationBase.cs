using Pea.Geometry2D.Shapes;

namespace Pea.Geometry.Geometry2D.Operations
{
	public abstract class ShapeOperationBase<T1, T2> : IShapeOperation<T1, T2>, IShapeOperation
		where T1 : IShape2D
		where T2 : IShape2D
	{
		public abstract bool DoOverlap(T1 shape1, T2 shape2);
		bool IShapeOperation.DoOverlap(IShape2D shape1, IShape2D shape2)
		{
			return DoOverlap((T1)shape1, (T2)shape2);
		}

		public abstract bool IsIncluded(T1 shape1, T2 shape2);

		bool IShapeOperation.IsIncluded(IShape2D shape1, IShape2D shape2)
		{
			return IsIncluded((T1)shape1, (T2)shape2);
		}

		public abstract double Distance(T1 shape1, T2 shape2);
		double IShapeOperation.Distance(IShape2D shape1, IShape2D shape2)
		{
			return Distance((T1)shape1, (T2)shape2);
		}

		public virtual double EuclideanDistanceOfCenters(T1 shape1, T2 shape2)
		{
			double xDistance = Abs(shape1.Center.X - shape2.Center.X);
			double yDistance = Abs(shape1.Center.Y - shape2.Center.Y);

			return System.Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
		}

		double IShapeOperation.EuclideanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			return EuclideanDistanceOfCenters((T1)shape1, (T2)shape2);
		}

		public virtual double ManhattanDistanceOfCenters(T1 shape1, T2 shape2)
		{
			double xDistance = Abs(shape1.Center.X - shape2.Center.X);
			double yDistance = Abs(shape1.Center.Y - shape2.Center.Y);
			return xDistance + yDistance;
		}

		double IShapeOperation.ManhattanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			return ManhattanDistanceOfCenters((T1)shape1, (T2)shape2);
		}

		public abstract double OverlappingArea(T1 shape1, T2 shape2);
		double IShapeOperation.OverlappingArea(IShape2D shape1, IShape2D shape2)
		{
			return OverlappingArea((T1)shape1, (T2)shape2);
		}

		protected double Min(double x, double y)
		{
			return x < y ? x : y;
		}

		protected double Max(double x, double y)
		{
			return x > y ? x : y;
		}

		protected double Abs(double val)
		{
			return val < 0 ? -1 * val : val;
		}
	}
}
