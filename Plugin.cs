using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
namespace eimm_ze5os
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool initialized = false;
        bool wasEnabled = false;
        Vector3 objPos = new Vector3(-69.1193f, 12.6312f, -81.7537f);
        Vector3 objRot = new Vector3(0f, 289.8361f, 0f);
        Vector3 objScale = new Vector3(1.5127f, 0.3236f, 1f);
        GameObject obj;
        GameObject copy;
        AssetBundle bundle;
        void Start()
        {
            Events.GameInitialized += OnGameInitialized;
        }
        void OnEnable()
        {
            if (initialized)
                initObj();
            wasEnabled = true;
            HarmonyPatches.ApplyHarmonyPatches();
        }
        void initObj()
        {
            copy = Instantiate(obj, objPos, Quaternion.Euler(objRot));
            copy.transform.localScale = objScale;
            copy.name = "everyone ignored my message";
        }
        void OnDisable()
        {
            if (initialized)
                Destroy(copy);
            wasEnabled = false;
            HarmonyPatches.RemoveHarmonyPatches();
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            Assembly executing = Assembly.GetExecutingAssembly();
            Stream stream = executing.GetManifestResourceStream("eimm-ze5os.AssetBundles.evryignmymsg");
            bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            obj = bundle.LoadAsset<GameObject>("thingus");
            if (wasEnabled)
                initObj();
        }
    }
}
