using Pea.Geometry2D.Shapes;

namespace Pea.Geometry.Geometry2D.Operations
{
	public abstract class ShapeOperationBase<T1, T2> : IShapeOperation<T1, T2>
		where T1 : IShape2D
		where T2 : IShape2D
	{
		public abstract bool DoOverlap(T1 shape1, T2 shape2);
		bool IShapeOperation.DoOverlap(IShape2D shape1, IShape2D shape2)
		{
			return DoOverlap((T1)shape1, (T2)shape2);
		}

		public abstract double Distance(T1 shape1, T2 shape2);
		double IShapeOperation.Distance(IShape2D shape1, IShape2D shape2)
		{
			return Distance((T1)shape1, (T2)shape2);
		}

		public abstract double EuclideanDistanceOfCenters(T1 shape1, T2 shape2);
		double IShapeOperation.EuclideanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			return EuclideanDistanceOfCenters((T1)shape1, (T2)shape2);
		}

		public abstract double ManhattanDistanceOfCenters(T1 shape1, T2 shape2);
		double IShapeOperation.ManhattanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			return ManhattanDistanceOfCenters((T1)shape1, (T2)shape2);
		}

		public abstract double OverlappingArea(T1 shape1, T2 shape2);
		double IShapeOperation.OverlappingArea(IShape2D shape1, IShape2D shape2)
		{
			return OverlappingArea((T1)shape1, (T2)shape2);
		}
	}
}
