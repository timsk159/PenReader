using UnityEngine;
using System.Collections;

public class GenericButtonMessenger : MonoBehaviour 
{
	public UIMessage messageType;

	void OnClick()
	{
		Messenger.Invoke(messageType.ToString());
	}
}
