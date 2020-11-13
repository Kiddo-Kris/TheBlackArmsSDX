using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;
using UnityEditor.Animations;
using UnityEditor.Playables;
using UnityEngine.Playables;
using VRC.SDK3.Avatars.Components;

[InitializeOnLoadAttribute]
public static class SDKAv3EditorSupport
{
    static Dictionary<VRCAvatarDescriptor.AnimLayerType, string> animLayerToDefaultFile = new Dictionary<VRCAvatarDescriptor.AnimLayerType, string> {
        {VRCAvatarDescriptor.AnimLayerType.TPose, "vrc_AvatarV3UtilityTPose"},
        {VRCAvatarDescriptor.AnimLayerType.IKPose, "vrc_AvatarV3UtilityIKPose"},
        {VRCAvatarDescriptor.AnimLayerType.SpecialIK, "vrc_AvatarV3UtilityTPose"},
        {VRCAvatarDescriptor.AnimLayerType.Base, "vrc_AvatarV3LocomotionLayer"},
        {VRCAvatarDescriptor.AnimLayerType.Sitting, "vrc_AvatarV3SittingLayer"},
        {VRCAvatarDescriptor.AnimLayerType.Additive, "vrc_AvatarV3IdleLayer"},
        {VRCAvatarDescriptor.AnimLayerType.FX, "vrc_AvatarV3FaceLayer"},
        {VRCAvatarDescriptor.AnimLayerType.Action, "vrc_AvatarV3ActionLayer"},
        {VRCAvatarDescriptor.AnimLayerType.Gesture, "vrc_AvatarV3HandsLayer"},
    };
    static Dictionary<VRCAvatarDescriptor.AnimLayerType, string> animLayerToDefaultAvaMaskFile = new Dictionary<VRCAvatarDescriptor.AnimLayerType, string>
    {
        {VRCAvatarDescriptor.AnimLayerType.TPose, "vrc_MusclesOnly"},
        {VRCAvatarDescriptor.AnimLayerType.IKPose, "vrc_MusclesOnly"},
        {VRCAvatarDescriptor.AnimLayerType.SpecialIK, "vrc_MusclesOnly"},
        {VRCAvatarDescriptor.AnimLayerType.Base, null},//"SDKFullMask"},
        {VRCAvatarDescriptor.AnimLayerType.Sitting, null},//"SDKFullMask"},
        {VRCAvatarDescriptor.AnimLayerType.Additive, null},//"SDKFullMask"},
        {VRCAvatarDescriptor.AnimLayerType.FX, "SDKEmptyMask"}, // TODO
        {VRCAvatarDescriptor.AnimLayerType.Action, null},//"vrc_MusclesOnly"},
        {VRCAvatarDescriptor.AnimLayerType.Gesture, "vrc_HandsOnly"},
    };

    static void InitDefaults() {
        foreach (var kv in animLayerToDefaultFile) {
            if (kv.Value == null) {
                SDKAv3Runtime.animLayerToDefaultController[kv.Key] = null;
            } else
            {
                AnimatorController ac = null;
                foreach (var guid in AssetDatabase.FindAssets(kv.Value))
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    ac = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
                }
                if (ac == null)
                {
                    Debug.LogWarning("Failed to resolve animator controller " + kv.Value + " for " + kv.Key);
                    ac = null;
                }
                SDKAv3Runtime.animLayerToDefaultController[kv.Key] = ac;
            }
        }
        foreach (var kv in animLayerToDefaultAvaMaskFile) {
            if (kv.Value == null) {
                SDKAv3Runtime.animLayerToDefaultAvaMask[kv.Key] = null;
            } else
            {
                AvatarMask mask = null;
                foreach (var guid in AssetDatabase.FindAssets(kv.Value))
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    mask = AssetDatabase.LoadAssetAtPath<AvatarMask>(path);
                }
                if (mask == null)
                {
                    Debug.LogWarning("Failed to resolve avatar mask " + kv.Value + " for " + kv.Key);
                    mask = new AvatarMask();
                }
                SDKAv3Runtime.animLayerToDefaultAvaMask[kv.Key] = mask;
            }
        }
        foreach (string guid in AssetDatabase.FindAssets("EmptyController")) {
            SDKAv3Emulator.EmptyController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(AssetDatabase.GUIDToAssetPath(guid));
        }

        SDKAv3Runtime.updateSelectionDelegate = (go) => {
            if (go == null && SDKAv3Emulator.emulatorInstance != null) {
                Debug.Log("Resetting selected object: " + SDKAv3Emulator.emulatorInstance);
                go = SDKAv3Emulator.emulatorInstance.gameObject;
            }
            Debug.Log("Setting selected object: " + go);
            Selection.SetActiveObjectWithContext(go, go);
            // Highlighter.Highlight("Inspector", "Animator To Debug");
        };

        SDKAv3Runtime.addRuntimeDelegate = (runtime) => {
            GameObject go = runtime.gameObject;
            try {
                if (PrefabUtility.IsPartOfAnyPrefab(go)) {
                    PrefabUtility.UnpackPrefabInstance(go, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                }
            } catch (System.Exception) {}
            int moveUpCalls = go.GetComponents<Component>().Length - 2;
            if (!PrefabUtility.IsPartOfAnyPrefab(go.GetComponents<Component>()[1])) {
                for (int i = 0; i < moveUpCalls; i++) {
                    UnityEditorInternal.ComponentUtility.MoveComponentUp(runtime);
                }
            }
        };
    }

    // register an event handler when the class is initialized
    static SDKAv3EditorSupport()
    {
        InitDefaults();
    }

    [MenuItem("Phoenix/SDK/Utilities/Enable Avatars 3.0 Emulator")]
    public static void EnableAv3Testing() {
        GameObject go = new GameObject("Avatars 3.0 Emulator Control");
        go.AddComponent<SDKAv3Emulator>();
    }
}