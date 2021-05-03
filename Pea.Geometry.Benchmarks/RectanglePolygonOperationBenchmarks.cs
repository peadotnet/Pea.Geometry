using BenchmarkDotNet.Attributes;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;

namespace Pea.Geometry.Benchmarks
{
	[MinColumn, MaxColumn]
	[HtmlExporter, RPlotExporter]
	public class RectanglePolygonOperationBenchmarks
	{
		[Params(100)]
		public int Count { get; set; }

		[Params(4, 5, 6, 10, 20)]
		public int PolygonSize { get; set; }

		public Polygon RandomPolygon;

		public List<Vector2D> Points = new List<Vector2D>();

		public Random random = new Random(DateTime.Now.Millisecond);

		public RectanglePolygonOperation Operation = new RectanglePolygonOperation();

		public RectanglePolygonOperationBenchmarks()
		{
		}

		[GlobalSetup]
		public void Setup()
		{
			List<Vector2D> points = new List<Vector2D>();
			Console.WriteLine($"PolygonSize: {PolygonSize}");
			for (int p = 0; p < PolygonSize; p++)
			{
				var x = random.NextDouble() * 100000;
				var y = random.NextDouble() * 100000;
				points.Add(new Vector2D(x, y));
			}
			this.RandomPolygon = PolygonFactory.CreateByPoints(points);

			for (int i = 0; i < Count; i++)
			{
				var x = random.NextDouble() * 100000;
				var y = random.NextDouble() * 100000;
				Points.Add(new Vector2D(x, y));
			}
		}

		[Benchmark]
		public int IsInsideBounding()
		{
			var insideCount = 0;
			for (int i = 0; i < Points.Count; i++)
			{
				if (Operation.IsInsideBounding(Points[i], RandomPolygon)) insideCount++;
			}
			return insideCount;
		}

		[Benchmark]
		public int cn_Poly()
		{
			var insideCount = 0;
			for (int i = 0; i < Count; i++)
			{
				if (Operation.cn_PnPoly(Points[i], RandomPolygon)) insideCount++;
			}
			return insideCount;
		}
	}
}
