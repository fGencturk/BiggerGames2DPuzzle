using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriangleWidget : MonoBehaviour
{
	Image image;

	private void Awake()
	{
		image = GetComponent<Image>();
		image.alphaHitTestMinimumThreshold = .1f;
	}

	public void Initialize(SquareVertex squareVertex, Color color)
	{
		image.color = color;
		if(squareVertex == SquareVertex.TopLeft)
		{
			transform.rotation = Quaternion.Euler(0, 0, 270f);
		} else if(squareVertex == SquareVertex.TopRight)
		{
			transform.rotation = Quaternion.Euler(0, 0, 180f);
		} else if (squareVertex == SquareVertex.BottomRight)
		{
			transform.rotation = Quaternion.Euler(0, 0, 90f);
		}
	}
}