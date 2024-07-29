using BepInEx;
using System;
using System.IO;
using UnityEngine;
using Utilla;

namespace eimm_ze5os
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        Vector3 texPos = new Vector3(-66.3869f, 11.9003f, -82.4537f);
        Texture2D tex;
        GameObject obj;

        void Start()
        {
            Debug.Log("STARTING 'EIMM' MOD");
            Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/
            obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            MeshCollider collider = obj.GetComponent<MeshCollider>();
            if (collider != null)
                collider.enabled = false;
            obj.GetComponent<MeshRenderer>().material.mainTexture = tex;
            obj.transform.position = texPos;
            obj.transform.localScale = new Vector3(5, 1, 1);
            obj.name = "everyone ignored my message";
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
            Destroy(obj);
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
            Debug.Log("init!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            string appDir = Path.GetDirectoryName(Application.dataPath);
            string modDir = Path.Combine(appDir, "BepInEx", "plugins", "eimm-ze5os");
            string bundlePath = Path.Combine(modDir, "evryignmymsg");
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            if (bundle == null)
                throw new Exception("bundle not found or failed to load");
            tex = bundle.LoadAsset<Texture2D>("everyone ignored my message");
            bundle.Unload(false);
        }

        void Update()
        {
            /* Code here runs every frame when the mod is enabled */
        }
    }
}
