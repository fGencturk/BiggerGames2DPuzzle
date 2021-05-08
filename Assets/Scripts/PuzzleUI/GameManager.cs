using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private LevelGenerator levelGenerator;
	[SerializeField] public Canvas rootCanvas;
	[SerializeField] private PuzzlePieceWidget puzzlePiecePrefab;

	public static GameManager instance;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		PuzzlePiece[] puzzlePieces = levelGenerator.GenerateBoard();
		
		for (int i = 0; i < puzzlePieces.Length; i++)
		{
			PuzzlePieceWidget spawnedPuzzlePieceWidget = Instantiate(puzzlePiecePrefab, rootCanvas.transform);
			spawnedPuzzlePieceWidget.Initialize(puzzlePieces[i], i);
		}
	}
}