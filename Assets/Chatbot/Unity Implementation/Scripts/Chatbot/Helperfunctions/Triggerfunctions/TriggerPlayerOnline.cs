using UnityEngine;
using System.Collections;

/// <summary>
/// Helperclass to recognise, if a user gives any input
/// and is therefor online.
/// </summary>
[AddComponentMenu("Chatbot/Helperfunctions/TriggerPlayerOnline")]
public class TriggerPlayerOnline : MonoBehaviour {
	// Mouse position
	private float x,y;
	
	// Use this for initialization
	void Start () {
		// Gather mouse position
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKey || Input.mousePosition.x>(x+20)||Input.mousePosition.x<(x-20) || Input.mousePosition.y>(y+20) || Input.mousePosition.y<(y-20)) {
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
						// If tmpChatbotCore exists call trigger function with
						// target Trigger as parameter via Send Message
						tmpChatbotCore.gameObject.SendMessage("TriggerFromHelperfunction","TriggerGreet");
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
	}
}
