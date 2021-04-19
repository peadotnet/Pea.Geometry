namespace Pea.Geometry.General
{
	public interface IDeepCloneable<out T>
	{
		T DeepClone();
	}
}