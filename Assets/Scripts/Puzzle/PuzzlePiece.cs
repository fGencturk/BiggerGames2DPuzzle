using System.Collections.Generic;

public class PuzzlePiece
{
	public List<Tile> tiles;
	public bool completed;

	public PuzzlePiece()
	{
		this.tiles = new List<Tile>();
		completed = false;
	}
}