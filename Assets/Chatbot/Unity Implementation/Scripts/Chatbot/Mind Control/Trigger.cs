using System;
using UnityEngine;
using System.Collections;
using Chatbot;

/// <summary>
/// Holds Information how a trigger reacts when
/// triggered by helperfunction
/// </summary>
[AddComponentMenu("Chatbot/Mind Control/Trigger")]
public class Trigger : MonoBehaviour {
	private Chatbot.Core bot;
	public GameObject Motive;
	public GameObject[] Settings;
	public string defaultSetting="0";
	public string enabledSetting="1";
	public double timeoutintervall;
	public double timetoreset;

	/// <summary>
	/// Initialyse Trigger instance by retrieve
	/// Chatbot.Core instance
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
				tmpChatbotCore = tmpTrans.gameObject.GetComponent<ChatbotCore> ();
				// Test wether tmpChatbotCore exists
				if (tmpChatbotCore != null) {
					// Set Triggers instance for later reference
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
	/// When values in Scenes Trigger instance are changed by Unity Editor,
	/// update AttatchedTrigger instance in TriggerList instance.
	/// </summary>
	void OnValidate() {
		// If bot exists
		if(bot!=null)
			// Retrieve Trigger settings from Scene. Pass Trigger name
			bot.RetrieveTriggerSettingsFromScene(this.gameObject.name);

	}
}