# Instructions

## Basic Setup

1. Check that the TextMeshPro package is installed (and install it with the Unity Package Manager if it is not) – this has to be done before importing the Simple-UI-and-Game-Manager assets.
3. Download the latest release of the Simple-UI-and-Game-Manager asset package from GitHub, or you can also download by clicking on the link in Google Classroom. The link in Google Classroom may be an older version, so get if from GitHub if you can.
4. Extract and import the .unitypackage assets file.
5. In the Project pane, you'll see a new UI-and-Game-Manager folder. From the UI-and-Game-Manager/Scenes folder, drag the UI-and-Game-Manager into the Hierarchy -- you'll now have two scenes in the Hierarchy.
6. Drag the UI-and-Game-Manager scene to the top. This is important, as the build process only loads the top scene (the one at index 0).
7. Test run your game – you should see the UI (and notice that it needs a bit of customization).
8. The UI customization is done in the GameInfo file which is in the UI-and-Game-Manager/Resources folder. The format of the file is called JSON (for JavaScript Object Notation). The contents of the GameInfo file are used to create a GameInfo object (named gameInfo) – which is used to fill in the UI.
9. Edit the GameInfo file to customize the UI to your liking – at the very least you'll want to set the values of the "title" and "description" fields.
10. When you build your game, be sure that your scene and the UI-and-Game-Manager scene are included in the build.

## Detecting Game Over

There are two ways to detect the "game over" state:

* By adding a **Sensor** prefab to the scene and positioning (and scaling) it so that a collision with it will end the game (this would work with Prototypes 1, 2, and 4 from Create with Code for example. You can do this without modifying your existing scripts.
* By adding code that sets the Game Manager's **gameEnding** boolean to **true**.

When the game ends, the UI is displayed again and, if it is enabled, the **Quit** button is displayed. Clicking on the Quit button exits the game. The label for the quit button can be changed in GameInfo.json.

## Tracking Score & Health

The GameManager methods:

* AddPoints(int points)
* IncreaseHealth(int amt), and
* DecreaseHealth(int amt)

can be used to adjust the values shown in the Score and Health panels. The panels themselves can be enabled or disabled in GameInfo.json. If the health value goes to zero (or below) the game will end.
