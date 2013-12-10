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
	private string[] numbersSelection = {"1","2","3","4","5","6","7","8","9"};
	private string[] lettersSelection = 
	{
		"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
	};

	PenSound[] penSounds;
	AudioSource audioSource;

	PenSound[] cachedLetters;
	PenSound[] cachedNumbers;

	public static PenSound lastPickedSound;

	public void Init(AudioSource audios)
	{
		this.audioSource = audios;
		penSounds = LoadAudioAssets().ToArray();
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

	public void PlaySound()
	{
		if(audioSource.clip != lastPickedSound.audioClip)
			audioSource.clip = lastPickedSound.audioClip;
		audioSource.Play();
		//audioSource.PlayOneShot(sound.audioClip);
	}

	void CacheCategories()
	{
		var soundList = penSounds.ToList();

		cachedLetters = soundList.Where(ps => ps.audioCategory == AudioCategory.Letter).ToArray();
		cachedNumbers = soundList.Where(ps => ps.audioCategory == AudioCategory.Number).ToArray();
	}

	List<PenSound> LoadAudioAssets()
	{
		List<PenSound> returnList = new List<PenSound>();
		var lettersPath = "Audio/Letters/";
		var numbersPath = "Audio/Numbers/";

		for(int i = 0; i < lettersSelection.Length; i++)
		{
			var audioCli = (AudioClip)Resources.Load(lettersPath + lettersSelection[i]);

			returnList.Add (new PenSound (AudioCategory.Letter, audioCli, lettersSelection [i]));
		}

		for(int i = 0; i < numbersSelection.Length; i++)
		{
			var audioCli = (AudioClip)Resources.Load(numbersPath + numbersSelection[i]);

			returnList.Add (new PenSound (AudioCategory.Number, audioCli, numbersSelection [i]));
		}
		return returnList;
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