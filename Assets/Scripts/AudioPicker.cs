using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum AudioCategory
{
	None, Number, Letter, Both
};

public class AudioPicker 
{
	PenSound[] penSounds;
	AudioSource audioSource;

	PenSound[] cachedLetters;
	PenSound[] cachedNumbers;

	public static PenSound lastPickedSound;

	public void Init(PenSound[] penSounds, AudioSource audios)
	{
		this.penSounds = penSounds;
		this.audioSource = audios;
		CacheCategories();
		Random.seed = Random.Range(1, 10000);
	}

	public PenSound PickPenSound(AudioCategory category)
	{
		if(category == AudioCategory.None || category == AudioCategory.Both)
			return PickPenSound();

		int randomIndex = 0;
		if(category == AudioCategory.Letter)
		{
			randomIndex = Random.Range(0, cachedLetters.Length);
			lastPickedSound = cachedLetters[randomIndex];
			return cachedLetters[randomIndex];
		}
		else
		{
			randomIndex = Random.Range(0, cachedNumbers.Length);
			lastPickedSound = cachedNumbers[randomIndex];
			return cachedNumbers[randomIndex];
		}
	}

	public PenSound PickPenSound()
	{
		int randomIndex = Random.Range(0, penSounds.Length);

		lastPickedSound = penSounds[randomIndex];
		return penSounds[randomIndex];
	}

	public void PlaySound(PenSound sound)
	{
		audioSource.PlayOneShot(sound.audioClip);
	}

	void CacheCategories()
	{
		var soundList = penSounds.ToList();

		cachedLetters = soundList.Where(ps => ps.audioCategory == AudioCategory.Letter).ToArray();
		cachedNumbers = soundList.Where(ps => ps.audioCategory == AudioCategory.Number).ToArray();
	}
}

[System.Serializable]
public class PenSound
{
	public AudioCategory audioCategory;
	public AudioClip audioClip;
	public string character;

	public PenSound(){}

	public PenSound(AudioCategory category, AudioClip clip, string character)
	{
		this.audioCategory = category;
		this.audioClip = clip;
		this.character = character;
	}
}