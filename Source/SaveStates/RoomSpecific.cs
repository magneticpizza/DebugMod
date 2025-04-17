using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace DebugMod
{
    public static class RoomSpecific
    {
        //This class is intended to recreate some scenarios, with more accuracy than that of the savestate class. 
        #region Rooms
        
        private static readonly float MAX_TIMESCALE_LAGGY = 10;
        private static readonly float MAX_TIMESCALE = 20;
                private static List<Coroutine> coroRefs =[];
        private static void StartCoro(IEnumerator routine)
        {
            coroRefs.Add(DebugMod.GM.StartCoroutine(routine));
        }
        public static void StopCoros() { 
            coroRefs.ForEach(DebugMod.GM.StopCoroutine);
            coroRefs = [];
        }
        private static IEnumerator SpiderTownHelper(int index)
        {
            float beforeFirstSpider = 1.39f;//all from roomsob
            float activateLeftTime = 2.02f;
            float activateRightTime = 26.64f;
            float lastSlashTime = 27.73f;

            float delay1 = beforeFirstSpider;
            float delay2 = activateLeftTime - beforeFirstSpider;
            float delay3 = activateRightTime - activateLeftTime;
            float delay4 = lastSlashTime - activateRightTime;

            float afterTimeScale = index; //janky but idc

            Vector2 roomStartPos = new(8.156f, 58.5f);
            Vector2 activateLeftPos = new(23f, 58.5f);
            Vector2 activateRightPos = new(44f, 58.5f);
            Vector2 trappedPos = new(263.1f, 52.406f);

            string goName = "RestBench Spider";
            string websFsmName = "Fade";
            string benchFsmName = "Bench Control Spider";

            DebugMod.HC.transform.position = roomStartPos;

            PlayMakerFSM websFSM = FindFsmGlobally(goName, websFsmName);
            PlayMakerFSM benchFSM = FindFsmGlobally(goName, benchFsmName);

            if (afterTimeScale > MAX_TIMESCALE_LAGGY) afterTimeScale = MAX_TIMESCALE_LAGGY;
            if (index >= 1)
            {
                GameManager.instance.hero_ctrl.RelinquishControl();
                WaitForTime(lastSlashTime, afterTimeScale);

                GameManager.instance.isPaused = false;
                
                DebugMod.HC.transform.position = roomStartPos;
                yield return new WaitForSeconds(delay1);
                DebugMod.HC.transform.position = activateLeftPos;
                yield return new WaitForSeconds(delay2);
                DebugMod.HC.transform.position = activateRightPos;
                yield return new WaitForSeconds(delay3);
                DebugMod.HC.transform.position = trappedPos;
            }
            else DebugMod.HC.transform.position = trappedPos;

            benchFSM.SetState("Start Rest");
            benchFSM.SendEvent("WAIT");
            benchFSM.SendEvent("FINISHED");
            benchFSM.SendEvent("STRUGGLE");
            websFSM.SendEvent("FIRST STRUGGLE");
            websFSM.SendEvent("FINISHED");
            websFSM.SendEvent("FINISHED");
            websFSM.SendEvent("FINISHED");
            websFSM.SendEvent("LAND");
            websFSM.SendEvent("FINISHED");
            websFSM.SendEvent("FINISHED"); //now can wiggle n stuff
            websFSM.FsmVariables.GetFsmInt("Struggles").Value = 4;
            if (index >= 1)
            {
                yield return new WaitForSeconds(delay4);
                websFSM.SetState("Struggle");
                Time.timeScale = 1f;
            }
            //auto break webs to normalize
        }
        private static void EnterSpiderTownTrap(int index) //Deepnest_Spider_Town
        {
            StartCoro(SpiderTownHelper(index));
        }
        private static void DoTHK(int index) //Room_Final_Boss
        {
            if (index == 2) RadianceEntrySequence();

            else StartCoro(BreakTHKChainscoro(index));
        }
        private static void RadianceEntrySequence()
        {
            PlayMakerFSM startFSM = FindFsmGlobally("Boss Control", "Battle Start");

            startFSM.SetState("Init");
            startFSM.SendEvent("Revisit");
            startFSM.SetState("Fight Start");

            string thkName = "Hollow Knight Boss";

            PlayMakerFSM phaseFSM = FindFsmGlobally(thkName, "Phase Control");
            phaseFSM.SetState("Set Phase 4");
            phaseFSM.SendEvent("HORNET READY");

            PlayMakerFSM controlFSM = FindFsmGlobally(thkName, "Control");

            // Init
            controlFSM.SendEvent("FINISHED");
            // Idle
            controlFSM.SendEvent("MOVE");
            // Roar Antic
            controlFSM.SendEvent("FINISHED");
            // Long Roar
            controlFSM.SendEvent("FINISHED");
            // H Stab Antic
            controlFSM.SendEvent("FINISHED");
            // H Scene 1
            controlFSM.SendEvent("FINISHED");
            //H Thread
            controlFSM.SendEvent("FINISHED");

            // Get rid of scream effect
            GameObject.Destroy(GameObject.Find("Roar Wave Emitter(Clone)"));

            // Make it so the Radiance encounter is not a Refight
            HeroController.instance.proxyFSM.FsmVariables.FindFsmBool("Faced Radiance").Value = false;
        }
        private static IEnumerator BreakTHKChainscoro(int index)
        {
            float time = 14.2f;
            float scale = index;
            if (index < 1) { scale = 1; }
            if (index > MAX_TIMESCALE) scale = MAX_TIMESCALE;
            string fsmName = "Control";
            string goName1 = "hollow_knight_chain_base";
            string goName2 = "hollow_knight_chain_base 2";
            string goName3 = "hollow_knight_chain_base 3";
            string goName4 = "hollow_knight_chain_base 4";
            PlayMakerFSM fsm1 = FindFsmGlobally(goName1, fsmName);
            PlayMakerFSM fsm2 = FindFsmGlobally(goName2, fsmName);
            PlayMakerFSM fsm3 = FindFsmGlobally(goName3, fsmName);
            PlayMakerFSM fsm4 = FindFsmGlobally(goName4, fsmName);
            fsm1.SetState("Break");
            fsm2.SetState("Break");
            fsm3.SetState("Break");
            fsm4.SetState("Break");
            bool right = HeroController.instance.cState.facingRight;
            Vector2 current = DebugMod.HC.transform.position;
            DebugMod.HC.transform.position = new Vector2(27.4200f, 6.410425f);
            WaitForTime(1, scale);
            yield return new WaitForSeconds(1);
            DebugMod.HC.transform.position = current;
            if (right)
            {
                HeroController.instance.FaceRight();
            }
            else
            {
                HeroController.instance.FaceLeft();
            }
            WaitForTime(time-1, scale);
            yield return new WaitForSeconds(time-1);
            Time.timeScale = 1f;
        } //Room_Final_Boss
        private static void ObtainDreamNail(int index)
        {
            string goName = "Witch Control";
            string fsmName = "Control";
            PlayMakerFSM fsm = FindFsmGlobally(goName, fsmName);
            fsm.SetState("Pause");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("DREAM WAKE");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("ZONE 1");
            fsm.SendEvent("ZONE 2");
            fsm.SendEvent("ZONE 3");
            fsm.SendEvent("FINISHED");
            DebugMod.HC.transform.position = new Vector2(263.1f, 52.406f);
        }
        private static void WaitForTime(float seconds, float scale)
        {
            StartCoro(WaitForTimeCoro(seconds,scale));
        }
        private static IEnumerator WaitForTimeCoro(float seconds, float scale)
        {
            float t = seconds + Time.time;
            Time.timeScale = scale;
            while (t > Time.time)
            {
                yield return new WaitUntil(() => t > Time.time | (Time.timeScale != scale));
                Time.timeScale = scale;
            }
            Time.timeScale = 1;
        }
        private static void FastSoulMaster(int index)
        {
            string goName = "Mage Lord"; //soul master gameobject
            string fsmName = "Mage Lord";//soul master fsm
            if (index == 1)
            {
                //start phase 1
                DebugMod.HC.transform.position = new Vector2(19.5810f, 29.41113f); //make sure youre at the right spot ig?
                PlayMakerFSM fsm = FindFsmGlobally(goName, fsmName);
                fsm.SetState("Init");
            }
            else if(index == 2)
            {
                //start phase 2
                DebugMod.HC.transform.position = new Vector2(19.5810f, 29.41113f); //make sure youre at the right spot ig?
                string quakegoname= "Quake Fake Parent";
                string quakefsmname = "Appear";
                PlayMakerFSM fsm = FindFsmGlobally(goName, fsmName);
                PlayMakerFSM quakeFakeFSM = FindFsmGlobally(quakegoname, quakefsmname);
                fsm.SetState("Init"); //to close gate and avoid save shenanigans
                GameObject.Destroy(GameObject.Find("Mage Lord"));
                quakeFakeFSM.SendEvent("QUAKE FAKE APPEAR");
            }
        }
        private static void FastDreamerCutscene(int index)
        {
            if (index == 1)
            {
                string goName ="Mask Break Cutscene";
                string fsmName = "Control";
                PlayMakerFSM CutsceneFSM = FindFsmGlobally(goName, fsmName);
                CutsceneFSM.SetState("Fade");
            }
        }

        private static void StartWatcherPair(int index)
        {
            PlayMakerFSM battleFSM = FindFsmGlobally("Battle Control", "Control");
            FsmInt battleEnemies = battleFSM.FsmVariables.FindFsmInt("Battle Enemies");

            // Idle
            battleFSM.SendEvent("ENTER");
            // Close
            battleFSM.SendEvent("FINISHED");

            if (index == 1)
            {
                SkipFirstKnight(battleFSM, battleEnemies);
                battleFSM.SetState("Knight 2");
            }

            if (index == 2)
            {
                SkipFirstKnight(battleFSM, battleEnemies);
                string wk2Name = "Black Knight 2";
                string wk3Name = "Black Knight 4";

                GameObject.Destroy(GameObject.Find(wk2Name));
                GameObject.Destroy(GameObject.Find(wk3Name));
                // need to decrement an extra one for the knight killed by the chandelier
                battleEnemies.Value -= 3;
                battleFSM.SetState("Knight 5");
            }

            // secret!
            if(index == 69)
            {
                // Start Notify
                battleFSM.SendEvent("FINISHED");
                // Knight 1
                battleFSM.SendEvent("NEXT");
                // Pause 6
                battleFSM.SendEvent("FINISHED");
                // Knight 2
                battleFSM.SendEvent("NEXT");
                // Pause 1
                battleFSM.SendEvent("FINISHED");
                // Knight 3
                battleFSM.SendEvent(PlayerData.instance.watcherChandelier ? "SKIP" : "NEXT");
                // Pause 2 / Skip
                battleFSM.SendEvent("FINISHED");
                // Knight 4
                battleFSM.SendEvent("NEXT");
                // Pause 3
                battleFSM.SendEvent("FINISHED");
                // Knight 5
                battleFSM.SendEvent("NEXT");
                // Pause 4
                battleFSM.SendEvent("FINISHED");
            }

            else
            {
                Console.AddLine("Watcher Knight RoomSpecific value " + index + " is invalid!");
            }
        }
        private static void SkipFirstKnight(PlayMakerFSM battleFSM, FsmInt battleEnemies)
        {
            // Destroy first Knight Object
            string wk1Name = "Black Knight 1";
            GameObject.Destroy(GameObject.Find(wk1Name));
            // Decrement Battle Enemies so the next knights spawn
            battleEnemies.Value--;
            if (!PlayerData.instance.watcherChandelier)
            {
                battleFSM.SetState("Knight 3");
                battleFSM.SendEvent("SKIP");
            }
        }
        private static void DoUumuu(int index)
        {
            float xMin, xMax, yMin, yMax, randOffset;

            // Set min/max Uumuu positions generally seen by different lures
            switch (index)
            {
                case 1: // Any%
                    xMin = 64.1f;
                    xMax = 65.5f;
                    yMin = 104.3f;
                    yMax = 108.4f;
                    break;
                case 2: // TE
                case 3: // All Skills
                    xMin = 66.5f;
                    xMax = 67.5f;
                    yMin = 108.9f;
                    yMax = 113.2f;
                    break;
                default:
                    Console.AddLine("Uumuu RoomSpecific value " + index + " is invalid!");
                    return;
            }

            var rand = new System.Random();

            // Set the random offset used in Quirrel Timer and Uumuu position
            do
            {
                randOffset = (float)rand.NextDouble() * 0.5f + (float)rand.NextDouble() * 0.5f + (float)rand.NextDouble() * 0.5f;
            } while (BossHandler.forceUumuuExtra && randOffset > 0.5f);

            // Give Dash Slash storage if using All Skills Roomspecific
            if (index == 3)
            {
                FindFsmGlobally("Knight", "Nail Arts").SetState("Dash Slash Ready");
            }

            // Start battle
            FindFsmGlobally("Battle Scene", "Control").SendEvent("ENTER");

            GameObject umuGo = GameObject.Find("Mega Jellyfish");
            PlayMakerFSM umuFSM = umuGo.LocateMyFSM("Mega Jellyfish");

            // Set Uumuu's position randomly based on the min/max values and our randOffset (only the y value uses randoffset)
            umuGo.transform.position = new Vector3(xMin + (float)rand.NextDouble() * (xMax - xMin), yMin + (yMax-yMin)/1.5f * (1.5f - randOffset));

            // Advance through Uumuu's FSM
            umuFSM.SendEvent("BATTLE START");
            // Wake Pause
            umuFSM.SendEvent("FINISHED");
            // Wake Rumble
            umuFSM.SendEvent("FINISHED");
            // Burst
            umuFSM.SendEvent("FINISHED");
            // Start

            // Add randomly generated time to Quirrel Timer
            umuFSM.FsmVariables.GetFsmFloat("Quirrel Timer").Value = 4.5f + randOffset;

            // Set Uumuu's FSM to the appropriate state
            umuFSM.SetState("Attack Recover");
        }
        // Abyss_12
        private static void FastAbyssShriek(int index)
        {
            StartCoro(FastAbyssShriekCoro(index));
        }
        private static IEnumerator FastAbyssShriekCoro(int index)
        {
            WaitForTime(14f,20);
            HeroController.instance.transform.position = new(427.1f, 14f);
            yield return new WaitForFixedUpdate();
            HeroController.instance.transform.position = new(47.1f, 14f);
            yield return new WaitForFixedUpdate();
            HeroController.instance.gameObject.LocateMyFSM("Spell Control").SetState("Has Scream?");

            yield break;
        }

        // Abyss_19
        private static void FastBrokenVessel(int index)
        {
            PlayMakerFSM bvFSM = FindFsmGlobally("Infected Knight", "IK Control");

            bvFSM.SetState("Sleep");
            FindFsmGlobally("Battle Start", "Battle Start").SendEvent("HIT");

            // Start Pause
            bvFSM.SendEvent("FINISHED");
            Console.AddLine(bvFSM.ActiveStateName);
            // Rumble Start
            bvFSM.SendEvent("FINISHED");
            Console.AddLine(bvFSM.ActiveStateName);
            // Spawning 1
            bvFSM.SendEvent("FINISHED");
            Console.AddLine(bvFSM.ActiveStateName);
            // Spawning 2
            bvFSM.SendEvent("FINISHED");
            Console.AddLine(bvFSM.ActiveStateName);
            // Spawning 3
            // Spawning 4
            // Stop Spawning
            // Check Final
        }

        #endregion

        public static void DoRoomSpecific(string scene, int index)
        {
            switch (scene)
            {
                case "Deepnest_Spider_Town":
                    EnterSpiderTownTrap(index);
                    break;
                case "Room_Final_Boss_Core":
                    DoTHK(index);
                    break;
                case "Dream_NailCollection":
                    ObtainDreamNail(index);
                    break;
                case "Ruins1_24":
                    FastSoulMaster(index);
                    break;
                case "Cutscene_Boss_Door":
                    FastDreamerCutscene(index); 
                    break;
                case "Ruins2_03":
                    StartWatcherPair(index);
                    break;
                case "Fungus3_archive_02":
                    DoUumuu(index);
                    break;
                case "Abyss_12":
                    FastAbyssShriek(index);
                    break;
                case "Abyss_19":
                    FastBrokenVessel(index);
                    break;
                default:
                    Console.AddLine("No Room Specific Function Found In: " + scene);
                    break;
            }
        }

        private static PlayMakerFSM FindFsmGlobally(string gameObjectName, string fsmName)
        {
            return GameObject.Find(gameObjectName).LocateMyFSM(fsmName);
        }
    }
}
