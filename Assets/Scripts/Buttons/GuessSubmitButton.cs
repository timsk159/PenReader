using UnityEngine;
using System.Collections;

public class GuessSubmitButton : MonoBehaviour 
{
	UIPopupList selectionList;

	void Start()
	{
		selectionList = GameObject.Find("GuessSelection").GetComponent<UIPopupList>();
	}

	void OnClick()
	{
		string selection = selectionList.selection;

		Messenger<string>.Invoke(UIMessage.GuessSubmitted.ToString(), selection);
	}
}