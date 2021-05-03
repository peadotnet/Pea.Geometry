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
	public class RectanglePolygonOverlapBenchmarks
	{
		[Params(1000)]
		public int Count { get; set; }

		[Params(4, 20)]
		public int PolygonSides { get; set; }

		[Params(0.1, 0.9)]
		public double OverlapProbability { get; set; }

		public Random random = new Random(DateTime.Now.Millisecond);

		public Polygon RandomPolygon;
		public Polygon RegularPolygon;
		public Polygon RadialPolygon;

		public List<Rectangle> Rectangles = new List<Rectangle>();

		public RectanglePolygonOperation Operation = new RectanglePolygonOperation();

		//public RectanglePolygonOperation2 Operation2 = new RectanglePolygonOperation2();


		[GlobalSetup]
		public void Setup()
		{
			List<Vector2D> points = new List<Vector2D>();
			Console.WriteLine($"PolygonSize: {PolygonSides}");
			for (int p = 0; p < PolygonSides; p++)
			{
				var x = random.NextDouble() * 60000 + 20000;
				var y = random.NextDouble() * 60000 + 20000;
				points.Add(new Vector2D(x, y));
			}
			this.RandomPolygon = PolygonFactory.CreateByPoints(points);
			this.RandomPolygon.MarginWidth = 2000;

			points = new List<Vector2D>();
			for (int p = 0; p < PolygonSides; p++)
			{
				var x = Math.Cos(Math.PI / PolygonSides * p) * OverlapProbability * 100000 + 50000;
				var y = Math.Cos(Math.PI / PolygonSides * p) * OverlapProbability * 100000 + 50000;
				points.Add(new Vector2D(x, y));
			}
			this.RegularPolygon = PolygonFactory.CreateByPoints(points);
			this.RegularPolygon.MarginWidth = 2000;

			points = new List<Vector2D>();
			for (int p = 0; p < PolygonSides; p++)
			{
				var radius = random.NextDouble() * OverlapProbability * 50000;
				var x = Math.Cos(Math.PI / PolygonSides * p) * radius + 50000;
				var y = Math.Cos(Math.PI / PolygonSides * p) * radius + 50000;
				points.Add(new Vector2D(x, y));
			}
			this.RadialPolygon = PolygonFactory.CreateByPoints(points);
			this.RadialPolygon.MarginWidth = 2000;

			for (int i = 0; i < Count; i++)
			{
				var x = random.NextDouble() * 90000;
				var y = random.NextDouble() * 90000;
				var width = random.NextDouble() * 20000;
				var height = random.NextDouble() * 20000;
				var rectangle = RectangleFactory.CreateByDimensions(x, y, width, height);
				rectangle.MarginWidth = random.NextDouble() * 3000;
				Rectangles.Add(rectangle);
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
		public int RegularPolygonOverlapWithMargin()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlapWithMargin(Rectangles[i], RegularPolygon)) overlapCount++;
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
		public int RadialPolygonOverlapWithMargin()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlapWithMargin(Rectangles[i], RadialPolygon)) overlapCount++;
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


		[Benchmark]
		public int RandomPolygonOverlapWithMargin()
		{
			var overlapCount = 0;
			for (int i = 0; i < Rectangles.Count; i++)
			{
				if (Operation.DoOverlapWithMargin(Rectangles[i], RandomPolygon)) overlapCount++;
			}
			return overlapCount;
		}
	}
}
