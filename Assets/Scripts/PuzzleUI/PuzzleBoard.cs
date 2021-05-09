using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleBoard : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private int boardSizeY, boardSizeX, puzzlePieceCount;

    private SquareBoardPiece[,] board;
    private float triangleSpriteSize;

    Dictionary<int, PlacedPuzzlePieceWidget> placedPuzzlePieceWidgets;

	public void Initialize()
    {
        GameManager gameManager = GameManager.instance;
        boardSizeY = gameManager.boardSizeY;
        boardSizeX = gameManager.boardSizeX;
        puzzlePieceCount = gameManager.puzzlePieceCount;

        triangleSpriteSize = gameManager.triangleSpriteSize;

        rectTransform = GetComponent<RectTransform>();
        this.transform.localScale = new Vector3(boardSizeX, boardSizeY);
        placedPuzzlePieceWidgets = new Dictionary<int, PlacedPuzzlePieceWidget>();
        board = new SquareBoardPiece[boardSizeY, boardSizeX];

        for (int y = 0; y < boardSizeY; y++)
        {
            for (int x = 0; x < boardSizeX; x++)
            {
                SquareVertex randomSquareVertex = RandomEnumGenerator.RandomEnumValue<SquareVertex>();
                board[y, x] = new SquareBoardPiece(x, y, randomSquareVertex);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        PuzzlePieceWidget puzzlePieceWidget;

        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent<PuzzlePieceWidget>(out puzzlePieceWidget))
        {
            TryPlacePuzzlePiece(puzzlePieceWidget);
        }
    }

    void TryPlacePuzzlePiece(PuzzlePieceWidget puzzlePieceWidget)
	{
        int x, y;
        if(TryGetClosestSquareIndexesOfPosition(puzzlePieceWidget.rectTransform.anchoredPosition, out x, out y))
        {
            // If length fits to the board with x,y start positions
            if (x + puzzlePieceWidget.GetXIndexLength() < boardSizeX && y + puzzlePieceWidget.GetYIndexLength() < boardSizeY)
			{
                if(TryPlacePuzzleTiles(puzzlePieceWidget, x, y))
                {
                    puzzlePieceWidget.rectTransform.anchoredPosition = GetRectPositionOfSquare(x, y);
                    PlacedPuzzlePieceWidget placedPuzzlePieceWidget = new PlacedPuzzlePieceWidget()
                    {
                        puzzlePieceWidget = puzzlePieceWidget,
                        placedXIndex = x,
                        placedYIndex = y
                    };
                    placedPuzzlePieceWidgets.Add(puzzlePieceWidget.id, placedPuzzlePieceWidget);
                    CheckGameOver();
                }
			}
        }
	}

	private void CheckGameOver()
	{
        if(placedPuzzlePieceWidgets.Count == puzzlePieceCount)
		{
            Debug.Log("Level Completed");
		}
    }

	public void RemovePlacedPuzzlePiece(int id)
    {
        if (placedPuzzlePieceWidgets.ContainsKey(id))
		{
            PlacedPuzzlePieceWidget placedPuzzlePieceWidget = placedPuzzlePieceWidgets[id];

            int startingX = placedPuzzlePieceWidget.placedXIndex - placedPuzzlePieceWidget.puzzlePieceWidget.minXIndex,
                startingY = placedPuzzlePieceWidget.placedYIndex - placedPuzzlePieceWidget.puzzlePieceWidget.minYIndex;

            // Place triangle tiles to the board
            foreach (Tile tile in placedPuzzlePieceWidget.puzzlePieceWidget.puzzlePiece.tiles)
            {
                int xIndex = tile.x + startingX,
                    yIndex = tile.y + startingY;
                board[yIndex, xIndex].SetOccupied(tile.triangleCenterPoint, false);
            }
            placedPuzzlePieceWidgets.Remove(id);
        }
	}

	private bool TryPlacePuzzleTiles(PuzzlePieceWidget puzzlePieceWidget, int x, int y)
	{
        int startingX = x - puzzlePieceWidget.minXIndex,
            startingY = y - puzzlePieceWidget.minYIndex;
        // Check if all triangle tiles of PuzzlePiece are Unoccupied in the board
        foreach (Tile tile in puzzlePieceWidget.puzzlePiece.tiles)
		{
            int xIndex = tile.x + startingX,
                yIndex = tile.y + startingY;
            if(!board[yIndex, xIndex].isEmpty() && board[yIndex, xIndex].IsOccupied(tile.triangleCenterPoint))
			{
                return false;
			}
		}
        // Place triangle tiles to the board
        foreach (Tile tile in puzzlePieceWidget.puzzlePiece.tiles)
        {
            int xIndex = tile.x + startingX,
                yIndex = tile.y + startingY;
            if (board[yIndex, xIndex].isEmpty())
            {
                board[yIndex, xIndex].SetTriangleCenterPoint(tile.triangleCenterPoint);
                board[yIndex, xIndex].SetOccupied(tile.triangleCenterPoint, true);
            }
        }
        return true;
    }

	Vector2 GetRectPositionOfSquare(int x, int y)
	{
        return new Vector2(x * triangleSpriteSize + rectTransform.anchoredPosition.x + triangleSpriteSize / 2, 
            -y * triangleSpriteSize + rectTransform.anchoredPosition.y - triangleSpriteSize / 2);
	}

    bool TryGetClosestSquareIndexesOfPosition(Vector2 anchorPosition, out int outX, out int outY)
	{
        Vector2 localizedPosition = new Vector2(anchorPosition.x - rectTransform.anchoredPosition.x,
            anchorPosition.y - rectTransform.anchoredPosition.y);
        outX = (int)(localizedPosition.x / triangleSpriteSize);
        outY = (int)(-localizedPosition.y / triangleSpriteSize);
        if(outX >= 0 && outX < boardSizeX && outY >= 0 && outY < boardSizeY)
		{
            return true;
		}
        return false;
    }

    private struct PlacedPuzzlePieceWidget
	{
        public PuzzlePieceWidget puzzlePieceWidget;
        public int placedXIndex,
            placedYIndex;
	}
}
