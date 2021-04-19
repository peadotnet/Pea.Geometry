using FluentAssertions;
using Pea.Geometry.General;
using Pea.Geometry2D;
using Xunit;

namespace Pea.Geometry.Tests
{
	public class MatrixTests
	{
		[Fact]
		public void Matrix_Identity2_ShouldReturnIdentityMatrix()
		{
			var matrix = Matrix<Vector2D>.Identity(2);
			var expected = new double[2, 2] { { 1, 0 }, { 0, 1 } };
			matrix.Values.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void Matrix_Identity3_ShouldReturnIdentityMatrix()
		{
			var matrix = Matrix<Vector2D>.Identity(3);
			var expected = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
			matrix.Values.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public void Matrix3_MultipleWithIdentity_ShouldRemainUnchanged()
		{
			double[,] values = { { 6.1, 9.2, 0.0 }, { 1.12, 0.33, -365.1 }, { 5.0, 2020.07, 31.13 } };
			var matrix = new Matrix<Vector2D>(values);
			var identity = Matrix<Vector2D>.Identity(3);

			var result = matrix * identity;

			result.Values.Should().BeEquivalentTo(values);
		}



	}
}
