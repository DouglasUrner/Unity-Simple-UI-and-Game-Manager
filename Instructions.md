# Instructions
## Basic Setup
1. Check that the TextMeshPro package is installed (and install it with the Unity Package Manager if it is not) – this has to be done before importing the Simple-UI-and-Game-Manager assets.
1. Download the Simple-UI-and-Game-Manager asset package from GitHub by clicking on the link in Google Classroom..
1. Extract and import the .unitypackage assets file.
1. In the Project pane, you'll see a new Simple-UI-and-Game-Manager folder. There will be a new scene in your Scenes folder called  UI-and-Game-Manager.
1. Drag the UI-and-Game-Manager scene into the Hierarchy.
1. Test run your game – you should see the UI (and notice that it needs a bit of customization).
1. The UI customization is done in the GameInfo file which is in the Simple-UI-and-Game-Manager/Resources folder. The format of the file is called JSON (for JavaScript Object Notation). The contents of the GameInfo file are used to create a GameInfo object (named gameInfo) – which is used to fill in the UI.
1. Edit the GameInfo file to customize the UI to your liking – at the very least you'll want to set the values of the "title" and "description" fields.
1. When you build your game, be sure that your scene and the UI-and-Game-Manager scene are included in the build.
