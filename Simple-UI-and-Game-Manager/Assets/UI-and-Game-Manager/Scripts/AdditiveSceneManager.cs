using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
  // 
  public string uiSceneName = "UI-and-Game-Manager";

  void Awake()
  {
    // This code could be in the !UNITY_EDITOR section below, but since
    // the Unity players don't have easy console access, it seems to make
    // sense to check here instead. If the UI scene is not at index 0, then
    // it won't be loaded by the player and there will be no UI.

    var indexZeroScene = GetSceneNameByIndex(0);
    if (indexZeroScene != uiSceneName)
    {
      Debug.LogErrorFormat(
        $"Scene at index 0 is {indexZeroScene}, expecting {uiSceneName}"
      );
      // XXX - throw exception?
    }

    #if !UNITY_EDITOR

      // Only need to load scenes manually in built game versions.
      // The editor handles loading for us. The assumption here is
      // that the UI-and-Game-Manager scene is at index 0, other
      // scenes are at higher indexes. This is kind of a hack... (XXX)

      var buildSceneCount = SceneManager.sceneCountInBuildSettings;
      if (buildSceneCount != 2)
      {
        // Would be nice to do this check in the editor, but it appears
        // to only run in a player.
        Debug.LogErrorFormat(
          $"There are {buildSceneCount} scenes in the build, the AdditiveSceneManager expects 2 (exactly)."
          // XXX - could loop through loading all scenes.
        );
      }
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
