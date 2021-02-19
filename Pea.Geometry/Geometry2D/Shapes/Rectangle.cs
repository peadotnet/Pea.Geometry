namespace Pea.Geometry2D.Shapes
{
	public class Rectangle : ShapeBase
	{
		public double Width { get; }
		public double Height { get; }
		public double Area { get; }
		public double Ratio { get; }

		public double Left => Center.X - Width / 2;
		public double Right => Center.X + Width / 2;
		public double Top => Center.Y + Height / 2;
		public double Bottom => Center.Y - Height / 2;

		public Rectangle(double centerX, double centerY, double width, double height)
		{
			Center = new Vector2D(centerX, centerY);
			Width = width;
			Height = height;
			Area = width * height;
			Ratio = height / width;

			Points.Add(new Vector2D(centerX - width / 2, centerX - width / 2));
			Points.Add(new Vector2D(centerX + width / 2, centerX - width / 2));
			Points.Add(new Vector2D(centerX + width / 2, centerX + width / 2));
			Points.Add(new Vector2D(centerX - width / 2, centerX + width / 2));
		}
	}
}
