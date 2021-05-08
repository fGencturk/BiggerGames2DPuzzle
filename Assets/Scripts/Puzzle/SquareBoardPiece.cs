using System.Collections.Generic;

public class SquareBoardPiece
{
	public int x, y;
	public SquareVertex triangleCenterPoint;
	public Dictionary<SquareVertex, bool> sideOccupied;

	public SquareBoardPiece(int x, int y, SquareVertex triangleCenterPoint)
	{
		this.x = x;
		this.y = y;
		this.triangleCenterPoint = triangleCenterPoint;
		sideOccupied = new Dictionary<SquareVertex, bool>();
		sideOccupied[triangleCenterPoint] = false;
		sideOccupied[triangleCenterPoint.GetOppositeVertex()] = false;
	}

	public List<SquareVertex> GetUnoccupiedSquareVertexes()
	{
		List<SquareVertex> sides = new List<SquareVertex>();
		foreach (var item in sideOccupied)
		{
			if (!item.Value)
			{
				sides.Add(item.Key);
			}
		}
		return sides;
	}

	public void SetOccupied(SquareVertex side)
	{
		sideOccupied[side] = true;
	}

	public bool IsOccupied(SquareVertex side)
	{
		return sideOccupied[side];
	}

	public SquareVertex GetTriangleCenterPointOfEdge(SquareEdge edge)
	{
		if(triangleCenterPoint.GetNeighborEdges().Contains(edge))
		{
			return triangleCenterPoint;
		}
		return triangleCenterPoint.GetOppositeVertex();
	}
}