using FluentAssertions;
using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Pea.Geometry.Tests._2DShapes
{
	public class ShapeBaseTests
	{
		[Fact]
		public void Polygon_GetBoundingRectangle_ShouldReturnProper()
		{
			var polygon = PolygonTests.CreateTestPolygon();
			polygon.BoundingRectangle.Top.Should().Be(1);
			polygon.BoundingRectangle.Bottom.Should().Be(-3);
			polygon.BoundingRectangle.Left.Should().Be(-4);
			polygon.BoundingRectangle.Right.Should().Be(2);
		}

		[Fact]
		public void Circle_GetBoundingRectangle_ShouldReturnProper()
		{
			var circle = CircleFactory.Create(4, 3, 5);
			circle.BoundingRectangle.Top.Should().Be(8);
			circle.BoundingRectangle.Bottom.Should().Be(-2);
			circle.BoundingRectangle.Left.Should().Be(-1);
			circle.BoundingRectangle.Right.Should().Be(9);
		}

		[Fact]
		public void Rectangle_GetBoundingRectangle_ShouldReturnProper()
		{
			var rectangle = RectangleFactory.CreateByDimensions(3, 4, 6, 2);
			rectangle.BoundingRectangle.Top.Should().Be(5);
			rectangle.BoundingRectangle.Bottom.Should().Be(3);
			rectangle.BoundingRectangle.Left.Should().Be(0);
			rectangle.BoundingRectangle.Right.Should().Be(6);
		}

	}
}
