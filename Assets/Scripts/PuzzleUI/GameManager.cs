using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum Difficulty
	{
		Easy,
		Medium,
		Hard
	}
	[System.Serializable]
	public struct PuzzleParameters
	{
		public int boardSizeX, boardSizeY, puzzlePieceCount;
	}
	[Header("Scene references")]
	[SerializeField] private LevelGenerator levelGenerator;
	[SerializeField] public PuzzleBoard puzzleBoard;
	[SerializeField] private PuzzleContentFitter puzzleContentFitter;
	[SerializeField] public Canvas rootCanvas;
	[SerializeField] public Transform puzzlePiecesParent;
	[Header("Prefab Information")]
	[SerializeField] private PuzzlePieceWidget puzzlePiecePrefab;
	[SerializeField] public float triangleSpriteSize;
	[Header("Puzzle Difficulty Parameters")]
	[SerializeField] private Difficulty defaultDifficulty;
	[SerializeField] private PuzzleParameters easyParameters;
	[SerializeField] private PuzzleParameters mediumParameters;
	[SerializeField] private PuzzleParameters hardParameters;

	// Selected level's parameters
	[HideInInspector] public int boardSizeX, boardSizeY, puzzlePieceCount;
	private Dictionary<Difficulty, PuzzleParameters> difficultyParameters;

	public static GameManager instance;

	private void Awake()
	{
		instance = this;
		difficultyParameters = new Dictionary<Difficulty, PuzzleParameters>()
		{
			{ Difficulty.Easy, easyParameters },
			{ Difficulty.Medium, mediumParameters },
			{ Difficulty.Hard, hardParameters },
		};
	}

	public void SetDifficulty(Difficulty difficulty)
	{
		PuzzleParameters difficultyParams = difficultyParameters[difficulty];
		boardSizeX = difficultyParams.boardSizeX;
		boardSizeY = difficultyParams.boardSizeY;
		puzzlePieceCount = difficultyParams.puzzlePieceCount;

		LoadNewLevel();
	}

	public void LoadNewLevel()
	{
		ClearPuzzlePieces();
		PuzzlePiece[] puzzlePieces = levelGenerator.GenerateBoard();
		puzzleBoard.Initialize();
		for (int i = 0; i < puzzlePieces.Length; i++)
		{
			PuzzlePieceWidget spawnedPuzzlePieceWidget = Instantiate(puzzlePiecePrefab, puzzlePiecesParent);
			spawnedPuzzlePieceWidget.Initialize(puzzlePieces[i], i);
		}
		puzzleContentFitter.FitPuzzleContent(boardSizeX, boardSizeY, triangleSpriteSize);
	}

	void ClearPuzzlePieces()
	{
		foreach (Transform puzzlePiece in puzzlePiecesParent)
		{
			Destroy(puzzlePiece.gameObject);
		}
	}

	void Start()
	{
		SetDifficulty(defaultDifficulty);
	}
}