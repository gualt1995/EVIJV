using System;
using UnityEngine;
using Chatbot;

/// <summary>
/// Chatbot MonoBehaviour, to be attatcht to the character root GameObject.
/// 
///	The Chatbot class introduces the advantage of Chatbot that generates an interactive
/// counterpart whose degree of realism mostly depends on the work yielded by the Gamedeveloper.
/// </summary>
[AddComponentMenu("Chatbot/ChatbotCore")]
public class ChatbotCore : MonoBehaviour {
	// Chatbot system that expands the prospects of Program # to match the claim of
	// simulate human behaviour in games.
	[HideInInspector]
	public Chatbot.Core bot = new Chatbot.Core();
	/// <summary>
	/// Initialize Chatbot core
	/// </summary>
	void Start () {
		// Simple initializes the Chatbot with
		// GameObject, this script is attatched to.
		// Includes loading Settings from Unity Scene
		// and load AIML files from aimldirectory. 			
		bot.Initialize (this.gameObject);
	}
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		// Update all AttatchedTrigger instances each frame
		bot.Update();
	}

	/// <summary>
	/// Just call Triggerfunction in Chatbot.core and
	/// pass trigger name as string.
	/// </summary>
	/// <param name="triggername">Triggername.</param>
	public void TriggerFromHelperfunction(string triggername) {
		// Call function and pass trigger name.
		bot.TriggerFromHelperfunction (triggername);
	}

	/// <summary>
	/// Called, when motive helperfunction finished.
	/// </summary>
	public void MotiveFinished() {
		// Send motive finished event
		bot.MotiveFinished();
	}
}