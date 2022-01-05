using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace theblackarmsSDX
{
    [InitializeOnLoad]
    public static class theblackarmsSDX_DiscordRpcRuntimeHelper
    {
        // register an event handler when the class is initialized
        static theblackarmsSDX_DiscordRpcRuntimeHelper()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
            EditorSceneManager.activeSceneChanged += sceneChanged;
        }

        private static void sceneChanged(Scene old, Scene next)
        {
            theblackarmsSDX_DiscordRPC.sceneChanged(next);
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            if(state == PlayModeStateChange.EnteredEditMode)
            {
                theblackarmsSDX_DiscordRPC.updateState(RpcState.EDITMODE);
                theblackarmsSDX_DiscordRPC.ResetTime();
            } else if(state == PlayModeStateChange.EnteredPlayMode)
            {
                theblackarmsSDX_DiscordRPC.updateState(RpcState.PLAYMODE);
                theblackarmsSDX_DiscordRPC.ResetTime();
            }
        }
    }
}