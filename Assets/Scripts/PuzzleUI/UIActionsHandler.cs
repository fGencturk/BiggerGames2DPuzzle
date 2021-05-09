using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionsHandler : MonoBehaviour
{
	[SerializeField] private GameObject settingsPanel,
		nextLevelPanel;

	private void Start()
	{
		GameManager.instance.puzzleBoard.OnLevelComplete += OnLevelComplete;
	}

	private void OnLevelComplete()
	{
		nextLevelPanel.SetActive(true);
	}

	public void OnNextLevelClick()
	{
		GameManager.instance.LoadNewLevel();
		nextLevelPanel.SetActive(false);
	}

	public void OnOpenSettingsClick()
	{
		settingsPanel.SetActive(true);
	}

	public void OnDifficultyChangeClick(int index)
	{
		GameManager.instance.SetDifficulty((GameManager.Difficulty)index);
		settingsPanel.SetActive(false);
	}

	public void OnRestartLevelClick()
	{
		GameManager.instance.LoadNewLevel();
		settingsPanel.SetActive(false);
	}
}
