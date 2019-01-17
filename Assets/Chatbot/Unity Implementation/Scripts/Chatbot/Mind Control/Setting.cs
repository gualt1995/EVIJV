using UnityEngine;
using System.Collections;

/// <summary>
/// Class to handle Setting in Unity Scene and synchronise
/// it with the Chatbot instance. Needs to be parent to
/// Gameobject with attached GlobalSettings:MonoBehaviour class.
/// 
/// The name of the class instance is at once the name of the
/// global setting.
/// </summary>
[AddComponentMenu("Chatbot/Mind Control/Setting")]
public class Setting : MonoBehaviour {
	// Value of the setting.
	private Chatbot.Core bot;
	public string value;

	/// <summary>
	/// Setup Setting class by get the Chatbot.core class
	/// </summary>
	void Setup() {
		// Counter to avoid endless loop
		int counter = 0;
		// Count till this value
		int countmax = 10;
		// Current transform
		Transform tmpTrans = this.transform;
		// Gathered ChatbotCore component
		ChatbotCore tmpChatbotCore=null;
		// Loop through transforms till counter reached countmax
		while (counter < countmax) {
			// Is Transform available?
			if (tmpTrans == null)
				// Abort global settings update
				counter = countmax;
			else {
				// Try to grab ChatbotCore component
				tmpChatbotCore = this.gameObject.GetComponent<ChatbotCore> ();
				// Test wether tmpChatbotCore exists
				if (tmpChatbotCore != null) {
					// Set bot for later reference
					bot = tmpChatbotCore.bot;
					// Abort loop
					counter = countmax;
				}
				// Get parent transform
				tmpTrans = tmpTrans.parent;
				// Increase counter
				counter++;
			}
		}
	}
	/// <summary>
	/// Function to save global settings, when a value is changed 
	/// within the Unity Editor. Only called in Editor, not in finished 
	/// game.
	/// </summary>
	public void OnValidate() {
		if (bot != null) {
			// If bot is assigned set/update global setting
			bot.SetGlobalSetting (this.gameObject.name, value);
		}
	}
}
