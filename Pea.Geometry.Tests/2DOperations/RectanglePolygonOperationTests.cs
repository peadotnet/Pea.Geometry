using FluentAssertions;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System.Collections.Generic;
using Xunit;
using static Pea.Geometry.Geometry2D.Operations.RectanglePolygonOperation;

namespace Pea.Geometry.Tests.Operations
{
	public class RectanglePolygonOperationTests
	{
		[Theory]
		[InlineData(0, 0, 21, 11, ShapeRelation.Intersected)]
		[InlineData(-5, -1, 2, 2, ShapeRelation.ToBeCalculated)]
		[InlineData(0, 4, 4, 2, ShapeRelation.ToBeCalculated)]
		[InlineData(0, 7, 4, 2, ShapeRelation.Outside)]
		[InlineData(-13, -2, 4, 2, ShapeRelation.Outside)]
		[InlineData(-11, -2, 4, 2, ShapeRelation.ToBeCalculated)]
		[InlineData(9, 4, 4, 3, ShapeRelation.Intersected)]

		public void Rectangle_CheckPolygonPoints_ShouldReturnProper(double x, double y, double width, double height, RectanglePolygonOperation.ShapeRelation expected)
		{
			Polygon polygon = CreateTestPolygon1();

			var rectangle = RectangleFactory.CreateByDimensions(x, y, width, height);
			var operation = new RectanglePolygonOperation();

			var result = operation.CheckPolygonPoints(rectangle, polygon);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 0, true)]
		[InlineData(-5, 3, true)]
		[InlineData(-3, 2, true)]
		[InlineData(5, 3, true)]
		[InlineData(3, 2, true)]

		public void Point_IsInside(double x, double y, bool expected)
		{
			Polygon polygon = CreateTestPolygon1();
			var operation = new RectanglePolygonOperation();
			var result = operation.IsInside(x, y, polygon);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 0, true)]
		[InlineData(-5, 3, true)]
		[InlineData(-3, 2, true)]
		[InlineData(5, 3, true)]
		[InlineData(3, 2, true)]

		public void Point_cn_Poly(double x, double y, bool expected)
		{
			Vector2D point = new Vector2D(x, y);
			Polygon polygon = CreateTestPolygon1();
			var operation = new RectanglePolygonOperation();
			var result = operation.cn_PnPoly(point, polygon);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 0, true)]
		[InlineData(-5, 3, true)]
		[InlineData(-3, 2, true)]
		[InlineData(5, 3, true)]
		[InlineData(3, 2, true)]

		public void Point_wn_Poly(double x, double y, bool expected)
		{
			Vector2D point = new Vector2D(x, y);
			Polygon polygon = CreateTestPolygon1();
			var operation = new RectanglePolygonOperation();
			var result = operation.wn_PnPoly(point, polygon);
			result.Should().Be(expected);
		}

		private static Polygon CreateTestPolygon1()
		{
			var points = new List<Vector2D>()
			{
				new Vector2D(-10, 5),
				new Vector2D(-5, 5),
				new Vector2D(-2, 2),
				new Vector2D(2, 2),
				new Vector2D(5, 5),
				new Vector2D(10, 5),
				new Vector2D(8, -5),
				new Vector2D(-8, -5),
				new Vector2D(-10, 5)
			};
			var polygon = PolygonFactory.CreateByPoints(points);
			return polygon;
		}
	}
}
