using Pea.Geometry2D.Shapes;

namespace Pea.Geometry.Geometry2D.Operations
{
	public interface IShapeOperation <T1, T2> 
		where T1: ShapeBase
		where T2: ShapeBase
	{
		bool DoOverlap(T1 shape1, T2 shape2);
		double OverlappingArea(T1 shape1, T2 shape2);
		double Distance(T1 shape1, T2 shape2);
		double ManhattanDistance(T1 shape1, T2 shape2);
	}
}
