using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Chatbot core class is intended to advance from Program # to a new Level of Artificial
/// Intelligence. Whereas Program # only gives the basic functionallity of parsing
/// AIML 1.0.1 files, Chatbot fills the gap between simple chat application
///	and advanced Gameprogramming, especially the use of artificial intelligent NPC's. 
/// </summary>
namespace Chatbot {
	/// <summary>
	/// Class to access bot and handle data.
	/// </summary>
	internal class Motives {
		// Chatbot's core instance
		public Chatbot.Core bot;
		// Reference to the GameObject, the core is attatched to.
		private GameObject chatbot;
		// The Gameobject, the "template" motives are attatched to
		GameObject assignedmotives;
		// List of attatched Motives
		public List<AttatchedMotive> MotiveList = new List<AttatchedMotive>();
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Chatbot.Motives"/> class.
		/// </summary>
		/// <param name="Chatbot">Chatbot.</param>
		public void Initialize(GameObject tmpchatbot) {
			// Check wether Gameobject is availabe and
			// throw exception if null
			if (tmpchatbot == null)
				Debug.LogWarning ("No Gameobject passed.");
			// Keep chatbot gameobject
			chatbot = tmpchatbot;
			bot = chatbot.GetComponent<ChatbotCore> ().bot;
			if(bot==null)
				Debug.LogWarning ("No Chatbot.Core instance passed.");

			// Does AssignedMotives instance exist?
			if (chatbot.GetComponentInChildren<AssignedMotives> () != null)
				// Search Gameobject with AssignedMotives Component
				assignedmotives = chatbot.GetComponentInChildren<AssignedMotives> ().gameObject;
			else
				Debug.LogWarning ("There is no AssignedMotives Script attatched.");
			// Temporary motive array
			Motive[] motive;
			// Gather all Motive instances
			motive = assignedmotives.GetComponentsInChildren<Motive>();
			// Loop through all motives if existing
			if(motive!=null&&bot!=null) {
				for(int i=0; i<motive.Length;i++){
					// If motive exists
					if (motive[i]!=null) {
						// Create new AttatchedMotive instance
						AttatchedMotive tmpmotive = new AttatchedMotive();
						// Initialize
						tmpmotive.Initialize(bot,motive[i]);
						// Attatch to MotiveList
						MotiveList.Add(tmpmotive);
					}
				}
			}
		}
		/// <summary>
		/// Trigger the template motive, the motive
		/// with gameobject.name==name
		/// </summary>
		/// <param name="name">Name.</param>
		public void Trigger(string name){
			// Loop through motive list
			foreach(AttatchedMotive tmpmotive in MotiveList) {
				// If names equal and thus the right
				// motive template is selected
				if(tmpmotive.name==name) {
					// Trigger Motive Template
					tmpmotive.Trigger();
				}
			}
		}

				
		/// <summary>
		/// To be updated once per frame
		/// </summary>
		public void Update() {

		}

		/// <summary>
		/// Retrieve Motive settings from scene
		/// </summary>
		/// <param name="motivename">Motivename.</param>
		public void RetrieveMotiveSettingsFromScene(string motivename) {
			// Loop through all attatched triggers
			foreach (AttatchedMotive motive in MotiveList) {
				// Call RetrieveTriggerSettingsFromScene, when names are equal.
				if (motive.name == motivename)
					// Retrieves motive settings from scene
					motive.RetrieveMotiveSettingsFromScene ();
			}
		}
	}

	/// <summary>
	/// Class for attatched Motives
	/// </summary>
	internal class AttatchedMotive {
		// Chatbot's core instance
		private Chatbot.Core bot;
		// Reference to motive
		private Motive motive;
		// The motives name
		public string name;
		// It might be used in future application
		#pragma warning disable 0414
		// Importance between 0 and 1
		private float Importance;
		#pragma warning restore 0414
		// It might be used in future application
		#pragma warning disable 0414
		// The need between 0 and 1
		private float Need;
		#pragma warning restore 0414
		// Might be used in future application
		#pragma warning disable 0414
		// The expected Time span in milliseconds
		private double ExpectedTimeSpan;
		#pragma warning restore 0414
		// Might be used in future application
		#pragma warning disable 0414
		// Expected relative timespan between 0 and 1
		private float RelativeExpectedTimeSpan;
		#pragma warning restore 0414
		// The linked Helper function to send trigger message to
		private GameObject LinkedHelperFunction;
		// Maybe use in future application
		#pragma warning disable 0414
		// Should all child motives be triggered bevore the others?
		private bool TriggerChildrenBeforeContinue;
		#pragma warning restore 0414

		/// <summary>
		/// Initialize AttatchedMotive instance.
		/// </summary>
		public void Initialize(Chatbot.Core parambot,Motive parammotive) {
			// Retrieve Core if existing, else
			// throw exception
			if (parambot!=null)
				bot = parambot;
			else
				Debug.LogWarning("Chatbot.Core instance not passed.");
			// Retrieve Motive if existing else throw exception
			if (parammotive != null)
				motive = parammotive;
			else
				Debug.LogWarning("Motive instance not passed.");
			// Retrieve settings from scene
			RetrieveMotiveSettingsFromScene();
		}

		/// <summary>
		/// Retrieves the motive settings from scene.
		/// </summary>
		public void RetrieveMotiveSettingsFromScene() {
			if (bot != null&&motive!=null) {
				// Transfer name to tmpTriggerItem
				name = motive.gameObject.name;
				// Retrieve importance
				Importance = motive.Importance;
				// Retrieve need
				Need = motive.Need;
				// Retrieve expected time span
				ExpectedTimeSpan = motive.ExpectedTimeSpan;
				// Retrieve relative expected time span
				RelativeExpectedTimeSpan = motive.RelativeExpectedTimeSpan;
				// Retrieve linked helper function
				LinkedHelperFunction = motive.LinkedHelperFunction;
				// Retrive another value
				TriggerChildrenBeforeContinue = motive.TriggerChildrenBeforeContinue;
			}
		}
		/// <summary>
		/// Trigger Helper Motive function
		/// Reciever gameobject needs to have
		/// global function Trigger() 
		/// </summary>
		public void Trigger() {
			if(LinkedHelperFunction!=null)
				LinkedHelperFunction.SendMessage("Trigger");
		}
	}
}

