﻿/* 
 * Smallest enclosing circle - Test suite (C#)
 * 
 * Copyright (c) 2017 Project Nayuki
 * https://www.nayuki.io/page/smallest-enclosing-circle
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program (see COPYING.txt and COPYING.LESSER.txt).
 * If not, see <http://www.gnu.org/licenses/>.
 */

using Pea.Geometry2D;
using Pea.Geometry2D.Shapes;
using System;
using System.Collections.Generic;

public sealed class SmallestEnclosingCircleTest
{

	public static void RunAllTest(string[] args)
	{
		TestMatchingNaiveAlgorithm();
		TestTranslation();
		TestScaling();
	}

	private static void TestMatchingNaiveAlgorithm()
	{
		int TRIALS = 1000;
		for (int i = 0; i < TRIALS; i++)
		{
			IList<Vector2D> points = MakeRandomPoints(rand.Next(30) + 1);
			Circle reference = SmallestEnclosingCircleNaive(points);
			Circle actual = SmallestEnclosingCircle.MakeCircle(points);
			AssertApproxEqual(reference.Center.X, actual.Center.X, EPSILON);
			AssertApproxEqual(reference.Center.Y, actual.Center.Y, EPSILON);
			AssertApproxEqual(reference.Radius, actual.Radius, EPSILON);
		}
	}


	private static void TestTranslation()
	{
		int TRIALS = 100;
		int CHECKS = 10;
		for (int i = 0; i < TRIALS; i++)
		{
			IList<Vector2D> points = MakeRandomPoints(rand.Next(300) + 1);
			Circle reference = SmallestEnclosingCircle.MakeCircle(points);

			for (int j = 0; j < CHECKS; j++)
			{
				double dx = NextGaussian();
				double dy = NextGaussian();
				IList<Vector2D> newPoints = new List<Vector2D>();
				foreach (Vector2D p in points)
					newPoints.Add(new Vector2D(p.X + dx, p.Y + dy));

				Circle translated = SmallestEnclosingCircle.MakeCircle(newPoints);
				AssertApproxEqual(reference.Center.X + dx, translated.Center.X, EPSILON);
				AssertApproxEqual(reference.Center.Y + dy, translated.Center.Y, EPSILON);
				AssertApproxEqual(reference.Radius, translated.Radius, EPSILON);
			}
		}
	}

	private static void TestScaling()
	{
		int TRIALS = 100;
		int CHECKS = 10;
		for (int i = 0; i < TRIALS; i++)
		{
			IList<Vector2D> points = MakeRandomPoints(rand.Next(300) + 1);
			Circle reference = SmallestEnclosingCircle.MakeCircle(points);

			for (int j = 0; j < CHECKS; j++)
			{
				double scale = NextGaussian();
				IList<Vector2D> newPoints = new List<Vector2D>();
				foreach (Vector2D p in points)
					newPoints.Add(new Vector2D(p.X * scale, p.Y * scale));

				Circle scaled = SmallestEnclosingCircle.MakeCircle(newPoints);
				AssertApproxEqual(reference.Center.X * scale, scaled.Center.X, EPSILON);
				AssertApproxEqual(reference.Center.Y * scale, scaled.Center.Y, EPSILON);
				AssertApproxEqual(reference.Radius * Math.Abs(scale), scaled.Radius, EPSILON);
			}
		}
	}

	/*---- Helper functions ----*/

	private static IList<Vector2D> MakeRandomPoints(int n)
	{
		IList<Vector2D> result = new List<Vector2D>();
		if (rand.NextDouble() < 0.2)
		{  // Discrete lattice (to have a chance of duplicated points)
			for (int i = 0; i < n; i++)
				result.Add(new Vector2D(rand.Next(10), rand.Next(10)));
		}
		else
		{  // Gaussian distribution
			for (int i = 0; i < n; i++)
				result.Add(new Vector2D(NextGaussian(), NextGaussian()));
		}
		return result;
	}


	// Returns the smallest enclosing circle in O(n^4) time using the naive algorithm.
	private static Circle SmallestEnclosingCircleNaive(IList<Vector2D> points)
	{
		// Degenerate cases
		if (points.Count == 0)
			return new Circle(new Vector2D(0, 0), -1);
		else if (points.Count == 1)
			return new Circle(points[0], 0);

		// Try all unique pairs
		Circle result = new Circle(new Vector2D(0, 0), -1);
		for (int i = 0; i < points.Count; i++)
		{
			for (int j = i + 1; j < points.Count; j++)
			{
				Circle c = SmallestEnclosingCircle.MakeDiameter(points[i], points[j]);
				if ((result.Radius < 0 || c.Radius < result.Radius) && Contains(c, points))
					result = c;
			}
		}
		if (result.Radius >= 0)
			return result;  // This optimization is not mathematically proven

		// Try all unique triples
		for (int i = 0; i < points.Count; i++)
		{
			for (int j = i + 1; j < points.Count; j++)
			{
				for (int k = j + 1; k < points.Count; k++)
				{
					Circle c = SmallestEnclosingCircle.MakeCircumcircle(points[i], points[j], points[k]);
					if (c.Radius >= 0 && (result.Radius < 0 || c.Radius < result.Radius) && Contains(c, points))
						result = c;
				}
			}
		}
		if (result.Radius < 0)
			throw new SystemException("Assertion error");
		return result;
	}


	private static double NextGaussian()
	{
		return Math.Sqrt(-2 * Math.Log(rand.NextDouble())) * Math.Cos(rand.NextDouble() * Math.PI * 2);
	}


	private static void AssertApproxEqual(double expect, double actual, double epsilon)
	{
		if (Math.Abs(expect - actual) > epsilon)
			throw new SystemException("Value mismatch");
	}


	private const double EPSILON = 1e-12;

	private static Random rand = new Random();


	private static bool Contains(Circle c, IList<Vector2D> points)
	{
		for (int i=0; i< points.Count; i++)
		{
			if (c.Contains(points[i])) return false;
		}
		return true;
	}
}