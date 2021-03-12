using Pea.Geometry2D.Shapes;
using System;

namespace Pea.Geometry.Geometry2D.Operations
{
	public interface IShapeOperation
	{
		bool DoOverlap(IShape2D shape1, IShape2D shape2);
		bool IsIncluded(IShape2D shape1, IShape2D shape2);
		double OverlappingArea(IShape2D shape1, IShape2D shape2);
		double Distance(IShape2D shape1, IShape2D shape2);
		double ManhattanDistanceOfCenters(IShape2D shape1, IShape2D shape2);
		double EuclideanDistanceOfCenters(IShape2D shape1, IShape2D shape2);
	}

	public interface IShapeOperation <T1, T2> : IShapeOperation
		where T1: IShape2D
		where T2: IShape2D
	{
		bool DoOverlap(T1 shape1, T2 shape2);
		bool IsIncluded(T1 shape1, T2 shape2);
		double OverlappingArea(T1 shape1, T2 shape2);
		double Distance(T1 shape1, T2 shape2);
		double ManhattanDistanceOfCenters(T1 shape1, T2 shape2);
		double EuclideanDistanceOfCenters(T1 shape1, T2 shape2);
	}
}
