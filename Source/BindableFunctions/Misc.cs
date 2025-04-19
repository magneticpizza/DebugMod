using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        private static readonly FieldInfo TimeSlowed = typeof(GameManager).GetField("timeSlowed", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo IgnoreUnpause = typeof(UIManager).GetField("ignoreUnpause", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        internal static readonly FieldInfo cameraGameplayScene = typeof(CameraController).GetField("isGameplayScene", BindingFlags.Instance | BindingFlags.NonPublic);
        private static bool corniferYeeteded = false;

        [BindableMethod(name = "Clear White Screen", category = BindableCategory.Misc)]
        public static void ClearWhiteScreen()
        {
            //fix white screen 
            string wakeControl = "Dream Return";
            GameObject knight = GameObject.Find("Knight");
            PlayMakerFSM wakeFSM = knight.LocateMyFSM(wakeControl);
            wakeFSM.SetState("GET UP");
            wakeFSM.SendEvent("FINISHED");
            GameObject.Find("Blanker White").LocateMyFSM("Blanker Control").SendEvent("FADE OUT");
            HeroController.instance.EnableRenderer();
        }

        [BindableMethod(name = "Reset Encounters", category = BindableCategory.Misc)]
        public static void ResetProxyFSMEncounters()
        {
            try
            {
                //literally couldnt figure out how to get this to not be awful to look at
                //this resets the fsm responsible for a couple weird persistent values with the knight
                GameObject knight = GameObject.Find("Knight");
                PlayMakerFSM proxyFSM = knight.LocateMyFSM("ProxyFSM");
                proxyFSM.FsmVariables.FindFsmBool("Faced Radiance").Value = false;
                proxyFSM.FsmVariables.FindFsmBool("Faced Nightmare").Value = false;
                proxyFSM.FsmVariables.FindFsmBool("Faced Zote").Value = false;
            }
            catch (Exception e)
            {
                Console.AddLine("Error while attempting to reset Proxy variables");
                DebugMod.instance.Log("Error while attempting to reset Knight-ProxyFSM variables: \n" + e);
            }
        }

        [BindableMethod(name = "Force Pause", category = BindableCategory.Misc)]
        public static void ForcePause()
        {
            try
            {
                if ((PlayerData.instance.disablePause || (bool)TimeSlowed.GetValue(GameManager.instance) || (bool)IgnoreUnpause.GetValue(UIManager.instance)) && DebugMod.GetSceneName() != "Menu_Title" && DebugMod.GM.IsGameplayScene())
                {
                    TimeSlowed.SetValue(GameManager.instance, false);
                    IgnoreUnpause.SetValue(UIManager.instance, false);
                    PlayerData.instance.disablePause = false;
                    UIManager.instance.TogglePauseGame();
                    Console.AddLine("Forcing Pause Menu because pause is disabled");
                }
                else
                {
                    Console.AddLine("Game does not report that Pause is disabled, requesting it normally.");
                    UIManager.instance.TogglePauseGame();
                }
            }
            catch (Exception e)
            {
                Console.AddLine("Error while attempting to pause, check ModLog.txt");
                DebugMod.instance.Log("Error while attempting force pause:\n" + e);
            }
        }

        [BindableMethod(name = "Hazard Respawn", category = BindableCategory.Misc)]
        public static void Respawn()
        {
            if (GameManager.instance.IsGameplayScene() && !HeroController.instance.cState.dead && PlayerData.instance.health > 0)
            {
                if (UIManager.instance.uiState.ToString() == "PAUSED")
                {
                    UIManager.instance.TogglePauseGame();
                    GameManager.instance.HazardRespawn();
                    Console.AddLine("Closing Pause Menu and respawning...");
                    return;
                }
                if (UIManager.instance.uiState.ToString() == "PLAYING")
                {
                    HeroController.instance.RelinquishControl();
                    GameManager.instance.HazardRespawn();
                    HeroController.instance.RegainControl();
                    Console.AddLine("Respawn signal sent");
                    return;
                }
                Console.AddLine("Respawn requested in some weird conditions, abort, ABORT");
            }
        }

        [BindableMethod(name = "Set Respawn", category = BindableCategory.Misc)]
        public static void SetHazardRespawn()
        {
            Vector3 manualRespawn = DebugMod.RefKnight.transform.position;
            HeroController.instance.SetHazardRespawn(manualRespawn, false);
            Console.AddLine("Manual respawn point on this map set to" + manualRespawn.ToString());
        }

        [BindableMethod(name = "Force Camera Follow", category = BindableCategory.Misc)]
        public static void ForceCameraFollow()
        {
            if (!DebugMod.cameraFollow)
            {
                Console.AddLine("Forcing camera follow");
                DebugMod.cameraFollow = true;
            }
            else
            {
                DebugMod.cameraFollow = false;
                BindableFunctions.cameraGameplayScene.SetValue(DebugMod.RefCamera, true);
                Console.AddLine("Returning camera to normal settings");
            }
        }

        [BindableMethod(name = "Toggle Infected Crossroads", category = BindableCategory.Misc)]
        public static void ToggleInfection()
        {
            PlayerData.instance.crossroadsInfected = !PlayerData.instance.crossroadsInfected;
            Console.AddLine($"Crossroads are " + (PlayerData.instance.crossroadsInfected ? "now" : "no longer") + " infected");
        }

        [BindableMethod(name = "Recover Shade", category = BindableCategory.Misc)]
        public static void RecoverShade()
        {
            PlayerData.instance.EndSoulLimiter();
            if (PlayerData.instance.geoPool > 0)
            {
                HeroController.instance.AddGeo(PlayerData.instance.geoPool);
                PlayerData.instance.geoPool = 0;
            }

            PlayerData.instance.shadeScene = "None";
            foreach (PlayMakerFSM fsm in GameCameras.instance.hudCanvas.transform.Find("Soul Orb")
                .GetComponentsInChildren<PlayMakerFSM>())
            {
                fsm.SendEvent("SOUL LIMITER DOWN");
            }

            PlayMakerFSM.BroadcastEvent("HOLLOW SHADE KILLED");
        }

        private static void CorniferYeeted(Scene current, Scene next) => CorniferYeeted();

        private static void CorniferYeeted()
        {
            (from x in UnityEngine.Object.FindObjectsOfType<GameObject>()
             where x.name.Contains("Cornifer")
             select x).ToList<GameObject>().ForEach(delegate (GameObject x)
             {
                 UnityEngine.Object.Destroy(x);
             });
        }

        [BindableMethod(name = "Yeet Cornifer-Toggle", category = BindableCategory.Misc)]
        public static void CorniferYeet()
        {
            corniferYeeteded = !corniferYeeteded;

            if (corniferYeeteded)
            {
                CorniferYeeted();
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged += CorniferYeeted;
                Console.AddLine("Cornifer yeeted on next loads lol");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= CorniferYeeted;
                Console.AddLine("Cornifer unyeeted from next loads on???");
            }
        }
    }
}
