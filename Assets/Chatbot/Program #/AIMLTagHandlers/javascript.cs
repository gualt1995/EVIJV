using System;
using System.Xml;
using System.Text;
using UnityEngine;
using Jurassic;

namespace AIMLbot.AIMLTagHandlers
{
    /// <summary>
    /// NOT IMPLEMENTED FOR SECURITY REASONS
    /// </summary>
    public class javascript : AIMLbot.Utils.AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public javascript(AIMLbot.Bot bot,
                        AIMLbot.User user,
                        AIMLbot.Utils.SubQuery query,
                        AIMLbot.Request request,
                        AIMLbot.Result result,
                        XmlNode templateNode)
            : base(bot, user, query, request, result, templateNode)
        {
			// Preprocess javascript, return result and then process result
			this.isRecursive = false;
        }

        protected override string ProcessChange()
        {
			if (this.templateNode.Name.ToLower() == "script")
			{
				// currently only AIML files in the local filesystem can be referenced
				if (this.templateNode.InnerText.Length > 0)
				{
					// String that function main returns
					string returnvalue="";
					// If using with chatbot
					if(bot.ProgramSharpJSWithChatbot) {
						// Transfer Program # public Values to Jurassic public Values
						if(this.bot.chatbotreference!=null)
							this.bot.chatbotreference.TransferGlobalSettingsFromProgramSharpToJurassic();
						else
							Debug.LogWarning("No reference to Chatbot.Core instance available! Did you miss Chatbot initialization?");
					}

					string jscript = this.templateNode.InnerText;
					try
					{
						// Execute Javascript  
						this.bot.jscript_engine.Execute(jscript);
						// Fill returnvalue with returned string from function main
						returnvalue = this.bot.jscript_engine.CallGlobalFunction<string>("main");
					}
					catch (JavaScriptException ex)
					{
						Debug.LogWarning("JS Line Number " + ex.LineNumber + " -> " + ex.ToString());
						this.bot.writeToLog("ERROR! Attempted (but failed) to execute following Javascript: " + jscript);
					}
					// If using with chatbot
					if(bot.ProgramSharpJSWithChatbot) {
						// Transfer Jurassic public Values to Program # public Values
						if(this.bot.chatbotreference!=null)
							this.bot.chatbotreference.TransferGlobalSettingsFromJurassicToProgramSharp();
							else
								Debug.LogWarning("No reference to Chatbot.Core instance available!Did you miss Chatbot initialization?");
					}
					return returnvalue;
				}
			}
			return string.Empty;
        }
    }
}
