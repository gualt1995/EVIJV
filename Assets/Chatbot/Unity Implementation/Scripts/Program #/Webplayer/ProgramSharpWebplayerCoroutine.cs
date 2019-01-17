using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using AIMLbot;
using AIMLbot.Utils;

/// <summary>
/// Calls Coroutine to load files via Unity WWW class.
/// Use this MonoBehaviour when using Webplayer plattform
/// </summary>
[AddComponentMenu("Chatbot/Program Sharp/Webplayer Component")]
public class ProgramSharpWebplayerCoroutine : MonoBehaviour {
	// The assigned bot
	private AIMLbot.Bot bot;
	// Is Program # currently loading?
	private bool isloading = false;
	private Rect loadingwindow = new Rect (20,40,280,50);
	// String representing the path to the Settings.xml file
	private string pathtosettings ="";
	// Should settings be loaded from scene?
	private bool loadsettingsfromscene = false;
	/// <summary>
	/// Pass AIMLbot.Bot instance.
	/// </summary>
	/// <param name="tmpbot">AIMLbot.Bot instance.</param>
	public void Initialize(AIMLbot.Bot tmpbot) {
		// Retrieve AIMLbot.Bot instance.
		bot = tmpbot;
		// Throw warning if no bot passed
		if (bot == null)
			Debug.LogWarning ("Initialize(AIMLbot.Bot tmpbot) function, tmpbot==null.");
		// try a safe default setting for the settings xml file
		pathtosettings = Application.dataPath + "/Chatbot/Program%20%23/config/Settings.xml";
		StartCoroutine(this.Load(pathtosettings));  
	}
	/// <summary>
	/// Pass AIMLbot.Bot instance.
	/// </summary>
	/// <param name="tmpbot">AIMLbot.Bot instance.</param>
	public void Initialize(AIMLbot.Bot tmpbot, string tmppath, bool tmploadsettingsfromscene = false) {
		// Should settings be loaded from scene or file.
		loadsettingsfromscene = tmploadsettingsfromscene;
		// Retrieve AIMLbot.Bot instance.
		bot = tmpbot;
		// Throw warning if no bot passed
		if (bot == null)
			Debug.LogWarning ("Initialize(AIMLbot.Bot tmpbot) function, tmpbot==null.");
		// Throw waring if no path passed
		if (tmppath == null) {
			Debug.LogWarning ("Initialize function, tmppath==null. Take default path");
			pathtosettings = Application.dataPath + "/Chatbot/Program%20%23/config/Settings.xml";
		} else
			pathtosettings = tmppath;
		StartCoroutine(this.Load(pathtosettings));
	}

	/// <summary>
	/// Loads the settings, splitters and aiml files.
	/// </summary>
	public IEnumerator Load(string path) {
		// Show loading message
		isloading = true;
		// Disable user input when loading
		bot.isAcceptingUserInput = false;
		// Load settings from file
		if (loadsettingsfromscene == false)
			// Load Settings.xml file and wait till it's loaded
			yield return StartCoroutine (this.loadSettingsWWW (path, this.bot.GlobalSettings));
		else {
			// Load settings from scene
			// Global Settings
			Setting[] settings;
			// Gather all attatched settings from Scene
			settings = this.gameObject.GetComponentsInChildren<Setting>();
			// Loop through settings and 
			foreach(Setting setting in settings){
				// Add global setting to Program #
				if (bot.GlobalSettings.containsSettingCalled (setting.gameObject.name))
					bot.GlobalSettings.updateSetting (setting.gameObject.name, setting.value);
				else 
					bot.GlobalSettings.addSetting (setting.gameObject.name, setting.value);
				// Ignore Jurassic, as there is no computation outside from
				// <script></script>
			}
		}
		// Check settings
		// Checks for some important default settings
		if (!this.bot.GlobalSettings.containsSettingCalled("botversion"))
		{
			this.bot.GlobalSettings.addSetting("botversion", Environment.Version.ToString());
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botname"))
		{
			this.bot.GlobalSettings.addSetting("botname", "Unknown");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botmaster"))
		{
			this.bot.GlobalSettings.addSetting("botmaster", "Unknown");
		} 
		if (!this.bot.GlobalSettings.containsSettingCalled("author"))
		{
			this.bot.GlobalSettings.addSetting("author", "Nicholas H.Tollervey");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botlocation"))
		{
			this.bot.GlobalSettings.addSetting("botlocation", "Unknown");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botgender"))
		{
			this.bot.GlobalSettings.addSetting("botgender", "-1");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botbirthday"))
		{
			this.bot.GlobalSettings.addSetting("botbirthday", "2006/11/08");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("botbirthplace"))
		{
			this.bot.GlobalSettings.addSetting("botbirthplace", "Towcester, Northamptonshire, UK");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("website"))
		{
			this.bot.GlobalSettings.addSetting("website", "http://sourceforge.net/projects/aimlbot");
		}
		if (this.bot.GlobalSettings.containsSettingCalled("adminemail"))
		{
			string emailToCheck = this.bot.GlobalSettings.grabSetting("adminemail");
			this.bot.AdminEmail = emailToCheck;
		}
		else
		{
			this.bot.GlobalSettings.addSetting("adminemail", "");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("islogging"))
		{
			this.bot.GlobalSettings.addSetting("islogging", "False");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("willcallhome"))
		{
			this.bot.GlobalSettings.addSetting("willcallhome", "False");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("timeout"))
		{
			this.bot.GlobalSettings.addSetting("timeout", "2000");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("timeoutmessage"))
		{
			this.bot.GlobalSettings.addSetting("timeoutmessage", "ERROR: The request has timed out.");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("culture"))
		{
			this.bot.GlobalSettings.addSetting("culture", "en-US");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("splittersfile"))
		{
			this.bot.GlobalSettings.addSetting("splittersfile", "Splitters.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("person2substitutionsfile"))
		{
			this.bot.GlobalSettings.addSetting("person2substitutionsfile", "Person2Substitutions.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("personsubstitutionsfile"))
		{
			this.bot.GlobalSettings.addSetting("personsubstitutionsfile", "PersonSubstitutions.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("gendersubstitutionsfile"))
		{
			this.bot.GlobalSettings.addSetting("gendersubstitutionsfile", "GenderSubstitutions.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("defaultpredicates"))
		{
			this.bot.GlobalSettings.addSetting("defaultpredicates", "DefaultPredicates.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("substitutionsfile"))
		{
			this.bot.GlobalSettings.addSetting("substitutionsfile", "Substitutions.xml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("aimldirectory"))
		{
			this.bot.GlobalSettings.addSetting("aimldirectory", "Chatbot/Program #/aiml");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("configdirectory"))
		{
			this.bot.GlobalSettings.addSetting("configdirectory", "Chatbot/Program #/config");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("logdirectory"))
		{
			this.bot.GlobalSettings.addSetting("logdirectory", "Chatbot/Program #/logs");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("usejavascript"))
		{
			this.bot.GlobalSettings.addSetting("usejavascript", "true");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("maxlogbuffersize"))
		{
			this.bot.GlobalSettings.addSetting("maxlogbuffersize", "64");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("notacceptinguserinputmessage"))
		{
			this.bot.GlobalSettings.addSetting("notacceptinguserinputmessage", "This bot is currently set to not accept user input.");
		}
		if (!this.bot.GlobalSettings.containsSettingCalled("stripperregex"))
		{
			this.bot.GlobalSettings.addSetting("stripperregex", "[^0-9a-zA-Z]");
		}
		// Load the dictionaries for this Bot from the various configuration files
		yield return StartCoroutine(this.loadSettingsWWW (this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("person2substitutionsfile"), this.bot.Person2Substitutions));
		yield return StartCoroutine(this.loadSettingsWWW (this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("personsubstitutionsfile"), this.bot.PersonSubstitutions));
		yield return StartCoroutine(this.loadSettingsWWW (this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("gendersubstitutionsfile"), this.bot.GenderSubstitutions));
		yield return StartCoroutine(this.loadSettingsWWW (this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("defaultpredicates"), this.bot.DefaultPredicates));
		yield return StartCoroutine(this.loadSettingsWWW (this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("substitutionsfile"), this.bot.Substitutions));
		
		// Grab the splitters for this bot
		yield return StartCoroutine(this.loadsplittersWWW(this.bot.PathToConfigFiles +"/"+ this.bot.GlobalSettings.grabSetting("splittersfile")));

		if (this.bot.Splitters.Count == 0)
		{
			// we don't have any splitters, so lets make do with these...
			this.bot.Splitters.Add(".");
			this.bot.Splitters.Add("!");
			this.bot.Splitters.Add("?");
			this.bot.Splitters.Add(";");
		}

		// Load AIML files
		yield return StartCoroutine (this.loadAIMLFromFilesWWW());
		// Enable user input when loading has finished
		bot.isAcceptingUserInput = true;
		// Disable loading message
		isloading = false;
	}
	/// <summary>
	/// Loads the AIML files via Unity WWW class.
	/// </summary>
	/// <returns>The AIML from files WW.</returns>
	public IEnumerator loadAIMLFromFilesWWW() 
	{
		// Webplayer plattform
		// Retrieve list of all AIML files to load.
		List<string> AIMLFiles = AIMLFilesForWebplayer;
		// If List contains entrys
		if (AIMLFiles.Count > 0)
		{
			// Loop through all entrys
			foreach (string filename in AIMLFiles)
			{
				yield return StartCoroutine(this.loadAIMLFileWWW(Application.dataPath + "/" + this.bot.GlobalSettings.grabSetting("aimldirectory")+ "/" + filename));
			}
			this.bot.writeToLog("Finished processing the AIML files. " + Convert.ToString(this.bot.Size) + " categories processed.");
		}
		else
		{
			Debug.LogWarning("Could not find any .aiml files defined in the config settings entry aimlfilesforwebplayer. Please make sure that your aiml file end in a lowercase aiml extension, for example - myFile.aiml is valid but myFile.AIML is not.");
		}
	}
	/// <summary>
	/// Returns a List of all AIML files to load. Only needed for Webplayer plattform, because
	/// Directory command is not allowed on every server by default.
	/// </summary>
	private List<string> AIMLFilesForWebplayer
	{
		get
		{
			// List of all AIML files to load.
			List<string> AIMLFiles = new List<string>();
			// String of all AIML files seperated by ','.
			string AIMLFilesString;
			// Load string from config.xml file
			AIMLFilesString = this.bot.GlobalSettings.grabSetting("aimlfilesforwebplayer");
			// Current file
			string CurrFile = "";
			// Loop through all chars
			for(int i=0; i<AIMLFilesString.Length;i++)
			{
				// If current char is no seperator
				if(AIMLFilesString[i]!=',')
				{
					// Add character to Current file string
					CurrFile+=AIMLFilesString[i];
				} 
				// If current char is seperator
				else
				{
					// If current file length greater zero
					if(CurrFile.Length!=0)
					{
						// Add current file string to list
						AIMLFiles.Add(CurrFile);
						// Reset current file string
						CurrFile = "";
					}
				}
			}
			// If current file length greater zero and after last entry
			// is no seperator
			if(CurrFile.Length!=0)
			{
				// Add current file string to list
				AIMLFiles.Add(CurrFile);
				// Reset current file string
				CurrFile = "";
			}
			return AIMLFiles;
		}
	}
	/// <summary>
	/// Given the name of a file in the AIML path directory, attempts to load it into the 
	/// graphmaster
	/// </summary>
	/// <param name="filename">The name of the file to process</param>
	public IEnumerator loadAIMLFileWWW(string pathToAIMLFile)
	{
		this.bot.writeToLog("Processing AIML file: " + ConvertToUrlFriendly(pathToAIMLFile));
		// Acces file through Unity WWW class
		WWW webReq = new WWW(ConvertToUrlFriendly(pathToAIMLFile));
		// Wait for webReq to finish downloading
		yield return webReq;
		// Did an error appear during loading?
		if (webReq.error != null)
			// Log error and skip this file
			Debug.LogWarning (webReq.error + " Skipped file.");
		else {
			// Create a XmlReader instance with the retrieved string
			XmlReader reader = XmlReader.Create (new StringReader (webReq.text));
			// Create XmlDocument equal to Windows, Linux and Mac OSX code.
			XmlDocument doc = new XmlDocument ();
			// Load from XmlReader instance
			doc.Load (reader);
			// Continue processing AIML file
			this.bot.loadAIMLFromXML (doc, pathToAIMLFile);
		}
	}
	/// <summary>
	/// Loads splitters into the class from the url referenced in urlToSplitters using WWW class.
	/// 
	/// The XML should have an XML declaration like this:
	/// 
	/// <?xml version="1.0" encoding="utf-8" ?> 
	/// 
	/// followed by a <root> tag with child nodes of the form:
	/// 
	/// <item name="name" value="value"/>
	/// </summary>
	/// <param name="urlToSplitters">The file containing the splitters</param>
	public IEnumerator loadsplittersWWW(string urlToSplitters)
	{
		// Acces files through Unity WWW class
		WWW webReq = new WWW(ConvertToUrlFriendly(urlToSplitters));
		// Wait for webReq to finish downloading
		yield return webReq;
		// Did an error appear during loading?
		if (webReq.error != null)
			// Log error and skip this file
			Debug.LogWarning (webReq.error + " Skipped file.");
		else {
			// Create a XmlReader instance with the retrieved string
			XmlReader reader = XmlReader.Create (new StringReader (webReq.text));
			// Create XmlDocument equal to Windows, Linux and Mac OSX code.
			XmlDocument splittersXmlDoc = new XmlDocument ();
			// Load splitters from XmlReader instance
			splittersXmlDoc.Load (reader);
			// the XML should have an XML declaration like this:
			// <?xml version="1.0" encoding="utf-8" ?> 
			// followed by a <root> tag with children of the form:
			// <item value="value"/>
			if (splittersXmlDoc.ChildNodes.Count == 2) {
				if (splittersXmlDoc.LastChild.HasChildNodes) {
					foreach (XmlNode myNode in splittersXmlDoc.LastChild.ChildNodes) {
						if ((myNode.Name == "item") & (myNode.Attributes.Count == 1)) {
							string value = myNode.Attributes ["value"].Value;
							this.bot.Splitters.Add (value);
						}
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Loads bespoke settings into the class from the url referenced in urlToSettings using WWW class.
	/// 
	/// The XML should have an XML declaration like this:
	/// 
	/// <?xml version="1.0" encoding="utf-8" ?> 
	/// 
	/// followed by a <root> tag with child nodes of the form:
	/// 
	/// <item name="name" value="value"/>
	/// </summary>
	/// <param name="urlToSettings">The file containing the settings</param>
	public IEnumerator loadSettingsWWW(string urlToSettings,AIMLbot.Utils.SettingsDictionary settingsdictionary)
	{ 
		// Acces files through Unity WWW class
		WWW webReq = new WWW(ConvertToUrlFriendly(urlToSettings));
		// Wait for webReq to finish downloading
		yield return webReq;
		// Did an error appear during loading?
		if (webReq.error!=null)
			// Log error and skip this file
			Debug.LogWarning (webReq.error + " Skipped file.");
		else {
		// Create a XmlReader instance with the retrieved string
		XmlReader reader = XmlReader.Create(new StringReader(webReq.text));
		// Create XmlDocument equal to Windows, Linux and Mac OSX code.
		XmlDocument xmlDoc = new XmlDocument();
		// Now load xml doc from XmlReader instance
		xmlDoc.Load(reader);
		if (settingsdictionary == null)
			Debug.LogWarning ("No AIMLbot.Utils.SettingsDictionary instance passed!");
		// And call loadSettings function
		settingsdictionary.loadSettings (xmlDoc);
		}
	}

	/// <summary>
	/// Converts to URL friendly string.
	/// </summary>
	/// <returns>Returns URL friendly string.</returns>
	/// <param name="url">URL.</param>
	private string ConvertToUrlFriendly(string url) {
		string newstring = "";
		if (url.Substring (0, 4) == "file" || url.Substring (0, 4) == "http" || url.Substring (0, 3) == "ftp") {
			// Do nothing
		}
		else
			// No pre-definition. Set to local.
			newstring="file://";
		// Loop through each character
		for(int i = 0; i< url.Length;i++){
			switch(url[i]) {
				// This character are reserved to url:
				// ! # $ % & ' ( ) * + , / : ; = ? @ [ ]
				// So think twice, when escaping them.
			case ' ':
				// Replace with %20
				newstring+="%20";
				break;
			case '#':
				// # is also an URL operator, so when working with url's you might
				// comment this out. But for this example
				// replace # with %23
				newstring+="%23";
				break;
			case '\\':
				// Replace \ with /
				newstring+="/";
				break;
			default:
				// Simple add char to new string
				newstring+=url[i].ToString();
				break;
			}
		}
		return newstring;
	}
	/// <summary>
	/// Raises the GU event.
	/// </summary>
	private void OnGUI() {
		// Show loading window when loading files.
		if(isloading==true)
			loadingwindow = GUI.Window (0, loadingwindow, DoLoadingWindow, "Loading");
	}

	/// <summary>
	/// Handles the loading window.
	/// </summary>
	/// <param name="windowID">Window I.</param>
	private void DoLoadingWindow(int windowID) {
		// Print loading label.
		GUI.Label (new Rect (80,20,270,60), "Loading... please wait!");
	}
}
