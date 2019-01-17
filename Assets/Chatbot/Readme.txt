Hello!

Thank you for downloading Chatbot.

This asset gives you the oppertunity to work with AIML files and create chatbots or speaking bots. It is realized by an implementation of Program# and Jurassic for runtime JavaScript support.

Installation:

1. Make sure, that you selected one of the supported plattforms (PC, Mac & Linux Standalone, Webplayer, WebGL, Android).
You can do this by clicking File->Build Settings-> Click your Plattform once ->Switch Plattform.

2. Now Chatbot is installed correctly and you can continue with the first steps in the manual.

Changelog 1.3.4:
- Fixed Topic categories not working.

Changelog 1.3.3:
- Fixed error when selecting Webplayer plattform. Read the manual for instructions on Webplayer/Web GL releases for more information on how to update gifs of advanced chatbot example.
- Updated manual.

Changelog 1.3.2:
- Added support for IOS. Full support for basic and advanced example.
- Updated manual.

Changelog 1.3.1:
- Added support for Android. Full support for basic and advanced example.
- Added full advanced example support for WebGL and Webplayer plattform.

Changelog 1.3.0:
- Added WebGL Plattform support for Unity 5.2.0f3 upwards.
- Roadmap added to company site with planned future features. Take a look for further development.

Changelog 1.2.7:
- Improved documentation on how to get your scenes compiled.
- Compiled and working examples for all scenes and plattforms as reference.

Changelog 1.2.6:
- Fixed problems with Win/Mac/Linux Standalone Build. 
- All aiml and setting files updated and now use utf-8 encoding.
- Updated manual with instruction how to build an executable for your plattform

Changelog 1.2.5:
- Jurassic.dll compiled and replaces the Jurassic source files. You can still use Jurassic as source code version. Just follow the readme.txt instructions in the JurassicSource.zip file. Or you can build the Jurassic.dll yourself. The complete Dll-Project is inside the JurassicDll.zip file.

Changelog 1.2.4:
- Compatibility from Unity 4.0.0 to 5.2.1
- You no longer need the full Net 2.0 support. Net 2.0 Subset is sufficient.
- Small changes in Advanced Example to prevent multiple expressions occuring consecutively.

Changelog 1.2.3:
- Program # in Basic Example now remembers your name and other information. Basic AIML files and config files were updated to prohibit overwriting bot variables by user variables. Keep in mind, that at the current state all variables being handled as global variables, so you have to pay attention that user and bot variables won't intersect. You may ask why i'm not using both user and bot variables seperately. In the original interpreter, get and set can be used to change user variables. For example the bot can remember your name. But the bot can only be accessed by the bot command. Thus it's only possible to read bot variables that were once set at the config file, but you can't change if the bot for example grows one year older, gets new friends etc. Despite this fact it is simpler relatet to Jurassic global variable handling to use only one layer of global variables, the bot variables, that represent the knowledge of the bot. They can be accessed in aiml throug set and get. The command bot works like get. If you want to use other AIML language packs, you need to rename the bot variables in config and AIML files to prevent overwriting the bot variables by user variables.

Changelog 1.2.1:
- Webplayer support integrated for Basic Example and Advanced Example. The Advanced Example is restricted. Gif animation will not work in compiled webplayer version jet, because the Webplayer plattform
  does not support some namespaces of System.Drawing like Streaming functionality. Still looking for a workaround.
- Updated manual with description, what is important when building in webplayer plattform.

Changelog 1.2.0:
- Fixed Mac error and replaced "\" by "/"
- Introduction of Chatbot Framework to connect Jurassic and Program # and simplify gameprogramming.
- The JavaScript executed in the aiml file now features exception handling in the Unity console. You get detailed information about the error and line, the error occured.
- New Scene Advanced Excample added, even though not all categorys have been translated. This will be done in further updates. It introduces the capabilitys of the chatbot framework.
- Jurassic is now again implemented in source code to grant transparency and flexibility.


Note for advanced example:
The developement of the advanced example is still in progress, as there are many thousend categorys to be written. New categorys will be added in further Updates. Therefore you might often retrieve the default response. Look in the advancedaiml folder to find out, what you can ask the bot.


Make your own AIML files with e.g. Gaitobot. You can find detailed Documentation in the manual and information on www.chatbotunityasset.com

If you have any questions feel free to mail us via mail@chatbotunityasset.com or post your contribution at www.chatbotunityasset.com/forum/

Please don’t forget to give feedback and consider, that the project is still in continous development.

Thank you in advance,
Nexus Gamesoft