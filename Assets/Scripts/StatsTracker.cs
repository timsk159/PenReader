using UnityEngine;
using System.Collections;

public class StatsTracker 
{
	private static int _currentStreak;

	public static int currentStreak {
		set
		{
			_currentStreak = value;
			if(_currentStreak > bestStreak)
				bestStreak = _currentStreak;
		}
		get
		{
			return _currentStreak;
		}
	}

	public static int bestStreak
	{
		get;
		private set;
	}

	public static void ResetCurrentStreak()
	{
		currentStreak = 0;
	}

	public static void SaveStats()
	{
		PlayerPrefs.SetInt("CurrentStreak", _currentStreak);
		PlayerPrefs.SetInt("BestStreak", bestStreak);
	}

	public static void LoadStats()
	{
		_currentStreak = PlayerPrefs.GetInt("CurrentStreak", 0);
		bestStreak = PlayerPrefs.GetInt("BestStreak", 0);
	}

	public static void ResetStats()
	{
		_currentStreak = 0;
		bestStreak = 0;
		PlayerPrefs.SetInt("CurrentStreak", 0);
		PlayerPrefs.SetInt("BestStreak", 0);
	}
}
