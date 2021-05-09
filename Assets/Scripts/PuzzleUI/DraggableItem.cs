using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	RectTransform rectTransform;
	CanvasGroup canvasGroup;
	PuzzlePieceWidget puzzlePieceWidget;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		puzzlePieceWidget = GetComponent<PuzzlePieceWidget>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = false;
		GameManager.instance.puzzleBoard.RemovePlacedPuzzlePiece(puzzlePieceWidget.id);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		rectTransform.SetAsLastSibling();
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / (GameManager.instance.rootCanvas.scaleFactor * transform.parent.localScale);
	}
}
