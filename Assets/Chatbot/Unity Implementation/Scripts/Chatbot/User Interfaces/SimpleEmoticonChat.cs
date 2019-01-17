using UnityEngine;
// If we're running in the Unity editor	
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using Chatbot;
/// <summary>
/// Chatbot emoticons struct containing gif filename and frames.
/// </summary>
public struct ChatbotEmoticons
{
	// Name of Gif
	public string _gifFile;
	// Frames of gif
	public List<Texture2D> _frames;
}
/// <summary>
/// User interface, that displays a grey box,
/// the emoticon and an input box.
/// </summary>
[AddComponentMenu("Chatbot/User Interfaces/Simple Emoticon Chat")]
public class SimpleEmoticonChat : MonoBehaviour {
	// Chat in- and output strings.
	private string Input_Text="";
	private string Output_Text="";
	// Reference to the Chatbot.Core instance.
	private Chatbot.Core bot=null;
	// Path to gif files.
	public string loadingGifPath;
	// Gif animation speed.
	public float speed = 0.12f;
	// Gif draw position (is not the center of the gif image)
	// used for intern calculation.
	private Vector2 drawPosition;
	// Center of gif.
	public Vector2 centerPosition = new Vector2(160.0f,160.0f);
	// All gif frames within gif file
	List<Texture2D> gifFrames = new List<Texture2D>();
	// List of different emoticons
	List<ChatbotEmoticons> _Emoticons = new List<ChatbotEmoticons>();
	// Scale of the emoticons
	public float _emoticonScale=1.0f;
	/// <summary>
	/// Awake is called before start. Here we convert the gif files to single textures if needed.
	/// This only works in the editor
	/// </summary>
	void Awake() {
// If we're running in the Unity editor and not unity webplayer nor webgl
#if UNITY_EDITOR && !UNITY_WEBPLAYER && !UNITY_WEBGL
		// Loop through gif files from folder .../Assets/Chatbot/Unity Implementation/Emoticons/
		foreach (string _file in Directory.GetFiles(Application.dataPath + "/Chatbot/Unity Implementation/Emoticons/"))
 		{ 
			// Declare frame variable
			Texture2D _gifFrame=null;
			// Try to load image from resources
			_gifFrame = Resources.Load("Chatbot/Unity Implementation/Emoticons/"+ (_file.Remove(_file.LastIndexOf("."))).Substring(_file.LastIndexOf("/")+1) + "_0", typeof(Texture2D)) as Texture2D;
			// Check if gif has already been split into single images
			if(_gifFrame==null&&_file.Substring(_file.LastIndexOf(".")+1).ToLower()=="gif")
			{
				// We need to parse the gif and save the frames saperately
				// Initialize gif function
				//var gifImage= Image.FromFile(_file);
				//var dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
				//int frameCount = gifImage.GetFrameCount(dimension);
				//for (int i = 0; i < frameCount; i++)
				{
					//gifImage.SelectActiveFrame(dimension, i);
					//var frame = new Bitmap(gifImage.Width, gifImage.Height);
					//System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, Point.Empty);
					//var frameTexture = new Texture2D(frame.Width, frame.Height);
					//for (int x = 0; x < frame.Width; x++)
					//	for (int y = 0; y < frame.Height; y++)
					{
						//System.Drawing.Color sourceColor = frame.GetPixel(x, y);
						//frameTexture.SetPixel(x, frame.Height -1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
					}
					// Apply texture changes
					//frameTexture.Apply();
					// Encode texture into PNG
					//byte[] _pngBytes = frameTexture.EncodeToPNG ();
					// Write Texture as png in Resources Folder
					//System.IO.File.WriteAllBytes (Application.dataPath + "/Chatbot/Resources/Chatbot/Unity Implementation/Emoticons/" + (_file.Remove(_file.LastIndexOf("."))).Substring(_file.LastIndexOf("/")+1) + "_" + i + ".png", _pngBytes);
				}				
			} 
		}
#if (!UNITY_4_0&&!UNITY_4_0_1)
		// Causes a freeze in Unity 4.0. Thus changes are first applied upon restart.
		// Save changed/new assets
		AssetDatabase.SaveAssets();
		// Refresh database
		AssetDatabase.Refresh();
#endif
#endif
		// Try to load frames from resources
		var _resourcesArray = Resources.LoadAll("Chatbot/Unity Implementation/Emoticons",typeof(Texture2D));
		// Create texture array
		Texture2D[] _resourcesGifFrames = new Texture2D[_resourcesArray.Length];
		// Loop through ressources array
		for(var i = 0; i < _resourcesArray.Length; i++)
			// Store in texture array
			_resourcesGifFrames[i] = _resourcesArray[i] as Texture2D;
		// Create new list of emoticons
		List<string> _emoticonList = new List<string>();
		// Loop through all frames
		foreach(Texture2D _currentFrame in _resourcesGifFrames) 
		{
			// Check if emoticon is new
			if(!_emoticonList.Contains(_currentFrame.name.Remove(_currentFrame.name.LastIndexOf("_"))))
			{
				// Add emoticon to list
				_emoticonList.Add(_currentFrame.name.Remove(_currentFrame.name.LastIndexOf("_")));
			}
		}
		// Now Loop through emoticons
		foreach(string _currentEmoticon in _emoticonList)
		{
			int _tmpIterator = 0;
			// Create new item
			ChatbotEmoticons _emoticon = new ChatbotEmoticons();
			// Create new list of frames
			_emoticon._frames = new List<Texture2D>();
			// Store filename
			_emoticon._gifFile=_currentEmoticon + ".gif";
			// Frame texture
			Texture2D _currentGifFrame=null;
			// Load all frames from resources
			while((_currentGifFrame = Resources.Load("Chatbot/Unity Implementation/Emoticons/"+ _currentEmoticon + "_" + _tmpIterator, typeof(Texture2D)) as Texture2D)!=null)
			{
// If we're running in the Unity editor	
#if UNITY_EDITOR
				// Set texture importer format
				SetTextureImporterFormat(_currentGifFrame);
#endif
				// Store Texture
				_emoticon._frames.Add(_currentGifFrame);
				// Increase Iterator
				_tmpIterator++;
			}
			// Add to list
			_Emoticons.Add(_emoticon);	
// In Unity Editor and Unity version not 4.0.x 
#if (UNITY_EDITOR&&!UNITY_4_0&&!UNITY_4_0_1)
			// Causes a freeze in Unity 4.0. Thus changes are first applied upon restart.
			// Save changed/new assets
			AssetDatabase.SaveAssets();
			// Refresh database
			AssetDatabase.Refresh();
#endif
		}
	}
// If we're running in the Unity editor	
#if UNITY_EDITOR	
	/// <summary>
	/// Sets the texture importer format
	/// </summary>
	public static void SetTextureImporterFormat( Texture2D texture)
	{
		// Nothing to do if no texture passed
		if ( null == texture ) return;
		// Retrieve textures file path
		string assetPath = AssetDatabase.GetAssetPath( texture );
		// Open path with texture importer class
		var tImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
		// Did we succeed wopen the file?
		if ( tImporter != null )
		{
			// Change texture type to advanced
			tImporter.textureType = TextureImporterType.Default;
			// Set to no n potential import scale to avoid deformation
			tImporter.npotScale = TextureImporterNPOTScale.None;
		}
	}
#endif
	/// <summary>
	/// Initializes gif draw function and tries
	/// to access the ChatbotCore.core.bot variable.
	/// </summary>
	void Start () {
		//Debug.Log ("Test: " + _Emoticons.Count);
		// Loop throug existing emoticons
		foreach (ChatbotEmoticons _entry in _Emoticons)
		{
			// If the gif file names match
			if(loadingGifPath==_entry._gifFile)
			{
				// Loop through frames
				foreach(Texture2D _frame in _entry._frames)
				{
					// Add frame
					gifFrames.Add(_frame);
				}
				// Copy the frame list
				gifFrames = _entry._frames;
				// If gif frames exist and frame count is bigger than zero
				if(gifFrames!=null&&gifFrames.Count>0)
					// Store draw position
					drawPosition = new Vector2 (centerPosition.x-(gifFrames[0].width*_emoticonScale)/2.0f,centerPosition.y-(gifFrames[0].height*_emoticonScale)/2.0f);
			}
		}
		// Try to access ChatbotCore component
		if(this.gameObject.GetComponent<ChatbotCore>())
			// Try to access bot
			if(this.gameObject.GetComponent<ChatbotCore>().bot!=null)
				bot = this.gameObject.GetComponent<ChatbotCore>().bot;
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
	/// Render the output of chatbot, like dialoges and more.
	/// </summary>
	void OnGUI() {
		// Draw Emoticon in front of other elements
		//GUI.depth = 0;
		// Render if reference to bot exists
		if (bot != null) {
			// Centered output style, color and wordwarp
			GUIStyle outputstyle = new GUIStyle ();
			outputstyle.alignment = TextAnchor.UpperCenter;
			outputstyle.normal.textColor = UnityEngine.Color.white;
			outputstyle.wordWrap = true;
			// Enable Word warp
			GUI.skin.label.wordWrap = true;
			// Make a background box
			GUI.Box (new Rect (10, 10, 600, 400), "Chat with a Chatbot");
			// Make output label
			GUI.Label (new Rect (20, 300, 580, 40), Output_Text, outputstyle);
			// Make a text field that modifies Input_Text.
			Input_Text = GUI.TextField (new Rect (20, 350, 580, 20), Input_Text, 100);
			// If send button or enter pressed
			if (((Event.current.keyCode == KeyCode.Return) || GUI.Button (new Rect (550, 380, 50, 20), "Send")) && (Input_Text != "")) {
				// Simple perform chat and return output string
				Output_Text = bot.Chat (Input_Text);
				// Setting components in Scene are updated intern
				// Reset input text again
				Input_Text = "";
			}
			// When image changes, gui might be called.
			// So prevent error, if no frames loaded.
			if(gifFrames!=null&&gifFrames.Count!=0)
				// Draw selected emoticon
				GUI.DrawTexture(new Rect(drawPosition.x, drawPosition.y, (gifFrames[0].width*_emoticonScale), (gifFrames[0].height*_emoticonScale)), gifFrames[(int)(Time.realtimeSinceStartup * speed) % gifFrames.Count]);
		} else {
			// Try to access ChatbotCore component
			if(this.gameObject.GetComponent<ChatbotCore>())
				// Try to access bot
				if(this.gameObject.GetComponent<ChatbotCore>().bot!=null)
					bot = this.gameObject.GetComponent<ChatbotCore>().bot;
		}
	}
	/// <summary>
	/// Changes path of gif file.
	/// </summary>
	/// <param name="newpath">Newpath.</param>
	public void ChangeGif(string newpath)
	{
		// Set new gif path
		loadingGifPath = newpath;
		// Create new list
		gifFrames = new List<Texture2D>();
		// Load file like in Start function.
		Start();
	}
}
