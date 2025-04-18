using System;
using System.Collections;
using UnityEngine;

namespace DebugMod
{
    public static partial class BindableFunctions
    {
        [BindableMethod(name = "Reload Radiance Fight", category = BindableCategory.Bosses)]
        public static void LoadRadiance()
        {
            GameManager.instance.StartCoroutine(LoadRadianceRoom());
        }
        private static IEnumerator LoadRadianceRoom()
        {
            //makes sure the initial platform and challage prompt appears
            HeroController.instance.gameObject.LocateMyFSM("ProxyFSM").FsmVariables.FindFsmBool("Faced Radiance").Value = false;

            HeroController.instance.RelinquishControl();
            HeroController.instance.StopAnimationControl();
            PlayMakerFSM.BroadcastEvent("START DREAM ENTRY");
            PlayMakerFSM.BroadcastEvent("DREAM ENTER");
            if (DebugMod.GM.IsGamePaused())
            {
                PlayerData.instance.disablePause = false;
                UIManager.instance.TogglePauseGame();
            }
            HeroController.instance.enterWithoutInput = true; // stop early control on scene load
            GameManager.instance.ChangeToScene("Dream_Final_Boss", "door1", 0f);
            yield return new WaitUntil(() => HeroController.instance.acceptingInput);
            yield return null;
            //cuz people mostly have full soul from thk fight
            HeroController.instance.AddMPCharge(198);
        }
        [BindableMethod(name = "Force Shade Fireball", category = BindableCategory.Bosses)]
        public static void ShadeFireball()
        {
            try
            {
                GameObject shade = GameObject.Find("Hollow Shade(Clone)");
                PlayMakerFSM fsm = shade.LocateMyFSM("Shade Control");
                //FsmState fsmState = fsm.FsmStates.First(t => t.Name == "Attack Choice"); i couldnt get this to work. the current solution works good enough.
                //SendRandomEvent sendRandom = fsmState.Actions.OfType<SendRandomEvent>().First();
                //sendRandom.weights[0] = 0f;
                //sendRandom.weights[1] = 1;
                fsm.SetState("Fireball Pos");
            }
            catch (Exception e)
            {
                Console.AddLine("Ignore below message if no shade is in scene");
                Console.AddLine(e.Message);
            }
        }
        [BindableMethod(name = "Force Uumuu extra attack", category = BindableCategory.Bosses)]
        public static void ForceUumuuExtra()
        {
            BossHandler.UumuuExtra();
        }

        [BindableMethod(name = "Respawn Ghost", category = BindableCategory.Bosses)]
        public static void RespawnGhost()
        {
            BossHandler.RespawnGhost();
        }

        [BindableMethod(name = "Respawn Boss", category = BindableCategory.Bosses)]
        public static void RespawnBoss()
        {
            BossHandler.RespawnBoss();
        }

        [BindableMethod(name = "Respawn Failed Champ", category = BindableCategory.Bosses)]
        public static void ToggleFailedChamp()
        {
            PlayerData.instance.falseKnightDreamDefeated = !PlayerData.instance.falseKnightDreamDefeated;

            Console.AddLine("Set Failed Champion killed: " + PlayerData.instance.falseKnightDreamDefeated);
        }

        [BindableMethod(name = "Respawn Soul Tyrant", category = BindableCategory.Bosses)]
        public static void ToggleSoulTyrant()
        {
            PlayerData.instance.mageLordDreamDefeated = !PlayerData.instance.mageLordDreamDefeated;

            Console.AddLine("Set Soul Tyrant killed: " + PlayerData.instance.mageLordDreamDefeated);
        }

        [BindableMethod(name = "Respawn Lost Kin", category = BindableCategory.Bosses)]
        public static void ToggleLostKin()
        {
            PlayerData.instance.infectedKnightDreamDefeated = !PlayerData.instance.infectedKnightDreamDefeated;

            Console.AddLine("Set Lost Kin killed: " + PlayerData.instance.infectedKnightDreamDefeated);
        }

        [BindableMethod(name = "Respawn NK Grimm", category = BindableCategory.Bosses)]
        public static void ToggleNKGrimm()
        {
            if (!DebugMod.GrimmTroupe())
            {
                Console.AddLine("Nightmare King Grimm does not exist on this patch");
                return;
            }

            if (PlayerData.instance.GetBoolInternal("killedNightmareGrimm") || PlayerData.instance.GetBoolInternal("destroyedNightmareLantern"))
            {
                PlayerData.instance.SetBoolInternal("troupeInTown", true);
                PlayerData.instance.SetBoolInternal("killedNightmareGrimm", false);
                PlayerData.instance.SetBoolInternal("destroyedNightmareLantern", false);
                PlayerData.instance.SetIntInternal("grimmChildLevel", 3);
                PlayerData.instance.SetIntInternal("flamesCollected", 3);
                PlayerData.instance.SetBoolInternal("grimmchildAwoken", false);
                PlayerData.instance.SetBoolInternal("metGrimm", true);
                PlayerData.instance.SetBoolInternal("foughtGrimm", true);
                PlayerData.instance.SetBoolInternal("killedGrimm", true);
            }
            else
            {
                PlayerData.instance.SetBoolInternal("troupeInTown", false);
                PlayerData.instance.SetBoolInternal("killedNightmareGrimm", true);
            }

            Console.AddLine("Set Nightmare King Grimm killed: " + PlayerData.instance.GetBoolInternal("killedNightmareGrimm"));
        }
    }
}
