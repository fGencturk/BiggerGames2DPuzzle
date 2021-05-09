using System.Collections.Generic;
using UnityEngine;

public class SquareBoardPiece
{
	public int x, y;
	public SquareVertex triangleCenterPoint;
	public Dictionary<SquareVertex, bool> sideOccupied;

	public SquareBoardPiece(int x, int y, SquareVertex triangleCenterPoint)
	{
		this.x = x;
		this.y = y;
		SetTriangleCenterPoint(triangleCenterPoint);
	}

	public void SetTriangleCenterPoint(SquareVertex triangleCenterPoint)
	{
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

	public void SetOccupied(SquareVertex side, bool value)
	{
		if(sideOccupied.ContainsKey(side)) {
			sideOccupied[side] = value;
		} else
		{
			Debug.LogError("Given SquareVertex does not exist in dict!");
		}
	}

	public bool isEmpty()
	{
		return GetUnoccupiedSquareVertexes().Count == 2;
	}

	public bool IsOccupied(SquareVertex side)
	{
		if(!sideOccupied.ContainsKey(side))
		{
			return true;
		}
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