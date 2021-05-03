using Pea.Geometry.General;
using System.Collections.Generic;

namespace Pea.Geometry2D.Shapes
{
	public abstract class ShapeBase : Transformable2DBase<ShapeBase>, IShape2D
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

		public double MarginWidth { get; set; }

		protected ShapeBase()
		{
		}

		public virtual void DoTransform()
		{
			var transformation = ComposeTransformations();
			TransformPoints(transformation);
			ResetTransformations();
		}

		protected void TransformPoints(Matrix<Vector2D> transformation)
		{
			Center = (Vector2D)transformation.Apply(Center);

			for(int p=0; p<Points.Count; p++)
			{
				Points[p] = (Vector2D)transformation.Apply(Points[p]);
			}
		}

		public static double CrossProductLength(Vector2D a, Vector2D b, Vector2D c)
		{
			return (b.X - a.X) * (c.Y - b.Y) - (b.Y - a.Y) * (c.X - b.X);
		}

		#region Explicite implementations for IShape2D interface
		Vector2D IShape2D.Center
		{
			get => _center;
			set
			{
				_center = value;
				Invalidate();
			}
		}
		List<Vector2D> IShape2D.Points => Points;
		public abstract void Invalidate();
		public abstract IShape2D DoOffset(double margin);
		public abstract IShape2D DeepClone();

		void IShape2D.DoTransform() => DoTransform();
		void IShape2D.Invalidate() => Invalidate();
		IShape2D IShape2D.DoOffset(double marginWidth) => DoOffset(marginWidth);
		IShape2D IDeepCloneable<IShape2D>.DeepClone() => DeepClone();
		IShape2D ITransformable<IShape2D, Vector2D>.Apply(Matrix<Vector2D> transformation) => Apply(transformation);
		IShape2D ITransformable<IShape2D, Vector2D>.Rotate(double angle) => Rotate(angle);
		IShape2D ITransformable<IShape2D, Vector2D>.Scale(double xFactor, double yFactor) => Scale(xFactor, yFactor);
		IShape2D ITransformable<IShape2D, Vector2D>.Translate(double xOffset, double yOffset) => Translate(xOffset, yOffset);
		IShape2D ITransformable<IShape2D, Vector2D>.XMirror() => XMirror();
		IShape2D ITransformable<IShape2D, Vector2D>.YMirror() => YMirror();
		IShape2D ITransformable<IShape2D, Vector2D>.XSkew(double angle) => XSkew(angle);
		IShape2D ITransformable<IShape2D, Vector2D>.YSkew(double angle) => YSkew(angle);
		#endregion
	}
}
