[System.Serializable]
public class Tile
{
	public int x, y;
	public SquareVertex triangleCenterPoint;
	[System.NonSerialized] public bool completed;

	public Tile(int x, int y, SquareVertex triangleCenterPoint)
	{
		this.x = x;
		this.y = y;
		this.triangleCenterPoint = triangleCenterPoint;
	}
}