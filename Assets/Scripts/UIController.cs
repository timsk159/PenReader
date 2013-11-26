using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UIMessage
{
	StartPress, 
	CategorySelected, CategoryBack, 
	GuessSubmitted, GuessBack, 
	NewSoundYes, NewSoundNo, WinExit,
	PlaySound
};

public class UIController : MonoBehaviour 
{
	//Inspector-Set:
	public PenSound[] penSounds;

	AudioPicker audioPicker;

	PenSound pickedSound;

	UIView uiView;

	void Start()
	{
		StatsTracker.LoadStats();

		audioPicker = new AudioPicker();

		audioPicker.Init(penSounds, gameObject.GetComponent<AudioSource>());

		uiView = GameObject.Find("UIView").GetComponent<UIView>();

		RegisterUIListeners();
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
		Messenger.AddListener(UIMessage.PlaySound.ToString(), PlaySound);
	}

	void StartPress()
	{

	}

	void CategorySelected(AudioCategory category)
	{
		pickedSound = audioPicker.PickPenSound(category);
	}

	void CategoryBack()
	{
		pickedSound = null;
	}

	void GuessBack()
	{
		pickedSound = null;
	}

	void GuessSubmitted(string guess)
	{
		if(GuessChecker.CheckGuess(guess))
		{
			StatsTracker.currentStreak += 1;
		}
		else
		{
			StatsTracker.ResetCurrentStreak();
		}
		uiView.UpdateStats();
		StatsTracker.SaveStats();
	}

	void NewSoundNo()
	{

	}

	void NewSoundYes()
	{
		var previousCategory = pickedSound.audioCategory;
		pickedSound = audioPicker.PickPenSound(previousCategory);
	}

	void WinExit()
	{
		pickedSound = null;
	}

	void PlaySound()
	{
		audioPicker.PlaySound(pickedSound);
	}
}

