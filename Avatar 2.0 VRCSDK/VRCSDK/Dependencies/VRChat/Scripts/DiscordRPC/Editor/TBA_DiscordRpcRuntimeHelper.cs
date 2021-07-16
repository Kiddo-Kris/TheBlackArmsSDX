using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TBA_SDK
{
    [InitializeOnLoadAttribute]
    public static class TBA_DiscordRpcRuntimeHelper
    {
        // register an event handler when the class is initialized
        static TBA_DiscordRpcRuntimeHelper()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
            EditorSceneManager.activeSceneChanged += sceneChanged;
        }

        private static void sceneChanged(Scene old, Scene next)
        {
            TBA_DiscordRPC.sceneChanged(next);
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            if(state == PlayModeStateChange.EnteredEditMode)
            {
                TBA_DiscordRPC.updateState(RpcState.EDITMODE);
                TBA_DiscordRPC.ResetTime();
            } else if(state == PlayModeStateChange.EnteredPlayMode)
            {
                TBA_DiscordRPC.updateState(RpcState.PLAYMODE);
                TBA_DiscordRPC.ResetTime();
            }
        }
    }
}