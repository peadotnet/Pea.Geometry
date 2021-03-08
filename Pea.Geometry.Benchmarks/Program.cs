using BenchmarkDotNet.Running;

namespace Pea.Geometry.Benchmarks
{
	class Program
	{
		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<RectanglePolygonOperationBenchmarks>();
		}
	}
}
