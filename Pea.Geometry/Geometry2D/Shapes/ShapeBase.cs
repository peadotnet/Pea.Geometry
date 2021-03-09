using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public abstract class ShapeBase : IShape2D
	{
		protected Vector2D center_original { get; set; }
		public Vector2D Center { get; set; }

		public virtual List<Vector2D> Points { get; protected set; }
		public IList<Transformation2D> Transformations { get; } = new List<Transformation2D>();
		
		protected ShapeBase()
		{
		}

		public virtual void DoTransform()
		{
			var transformation = ComposeTransformations();
			TransformPoints(transformation);
		}

		protected Transformation2D ComposeTransformations()
		{
			if (Transformations.Count == 0) return Transformation2D.Identity(3) as Transformation2D;

			var transformation = Transformations[0];

			for (int t = 1; t < Transformations.Count; t++)
			{
				transformation = transformation * Transformations[t];
			}

			return transformation;
		}

		protected void TransformPoints(Transformation2D transformation)
		{
			Center = (Vector2D)transformation.Apply(Center);

			for(int p=0; p<Points.Count; p++)
			{
				Points[p] = (Vector2D)transformation.Apply(Points[p]);
			}
		}
	}
}
