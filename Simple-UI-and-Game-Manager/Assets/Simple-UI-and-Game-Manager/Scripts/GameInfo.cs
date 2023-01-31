using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
  public string title = "Default Title (from GameInfo class)";
  public string description = "No description (from GameInfo class)";
  public string gameOverMessage = "Game Over";
  public string startButtonLabel = "Start";
  // Label for start button when restarting.
  public string replayButtonLabel = "Play Again?";
  public string quitButtonLabel = "Quit";
  public bool displayQuitButton = false;
  public string scoreLabel = "Score";
  public bool displayScore = false;
  public string healthLabel = "Health";
  public bool displayHealth = false;
  public int initialHealth = 3;
}