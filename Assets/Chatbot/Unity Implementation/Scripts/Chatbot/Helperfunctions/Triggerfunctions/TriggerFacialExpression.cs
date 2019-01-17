using UnityEngine;
using System.Collections;

/// <summary>
/// Helperfunction to trigger facial expression, here acticator
/// is steered intrinsic, dependent on 1 or 0 values of settings.
/// Multiple settings can be considered to be either 1 for
/// enabled or 0 for disabled.
/// </summary>
[AddComponentMenu("Chatbot/Helperfunctions/TriggerFacialExpression")]
public class TriggerFacialExpression : MonoBehaviour {
	private Chatbot.Core bot;
	public GameObject[] MessageReciever;
	public GameObject[] SettingsToCheck;
	public GameObject[] SettingsMustNotBeTriggered;
	// Use this for initialization
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
					// Set bot and trigger for later reference
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
	
	// Update is called once per frame
	void Update () {
		// If instance is initialized
		if(bot!=null) {
			bool triggerbool = true;
			// Every considered setting must be "1"
			foreach (GameObject setting in SettingsToCheck) {
				// Does setting exist?
				if(setting!=null)
					if(bot.GetGlobalSetting(setting.name)!="1")
						triggerbool=false;
			}
			// Settings that must be "0"
			foreach (GameObject setting in SettingsMustNotBeTriggered) {
				// Does setting exist?
				if(setting!=null)
					if(bot.GetGlobalSetting(setting.name)=="1")
						triggerbool=false;
			}
			if (triggerbool) {
				foreach(GameObject tmpMessageReciever in MessageReciever) {
					// Does tmpMessageReciever exist?
					if(tmpMessageReciever)
						bot.TriggerFromHelperfunction(tmpMessageReciever.name);
				}
			}
		}
	}
}