using UnityEngine;
using System.Collections;

public class GuessChecker 
{
	public static bool CheckGuess(string guess)
	{
		if(guess.Length > 1)
		{
			Debug.LogWarning("Given guess of: " + guess + " is incorrect. Only 1 character can be provided");
			return false;
		}
		var chara = guess[0];

		if(AudioPicker.lastPickedSound.character[0] == chara)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool CheckGuess(char guess)
	{
		if(AudioPicker.lastPickedSound.character[0] == guess)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
