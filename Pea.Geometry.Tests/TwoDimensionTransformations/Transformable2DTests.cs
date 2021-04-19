using FluentAssertions;
using Pea.Geometry2D.Shapes;
using System;
using Xunit;

namespace Pea.Geometry.Tests.TwoDimensionTransformations
{
	public class Transformable2DTests
	{
		[Fact]
		public void Shape_ApplyTransformation_ShouldAdded()
		{
			var shape = new Rectangle(0, 0, 4, 3);

			shape.XMirror();
			shape.Rotate(Math.PI);
			shape.Translate(5, 0);

			shape.Transformations.Count.Should().Be(3);
		}
	}
}
