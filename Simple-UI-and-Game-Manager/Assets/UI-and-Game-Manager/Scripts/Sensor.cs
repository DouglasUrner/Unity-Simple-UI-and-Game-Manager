using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
  // What to do on a collision.
  public enum CollisionAction
  {
    endGame,
    addPoints,
    subPoints,
    addHealth,
    subHealth
  }

  public bool destroySelf = false;
  public bool destroyOther = false;

  public enum CollisionEvent
  {
    onEnter,
    onExit
  }

  public CollisionAction action = CollisionAction.endGame;
  public int amount = 1;
  public CollisionEvent collisionEvent = CollisionEvent.onEnter;
  public bool hideOnPlay = false;
  
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    if (hideOnPlay) { GetComponent<Renderer>().enabled = false; }
  }

  void OnTriggerEnter(Collider c)
  {
    if (collisionEvent == CollisionEvent.onEnter)
    {
      TriggerAction(c);
    }
    else
    {
      return;
    }
  }

  void OnTriggerExit(Collider c)
  {
    if (collisionEvent == CollisionEvent.onExit)
    {
      TriggerAction(c);
    }
    else
    {
      return;
    }
  }

  void TriggerAction(Collider collider)
  {
    if (destroySelf) { Destroy(gameObject); }
    if (destroyOther) { Destroy(collider.gameObject); }

    switch (action)
    {
      case CollisionAction.endGame:
        gameManager.gameEnding = true;
        break;
      case CollisionAction.addPoints:
        gameManager.AddPoints(amount);
        break;
      case CollisionAction.subPoints:
        gameManager.AddPoints(-amount);
        break;
      case CollisionAction.addHealth:
        gameManager.IncreaseHealth(amount);
        break;
      case CollisionAction.subHealth:
        gameManager.DecreaseHealth(amount);
        break;
      default:
        Debug.LogErrorFormat($"unhandled CollisionAction: {action}");
        break;
    }
  }
}
