using System.Collections.Generic;

public enum SquareEdge
{
	Bottom,
	Left,
	Top,
	Right
}
static class SquareEdgeMethods
{
	static Dictionary<SquareEdge, SquareEdge> oppositeEdges = new Dictionary<SquareEdge, SquareEdge>()
	{
		{ SquareEdge.Top,		SquareEdge.Bottom	},
		{ SquareEdge.Bottom,    SquareEdge.Top		},
		{ SquareEdge.Left,		SquareEdge.Right    },
		{ SquareEdge.Right,		SquareEdge.Left		}
	};

	static Dictionary<SquareEdge, List<SquareVertex>> vertexesOfEdge = new Dictionary<SquareEdge, List<SquareVertex>>()
	{
		{ SquareEdge.Top,		new List<SquareVertex>() { SquareVertex.TopLeft,	SquareVertex.TopRight	 } },
		{ SquareEdge.Bottom,	new List<SquareVertex>() { SquareVertex.BottomLeft, SquareVertex.BottomRight } },
		{ SquareEdge.Left,		new List<SquareVertex>() { SquareVertex.TopLeft,	SquareVertex.BottomLeft	 } },
		{ SquareEdge.Right,		new List<SquareVertex>() { SquareVertex.TopRight,	SquareVertex.BottomRight } },
	};

	public static SquareEdge GetOppositeEdge(this SquareEdge squareEdge)
	{
		return oppositeEdges[squareEdge];
	}

	public static List<SquareVertex> GetNeighborVertexes(this SquareEdge squareEdge)
	{
		return vertexesOfEdge[squareEdge];
	}
}