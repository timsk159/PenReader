using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIView : MonoBehaviour 
{
	public UIPanel frontPanel;
	public UIPanel categorySelectPanel;
	public UIPanel guessPanel;
	public UIPanel revealWinPanel;
	public UIPanel revealLosePanel;

	public UIPopupList guessSelectionPopUp;


	public UILabel currentStreakLabel;
	public UILabel bestStreakLabel;
	UIPanel activePanel;

	private string[] numbersSelection = {"1","2","3","4","5","6","7","8","9"};
	private string[] lettersSelection = 
	{
		"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
	};

	void Start()
	{
		RegisterUIListeners();
		activePanel = frontPanel;
	}

	void RegisterUIListeners()
	{
		Messenger.AddListener(UIMessage.StartPress.ToString(), StartPress);
		Messenger<AudioCategory>.AddListener(UIMessage.CategorySelected.ToString(), CategorySelected);
		Messenger.AddListener(UIMessage.CategoryBack.ToString(), CategoryBack);
		Messenger.AddListener(UIMessage.GuessBack.ToString(), GuessBack);
		Messenger<string>.AddListener(UIMessage.GuessSubmitted.ToString(), GuessSubmitted);
		Messenger.AddListener(UIMessage.NewSoundNo.ToString(), NewSoundNo);
		Messenger.AddListener(UIMessage.NewSoundYes.ToString(), NewSoundYes);
		Messenger.AddListener(UIMessage.WinExit.ToString(), WinExit);
	}

	void GoToPanel(UIPanel panel)
	{
		if(activePanel == null)
			Debug.LogError("No active panel has been set!");

		activePanel.gameObject.SetActive(false);
		panel.gameObject.SetActive(true);
		activePanel = panel;
	}

	void StartPress()
	{
		GoToPanel(categorySelectPanel);
	}

	void CategorySelected(AudioCategory category)
	{
		GoToPanel(guessPanel);

		//Set input filter based on category.
		var selectionList = new List<string>();
		if(category == AudioCategory.Both)
		{
			selectionList.AddRange(numbersSelection);
			selectionList.AddRange(lettersSelection);
		}
		else if(category == AudioCategory.Number)
		{
			selectionList.AddRange(numbersSelection);
		}
		else if(category == AudioCategory.Letter)
		{
			selectionList.AddRange(lettersSelection);
		}
		guessSelectionPopUp.items = selectionList;
		guessSelectionPopUp.selection = guessSelectionPopUp.items[0];
	}

	void CategoryBack()
	{
		GoToPanel(frontPanel);
	}

	void GuessBack()
	{
		GoToPanel(categorySelectPanel);
	}

	void GuessSubmitted(string guess)
	{
		if(GuessChecker.CheckGuess(guess))
		{
			GoToPanel(revealWinPanel);
		}
		else
		{
			GoToPanel(revealLosePanel);
		}
	}

	void NewSoundNo()
	{
		GoToPanel(guessPanel);
	}

	void NewSoundYes()
	{
		GoToPanel(guessPanel);
	}

	void WinExit()
	{
		GoToPanel(frontPanel);
	}

	public void UpdateStats()
	{
		currentStreakLabel.text = "Current Streak: " + StatsTracker.currentStreak;
		bestStreakLabel.text = "Best Streak: " + StatsTracker.bestStreak;
	}

}
