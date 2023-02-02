using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
  void Awake()
  {
    #if !UNITY_EDITOR
      // Only need to load scenes manually in built game versions.
      // The editor handles loading for us. The assumption here is
      // that the UI-and-Game-Manager scene is at index 0, other
      // scenes are at higher indexes. This is kind of a hack... (XXX)
      SceneManager.LoadScene(1, LoadSceneMode.Additive);
      var sceneName = GetSceneNameByIndex(1);
      SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    #endif
  }

  static string GetSceneNameByIndex(int buildIndex)
  {
    if (buildIndex >= SceneManager.sceneCountInBuildSettings)
    {
      Debug.LogErrorFormat($"buildIndex ({buildIndex}) out of bounds");
      return null;
    }
    var scene = SceneManager.GetSceneByBuildIndex(buildIndex);
    return scene.name;
  }
}
