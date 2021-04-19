using Pea.Geometry.General;
using Pea.Geometry.Geometry2D;
using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public abstract class Transformable2DBase : ITransformable<Vector2D>
	{
		public IList<Matrix<Vector2D>> Transformations { get; } = new List<Matrix<Vector2D>>();

		public void Apply(Matrix<Vector2D> transformation)
		{
			Transformations.Add(transformation);
		}

		public void Rotate(double angle)
		{
			Transformations.Add(TransformationFactory.Rotation(angle));
		}

		public void Scale(double xFactor, double yFactor)
		{
			Transformations.Add(TransformationFactory.Scaling(xFactor, yFactor));
		}

		public void Translate(double xOffset, double yOffset)
		{
			Transformations.Add(TransformationFactory.Translation(xOffset, yOffset));
		}

		public void XMirror()
		{
			Transformations.Add(TransformationFactory.XMirror());
		}

		public void YMirror()
		{
			Transformations.Add(TransformationFactory.YMirror());
		}

		public void XSkew(double angle)
		{
			Transformations.Add(TransformationFactory.XSkew(angle));
		}

		public void YSkew(double angle)
		{
			Transformations.Add(TransformationFactory.YSkew(angle));
		}

		protected Matrix<Vector2D> ComposeTransformations()
		{
			if (Transformations.Count == 0) return Transformation2D.Identity(3) as Transformation2D;

			var transformation = Transformations[0];

			for (int t = 1; t < Transformations.Count; t++)
			{
				transformation = transformation * Transformations[t];
			}

			return transformation;
		}
	}
}
