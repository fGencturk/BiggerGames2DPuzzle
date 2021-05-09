using UnityEngine;

public class PuzzlePieceWidget : MonoBehaviour
{
	[SerializeField] private TriangleWidget triangleWidget;
	public int minXIndex { get; private set; }
	public int maxXIndex { get; private set; }
	public int minYIndex { get; private set; }
	public int maxYIndex { get; private set; }
	public int id { get; private set; }
	[HideInInspector] public PuzzlePiece puzzlePiece { get; private set; }
	public RectTransform rectTransform { get; private set; }
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}
	public void Initialize(PuzzlePiece puzzlePiece, int id)
	{
		this.id = id;
		this.puzzlePiece = puzzlePiece;
		Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		SetMaxMinIndexes();

		float triangleSpriteSize = GameManager.instance.triangleSpriteSize;

		foreach (var tile in puzzlePiece.tiles)
		{
			TriangleWidget spawnedTriangleWidget = Instantiate(triangleWidget, transform);
			spawnedTriangleWidget.Initialize(tile.triangleCenterPoint, color);
			// TODO remove get component
			spawnedTriangleWidget.GetComponent<RectTransform>().anchoredPosition = new Vector3((tile.x - minXIndex) * triangleSpriteSize, -(tile.y - minYIndex) * triangleSpriteSize);
		}
	}

	void SetMaxMinIndexes()
	{
		minXIndex = System.Int32.MaxValue;
		maxXIndex = System.Int32.MinValue;
		minYIndex = System.Int32.MaxValue;
		maxYIndex = System.Int32.MinValue;
		foreach (var tile in puzzlePiece.tiles)
		{
			if (minXIndex > tile.x)
			{
				minXIndex = tile.x;
			}
			if (maxXIndex < tile.x)
			{
				maxXIndex = tile.x;
			}
			if (minYIndex > tile.y)
			{
				minYIndex = tile.y;
			}
			if (maxYIndex < tile.y)
			{
				maxYIndex = tile.y;
			}
		}
	}

	public int GetXIndexLength()
	{
		return maxXIndex - minXIndex;
	}
	public int GetYIndexLength()
	{
		return maxYIndex - minYIndex;
	}
}