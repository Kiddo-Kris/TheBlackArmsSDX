using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace TBA_SDK
{
    [InitializeOnLoad]
    public class TBA_DiscordRPC
    {
        private static readonly DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();

        private static TimeSpan time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        private static long timestamp = (long)time.TotalSeconds;

        private static RpcState rpcState = RpcState.EDITMODE;
        private static string GameName = Application.productName;
        private static string SceneName = SceneManager.GetActiveScene().name;

        static TBA_DiscordRPC()
        {
            if(!EditorPrefs.HasKey("TBA_discordRPC"))
            {
                EditorPrefs.SetBool("TBA_discordRPC", true);
            }

            if (EditorPrefs.GetBool("TBA_discordRPC"))
            {
                BRSLog("Starting discord rpc");
                DiscordRpc.EventHandlers eventHandlers = default(DiscordRpc.EventHandlers);
                DiscordRpc.Initialize("741036203880742992", ref eventHandlers, false, string.Empty);
                updateDRPC();
            }
        }

        public static void updateDRPC()
        {
            BRSLog("Updating everything");
            SceneName = SceneManager.GetActiveScene().name;
            presence.details = string.Format("Project: {0} Scene: {1}", GameName, SceneName);
            presence.state = "State: " + rpcState.StateName();
            presence.startTimestamp = timestamp;
            presence.largeImageKey = "sdx";
            presence.largeImageText = "Forget The Limits";
            presence.smallImageText = "SDX by PhoenixAceVFX";
            presence.smallImageKey = "ace";
            DiscordRpc.UpdatePresence(presence);
        }

        public static void updateState(RpcState state)
        {
            BRSLog("Updating state to '" + state.StateName() + "'");
            rpcState = state;
            presence.state = "State: " + state.StateName();
            DiscordRpc.UpdatePresence(presence);
        }

        public static void sceneChanged(Scene newScene)
        {
            BRSLog("Updating scene name");
            SceneName = newScene.name;
            presence.details = string.Format("Project: {0} Scene: {1}", GameName, SceneName);
            DiscordRpc.UpdatePresence(presence);
        }

        public static void ResetTime()
        {
            BRSLog("Reseting timer");
            time = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            timestamp = (long)time.TotalSeconds;
            presence.startTimestamp = timestamp;

            DiscordRpc.UpdatePresence(presence);
        }

        private static void BRSLog(string message)
        {
            Debug.Log("[BlackArms SDK] DiscordRPC: " + message);
        }
    }
}