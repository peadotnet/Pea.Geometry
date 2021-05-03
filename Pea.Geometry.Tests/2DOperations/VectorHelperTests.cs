using FluentAssertions;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D;
using Xunit;

namespace Pea.Geometry.Tests._2DOperations
{
	public class VectorHelperTests
	{
		[Theory]
		[InlineData(1, 3, 5.0)]
		[InlineData(5, 0, 5.0)]
		[InlineData(4, -4, 2.0)]
		[InlineData(2, -8, 4.0)]
		[InlineData(-3, 6, 5.0)]
		[InlineData(-1, -8, 5.0)]
		[InlineData(-5, -5, 5.0)]
		[InlineData(-9, -2, 5.0)]
		[InlineData(-8, 2, 2.0)]
		[InlineData(-6, 6, 4.0)]
		public void SectionAndPoint_GetDistance_ShouldReturnProper(double px, double py, double expected)
		{
			var l0 = new Vector2D(-6, 2);
			var l1 = new Vector2D(2, -4);
			var p = new Vector2D(px, py);

			var distance = VectorHelper.LinePointDistance(l0, l1, p);

			distance.Should().BeApproximately(expected, double.Epsilon);
		}
	}
}
