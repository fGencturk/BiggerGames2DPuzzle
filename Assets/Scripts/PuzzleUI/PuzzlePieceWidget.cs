using UnityEngine;

public class PuzzlePieceWidget : MonoBehaviour
{
	[SerializeField] private TriangleWidget triangleWidget;
	int minXIndex,
		minYIndex,
		id;
	public void Initialize(PuzzlePiece puzzlePiece, int id)
	{
		this.id = id;
		Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		minXIndex = System.Int32.MaxValue;
		minYIndex = System.Int32.MaxValue;
		foreach (var tile in puzzlePiece.tiles)
		{
			if(minXIndex > tile.x)
			{
				minXIndex = tile.x;
			}
			if(minYIndex > tile.y)
			{
				minYIndex = tile.y;
			}
		}
		foreach (var tile in puzzlePiece.tiles)
		{
			TriangleWidget spawnedTriangleWidget = Instantiate(triangleWidget, transform);
			spawnedTriangleWidget.Initialize(tile.triangleCenterPoint, color);
			// TODO remove get component
			spawnedTriangleWidget.GetComponent<RectTransform>().anchoredPosition = new Vector3((tile.x) * 100f, -(tile.y) * 100f);
		}
	}
}