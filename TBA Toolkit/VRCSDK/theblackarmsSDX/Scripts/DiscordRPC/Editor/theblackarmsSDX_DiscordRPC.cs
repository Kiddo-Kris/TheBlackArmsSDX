using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace theblackarmsSDX
{
    [InitializeOnLoad]
    public class theblackarmsSDX_DiscordRPC
    {
        private static readonly DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();

        private static TimeSpan time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        private static long timestamp = (long)time.TotalSeconds;

        private static RpcState rpcState = RpcState.EDITMODE;
        private static string GameName = Application.productName;
        private static string SceneName = SceneManager.GetActiveScene().name;

        static theblackarmsSDX_DiscordRPC()
        {
            if (EditorPrefs.GetBool("theblackarmsSDX_discordRPC", true))
            {
                chill_zoneLog("Starting discord rpc");
                DiscordRpc.EventHandlers eventHandlers = default(DiscordRpc.EventHandlers);
                DiscordRpc.Initialize("928328701911916594", ref eventHandlers, false, string.Empty);
                UpdateDRPC();
            }
        }

        public static void UpdateDRPC()
        {
            chill_zoneLog("Updating everything");
            SceneName = SceneManager.GetActiveScene().name;
            presence.details = string.Format("Project: {0} Scene: {1}", GameName, SceneName);
            presence.state = "State: " + rpcState.StateName();
            presence.startTimestamp = timestamp;
            presence.largeImageKey = "tba";
            presence.largeImageText = "Developing with The Black Arms DevKit";
            presence.smallImageKey = "phoenix";
            presence.smallImageText = "SDK VER. 7.9.6 (udon)";
            DiscordRpc.UpdatePresence(presence);
        }

        public static void updateState(RpcState state)
        {
            chill_zoneLog("Updating state to '" + state.StateName() + "'");
            rpcState = state;
            presence.state = "State: " + state.StateName();
            DiscordRpc.UpdatePresence(presence);
        }

        public static void sceneChanged(Scene newScene)
        {
            chill_zoneLog("Updating scene name");
            SceneName = newScene.name;
            presence.details = string.Format("Project: {0} Scene: {1}", GameName, SceneName);
            DiscordRpc.UpdatePresence(presence);
        }

        public static void ResetTime()
        {
            chill_zoneLog("Reseting timer");
            time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            timestamp = (long)time.TotalSeconds;
            presence.startTimestamp = timestamp;

            DiscordRpc.UpdatePresence(presence);
        }

        private static void chill_zoneLog(string message)
        {
            Debug.Log("[theblackarmsSDX] DiscordRPC: " + message);
        }
    }
}