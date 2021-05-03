using FluentAssertions;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D;
using System.Collections.Generic;
using Xunit;

namespace Pea.Geometry.Tests.Operations
{
	public class GeneralShapeOperationTests
	{
		[Fact]
		public void TwoRectangle_DoOverlap_ShouldReturn()
		{
			var operation = new GeneralShapeOperation();
			var rectangle1 = RectangleFactory.CreateByDimensions(0, 0, 3, 3);
			var rectangle2 = RectangleFactory.CreateByDimensions(2, 2, 3, 3);

			var result = operation.DoOverlap(rectangle1, rectangle2);
			result.Should().BeTrue();
		}

		[Fact]
		public void RectangleAndPolygon_DoOverlap_ShouldReturn()
		{
			var operation = new GeneralShapeOperation();
			var rectangle = RectangleFactory.CreateByDimensions(0, 0, 3, 3);
			var polygon = PolygonFactory.CreateByPoints(new List<Vector2D>()
			{
				new Vector2D(1, 1),
				new Vector2D(1, 2),
				new Vector2D(2, 1)
			});

			var result = operation.DoOverlap(rectangle, polygon);
			result.Should().BeTrue();
		}

	}
}
