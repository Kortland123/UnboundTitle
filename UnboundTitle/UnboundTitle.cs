
using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;

[assembly: MelonInfo(typeof(UnboundTitle.Main), "UnboundTitle", "1.0.0", "Kortland123")]

namespace UnboundTitle
{
    public class Main : MelonMod
    {
        private global::HarmonyLib.Harmony _harmony;

        public override void OnInitializeMelon()
        {
            _harmony = new global::HarmonyLib.Harmony("Kortland123.UnboundTitle");

            _harmony.Patch(
                typeof(SteamCustomLobbyBrowserUI).GetMethod("Awake"),
                postfix: new HarmonyMethod(typeof(LobbyTitlePatch).GetMethod(nameof(LobbyTitlePatch.AwakePostfix)))
            );

            _harmony.Patch(
                typeof(SteamCustomLobbyBrowserUI).GetMethod("OnEnable"),
                postfix: new HarmonyMethod(typeof(LobbyTitlePatch).GetMethod(nameof(LobbyTitlePatch.OnEnablePostfix)))
            );

            MelonLogger.Msg("[UnboundTitle] Initialized.");
        }
    }

    public static class LobbyTitlePatch
    {
        public static void AwakePostfix(SteamCustomLobbyBrowserUI __instance)
        {
            RemoveLimit(__instance);
        }

        public static void OnEnablePostfix(SteamCustomLobbyBrowserUI __instance)
        {
            RemoveLimit(__instance);
        }

        private static void RemoveLimit(SteamCustomLobbyBrowserUI instance)
        {
            if (instance == null || instance.nameInputField == null)
                return;

            instance.nameInputField.characterLimit = 0;
        }
    }
}
