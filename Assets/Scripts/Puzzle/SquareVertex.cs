using System.Collections.Generic;

public enum SquareVertex
{
	TopLeft,
	TopRight,
	BottomLeft,
	BottomRight
}

static class SquareVertexMethods
{
	/// <summary>
	/// Returns complementary triangle side to form a square.
	/// </summary>
	static Dictionary<SquareVertex, SquareVertex> oppositeVertexes = new Dictionary<SquareVertex, SquareVertex>()
	{
		{ SquareVertex.TopLeft,		SquareVertex.BottomRight },
		{ SquareVertex.TopRight,	SquareVertex.BottomLeft },
		{ SquareVertex.BottomRight, SquareVertex.TopLeft	},
		{ SquareVertex.BottomLeft,	SquareVertex.TopRight	}
	};

	static Dictionary<SquareVertex, List<SquareEdge>> edgesOfVertex = new Dictionary<SquareVertex, List<SquareEdge>>()
	{
		{ SquareVertex.TopLeft,		new List<SquareEdge>() { SquareEdge.Top,	SquareEdge.Left } },
		{ SquareVertex.TopRight,	new List<SquareEdge>() { SquareEdge.Top,	SquareEdge.Right } },
		{ SquareVertex.BottomRight, new List<SquareEdge>() { SquareEdge.Bottom, SquareEdge.Right } },
		{ SquareVertex.BottomLeft,	new List<SquareEdge>() { SquareEdge.Bottom, SquareEdge.Left } },
	};

	public static SquareVertex GetOppositeVertex(this SquareVertex squareVertex)
	{
		return oppositeVertexes[squareVertex];
	}

	public static List<SquareEdge> GetNeighborEdges(this SquareVertex squareVertex)
	{
		return edgesOfVertex[squareVertex];
	}
}