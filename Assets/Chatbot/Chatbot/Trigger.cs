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
	/// Class to process Inner and Outer Triggers
	/// Inner triggers are things like feelings,
	/// needs e.g.
	/// Outer triggers are things bots see, hear,
	/// e.g.
	/// </summary>
	internal class Triggers {
		// Reference to Chatbot.Core Instance
		private Chatbot.Core bot;
		// List of attatched Trigger
		public List<AttatchedTrigger> TriggerList = new List<AttatchedTrigger>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Chatbot.Trigger"/> class.
		/// </summary>
		/// <param name="Chatbot">Chatbot.</param>
		public void Initialize(GameObject tmpgameobj,Chatbot.Core tmpbot ) {
			// Throw error if not attached to a Game Object
			if(tmpgameobj==null)
				Debug.LogWarning("To use chatbot you need to attatch ChatbotCore.cs script to a GameObject and pass GameObject to Chatbot. GameObject is empty.");
			// Get Chatbot.Core Instance
			this.bot = tmpbot;
			if(this.bot==null)
				Debug.LogWarning("To use chatbot you need to attatch ChatbotCore.cs script to a GameObject and pass GameObject to Chatbot. GameObject is empty.");
			// Create list of attatched triggers.
			Trigger[] AttatchedTriggers;
			// Get all Triggers attatched to the Chatbot Gameobject
			AttatchedTriggers = tmpgameobj.GetComponentsInChildren<Trigger>();
			// Loop throug all Triggers
			foreach (Trigger tmpTrigger in AttatchedTriggers) {
				// And add them to the TriggerList
				AttatchedTrigger tmpTriggerItem = new AttatchedTrigger();
				// Initialize AttatchedTrigger instance
				tmpTriggerItem.Initialize(this.bot,tmpTrigger);
				// And add tmpTriggerItem to TriggerList
				TriggerList.Add(tmpTriggerItem);
			}
		}
		
		/// <summary>
		/// To make Chatbot flexible and to fit in any environment,
		/// there are helperfunctions for motives and triggers. A tool
		/// like PlayMaker or own scripts could perhaps send a 
		/// Trigger message.
		/// </summary>
		public void TriggerFromHelperfunction(string triggername){
			// Loop through all attatched triggers
			foreach (AttatchedTrigger trigger in TriggerList) {
				// Call RecieveTrigger, when names are equal.
				if(trigger.name == triggername)
					trigger.RecieveTrigger();
			}
		}

		/// <summary>
		/// Retrieve Trigger settings from scene
		/// </summary>
		/// <param name="triggername">Triggername.</param>
		public void RetrieveTriggerSettingsFromScene(string triggername) {
			// Loop through all attatched triggers
			foreach (AttatchedTrigger trigger in TriggerList) {
				// Call RetrieveTriggerSettingsFromScene, when names are equal.
				if(trigger.name == triggername)
					trigger.RetrieveTriggerSettingsFromScene();
			}
		}

		/// <summary>
		/// Update all AttatchedTrigger instances per frame
		/// </summary>
		public void Update() {
			// Loop through all attatched triggers
			foreach (AttatchedTrigger trigger in TriggerList) {
				// Call Update.
				trigger.Update();
			}
		}
	}

	/// <summary>
	/// Represents all attatched Triggers
	/// hold in TriggerList
	/// </summary>
	internal class AttatchedTrigger {
		private Chatbot.Core bot;
		private Trigger trigger;
		public string name;
		private GameObject Motive;
		private string[] Settings;
		private string defaultSetting="0";
		private string enabledSetting="1";
		private double timeoutintervall;
		private double timetoreset;
		private DateTime timelastactive;
		private bool isresetted;
		// Wether it only triggers one time
		private bool onetime;

		/// <summary>
		/// Initializes the AttatchedTrigger instance.
		/// </summary>
		public void Initialize(Chatbot.Core tmpbot,Trigger tmpTrigger){
			// Throw exception if tmpTrigger is not assigned
			if (tmpTrigger == null)
				Debug.LogWarning ("Trigger == null");
			// Register Trigger instance
			trigger = tmpTrigger;
			// Set last time active to now and
			// subtract timeoutintervall, to be able
			// to recieve trigger from helperfunction
			// immediately.
			timelastactive = System.DateTime.Now.Subtract(TimeSpan.FromMilliseconds(tmpTrigger.timeoutintervall));
			// Check, wether bot exists
			if(tmpbot==null)
				Debug.LogWarning ("Passed Chatbot.Core instance == null");
			// Retrieve Chatbor.Core instance
			bot = tmpbot;
			// Settings resetted to default (code below)
			isresetted = true;
			//Check if there are assigned settings
			if (trigger.Settings != null) {
				// Settings string array should have same size 
				// as Settings GameObject array.
				Settings = new string[trigger.Settings.Length];
				// Set settings name and value to default
				for (int i=0; i<Settings.Length; i++) {
					// First check if user already assigned Setting GameObject
					// and wether it has Setting instance attatched
					if(trigger.Settings[i]!=null&&trigger.Settings[i].gameObject.GetComponent<Setting>()) {
						// Retrieve settings name
						Settings[i]=trigger.Settings[i].gameObject.name;
						// And set settings value to defaultSetting
						bot.SetGlobalSetting(Settings[i],defaultSetting);
						// Also change Settings in Unity scene to default
						trigger.Settings[i].gameObject.GetComponent<Setting>().value=defaultSetting;
					}
				}
			}
			// Grab Trigger values from Scene
			RetrieveTriggerSettingsFromScene();
		}

		/// <summary>
		/// Retrieve information from Unity Scenes Trigger instance
		/// </summary>
		/// <param name="item">Item.</param>
		public void RetrieveTriggerSettingsFromScene(){
			// If trigger and bot is assigned
			if (bot!=null&&trigger != null) {
				// Transfer name to tmpTriggerItem
				name = trigger.gameObject.name;
				// Get motive if assigned
				if(trigger.Motive!=null)
				// Get Motive name
				Motive = trigger.Motive;
				// Retrieve default and enabled string
				defaultSetting = trigger.defaultSetting;
				enabledSetting = trigger.enabledSetting;
				// Retrieve Timeintervalls
				timeoutintervall = trigger.timeoutintervall;
				timetoreset = trigger.timetoreset;
				// Erase existing settings when
				// there are no more settings left
				// in the scene
				if(trigger.Settings==null) {
					// And set to null, no need to destroy array in C#. Just set to null.
					Settings=null;

				} else {
					// If array length differs
					if(trigger.Settings.Length!=Settings.Length) {
						int oldarraysize=Settings.Length;
						// Use Systems array resize function to do it fast
						System.Array.Resize<String>(ref Settings,trigger.Settings.Length);
						// Calculate region to retrieve settings from
						int tmpsize=oldarraysize;
						if(trigger.Settings.Length<oldarraysize)
							tmpsize=trigger.Settings.Length;
						// We know that arraysize!=null, so get settings in old size area.
						for (int i=0; i<tmpsize; i++) {
							// First check if user already assigned Setting GameObject
							// and wether it has Setting instance attatched
							if(trigger.Settings[i]&&
							   trigger.Settings[i].gameObject.GetComponent<Setting>()) {
								// Retrieve settings name
								Settings[i]=trigger.Settings[i].gameObject.name;
								// And set settings value to existing value
								bot.SetGlobalSetting(Settings[i],trigger.Settings[i].gameObject.GetComponent<Setting>().value);
								// Settings in Unity scene remains the same
							}
						}
						// If new array is bigger than old one, set new arising 
						// Settings to default
						if(Settings.Length>oldarraysize) {
							// if oldarraysize==7 start at element 6
							for (int i=oldarraysize-1; i<trigger.Settings.Length; i++) {
								// First check if user already assigned Setting GameObject
								// and wether it has Setting instance attatched
								if(trigger.Settings[i]&&
								   trigger.Settings[i].gameObject.GetComponent<Setting>()) {
									// Retrieve settings name
									Settings[i]=trigger.Settings[i].gameObject.name;
									// And set settings value to defaultSetting
									bot.SetGlobalSetting(Settings[i],defaultSetting);
									// Also change Settings in Unity scene to default
									trigger.Settings[i].gameObject.GetComponent<Setting>().value=defaultSetting;
								}
							}
						}
					}
					// If array sizes equal
					else {
						// We know that arraysize!=null and arry.length equals.
						// So simple get settings from scenes settings.
						for (int i=0; i<Settings.Length; i++) {
							// First check if user already assigned Setting GameObject
							// and wether it has Setting instance attatched
							if(trigger.Settings[i]&&
							   trigger.Settings[i].gameObject.GetComponent<Setting>()) {
								// Retrieve settings name
								Settings[i]=trigger.Settings[i].gameObject.name;
								// And set settings value to existing value
								bot.SetGlobalSetting(Settings[i],trigger.Settings[i].gameObject.GetComponent<Setting>().value);
								// Settings in Unity scene remains the same
							}
						}
					}
				}
			}
		}

		// Update is called once per frame
		public void Update () {
			// Enable trigger if timeoutintervall!=zero
			if(timeoutintervall!=0.0f) {
				// Is trigger assigned
				if(trigger)
					// Disable trigger in Scene Editor to have
					// visual feedback
					trigger.gameObject.SetActive(true);
				// Set to trigger only one time
				onetime = false;
			}
			// If trigger and bot is assigned and onetime == false
			if (bot != null && trigger != null&&onetime==false) {
				// First test wheter Settings are referenced
				if (Settings != null) {
					// If timeoutintervall equals zero, there will we no reset and thus the setting will stay enabled
					if (!isresetted && System.DateTime.Now >= timelastactive.AddMilliseconds (timetoreset)) {
						for (int i=0; i<Settings.Length; i++) {
							// Set Settings to default
							bot.SetGlobalSetting (Settings [i], defaultSetting);
							// Be sure to update value in Scene if Setting component is available
							if (trigger.Settings [i].gameObject.GetComponent<Setting> () != null)
								trigger.Settings [i].gameObject.GetComponent<Setting> ().value = defaultSetting;
						}
						isresetted = true;
					}
				}
			}
		}

		/// <summary>
		/// Recieves trigger from Helperfunction.
		/// </summary>
		public void RecieveTrigger() {
			// If trigger and bot is assigned
			if (bot != null && trigger != null&&onetime==false) {
				// At least one Setting needs to be attatched to Trigger
				if (Settings != null) {
					// Check if we're not in timeout intervall
					if (System.DateTime.Now >= timelastactive.AddMilliseconds (timeoutintervall)) {
						// Else trigger is nomore resettet
						isresetted = false;
						// Loop through all settings
						for (int i=0; i<Settings.Length; i++) {
							// Set new value
							bot.SetGlobalSetting(Settings[i],enabledSetting);
							// Be sure to update value in Scene if Setting component is available
							if (trigger.Settings!=null&&trigger.Settings[i]!=null&&trigger.Settings [i].gameObject.GetComponent<Setting> () != null)
								trigger.Settings [i].gameObject.GetComponent<Setting> ().value = enabledSetting;
						}
						// If motive gameobject attatched
						if (Motive != null) {
							// Add it to planner
							bot.AddMotiveToPlanner(Motive);
						}
						// Set to current time
						timelastactive = System.DateTime.Now;
						// Disable trigger if triggered and timeotintervall equals zero
						if(timeoutintervall==0.0f) {
							// Is trigger assigned
							if(trigger)
								// Disable trigger in Scene Editor to have
								// visual feedback
								trigger.gameObject.SetActive(false);
							// Set to trigger only one time
							onetime = true;
						}
					}
				}
			}
		}
	}
}
