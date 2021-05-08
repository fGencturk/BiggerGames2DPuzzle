using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField]
	private int boardSizeX = 4,
		boardSizeY = 4,
		puzzlePieceCount = 4;

	private SquareBoardPiece[,] board;
	private PuzzlePiece[] puzzlePieces;

	public PuzzlePiece[] GenerateBoard()
	{
		// Initialize squares with random triangle sides
		board = new SquareBoardPiece[boardSizeY, boardSizeX];
		for (int y = 0; y < boardSizeY; y++)
		{
			for (int x = 0; x < boardSizeX; x++)
			{
				SquareVertex randomSquareVertex = RandomEnumGenerator.RandomEnumValue<SquareVertex>();
				board[y, x] = new SquareBoardPiece(x, y, randomSquareVertex);
			}
		}

		// Initialize puzzle pieces with random initialTile
		puzzlePieces = new PuzzlePiece[puzzlePieceCount];
		for (int i = 0; i < puzzlePieceCount; i++)
		{
			puzzlePieces[i] = new PuzzlePiece();
			Tile randomTile = GetRandomInitialTile();
			puzzlePieces[i].tiles.Add(randomTile);
		}

		// Generate random board

		//// Generate random starting points for each puzzle piece
		bool completed = false;
		while (!completed)
		{
			completed = true;
			for (int i = 0; i < puzzlePieceCount; i++)
			{
				if (puzzlePieces[i].completed)
				{
					continue;
				}
				puzzlePieces[i].completed = true;
				List<Tile> shuffledTiles = puzzlePieces[i].tiles.Shuffle().ToList();

				foreach (Tile tile in shuffledTiles)
				{
					Tile selectedRandomNeighbor = GetRandomUnoccupiedNeighbor(tile);
					if (selectedRandomNeighbor != null)
					{
						puzzlePieces[i].tiles.Add(selectedRandomNeighbor);
						puzzlePieces[i].completed = false;
						completed = false;
					}
				}
			}
		}

		// Print
		for (int i = 0; i < puzzlePieceCount; i++)
		{
			Debug.Log("-------------TILE-------------------");
			foreach (var item in puzzlePieces[i].tiles)
			{
				Debug.Log(item.y + " " + item.x + " " + item.triangleCenterPoint);
			}
		}

		return puzzlePieces;
	}

	Tile GetRandomInitialTile()
	{
		int y = Random.Range(0, boardSizeY);
		int x = Random.Range(0, boardSizeX);

		List<SquareVertex> sides = board[y, x].GetUnoccupiedSquareVertexes();
		if (sides.Count == 0)
		{
			return GetRandomInitialTile();
		}
		SquareVertex side = PickRandomElementOfList(sides);
		board[y, x].SetOccupied(side);
		return new Tile(x, y, side);
	}

	Tile GetRandomUnoccupiedNeighbor(Tile tile)
	{
		List<Tile> unoccupiedNeighbors = new List<Tile>();

		// Check if opposite triangle of square is Unoccupied
		if (!board[tile.y, tile.x].IsOccupied(tile.triangleCenterPoint.GetOppositeVertex()))
		{
			unoccupiedNeighbors.Add(new Tile(tile.x, tile.y, tile.triangleCenterPoint.GetOppositeVertex()));
		}

		// Check if touching triangle of neighbor squares is Unoccupied
		List<SquareEdge> edgesOfTriangleVertex = tile.triangleCenterPoint.GetNeighborEdges();

		void AddUnoccupiedVertex(int x, int y, SquareEdge edge)
		{
			Tile unoccupiedTile = TryGetUnoccupiedTriangleCenterPointOfEdge(x, y, edge);
			if (unoccupiedTile != null)
			{
				unoccupiedNeighbors.Add(unoccupiedTile);
			}
		}

		foreach (SquareEdge edge in edgesOfTriangleVertex)
		{
			if (edge == SquareEdge.Bottom && tile.y != boardSizeY - 1)
			{
				AddUnoccupiedVertex(tile.x, tile.y + 1, edge);
			}
			else if (edge == SquareEdge.Top && tile.y != 0)
			{
				AddUnoccupiedVertex(tile.x, tile.y - 1, edge);
			}
			else if (edge == SquareEdge.Left && tile.x != 0)
			{
				AddUnoccupiedVertex(tile.x - 1, tile.y, edge);
			}
			else if (tile.x != boardSizeX - 1)
			{
				AddUnoccupiedVertex(tile.x + 1, tile.y, edge);
			}
		}

		if (unoccupiedNeighbors.Count == 0)
		{
			return null;
		}
		Tile neighbor = PickRandomElementOfList(unoccupiedNeighbors);
		board[neighbor.y, neighbor.x].SetOccupied(neighbor.triangleCenterPoint);
		return neighbor;
	}

	Tile TryGetUnoccupiedTriangleCenterPointOfEdge(int x, int y, SquareEdge edge)
	{
		SquareBoardPiece boardPiece = board[y, x];
		SquareVertex triangleCenterPoint = boardPiece.GetTriangleCenterPointOfEdge(edge.GetOppositeEdge());
		if (!boardPiece.IsOccupied(triangleCenterPoint))
		{
			return new Tile(x, y, triangleCenterPoint);
		}
		return null;
	}

	T PickRandomElementOfList<T>(List<T> list)
	{
		int index = Random.Range(0, list.Count);
		return list[index];
	}
}