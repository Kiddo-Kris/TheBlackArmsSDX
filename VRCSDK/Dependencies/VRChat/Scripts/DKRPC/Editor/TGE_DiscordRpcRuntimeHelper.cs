using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TGE_SDK
{
    [InitializeOnLoadAttribute]
    public static class TGE_DiscordRpcRuntimeHelper
    {
        // register an event handler when the class is initialized
        static TGE_DiscordRpcRuntimeHelper()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
            EditorSceneManager.activeSceneChanged += sceneChanged;
        }

        private static void sceneChanged(Scene old, Scene next)
        {
            TGE_DiscordRPC.sceneChanged(next);
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            if(state == PlayModeStateChange.EnteredEditMode)
            {
                TGE_DiscordRPC.updateState(RpcState.EDITMODE);
                TGE_DiscordRPC.ResetTime();
            } else if(state == PlayModeStateChange.EnteredPlayMode)
            {
                TGE_DiscordRPC.updateState(RpcState.PLAYMODE);
                TGE_DiscordRPC.ResetTime();
            }
        }
    }
}