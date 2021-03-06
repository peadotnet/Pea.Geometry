﻿using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;

namespace Pea.Geometry.Geometry2D.Operations
{
	public class GeneralShapeOperation : ShapeOperationBase<IShape2D, IShape2D>
	{
		Dictionary<Tuple<Type, Type>, IShapeOperation> Operations
			= new Dictionary<Tuple<Type, Type>, IShapeOperation>();

		public GeneralShapeOperation()
		{
			RegisterOperation<Rectangle, Rectangle>(new UnrotatedRectangleOperation());
			RegisterOperation<Rectangle, Polygon>(new RectanglePolygonOperation());
			RegisterOperation<Rectangle, Circle>(new RectangleCircleOperation());
		}

		public void RegisterOperation<T1, T2>(IShapeOperation<T1, T2> operation)
			where T1 : IShape2D
			where T2 : IShape2D
		{
			var key = Tuple.Create<Type, Type>(typeof(T1), typeof(T2));
			Operations.Add(key, operation);
		}

		public override bool DoOverlap(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].DoOverlap(shape1, shape2);
		}

		public override bool DoOverlapWithMargin(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].DoOverlapWithMargin(shape1, shape2);
		}

		public override double Distance(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].Distance(shape1, shape2);
		}

		public override double EuclideanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].EuclideanDistanceOfCenters(shape1, shape2);
		}

		public override double OverlappingArea(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].OverlappingArea(shape1, shape2);
		}

		public override double ManhattanDistanceOfCenters(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].ManhattanDistanceOfCenters(shape1, shape2);
		}

		public override bool IsIncluded(IShape2D shape1, IShape2D shape2)
		{
			var key = Tuple.Create<Type, Type>(shape1.GetType(), shape2.GetType());
			return Operations[key].IsIncluded(shape1, shape2);
		}
	}
}
