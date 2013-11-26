using UnityEngine;
using System.Collections;

public class CategorySelectButton : MonoBehaviour 
{
	public AudioCategory buttonsCategory;

	void OnClick()
	{
		Messenger<AudioCategory>.Invoke(UIMessage.CategorySelected.ToString(), buttonsCategory);
	}
}
