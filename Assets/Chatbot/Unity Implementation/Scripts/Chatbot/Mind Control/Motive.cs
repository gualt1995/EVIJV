using UnityEngine;
using System.Collections;

/// <summary>
/// Motive script for Unity Scene.
/// </summary>
[AddComponentMenu("Chatbot/Mind Control/Motive")]
public class Motive : MonoBehaviour {
	// Just disable warning, because we might need
	// bot in the future
	#pragma warning disable 0414
	// Reference to Chatbot.Core instance
	private Chatbot.Core bot;
	#pragma warning restore 0414
	public float Importance;
	public float Need;
	public float ExpectedTimeSpan;
	public float RelativeExpectedTimeSpan;
	public GameObject LinkedHelperFunction;
	public bool TriggerChildrenBeforeContinue;
	/// <summary>
	/// Initialize this instance, especially retrieve
	/// the Chatbot.Core intance.
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
}
