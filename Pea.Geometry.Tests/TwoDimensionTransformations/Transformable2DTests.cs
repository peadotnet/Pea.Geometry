using FluentAssertions;
using Pea.Geometry.Geometry2D;
using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using Pea.Geometry2D.Transformations;
using System;
using System.Collections.Generic;
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

		[Fact]
		public void Scene_FluentBuildTransformations_ShouldAdded()
		{
			var rotationCenterX = 10.0;
			var rotationCenterY = 15.0;
			var rotationAngle = Math.PI/2;

			var shapes1 = CreateShapes();
			var shapes2 = CreateShapes();

			//Old syntax: method to method
			var translation1 = new Translation2D(rotationCenterX, rotationCenterY);
			var rotation = new Rotation2D(rotationAngle);
			var translation2 = new Translation2D(-rotationCenterX, -rotationCenterY);
			var transformation = Transformation2D.Compose(translation1, rotation, translation2);

			for(int i=0; i< shapes1.Count;i++)
			{
				shapes1[i].Apply(transformation);
				shapes1[i].DoTransform();
			}

			//New syntax: fluent builder
			shapes2.Translate(rotationCenterX, rotationCenterY).Rotate(rotationAngle).Translate(-rotationCenterX, -rotationCenterY).DoTransform();

			shapes1.Shapes.Should().BeEquivalentTo(shapes2.Shapes, options => 
			{
				options.Excluding(shape => shape.BoundingRectangle);
				return options;
			});
		}


		private Scene2D CreateShapes()
		{
			var scene = new Scene2D();
			scene.Shapes.Add(new Circle(new Vector2D(3, 5), 8));
			scene.Shapes.Add(RectangleFactory.CreateByDimensions(7, -2, 12, 8));
			var points = new List<Vector2D>()
			{
				new Vector2D(2,2), new Vector2D(6, 7), new Vector2D(4, -3), new Vector2D(-2, 1)
			};
			scene.Shapes.Add(PolygonFactory.CreateByPoints(points));
			return scene;
		}
	}
}
