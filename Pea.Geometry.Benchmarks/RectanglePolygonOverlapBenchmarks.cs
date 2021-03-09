using BenchmarkDotNet.Attributes;
using Pea.Geometry.Geometry2D.Operations;
using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pea.Geometry.Benchmarks
{
	[MinColumn, MaxColumn]
	[HtmlExporter, RPlotExporter]
	public class RectanglePolygonOverlapBenchmarks
	{
		[Params(100)]
		public int Count { get; set; }

		[Params(4, 5, 6, 10, 20)]
		public int PolygonSize { get; set; }

		public Random random = new Random(DateTime.Now.Millisecond);

		public Polygon RandomPolygon;
		public Polygon RegularPolygon;
		public Polygon RadialPolygon;

		public List<Rectangle> Rectangles = new List<Rectangle>();

		public RectanglePolygonOperation Operation = new RectanglePolygonOperation();


		[GlobalSetup]
		public void Setup()
		{
			List<Vector2D> points = new List<Vector2D>();
			Console.WriteLine($"PolygonSize: {PolygonSize}");
			for (int p = 0; p < PolygonSize; p++)
			{
				var x = random.NextDouble() * 60000 + 20000;
				var y = random.NextDouble() * 60000 + 20000;
				points.Add(new Vector2D(x, y));
			}
			this.RandomPolygon = PolygonFactory.CreateByPoints(points);

			points = new List<Vector2D>();
			for (int p = 0; p < PolygonSize; p++)
			{
				var x = Math.Cos(Math.PI / PolygonSize * p) * 35000 + 50000;
				var y = Math.Cos(Math.PI / PolygonSize * p) * 35000 + 50000;
				points.Add(new Vector2D(x, y));
			}
			this.RegularPolygon = PolygonFactory.CreateByPoints(points);

			points = new List<Vector2D>();
			for (int p = 0; p < PolygonSize; p++)
			{
				var radius = random.NextDouble() * 50000;
				var x = Math.Cos(Math.PI / PolygonSize * p) * radius + 50000;
				var y = Math.Cos(Math.PI / PolygonSize * p) * radius + 50000;
				points.Add(new Vector2D(x, y));
			}
			this.RadialPolygon = PolygonFactory.CreateByPoints(points);


			for (int i = 0; i < Count; i++)
			{
				var x = random.NextDouble() * 90000;
				var y = random.NextDouble() * 90000;
				var width = random.NextDouble() * 20000;
				var height = random.NextDouble() * 20000;
				Rectangles.Add(new Rectangle(x, y, width, height));
			}
		}

		[Benchmark]
		public int RegularPolygonOverlap()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlap(Rectangles[i], RegularPolygon)) overlapCount++;
			}
			return overlapCount;
		}

		[Benchmark]
		public int RadialPolygonOverlap()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlap(Rectangles[i], RadialPolygon)) overlapCount++;
			}
			return overlapCount;
		}

		[Benchmark]
		public int RandomPolygonOverlap()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlap(Rectangles[i], RandomPolygon)) overlapCount++;
			}
			return overlapCount;
		}
	}
}
