using System.IO;
using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "SceneData to file", category = BindableCategory.ExportData)]
        public static void SceneDataToFile()
        {
            File.WriteAllText(string.Concat(
                new object[] { Application.persistentDataPath, "/SceneData.json" }),
                JsonUtility.ToJson(
                    SceneData.instance,
                    prettyPrint: true
                )
            );
        }

        [BindableMethod(name = "PlayerData to file", category = BindableCategory.ExportData)]
        public static void PlayerDataToFile()
        {
            File.WriteAllText(string.Concat(
                new object[] { Application.persistentDataPath, "/PlayerData.json" }),
                JsonUtility.ToJson(
                    PlayerData.instance,
                    prettyPrint: true
                )
            );
        }

        /*
        [BindableMethod(name = "Scene FSMs to file", category = BindableCategory.ExportData)]
        public static void FSMsToFile()
        {
            foreach (var fsm in GameManager.instance.GetComponents<VariableType>())
            {
                DebugMod.instance.Log(fsm);
            }

        }
        */

        // Use some threading or coroutine lol
        // commented out for being painfully unoptimised.
        // if you have use for this in its current state, you know how to uncomment and compile, or to ask me for the list :p
        /*
        [BindableMethod(name = "AllScenesByIndex (SLOW)", category = BindableCategory.ExportData)]
        public static void SceneIndexesToFile()
        {
            Console.AddLine("sceneCountInBuildSettings: " + UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
            Console.AddLine("sceneCount: " + UnityEngine.SceneManagement.SceneManager.sceneCount);
            
            Dictionary<int, string> sceneIndexList = new Dictionary<int, string>();
            Scene tmp;
            int curr = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
            {
                if (curr != i) UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(i);
                tmp = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                sceneIndexList.Add(tmp.buildIndex, tmp.name);
                //Console.AddLine(tmp.name + " == " + tmp.buildIndex);
                if (curr != i) UnityEngine.SceneManagement.SceneManager.UnloadScene(i);
            }
            
            File.WriteAllLines(string.Concat(new object[] {Application.persistentDataPath, "/SceneIndexList.txt"}),
                    sceneIndexList.Select(x => "[" + x.Key + " -- " + x.Value + "]").ToArray()
                    );
        }
        */
    }
}
