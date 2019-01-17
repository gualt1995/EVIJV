using UnityEngine;
using System.Collections;
using System;
using Chatbot;

/// <summary>
/// Helperfunction, to perform facial expressions, currently realized with 
/// emoticons.
/// </summary>
[AddComponentMenu("Chatbot/Helperfunctions/MotiveFacialExpression")]
public class MotiveFacialExpression : MonoBehaviour {
	// Gameobject with attatched ChatbotCore script
	private GameObject bot;
	// Enabled emoticon path
	public string EmoticonPath;
	// Default emoticon path
	public string defaultEmoticonPath;
	// Seconds to wait before send Finish event
	public float WaitSeconds;

	/// <summary>
	/// Initialie this instance. Retrieves Chatbot.Core instance.
	/// </summary>
	void Start () {
		// Counter to avoid endless loop
		int counter = 0;
		// Count till this value
		int countmax = 10;
		// Current transform
		Transform tmpTrans = this.transform;
		// Gathered ChatbotCore component
		ChatbotCore tmpChatbotCore=null;
		// Loop through transforms till counter reached countmax
		while(counter < countmax) {
			// Is Transform available?
			if(tmpTrans==null)
				// Abort global settings update
				counter=countmax;
			else {
				// Try to grab ChatbotCore component
				tmpChatbotCore = tmpTrans.gameObject.GetComponent<ChatbotCore>();
				// Test wether tmpChatbotCore exists
				if(tmpChatbotCore!=null) {
					// Set bot gameobject
					bot=tmpChatbotCore.gameObject;
					// Abort loop
					counter=countmax;
				}
				// Get parent transform
				tmpTrans = tmpTrans.parent;
				// Increase counter
				counter++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Coroutine called by Trigger
	/// </summary>
	IEnumerator Wait() {
		// The Simple emoticon chat user interface
		SimpleEmoticonChat emoticon;
		// Retrieve it if existing
		emoticon = bot.GetComponent<SimpleEmoticonChat> ();
		// Only change Gifs if needed user interface exists.
		if (emoticon) {
			// Set enabled Gif path
			emoticon.ChangeGif(EmoticonPath);
		}
		// Wait for several seconds
		yield return new WaitForSeconds (WaitSeconds);	
		// Only change Gifs if needed user interface exists.
		if (emoticon) {
			// Set default Gif path
			emoticon.ChangeGif(defaultEmoticonPath);
		}
		// Next try to send Finished message
		if (bot != null)
			bot.SendMessage ("MotiveFinished");
		else
			Debug.LogWarning ("No Motive to send Finish event to.");
	}

	/// <summary>
	/// Trigger this instance.
	/// </summary>
	void Trigger() {
		// Start coroutine
		StartCoroutine("Wait");
	}
}
