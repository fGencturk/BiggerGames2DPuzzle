using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleContentFitter : MonoBehaviour
{
    [SerializeField] private float marginInPercentage = .2f;
    [SerializeField] private float pieceSpaceAtBottomInPercentage = .5f;
    [SerializeField]
    private RectTransform boardRectTransform,
        puzzlePiecesRectTransform;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        FitPuzzleContent(GameManager.instance.boardSizeX, GameManager.instance.boardSizeY, GameManager.instance.triangleSpriteSize);
    }

    public void FitPuzzleContent(int boardSizeX, int boardSizeY, float triangleSpriteSize)
    {
        GameManager.instance.rootCanvas.scaleFactor = 1f;
        Vector2 screenSizeInRectUnits = new Vector2(Screen.width / GameManager.instance.rootCanvas.scaleFactor,
            Screen.height / GameManager.instance.rootCanvas.scaleFactor);
        Vector2 boardSizeInRectUnits = new Vector2(boardSizeX * triangleSpriteSize, boardSizeY * triangleSpriteSize);

        float scaleForWidth = screenSizeInRectUnits.x / (boardSizeInRectUnits.x * (1 + marginInPercentage));
        float scaleForHeight = screenSizeInRectUnits.y / (boardSizeInRectUnits.y * (1 + marginInPercentage + pieceSpaceAtBottomInPercentage * 2));
        float scale = Mathf.Min(scaleForHeight, scaleForWidth);

        rectTransform.anchoredPosition = new Vector2(-boardSizeInRectUnits.x / 2f, boardSizeInRectUnits.y / 2f);
        boardRectTransform.anchoredPosition = new Vector2(0, boardSizeInRectUnits.y * pieceSpaceAtBottomInPercentage);
		foreach (Transform puzzlePiece in puzzlePiecesRectTransform)
		{
            puzzlePiece.GetComponent<RectTransform>().anchoredPosition = 
                new Vector2(triangleSpriteSize, -boardSizeInRectUnits.y * pieceSpaceAtBottomInPercentage - triangleSpriteSize);
        }        

        GameManager.instance.rootCanvas.scaleFactor = scale;
    }
}
