using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Scene references")]
	[SerializeField] private LevelGenerator levelGenerator;
	[SerializeField] public PuzzleBoard puzzleBoard;
	[SerializeField] public Canvas rootCanvas;
	[SerializeField] public Transform puzzlePiecesParent;
	[Header("Prefab Information")]
	[SerializeField] private PuzzlePieceWidget puzzlePiecePrefab;
	[SerializeField] public float triangleSpriteSize;
	[Header("Puzzle Level Information")]
	public int boardSizeX;
	public int boardSizeY,
		puzzlePieceCount;

	public static GameManager instance;

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		PuzzlePiece[] puzzlePieces = levelGenerator.GenerateBoard();
		puzzleBoard.Initialize();
		for (int i = 0; i < puzzlePieces.Length; i++)
		{
			PuzzlePieceWidget spawnedPuzzlePieceWidget = Instantiate(puzzlePiecePrefab, puzzlePiecesParent);
			spawnedPuzzlePieceWidget.Initialize(puzzlePieces[i], i);
		}
	}
}