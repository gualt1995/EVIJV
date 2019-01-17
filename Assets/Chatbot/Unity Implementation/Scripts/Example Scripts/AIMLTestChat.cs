using System;
using UnityEngine;
using System.Collections;
using AIMLbot.Utils;

/// <summary>
/// Despite the progress of Chatbot, the basic functional capability of Program #
/// parsing AIML 1.0.1 files is to be conserved. This is an example script of
/// parsing AIML files with optional Jurassic functionality. As an example, the original
/// AIML Set AIML-en-us-foundation-ALICE from http://www.alicebot.org/downloads/sets.html
/// is utilized.
/// 
/// Chatbot MonoBehaviour, to be attatcht to the character root GameObject.
/// </summary>
[AddComponentMenu("Chatbot/Example Scripts/AIML Test Chat")]
public class AIMLTestChat : MonoBehaviour {
	// Simple I/O string variables
	private string Input_Text = "";
	private string Output_Text =""; 
	// Program # Variables
	private AIMLbot.Bot bot;
	private AIMLbot.User user;
	private AIMLbot.Request request;
	private AIMLbot.Result result;
	/// <summary>
	/// Initialize our derived MonoBehaviour
	/// </summary>
	void Start() {
		// Initialize Program # variables
// For Webplayer plattform and WebGl
#if (UNITY_WEBPLAYER || UNITY_WEBGL)
		ProgramSharpWebplayerCoroutine WebplayerCoroutine = this.gameObject.GetComponent<ProgramSharpWebplayerCoroutine>();
		if(WebplayerCoroutine!=null)
			bot = new AIMLbot.Bot(WebplayerCoroutine,Application.dataPath + "/Chatbot/Program #/config/Settings.xml");
		else
			Debug.LogWarning("You need to attatch the Webplayer Component in Webplayer plattform mode.");
// Other plattforms
#else
		// Only for Windows, Linux and Mac OSX
		bot = new AIMLbot.Bot();
#endif
		user = new AIMLbot.User("User",bot);
		request = new AIMLbot.Request("",user,bot);
		result = new AIMLbot.Result(user,bot,request);
// Only for Windows, Linux, Mac OSX Android and IOS
#if !(UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS)
		// Load Settings from Xml file in config directory. Plattform dependend Path issues may
		// occur.
		bot.loadSettings(Application.dataPath + "/Chatbot/Program #/config/Settings.xml");
		// Load AIML files from AIML path defined in Settings.xml
		bot.loadAIMLFromFiles();
#endif
// Android and IOS release
#if (UNITY_ANDROID||UNITY_IOS)
		// Load Settings from Xml file in config directory within resources folder.
		bot.loadSettings("Chatbot/Program #/config/Settings");
		// Load AIML files from AIML path defined in Settings.xml
		bot.loadAIMLFromFiles();
#endif
		// Define to or not to use JavaScript (Jurassic) in AIML
		if(bot!=null)
			bot.UseJavaScript = true;
	}

	void OnGUI () {
		// Enable Word warp
		GUI.skin.label.wordWrap = true;
		// Make a background box
		GUI.Box(new Rect(10,10,300,500), "Chat with a Chatbot");
		// Make output label
		GUI.Label (new Rect (20, 30, 280, 40), Output_Text);
		// Make a text field that modifies Input_Text.
		Input_Text = GUI.TextField (new Rect (20, 100, 280, 20), Input_Text, 100);
		// If send button or enter pressed
		if(((Event.current.keyCode == KeyCode.Return)||GUI.Button(new Rect(250,130,50,20),"Send")) && (Input_Text != "")) {
			// Prepare Variables
			// You don't need to care, wether Only Jurassics or only Program #'s Variables
			// are changed. This is managed immediate intern every time you change a global
			// variable in Program # or Jurassic.
			// bot.jscript_engine.SetGlobalValue("abc",15);

			request.rawInput = Input_Text;
			request.StartedOn = DateTime.Now;
			result = bot.Chat(request);
			Output_Text = result.Output;
			Input_Text = "";
		
			// Gather Variables
			// bot.jscript_engine.GetGlobalValue<string>("abc");
		}
	}
}
