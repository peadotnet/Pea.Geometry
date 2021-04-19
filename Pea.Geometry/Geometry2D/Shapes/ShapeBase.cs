using Pea.Geometry.General;
using Pea.Geometry2D.Transformations;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public abstract class ShapeBase : Transformable2DBase, IShape2D
	{
		protected Vector2D center_original { get; set; }

		private Vector2D _center;
		public Vector2D Center 
		{
			get => _center;
			set
			{
				_center = value;
				Invalidate();
			}
		}

		public virtual List<Vector2D> Points { get; protected set; }
		
		protected ShapeBase()
		{
		}

		public virtual void DoTransform()
		{
			var transformation = ComposeTransformations();
			TransformPoints(transformation);
		}

		protected void TransformPoints(Matrix<Vector2D> transformation)
		{
			Center = (Vector2D)transformation.Apply(Center);

			for(int p=0; p<Points.Count; p++)
			{
				Points[p] = (Vector2D)transformation.Apply(Points[p]);
			}
		}

		public abstract void Invalidate();
		public abstract IShape2D DoOffset(double margin);
		public abstract IShape2D DeepClone();
	}
}
