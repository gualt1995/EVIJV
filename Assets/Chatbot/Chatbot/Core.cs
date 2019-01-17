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
	public class Core {
		// Reference to the GameObject, the core is attatched to.
		private GameObject chatbot;
		// Trigger Instance
		private Triggers trigger;
		// Motives Instance
		private Motives motive;
		// Planner Instance
		private NestedPlanner planner;
		// Program # Variables
		private AIMLbot.Bot bot;
		private AIMLbot.User user;
		private AIMLbot.Request request;
		private AIMLbot.Result result;

		// Enables/disables debug function to
		// log to Unity console and Logs folder
		public bool debug
		{
			get
			{
				if(bot!=null)
					return bot.debug_unity;
				else {
					Debug.LogWarning("AIMLbot.Bot instance == null.");
					// No bot, no Debugging
					return false;
				}
			}
			set
			{
				if(bot!=null) {
					bot.debug_unity=value;
				}
				else
					Debug.LogWarning("AIMLbot.Bot instance == null.");
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chatbot.Trigger"/> class.
		/// </summary>
		/// <param name="Chatbot">Chatbot.</param>
		public void Initialize(GameObject tmpchatbot) {
			// Save delivered GameObject
			chatbot = tmpchatbot;
			// Throw error if not Game Object delivered
			if(!chatbot)
				Debug.LogWarning("To use chatbot you need to attatch ChatbotCore.cs script to a GameObject and pass GameObject to Chatbot. GameObject is empty.");
			// Windows, Linux and Mac OSX
			#if !(UNITY_WEBPLAYER || UNITY_WEBGL)
			else {
				// Initialize Program # variables
				bot = new AIMLbot.Bot(this);
				user = new AIMLbot.User("User",bot);
				request = new AIMLbot.Request("",user,bot);
				result = new AIMLbot.Result(user,bot,request);
				// Global Settings
				Setting[] settings;
				// Gather all attatched settings from Scene
				settings = chatbot.GetComponentsInChildren<Setting>();
				// Loop through settings and 
				foreach(Setting setting in settings){
					// Set global settings in Program# and Jurassic
					SetGlobalSetting(setting.gameObject.name,setting.value);
				}
				// Load Settings from Scene Setting Components.
				bot.loadSettingsFromScene ();
				// Load AIML files from AIML path defined in Settings.xml
				bot.loadAIMLFromFiles();

				// Define to or not to use JavaScript (Jurassic) in AIML
				bot.UseJavaScript = true;
				// Use Program # with Jurassic and Chatbot
				bot.ProgramSharpJSWithChatbot=true;
				// Create Triggers instance
				trigger = new Chatbot.Triggers();
				// Initializes triggers that are attatched
				// to the Chatbot. Needs to pass the 
				// Chatbot Gameobject and Triggers instance
				trigger.Initialize (chatbot,this);
				// Create Motives instance
				motive = new Chatbot.Motives();
				// Initialize it
				motive.Initialize(chatbot);
				// Create Planner instance
				planner = new Chatbot.NestedPlanner();
				// Initialize planner
				planner.Initialize (chatbot,this,motive);
			}
			#else
			// Webplayer plattform or WebGl
			ProgramSharpWebplayerCoroutine WebplayerCoroutine = chatbot.GetComponent<ProgramSharpWebplayerCoroutine>();
			if(WebplayerCoroutine!=null&&chatbot!=null) {
				// Load settings from scene
				bot = new AIMLbot.Bot(WebplayerCoroutine,Application.dataPath + "/Chatbot/Program #/advancedconfig/Settings.xml",true,this);
				user = new AIMLbot.User("User",bot);
				request = new AIMLbot.Request("",user,bot);
				result = new AIMLbot.Result(user,bot,request);

				// Define to or not to use JavaScript (Jurassic) in AIML
				bot.UseJavaScript = true;
				// Use Program # with Jurassic and Chatbot
				bot.ProgramSharpJSWithChatbot=true;
				// Create Triggers instance
				trigger = new Chatbot.Triggers();
				// Initializes triggers that are attatched
				// to the Chatbot. Needs to pass the 
				// Chatbot Gameobject and Triggers instance
				trigger.Initialize (chatbot,this);
				// Create Motives instance
				motive = new Chatbot.Motives();
				// Initialize it
				motive.Initialize(chatbot);
				// Create Planner instance
				planner = new Chatbot.NestedPlanner();
				// Initialize planner
				planner.Initialize (chatbot,this,motive);
			}
			else
				Debug.LogWarning("You need to attatch the Webplayer Component in Webplayer plattform mode.");
			#endif
		}

		/// <summary>
		/// Simple Chat function. Processes Input
		/// and returns output.
		/// </summary>
		/// <param name="input">Input.</param>
		public string Chat(string input) {
			// Debugging
			if (debug)
				Debug.Log (input);
			// Build request
			request.rawInput = input;
			// Time when chat begins to handle
			// timeout errors
			request.StartedOn = DateTime.Now;
			// Perform chat if bot exists
			if (bot != null)
				result = bot.Chat (request);
			else 
				// Else throw warning
				Debug.LogWarning ("Can't retrieve AIMLbot.Bot bot. Did you initialize Chatbot correct?");
			// Debugging
			if (debug)
				Debug.Log (result.Output);
			return result.Output;
		}

		/// <summary>
		/// To be updated once per frame
		/// </summary>
		public void Update() {
			// If trigger exists
			if(trigger!=null)
				// Update Triggers
				trigger.Update ();
			//If planner exists
			if (planner!=null)
				// Update Planner
				planner.Update ();
		}

		/// <summary>
		/// Just call Triggerfunction in Chatbot.Trigger and
		/// pass trigger name as string.
		/// </summary>
		/// <param name="triggername">Triggername.</param>
		public void TriggerFromHelperfunction(string triggername) {
			// If triggerfunction exists
			if(trigger!=null)
				// Call function and pass trigger name.
				trigger.TriggerFromHelperfunction (triggername);
		}

		/// <summary>
		/// Retrieve Trigger settings from scene
		/// </summary>
		/// <param name="triggername">Triggername.</param>
		public void RetrieveTriggerSettingsFromScene(string triggername) {
			// Check, if trigger exists
			if(trigger!=null)
				// Call function and pass trigger name.
				trigger.RetrieveTriggerSettingsFromScene(triggername);
		}

		/// <summary>
		/// Adds the motive to the planner.
		/// </summary>
		/// <param name="motive">Motive.</param>
		public void AddMotiveToPlanner(GameObject motive) {
			// First watch for planner gameobject
			GameObject tmpplanner = chatbot.GetComponentInChildren<Planner>().gameObject;
			// Throw error if no planner attached
			if(tmpplanner==null)
				Debug.LogWarning("To use chatbot you need to attatch Planner.cs script to the Planner GameObject and pass Planner GameObject to Chatbot. GameObject is empty.");
// Unity 4.x
#if (UNITY_4_0||UNITY_4_0_1||UNITY_4_1||UNITY_4_2||UNITY_4_3||UNITY_4_5||UNITY_4_6)
			// Instantiate Motive from Prefabs
			GameObject MotiveCopy = UnityEngine.Object.Instantiate(motive) as GameObject;
#else
// Unity 5.x
// Instantiate Motive from Prefabs
GameObject MotiveCopy = UnityEngine.Object.Instantiate(motive);
#endif
			// Remain GameObject's name
			MotiveCopy.name = motive.name;
			// Parent to Planner
			MotiveCopy.transform.parent = tmpplanner.gameObject.transform;
		}

		/// <summary>
		/// Called, when motive helperfunction finished.
		/// </summary>
		public void MotiveFinished() {
			// If planner exists
			if(planner!=null)
				// Call planner finished function
				planner.Finished ();
		}
		
		/// <summary>
		/// Adds Setting with special name.
		/// </summary>
		/// <param name="name">Name.</param>
		public void SetGlobalSetting(string name, string value){
			// If bot is assigned
			if (bot!=null&&name!=null&&value!=null) {
				// Add setting to Program #
				if (bot.GlobalSettings.containsSettingCalled (name))
					bot.GlobalSettings.updateSetting (name, value);
				else 
					bot.GlobalSettings.addSetting (name, value);
				// Ignore Jurassic, as there is no computation outside from
				// <script></script>
			} else {
				// Throw warning
				Debug.LogWarning("SetGlobalSetting: AIMLbot.Bot instance is not assigned or string not passed! Are all Trigger settings assigned?");
			}
		}
		
		/// <summary>
		///  Get Setting by name.
		/// </summary>
		/// <param name="name">Name.</param>
		public string GetGlobalSetting(string name){
			// First check if bot is assigned
			if (bot != null) {
				string programsharp = bot.GlobalSettings.grabSetting (name);
				return programsharp;
			}
			return "";
			// Ignore Jurassic, as there is no computation outside from
			// <script></script>
		}

		/// <summary>
		/// This function is called, when Program # parses AIML files
		/// and enters <script></script> element. 
		/// All global settings from Program # are now transfered
		/// to Jurassic.
		/// </summary>
		public void TransferGlobalSettingsFromProgramSharpToJurassic() {
			// Global Settings
			Setting[] settings;
			// Gather all attatched settings from Scene
			settings = chatbot.GetComponentsInChildren<Setting>();
			// Loop through settings and 
			foreach(Setting setting in settings){
				// Set global settings in Jurassic
				bot.jscript_engine.SetGlobalValue(setting.gameObject.name,GetGlobalSetting(setting.gameObject.name));
			}
		}

		/// <summary>
		/// This function is called, when Program # parses AIML files
		/// and exits <script></script> element. 
		/// All global settings that were passed earlier to Jurassic are now
		/// (after JavaScript processing) transfered back to Program #.
		/// All Settings in the Unity Scene are updated also.
		/// </summary>
		public void TransferGlobalSettingsFromJurassicToProgramSharp() {
			// Global Settings
			Setting[] settings;
			// Gather all attatched settings from Scene
			settings = chatbot.GetComponentsInChildren<Setting>();
			// Loop through settings and 
			foreach(Setting setting in settings){
				// Set global settings in Program #
				SetGlobalSetting(setting.gameObject.name,bot.jscript_engine.GetGlobalValue(setting.gameObject.name).ToString());
				// Set global settings in Unity Scene
				setting.value = GetGlobalSetting(setting.gameObject.name);
			}
		}
	}
}
