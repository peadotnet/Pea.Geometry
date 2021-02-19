using FluentAssertions;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D.Shapes;
using Xunit;

namespace Pea.Geometry.Tests.Operations
{
	public class UnrotatedRectangleOpearationTests
	{
		[Theory]
		[InlineData(2, 1, 4, 2, -3, 2, 4, 2)]
		[InlineData(2, 1, 4, 2, 2, 4, 4, 2)]
		public void DisjuctRectangles_DoOverlap_ShouldReturnFalse(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
		{
			var rectangle1 = new Rectangle(x1, y1, w1, h1);
			var rectangle2 = new Rectangle(x2, y2, w2, h2);
			var intersection = new UnrotatedRectangleOperation();

			var result = intersection.DoOverlap(rectangle1, rectangle2);
			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(2, 1, 4, 2, -1, 2, 4, 2)]
		[InlineData(2, 1, 4, 2, 2, 3, 4, 2)]
		[InlineData(0, 0, 4, 2, 0, 0, 2, 1)]
		public void IntersectedRectangles_DoOverlap_ShouldReturnTrue(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
		{
			var rectangle1 = new Rectangle(x1, y1, w1, h1);
			var rectangle2 = new Rectangle(x2, y2, w2, h2);
			var intersection = new UnrotatedRectangleOperation();

			var result = intersection.DoOverlap(rectangle1, rectangle2);
			result.Should().BeTrue();
		}

		[Theory]
		[InlineData(0, 0, 6, 4, 0, 0, 4, 2, -3)]
		[InlineData(0, 3, 4, 2, 0, 0, 4, 2, 1)]
		[InlineData(0, 3, 6, 4, 0, 0, 4, 2, 0)]
		[InlineData(1, 1, 4, 2, 7, 2, 4, 2, 2)]
		[InlineData(1, 1, 4, 2, 7, 4, 4, 2, 2)]
		[InlineData(1, 1, 4, 2, 7, 6, 4, 2, 3)]
		[InlineData(0, 0, 6, 4, 4, 2, 4, 2, -1)]
		public void Rectangles_Distance_ShouldReturnCorrect(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2, double expected)
		{
			var rectangle1 = new Rectangle(x1, y1, w1, h1);
			var rectangle2 = new Rectangle(x2, y2, w2, h2);
			var opreation = new UnrotatedRectangleOperation();

			var result = opreation.Distance(rectangle1, rectangle2);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(1, 1, 4, 2, 7, 2, 4, 2, 2)]
		[InlineData(1, 1, 4, 2, -3, 4, 2, 2, 2)]
		[InlineData(1, 1, 4, 2, 1, -2, 3, 2, 1)]
		public void Rectangles_ManhattanDistance_ShouldReturnCorrect(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2, double expected)
		{
			var rectangle1 = new Rectangle(x1, y1, w1, h1);
			var rectangle2 = new Rectangle(x2, y2, w2, h2);
			var opreation = new UnrotatedRectangleOperation();

			var result = opreation.ManhattanDistance(rectangle1, rectangle2);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 0, 4, 2, 4, 0, 4, 2, 0)]
		[InlineData(0, 0, 4, 2, 3, 0, 4, 2, 2)]
		[InlineData(0, 0, 4, 2, 3, 1, 4, 2, 1)]
		[InlineData(0, 0, 6, 4, 0, 0, 4, 2, 8)]
		[InlineData(0, 0, 6, 4, 0, 3, 2, 4, 2)]
		public void Rectangles_OverlappingArea_ShouldReturnCorrect(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2, double expected)
		{
			var rectangle1 = new Rectangle(x1, y1, w1, h1);
			var rectangle2 = new Rectangle(x2, y2, w2, h2);
			var opreation = new UnrotatedRectangleOperation();

			var result = opreation.OverlappingArea(rectangle1, rectangle2);
			result.Should().Be(expected);
		}
	}
}
