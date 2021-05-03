using Pea.Geometry.General;
using Pea.Geometry.Geometry2D;
using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public abstract class Transformable2DBase<TC> : ITransformable<TC, Vector2D>
		where TC: Transformable2DBase<TC>
	{
		public IList<Matrix<Vector2D>> Transformations { get; } = new List<Matrix<Vector2D>>();

		public virtual TC Apply(Matrix<Vector2D> transformation)
		{
			Transformations.Add(transformation);
			return (TC)this;
		}

		public virtual TC Rotate(double angle)
		{
			Transformations.Add(TransformationFactory.Rotation(angle));
			return (TC)this;
		}

		public virtual TC Scale(double xFactor, double yFactor)
		{
			Transformations.Add(TransformationFactory.Scaling(xFactor, yFactor));
			return (TC)this;
		}

		public virtual TC Translate(double xOffset, double yOffset)
		{
			Transformations.Add(TransformationFactory.Translation(xOffset, yOffset));
			return (TC)this;
		}

		public virtual TC XMirror()
		{
			Transformations.Add(TransformationFactory.XMirror());
			return (TC)this;
		}

		public virtual TC YMirror()
		{
			Transformations.Add(TransformationFactory.YMirror());
			return (TC)this;
		}

		public virtual TC XSkew(double angle)
		{
			Transformations.Add(TransformationFactory.XSkew(angle));
			return (TC)this;
		}

		public virtual TC YSkew(double angle)
		{
			Transformations.Add(TransformationFactory.YSkew(angle));
			return (TC)this;
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

		protected void ResetTransformations()
		{
			Transformations.Clear();
		}
	}
}
