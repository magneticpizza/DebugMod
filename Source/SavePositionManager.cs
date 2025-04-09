using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


namespace DebugMod
{
    public class SavePositionManager
    {
        private static List<GameObject> SavedGameObjects = [];
        private static List<GameObject> DuplicatedObjects = [];
        //public static List<Vector3>;
        //public static List<Vector3,>
        // too complicated public static PlayMakerFSM KnightFsm = DebugMod.RefKnight.LocateMyFSM("Knight-ProxyFSM") ;
        private static Vector3 KnightPos;
        private static Vector3 CamPos;
        private static Vector2 KnightVel;
        private static string PositionScene;
        private static bool InitializedPosition = false;
        // public static List
        public static void SaveState()
        {
            KnightPos = DebugMod.RefKnight.gameObject.transform.position;
            KnightVel = HeroController.instance.current_velocity;
            CamPos = DebugMod.RefCamera.gameObject.transform.position;
            PositionScene = DebugMod.GetSceneName();
            SavedGameObjects = GetAllEnemies();
            //TODO: check this . used to be FSMs = GetAllEnemies(CreatedFSMs);
            foreach (GameObject savedObject in SavedGameObjects) savedObject.SetActive(false);
            Console.AddLine("Positional save set in " + DebugMod.GetSceneName());
            InitializedPosition = true;
            LoadState();
        }
        public static void LoadState()
        {
            if (PositionScene == DebugMod.GetSceneName() && InitializedPosition) { 
                try
                {
                    RemoveAllCopies();
                    DuplicatedObjects = Create();
                    // Move knight to saved location, change velocity to saved velocity, Move Camera to saved camera position, 
                    DebugMod.RefKnight.gameObject.transform.position = KnightPos;
                    HeroController.instance.current_velocity = KnightVel;
                    DebugMod.RefCamera.gameObject.transform.position = CamPos;
                }
                catch (Exception)
                {
                    Console.AddLine("No positional save in " + DebugMod.GetSceneName()+", or a bug has occured.");
                }
            } else {
                Console.AddLine("No positional save in " + DebugMod.GetSceneName());
            }
        }

        private static List<GameObject> Create() 
        {
            List<GameObject> InstantiatedEnemies = [];
            List<GameObject> nonNulled = SavedGameObjects.FindAll(ed => ed.gameObject != null);
            for (int i = 0; i < nonNulled.Count; i++)
            {
                GameObject copying = nonNulled[i];
                GameObject gameObject = UnityEngine.Object.Instantiate(copying, copying.transform.position, copying.transform.rotation) as GameObject;
                gameObject.SetActive(true);
                InstantiatedEnemies.Add(gameObject);
            };
            return InstantiatedEnemies;
        }   
        private static void RemoveAllCopies()
        {
            //get all copies and remove them.
            foreach (GameObject obj in DuplicatedObjects)
            {
               if (!SavedGameObjects.Any(ed => ed == obj))
                    GameObject.Destroy(obj);
            };
        }
        private static List<GameObject> GetAllEnemies()
        {
            List<GameObject> ret = [];
            int layerMask =   
            (1 << 8) + //terrain
            (1 << 11) + //enemies
            //(1 << 17) + //attack
            (1 << 19); //interactive objects
            float boxSize = 250f;
            if (HeroController.instance != null && !HeroController.instance.cState.transitioning && DebugMod.GM.IsGameplayScene())
            {
                Collider2D[] array = Physics2D.OverlapBoxAll(DebugMod.RefKnight.transform.position, new Vector2(boxSize, boxSize), 1f, layerMask);
                if (array == null) return ret;
                for (int i = 0; i < array.Length; i++)
                {
                    ret.Add(array[i].gameObject);
                }
            }
            ret = ret.Distinct().ToList();
            return ret;
        }
    }
}
