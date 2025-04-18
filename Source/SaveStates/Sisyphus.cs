using GlobalEnums;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using UnityEngine;

namespace DebugMod
{
    internal static class Sisyphus
    {
        // TODO make it work with doors stags and other kinds of transitions
        static bool SetState = false;
        static bool faceRight;
        private static int geo;
        private static int health;
        private static int healthBlue;
        private static bool isSaved = false;
        static TransPoint savedTrans;
        struct TransPoint
        {
            //public bool isADoor;
            //public bool dontWalkOutOfDoor;
            //public float entryDelay;
            //public bool alwaysEnterRight;
            //public bool alwaysEnterLeft;
            //public bool hardLandOnExit;
            public string targetScene;
            public string entryPoint;
            //public Vector2 entryOffset;
            //public bool nonHazardGate;
            //public string name;
            public TransPoint(TransitionPoint trans)
            {
                targetScene = trans.targetScene;
                entryPoint = trans.entryPoint;
            }

            public TransPoint(PlayMakerFSM door)
            {
                entryPoint = door.FsmVariables.GetFsmString("Entry Gate").Value;
                targetScene = door.FsmVariables.GetFsmString("New Scene").Value;
            }

            public void SetTrans(TransitionPoint trans)
            {
                trans.targetScene = targetScene;
                trans.entryPoint = entryPoint;
            }
            public void SetDoor(PlayMakerFSM trans)
            {
                trans.FsmVariables.GetFsmString("Entry Gate").Value = entryPoint;
                trans.FsmVariables.GetFsmString("New Scene").Value = targetScene;
            }
        }
        public static void Toggle()
        {
            if (isSaved) isSaved = false;
            else
            {
                SetState = !SetState;
            }
            if (SetState) Console.AddLine("Sisyphus enabled, enter a transition to start ");
            else Console.AddLine("Sisyphus disabled");
        }
        private static bool IsDoor(TransitionPoint trans) => trans.isADoor || trans.name.Contains("door");
        private static void TransitionEntered(Action<TransitionPoint, Collider2D> orig, TransitionPoint self, Collider2D movingObj)
        {
            if (!string.IsNullOrEmpty(self.targetScene) && !string.IsNullOrEmpty(self.entryPoint))
            {
                if (movingObj.gameObject.layer == 9 && GameManager.instance.gameState == GameState.PLAYING)
                {
                    if (SetState)
                    {
                        Console.AddLine("Sisyphus save started");
                        isSaved = true;
                        SetState = false;
                        savedTrans = new(self);
                        health = PlayerData.instance.health;
                        geo = PlayerData.instance.geo;
                        healthBlue = PlayerData.instance.healthBlue;
                        faceRight = HeroController.instance.cState.facingRight;
                    }
                    else if (isSaved)
                    {
                        if (!IsDoor(self))
                        {
                            if (faceRight) HeroController.instance.FaceRight();
                            else HeroController.instance.FaceLeft();
                            savedTrans.SetTrans(self);
                            HUDFixes();
                        }
                        else
                        {
                            savedTrans.SetDoor(self.gameObject.LocateMyFSM("Door Control"));
                        }
                    }
                }
            }
            orig(self, movingObj);
        }
        public static void Init()
        {
            //activatedFieldInfo = typeof(TransitionPoint).GetField("activated", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo OnTransEnter = typeof(TransitionPoint).GetMethod("OnTriggerEnter2D", BindingFlags.NonPublic | BindingFlags.Instance);
            if (OnTransEnter != null) new Hook(OnTransEnter, TransitionEntered);
        }
        //copied from SaveState.cs
        private static void HUDFixes()
        {
            GameCameras.instance.hudCanvas.gameObject.SetActive(true);
            HeroController.instance.playerData.geo = geo;
            HeroController.instance.geoCounter.geoTextMesh.text = geo.ToString();
            //PlayerData.instance.hasXunFlower = false; // prevent breaking flower
            PlayerData.instance.health = health;
            PlayerData.instance.healthBlue = healthBlue;
            HeroController.instance.proxyFSM.SendEvent("HeroCtrl-Healed");
            HeroController.instance.proxyFSM.SendEvent("HeroCtrl-HeroDamaged");
            //PlayerData.instance.hasXunFlower = data.savedPd.hasXunFlower;

            //should fix hp
            //the "Idle" mesh never gets disabled when Charm Indicator runs
            //running the animation quickly would work but this does too and i understand it
            int maxHP = GameManager.instance.playerData.maxHealth;
            for (int i = maxHP; i > 0; i--)
            {
                GameObject health = GameObject.Find("Health " + i.ToString());
                PlayMakerFSM fsm = health.LocateMyFSM("health_display");
                Transform idle = health.transform.Find("Idle");
                //This is the "HP Full" mesh, which covers the empty mesh
                idle.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //This is the "HP Empty" mesh
                health.GetComponent<MeshRenderer>().enabled = true;
                //This turns back on the "HP Full" mesh if we should
                fsm.SetState("Check if Full");
            }

            if (PlayerData.instance.MPReserveMax < 33) GameObject.Find("Vessel 1").LocateMyFSM("vessel_orb").SetState("Init");
            else GameObject.Find("Vessel 1").LocateMyFSM("vessel_orb").SetState("Up Check");
            if (PlayerData.instance.MPReserveMax < 66) GameObject.Find("Vessel 2").LocateMyFSM("vessel_orb").SetState("Init");
            else GameObject.Find("Vessel 2").LocateMyFSM("vessel_orb").SetState("Up Check");
            if (PlayerData.instance.MPReserveMax < 99) GameObject.Find("Vessel 3").LocateMyFSM("vessel_orb").SetState("Init");
            else GameObject.Find("Vessel 3").LocateMyFSM("vessel_orb").SetState("Up Check");
            if (PlayerData.instance.MPReserveMax < 132) GameObject.Find("Vessel 4").LocateMyFSM("vessel_orb").SetState("Init");
            else GameObject.Find("Vessel 4").LocateMyFSM("vessel_orb").SetState("Up Check");

            HeroController.instance.TakeMP(1);
            HeroController.instance.AddMPChargeSpa(1);

            PlayMakerFSM.BroadcastEvent("MP DRAIN");
            PlayMakerFSM.BroadcastEvent("MP LOSE");
            PlayMakerFSM.BroadcastEvent("MP RESERVE DOWN");
        }
    }
}