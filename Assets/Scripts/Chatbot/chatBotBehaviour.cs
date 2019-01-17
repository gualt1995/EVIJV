using System;
using UnityEngine;
using UnityEngine.UI;

public class chatBotBehaviour : MonoBehaviour
{
    // Simple I/O string variables
    private string Input_Text = "";
    private string Output_Text = "";
    // Program # Variables
    private AIMLbot.Bot bot;
    private AIMLbot.User user;
    private AIMLbot.Request request;
    private AIMLbot.Result result;
    private Text CommText;
    private Text ResponseText;

    /// <summary>
    /// Initialize our derived MonoBehaviour
    /// </summary>
    void Start()
    {
        CommText = GameObject.Find("CommText").GetComponent<Text>();
        ResponseText = GameObject.Find("ResponseText").GetComponent<Text>();
        bot = new AIMLbot.Bot();
        user = new AIMLbot.User("User", bot);
        request = new AIMLbot.Request("", user, bot);
        result = new AIMLbot.Result(user, bot, request);
        bot.loadSettings(Application.dataPath + "/Chatbot/Program #/config/Settings.xml");
        bot.loadAIMLFromFiles();
        if (bot != null)
            bot.UseJavaScript = true;
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void OnGUI()
    {

        //Input_Text = CommText.text.ToString();
        // Enable Word warp
        //GUI.skin.label.wordWrap = true;
        // Make a background box
        //GUI.Box(new Rect(10, 10, 300, 500), "Chat with a Chatbot");
        // Make output label
        //GUI.Label(new Rect(20, 30, 280, 40), Output_Text);
        // Make a text field that modifies Input_Text.
        if (Event.current.keyCode == KeyCode.Return)
        {
            // Prepare Variables
            // You don't need to care, wether Only Jurassics or only Program #'s Variables
            // are changed. This is managed immediate intern every time you change a global
            // variable in Program # or Jurassic.
            // bot.jscript_engine.SetGlobalValue("abc",15);

            request.rawInput = CommText.text.ToString();
            request.StartedOn = DateTime.Now;
            result = bot.Chat(request);
            ResponseText.text = result.Output;
            Input_Text = "";
            CommText.text = "";

            // Gather Variables
            // bot.jscript_engine.GetGlobalValue<string>("abc");
        }
    }
}
