using System.Collections.Generic;
[System.Serializable]
public class PuzzlePiece
{
	public List<Tile> tiles;
	[System.NonSerialized] public bool completed;

	public PuzzlePiece()
	{
		this.tiles = new List<Tile>();
		completed = false;
	}
}