﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;

// [RequireComponent(typeof(Animator))]
public class SDKAv3Runtime : MonoBehaviour
{
    static public Dictionary<VRCAvatarDescriptor.AnimLayerType, RuntimeAnimatorController> animLayerToDefaultController = new Dictionary<VRCAvatarDescriptor.AnimLayerType, RuntimeAnimatorController>();
    static public Dictionary<VRCAvatarDescriptor.AnimLayerType, AvatarMask> animLayerToDefaultAvaMask = new Dictionary<VRCAvatarDescriptor.AnimLayerType, AvatarMask>();
    public delegate void UpdateSelectionFunc(GameObject obj);
    public static UpdateSelectionFunc updateSelectionDelegate;
    public delegate void AddRuntime(SDKAv3Runtime runtime);
    public static AddRuntime addRuntimeDelegate;

    public bool ResetAvatar;
    public bool ResetAndHold;
    [Tooltip("Selects the playable layer to be visible in Unity's Animator window. Because this layer is moved to the top, the output may no longer be correct unless this is set to Base.")] public VRCAvatarDescriptor.AnimLayerType AnimatorToDebug;
    private char PrevAnimatorToDebug;
    [HideInInspector] public string SourceObjectPath;
    [Header("Assign to non-local duplicate")]public SDKAv3Runtime AvatarSyncSource;
    private int CloneCount;
    public bool CreateNonLocalClone;
    VRCAvatarDescriptor avadesc;
    Avatar animatorAvatar;
    Animator animator;
    private RuntimeAnimatorController origAnimatorController;

    private List<AnimatorControllerPlayable> playables = new List<AnimatorControllerPlayable>();
    private List<Dictionary<string, int>> playableParamterIds = new List<Dictionary<string, int>>();
    private List<Dictionary<int, float>> playableParamterFloats = new List<Dictionary<int, float>>();
    private List<Dictionary<int, int>> playableParamterInts = new List<Dictionary<int, int>>();
    AnimationLayerMixerPlayable playableMixer;
    PlayableGraph playableGraph;
    VRCExpressionsMenu expressionsMenu;
    VRCExpressionParameters stageParameters;
    int sittingIndex;
    int fxIndex;
    int actionIndex;
    int additiveIndex;
    int gestureIndex;

    public static float ClampFloat(float val) {
        if (val < -1.0f) {
            val = -1.0f;
        }
        if (val > 1.0f) {
            val = 1.0f;
        }
        if (val > 0.0f) {
            val *= 128f / 127; // apply bias.
        }
        val = (((sbyte)((val) * -127.0f)) / -127.0f);
        if (val > 1.0f)
        {
            val = 1.0f;
        }
        return val;
    }
    public static int ClampByte(int val) {
        if (val < 0) {
            val = 0;
        }
        if (val > 255) {
            val = 255;
        }
        return val;
    }

    public enum VisemeIndex {
        sil, PP, FF, TH, DD, kk, CH, SS, nn, RR, aa, E, I, O, U
    }
    public enum GestureIndex {
        Neutral, Fist, HandOpen, Fingerpoint, Victory, RockNRoll, HandGun, ThumbsUp
    }
    static HashSet<string> BUILTIN_PARAMETERS = new HashSet<string> {
        "Viseme", "GestureLeft", "GestureLeftWeight", "GestureRight", "GestureRightWeight", "VelocityX", "VelocityY", "VelocityZ", "LocomotionMode", "Upright", "AngularY", "GroundProximity", "Grounded", "Supine", "FootstepDisable", "Seated", "AFK"
    };
    [Header("Built-in locomotion inputs")]
    public int VisemeI;
    public VisemeIndex VisemeDD;
    private int Viseme;
    public GestureIndex GestureLeft;
    [Range(0, 9)] public int GestureLeftIdx;
    private char GestureLeftIdxInt;
    [Range(0, 1)] public float GestureLeftWeight;
    public GestureIndex GestureRight;
    [Range(0, 9)] public int GestureRightIdx;
    private char GestureRightIdxInt;
    [Range(0, 1)] public float GestureRightWeight;
    public Vector3 Velocity;
    [Range(-1, 1)] public float AngularY;
    [Range(0, 1)] public float Upright;
    [Range(-1, 1)] public float GroundProximity; // Not implemented
    private int LocomotionMode; // Does not exist.
    public bool Grounded;
    private bool PrevSeated;
    public bool Seated;
    public bool AFK;
    //TODO:
    public bool Supine; // Not implemented
    private bool FootstepDisable; // Does not exist.

    [Header("Output State (Read-only)")]
    public bool IsLocal;
    public bool LocomotionIsDisabled;
    private Vector3 HeadRelativeViewPosition;
    public Vector3 ViewPosition;
    float AvatarScaleFactor;
    public VRCAnimatorTrackingControl.TrackingType trackingHead;
    public VRCAnimatorTrackingControl.TrackingType trackingLeftHand;
    public VRCAnimatorTrackingControl.TrackingType trackingRightHand;
    public VRCAnimatorTrackingControl.TrackingType trackingHip;
    public VRCAnimatorTrackingControl.TrackingType trackingLeftFoot;
    public VRCAnimatorTrackingControl.TrackingType trackingRightFoot;
    public VRCAnimatorTrackingControl.TrackingType trackingLeftFingers;
    public VRCAnimatorTrackingControl.TrackingType trackingRightFingers;
    public VRCAnimatorTrackingControl.TrackingType trackingEyesAndEyelids;
    public VRCAnimatorTrackingControl.TrackingType trackingMouthAndJaw;

    [Serializable]
    public class FloatParam
    {
        [HideInInspector] public string stageName;
        public string name;
        [Range(-1, 1)] public float value;
        [HideInInspector] public float lastValue;
    }
    [Header("User-generated inputs")]
    public List<FloatParam> Floats = new List<FloatParam>();
    public Dictionary<string, int> FloatToIndex = new Dictionary<string, int>();

    [Serializable]
    public class IntParam
    {
        [HideInInspector] public string stageName;
        public string name;
        public int value;
        [HideInInspector] public int lastValue;
    }
    public List<IntParam> Ints = new List<IntParam>();
    public Dictionary<string, int> IntToIndex = new Dictionary<string, int>();

    [Serializable]
    public class BoolParam
    {
        public string name;
        public bool value;
        [HideInInspector] public bool lastValue;
    }
    public List<BoolParam> Bools = new List<BoolParam>();
    public Dictionary<string, int> BoolToIndex = new Dictionary<string, int>();

    public Dictionary<string, string> StageParamterToBuiltin = new Dictionary<string, string>();

    static public Dictionary<Animator, SDKAv3Runtime> animatorToTopLevelRuntime = new Dictionary<Animator, SDKAv3Runtime>();
    private HashSet<Animator> attachedAnimators;

    const float BASE_HEIGHT = 1.4f;

    public IEnumerator DelayedEnterPoseSpace(bool setView, float time) {
        yield return new WaitForSeconds(time);
        if (setView) {
            Transform head = animator.GetBoneTransform(HumanBodyBones.Head);
            ViewPosition = animator.transform.InverseTransformPoint(head.TransformPoint(HeadRelativeViewPosition));
        } else {
            ViewPosition = avadesc.ViewPosition;
        }
    }

    class BlendingState {
        float startWeight;
        float goalWeight;
        float blendStartTime;
        float blendDuration;
        public bool blending;

        public float UpdateBlending() {
            if (blendDuration <= 0) {
                blending = false;
                return goalWeight;
            }
            float amt = (Time.time - blendStartTime) / blendDuration;
            if (amt >= 1) {
                blending = false;
                return goalWeight;
            }
            return Mathf.Lerp(startWeight, goalWeight, amt);
        }
        public void StartBlend(float startWeight, float goalWeight, float duration) {
            this.startWeight = startWeight;
            this.blendDuration = duration;
            this.blendStartTime = Time.time;
            this.goalWeight = goalWeight;
            this.blending = true;
        }
    }
    class PlayableBlendingState : BlendingState {
        public List<BlendingState> layerBlends = new List<BlendingState>();

    }
    List<PlayableBlendingState> playableBlendingStates = new List<PlayableBlendingState>();

    static bool getTopLevelRuntime(string component, Animator innerAnimator, out SDKAv3Runtime runtime) {
        if (animatorToTopLevelRuntime.TryGetValue(innerAnimator, out runtime)) {
            return true;
        }
        Transform transform = innerAnimator.transform;
        while (transform != null && runtime == null) {
            runtime = transform.GetComponent<SDKAv3Runtime>();
            transform = transform.parent;
        }
        if (runtime != null) {
            if (runtime.attachedAnimators != null) {
                Debug.Log("[" + component + "]: " + innerAnimator + " found parent runtime after it was Awoken! Adding to cache. Did you move me?");
                animatorToTopLevelRuntime.Add(innerAnimator, runtime);
                runtime.attachedAnimators.Add(innerAnimator);
            } else {
                Debug.Log("[" + component + "]: " + innerAnimator + " found parent runtime without being Awoken! Wakey Wakey...");
                runtime.Awake();
            }
            return true;
        }
        Debug.LogError("[" + component + "]: outermost Animator is not known: " + innerAnimator + ". If you changed something, consider resetting avatar", innerAnimator);

        return false;
    }

    static SDKAv3Runtime () {
        VRCAvatarParameterDriver.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCAvatarParameterDriver", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCAvatarParameterDriver:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }
                foreach (var parameter in behaviour.parameters)
                {
                    string actualName;
                    if (!runtime.StageParamterToBuiltin.TryGetValue(parameter.name, out actualName)) {
                        actualName = parameter.name;
                    }
                    int idx;
                    if (runtime.IntToIndex.TryGetValue(actualName, out idx)) {
                        runtime.Ints[idx].value = (int)parameter.value;
                    }
                    if (runtime.FloatToIndex.TryGetValue(actualName, out idx)) {
                        runtime.Floats[idx].value = parameter.value;
                    }
                    if (runtime.BoolToIndex.TryGetValue(actualName, out idx)) {
                        runtime.Bools[idx].value = parameter.value != 0;
                    }
                }
            };
        };
        VRCPlayableLayerControl.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCPlayableLayerControl", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCPlayableLayerControl:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }
                int idx = -1;
                switch (behaviour.layer)
                {
                    case VRCPlayableLayerControl.BlendableLayer.Action:
                        idx = runtime.actionIndex;
                        break;
                    case VRCPlayableLayerControl.BlendableLayer.Additive:
                        idx = runtime.additiveIndex;
                        break;
                    case VRCPlayableLayerControl.BlendableLayer.FX:
                        idx = runtime.fxIndex;
                        break;
                    case VRCPlayableLayerControl.BlendableLayer.Gesture:
                        idx = runtime.gestureIndex;
                        break;
                }
                if (idx >= 0 && idx < runtime.playableBlendingStates.Count)
                {
                    runtime.playableBlendingStates[idx].StartBlend(runtime.playableMixer.GetInputWeight(idx), behaviour.goalWeight, behaviour.blendDuration);
                }
            };
        };
        VRCAnimatorLayerControl.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCAnimatorLayerControl", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCAnimatorLayerControl:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }
                int idx = -1;
                switch (behaviour.playable)
                {
                    case VRCAnimatorLayerControl.BlendableLayer.Action:
                        idx = runtime.actionIndex;
                        break;
                    case VRCAnimatorLayerControl.BlendableLayer.Additive:
                        idx = runtime.additiveIndex;
                        break;
                    case VRCAnimatorLayerControl.BlendableLayer.FX:
                        idx = runtime.fxIndex;
                        break;
                    case VRCAnimatorLayerControl.BlendableLayer.Gesture:
                        idx = runtime.gestureIndex;
                        break;
                }
                if (idx >= 0 && idx < runtime.playableBlendingStates.Count)
                {
                    if (behaviour.layer >= 0 && behaviour.layer < runtime.playableBlendingStates[idx].layerBlends.Count)
                    {
                        runtime.playableBlendingStates[idx].layerBlends[behaviour.layer].StartBlend(runtime.playables[idx].GetLayerWeight(behaviour.layer), behaviour.goalWeight, behaviour.blendDuration);
                    }
                }
            };
        };
        VRCAnimatorLocomotionControl.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCAnimatorLocomotionControl", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCAnimatorLocomotionControl:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }
                // I legit don't know
                runtime.LocomotionIsDisabled = behaviour.disableLocomotion;
            };
        };
        VRCAnimatorTemporaryPoseSpace.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCAnimatorSetView", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCAnimatorSetView:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }
                // fixedDelay: Is the delay fixed or normalized...
                // The layerIndex is not passed into the delegate, so we cannot reimplement fixedDelay.
                runtime.StartCoroutine(runtime.DelayedEnterPoseSpace(behaviour.enterPoseSpace, behaviour.delayTime));
            };
        };
        VRCAnimatorTrackingControl.Initialize += (x) => {
            x.ApplySettings += (behaviour, animator) =>
            {
                SDKAv3Runtime runtime;
                if (!getTopLevelRuntime("VRCAnimatorTrackingControl", animator, out runtime)) {
                    return;
                }
                if (behaviour.debugString != null && behaviour.debugString.Length > 0)
                {
                    Debug.Log("[VRCAnimatorTrackingControl:" + (runtime == null ? "null" : runtime.name) + "]" + behaviour.name + ": " + behaviour.debugString, behaviour);
                }
                if (!runtime)
                {
                    return;
                }

                if (behaviour.trackingMouth != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingMouthAndJaw = behaviour.trackingMouth;
                }
                if (behaviour.trackingHead != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingHead = behaviour.trackingHead;
                }
                if (behaviour.trackingRightFingers != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingRightFingers = behaviour.trackingRightFingers;
                }
                if (behaviour.trackingEyes != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingEyesAndEyelids = behaviour.trackingEyes;
                }
                if (behaviour.trackingLeftFingers != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingLeftFingers = behaviour.trackingLeftFingers;
                }
                if (behaviour.trackingLeftFoot != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingLeftFoot = behaviour.trackingLeftFoot;
                }
                if (behaviour.trackingHip != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingHip = behaviour.trackingHip;
                }
                if (behaviour.trackingRightHand != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingRightHand = behaviour.trackingRightHand;
                }
                if (behaviour.trackingLeftHand != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingLeftHand = behaviour.trackingLeftHand;
                }
                if (behaviour.trackingRightFoot != VRCAnimatorTrackingControl.TrackingType.NoChange)
                {
                    runtime.trackingRightFoot = behaviour.trackingRightFoot;
                }
            };
        };
    }

    void OnDestroy () {
        if (this.playableGraph.IsValid()) {
            this.playableGraph.Destroy();
        }
        foreach (var anim in attachedAnimators) {
            SDKAv3Runtime runtime;
            if (animatorToTopLevelRuntime.TryGetValue(anim, out runtime) && runtime == this)
            {
                animatorToTopLevelRuntime.Remove(anim);
            }
        }
        if (animator.playableGraph.IsValid())
        {
            animator.playableGraph.Destroy();
        }
        animator.runtimeAnimatorController = origAnimatorController;
    }

    void Awake()
    {
        if (attachedAnimators != null) {
            Debug.Log("Deduplicating Awake() call if we already got awoken by our children.");
            return;
        }
        attachedAnimators = new HashSet<Animator>();
        if (AvatarSyncSource == null) {
            Transform transform = this.transform;
            SourceObjectPath = "";
            while (transform != null) {
                SourceObjectPath = "/" + transform.name + SourceObjectPath;
                transform = transform.parent;
            }
            AvatarSyncSource = this;
        } else {
            AvatarSyncSource = GameObject.Find(SourceObjectPath).GetComponent<SDKAv3Runtime>();
        }

        animator = this.gameObject.GetOrAddComponent<Animator>();
        animatorAvatar = animator.avatar;
        // Default values.
        Grounded = true;
        Upright = 1.0f;
        AnimatorToDebug = VRCAvatarDescriptor.AnimLayerType.Base;
        // AnimatorToDebug = VRCAvatarDescriptor.AnimLayerType.FX;

        InitializeAnimator();
        if (addRuntimeDelegate != null) {
            addRuntimeDelegate(this);
        }
    }

    private void InitializeAnimator()
    {
        ResetAvatar = false;
        PrevAnimatorToDebug = (char)(int)AnimatorToDebug;

        animator = this.gameObject.GetOrAddComponent<Animator>();
        animator.avatar = animatorAvatar;
        animator.applyRootMotion = false;
        animator.updateMode = AnimatorUpdateMode.Normal;
        animator.cullingMode = AnimatorCullingMode.CullCompletely;
        animator.runtimeAnimatorController = null;

        avadesc = this.gameObject.GetComponent<VRCAvatarDescriptor>();
        ViewPosition = avadesc.ViewPosition;
        AvatarScaleFactor = ViewPosition.magnitude / BASE_HEIGHT; // mostly guessing...
        HeadRelativeViewPosition = ViewPosition;
        if (animator.avatar != null)
        {
            Transform head = animator.GetBoneTransform(HumanBodyBones.Head);;
            HeadRelativeViewPosition = head.InverseTransformPoint(animator.transform.TransformPoint(ViewPosition));
        }
        expressionsMenu = avadesc.expressionsMenu;
        if (expressionsMenu != null)
        {
            stageParameters = avadesc.expressionParameters;
        }
        if (origAnimatorController != null) {
            origAnimatorController = animator.runtimeAnimatorController;
        }

        VRCAvatarDescriptor.CustomAnimLayer[] baselayers = avadesc.baseAnimationLayers;
        VRCAvatarDescriptor.CustomAnimLayer[] speciallayers = avadesc.specialAnimationLayers;
        List<VRCAvatarDescriptor.CustomAnimLayer> allLayers = new List<VRCAvatarDescriptor.CustomAnimLayer>();
        // foreach (VRCAvatarDescriptor.CustomAnimLayer cal in baselayers) {
        //     if (AnimatorToDebug == cal.type) {
        //         allLayers.Add(cal);
        //     }
        // }
        // foreach (VRCAvatarDescriptor.CustomAnimLayer cal in speciallayers) {
        //     if (AnimatorToDebug == cal.type) {
        //         allLayers.Add(cal);
        //     }
        // }
        int i;
        int layerToDebug = 1;
        i = 0;
        foreach (VRCAvatarDescriptor.CustomAnimLayer cal in baselayers) {
            if (cal.type == VRCAvatarDescriptor.AnimLayerType.Base || cal.type == VRCAvatarDescriptor.AnimLayerType.Additive) {
                i++;
                if (AnimatorToDebug == cal.type) {
                    layerToDebug = i;
                    // continue;
                }
                allLayers.Add(cal);
            }
        }
        foreach (VRCAvatarDescriptor.CustomAnimLayer cal in speciallayers) {
            i++;
            if (AnimatorToDebug == cal.type) {
                layerToDebug = i;
                // continue;
            }
            allLayers.Add(cal);
        }
        foreach (VRCAvatarDescriptor.CustomAnimLayer cal in baselayers) {
            if (!(cal.type == VRCAvatarDescriptor.AnimLayerType.Base || cal.type == VRCAvatarDescriptor.AnimLayerType.Additive)) {
                i++;
                if (AnimatorToDebug == cal.type) {
                    layerToDebug = i;
                    // continue;
                }
                allLayers.Add(cal);
            }
        }

        if (playableGraph.IsValid()) {
            playableGraph.Destroy();
        }
        playables.Clear();
        playableBlendingStates.Clear();

        for (i = 0; i < allLayers.Count; i++) {
            playables.Add(new AnimatorControllerPlayable());
            playableBlendingStates.Add(null);
        }

        actionIndex = fxIndex = gestureIndex = additiveIndex = sittingIndex = -1;

        foreach (var anim in attachedAnimators) {
            SDKAv3Runtime runtime;
            if (animatorToTopLevelRuntime.TryGetValue(anim, out runtime) && runtime == this)
            {
                animatorToTopLevelRuntime.Remove(anim);
            }
        }
        attachedAnimators.Clear();
        Animator[] animators = this.gameObject.GetComponentsInChildren<Animator>(true);
        Debug.Log("anim len "+animators.Length);
        foreach (Animator anim in animators)
        {
            attachedAnimators.Add(anim);
            animatorToTopLevelRuntime.Add(anim, this);
        }

        Ints.Clear();
        Bools.Clear();
        Floats.Clear();
        StageParamterToBuiltin.Clear();
        IntToIndex.Clear();
        FloatToIndex.Clear();
        BoolToIndex.Clear();
        playableParamterFloats.Clear();
        playableParamterIds.Clear();
        playableParamterInts.Clear();
        HashSet<string> usedparams = new HashSet<string>(BUILTIN_PARAMETERS);
        i = 0;
        if (stageParameters != null)
        {
            int stageId = 0;
            foreach (var stageParam in stageParameters.parameters)
            {
                stageId++; // one-indexed
                if (stageParam.name == null || stageParam.name.Length == 0) {
                    continue;
                }
                string stageName = "Stage" + stageId;
                StageParamterToBuiltin.Add(stageName, stageParam.name);
                if ((int)stageParam.valueType == 0)
                {
                    IntParam param = new IntParam();
                    param.stageName = stageName;
                    param.name = stageParam.name;
                    param.value = 0;
                    param.lastValue = 0;
                    IntToIndex[param.name] = Ints.Count;
                    Ints.Add(param);
                }
                else
                {
                    FloatParam param = new FloatParam();
                    param.stageName = stageName;
                    param.name = stageParam.name;
                    param.value = 0;
                    param.lastValue = 0;
                    FloatToIndex[param.name] = Floats.Count;
                    Floats.Add(param);
                }
                usedparams.Add(stageParam.name);
                i++;
            }
        }

        if (animator.playableGraph.IsValid())
        {
            animator.playableGraph.Destroy();
        }
        // var director = avadesc.gameObject.GetComponent<PlayableDirector>();
        playableGraph = PlayableGraph.Create("SDKAvatarRuntime - " + this.gameObject.name);
        var externalOutput = AnimationPlayableOutput.Create(playableGraph, "ExternalAnimator", animator);
        playableMixer = AnimationLayerMixerPlayable.Create(playableGraph, allLayers.Count + 1);
        externalOutput.SetSourcePlayable(playableMixer);
        animator.applyRootMotion = false;

        i = 0;
        foreach (VRCAvatarDescriptor.CustomAnimLayer vrcAnimLayer in allLayers) {
            i++; // Ignore zeroth layer.
        }

        AnimatorControllerPlayable mainPlayable;

        i = 0;
        // playableMixer.ConnectInput(0, AnimatorControllerPlayable.Create(playableGraph, allLayers[layerToDebug - 1].animatorController), 0, 0);
        foreach (VRCAvatarDescriptor.CustomAnimLayer vrcAnimLayer in allLayers)
        {
            i++; // Ignore zeroth layer.
            bool additive = (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.Additive);
            RuntimeAnimatorController ac = null;
            AvatarMask mask;
            if (vrcAnimLayer.isDefault) {
                ac = animLayerToDefaultController[vrcAnimLayer.type];
                mask = animLayerToDefaultAvaMask[vrcAnimLayer.type];
            } else
            {
                ac = vrcAnimLayer.animatorController;
                mask = vrcAnimLayer.mask;
                if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.FX) {
                    mask = animLayerToDefaultAvaMask[vrcAnimLayer.type]; // Force mask to prevent muscle overrides.
                }
            }
            if (ac == null) {
                // i is not incremented.
                continue;
            }
            AnimatorControllerPlayable humanAnimatorPlayable = AnimatorControllerPlayable.Create(playableGraph, ac);
            PlayableBlendingState pbs = new PlayableBlendingState();
            for (int j = 0; j < humanAnimatorPlayable.GetLayerCount(); j++)
            {
                humanAnimatorPlayable.SetLayerWeight(j, 1f);
                pbs.layerBlends.Add(new BlendingState());
            }

            // If we are debugging a particular layer, we must put that first.
            // The Animator Controller window only shows the first layer.
            int effectiveIdx = i == layerToDebug ? 1 : (i < layerToDebug ? i + 1 : i);

            playableMixer.ConnectInput((int)effectiveIdx, humanAnimatorPlayable, 0, 1);
            playables[effectiveIdx - 1] = humanAnimatorPlayable;
            playableBlendingStates[effectiveIdx - 1] = pbs;
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.Sitting) {
                sittingIndex = effectiveIdx;
                playableMixer.SetInputWeight(effectiveIdx, 0f);
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.IKPose)
            {
                playableMixer.SetInputWeight(effectiveIdx, 0f);
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.TPose)
            {
                playableMixer.SetInputWeight(effectiveIdx, 0f);
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.Action)
            {
                playableMixer.SetInputWeight(i, 0f);
                actionIndex = effectiveIdx;
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.Gesture)
            {
                gestureIndex = effectiveIdx;
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.Additive)
            {
                additiveIndex = effectiveIdx;
            }
            if (vrcAnimLayer.type == VRCAvatarDescriptor.AnimLayerType.FX)
            {
                fxIndex = effectiveIdx;
            }
            // AnimationControllerLayer acLayer = new AnimationControllerLayer()
            if (mask != null)
            {
                playableMixer.SetLayerMaskFromAvatarMask((uint)effectiveIdx, mask);
            }
            if (additive)
            {
                playableMixer.SetLayerAdditive((uint)effectiveIdx, true);
            }
        }

        //playableParamterIds
        int whichcontroller = 0;
        playableParamterIds.Clear();
        foreach (AnimatorControllerPlayable playable in playables) {
            int pcnt = playable.GetParameterCount();
            Dictionary<string, int> parameterIndices = new Dictionary<string, int>();
            playableParamterInts.Add(new Dictionary<int, int>());
            playableParamterFloats.Add(new Dictionary<int, float>());
            // Debug.Log("SETUP index " + whichcontroller + " len " + playables.Count);
            playableParamterIds.Add(parameterIndices);
            for (i = 0; i < pcnt; i++) {
                AnimatorControllerParameter aparam = playable.GetParameter(i);
                string actualName;
                if (!StageParamterToBuiltin.TryGetValue(aparam.name, out actualName)) {
                    actualName = aparam.name;
                }
                parameterIndices[actualName] = aparam.nameHash;
                if (usedparams.Contains(actualName)) {
                    continue;
                }
                if (aparam.type == AnimatorControllerParameterType.Int) {
                    IntParam param = new IntParam();
                    param.stageName = aparam.name + " (local)";
                    param.name = aparam.name;
                    param.value = aparam.defaultInt;
                    param.lastValue = param.value;
                    IntToIndex[param.name] = Ints.Count;
                    Ints.Add(param);
                    usedparams.Add(aparam.name);
                } else if (aparam.type == AnimatorControllerParameterType.Float) {
                    FloatParam param = new FloatParam();
                    param.stageName = aparam.name + " (local)";
                    param.name = aparam.name;
                    param.value = aparam.defaultFloat;
                    param.lastValue = param.value;
                    FloatToIndex[param.name] = Floats.Count;
                    Floats.Add(param);
                    usedparams.Add(aparam.name);
                } else if (aparam.type == AnimatorControllerParameterType.Bool) {
                    BoolParam param = new BoolParam();
                    param.name = aparam.name;
                    param.value = aparam.defaultBool;
                    param.lastValue = param.value;
                    BoolToIndex[param.name] = Bools.Count;
                    Bools.Add(param);
                    usedparams.Add(aparam.name);
                }
            }
            whichcontroller++;
        }

        // Plays the Graph.
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        Debug.Log(this.gameObject.name + " : Awoken and ready to Play.");
        playableGraph.Play();
        Debug.Log(this.gameObject.name + " : Playing.");
    }


    private bool isResetting;
    private bool isResettingHold;
    private bool isResettingSel;
    // Update is called once per frame
    void Update()
    {
        if (isResettingSel) {
            isResettingSel = false;
            if (updateSelectionDelegate != null) {
                updateSelectionDelegate(this.gameObject);
            }
        }
        if (isResettingHold && (!ResetAvatar || !ResetAndHold)) {
            ResetAndHold = ResetAvatar = false;
            isResettingSel = true;
            if (updateSelectionDelegate != null) {
                updateSelectionDelegate(null);
            }
        }
        if (ResetAvatar && ResetAndHold) {
            return;
        }
        if (ResetAndHold && !ResetAvatar && !isResetting) {
            ResetAvatar = true;
            isResettingHold = true;
        }
        if (isResetting && !ResetAndHold) {
            InitializeAnimator();
            isResetting = false;
            isResettingHold = false;
        }
        if (PrevAnimatorToDebug != (char)(int)AnimatorToDebug || ResetAvatar) {
            // animator.runtimeAnimatorController = null;
            if (playableGraph.IsValid()) {
                playableGraph.Destroy();
            }
            if (animator.playableGraph.IsValid()) {
                animator.playableGraph.Destroy();
            }
            animator.Update(0);
            animator.Rebind();
            animator.Update(0);
            animator.StopPlayback();
            GameObject.DestroyImmediate(animator);
            // animator.runtimeAnimatorController = EmptyController;
            if (updateSelectionDelegate != null) {
                updateSelectionDelegate(null);
            }
            isResetting = true;
            isResettingSel = true;
            return;
        }
        if (CreateNonLocalClone) {
            CreateNonLocalClone = false;
            GameObject go = GameObject.Instantiate(AvatarSyncSource.gameObject);
            AvatarSyncSource.CloneCount++;
            go.name = go.name.Substring(0, go.name.Length - 7) + " (Non-Local " + AvatarSyncSource.CloneCount + ")";
            go.transform.position = go.transform.position + AvatarSyncSource.CloneCount * new Vector3(0.4f, 0.0f, 0.4f);
        }
        if (AvatarSyncSource != this) {
            for (int i = 0; i < Ints.Count; i++) {
                if (StageParamterToBuiltin.ContainsKey(Ints[i].stageName)) {
                    Ints[i].value = ClampByte(AvatarSyncSource.Ints[i].value);
                }
            }
            for (int i = 0; i < Floats.Count; i++) {
                if (StageParamterToBuiltin.ContainsKey(Floats[i].stageName)) {
                    Floats[i].value = ClampFloat(AvatarSyncSource.Floats[i].value);
                }
            }
            Viseme = VisemeI = AvatarSyncSource.Viseme;
            VisemeDD = (VisemeIndex)Viseme;
            GestureLeft = AvatarSyncSource.GestureLeft;
            GestureLeftIdx = AvatarSyncSource.GestureLeftIdx;
            GestureLeftIdxInt = AvatarSyncSource.GestureLeftIdxInt;
            GestureLeftWeight = AvatarSyncSource.GestureLeftWeight;
            GestureRight = AvatarSyncSource.GestureRight;
            GestureRightIdx = AvatarSyncSource.GestureRightIdx;
            GestureRightIdxInt = AvatarSyncSource.GestureRightIdxInt;
            GestureRightWeight = AvatarSyncSource.GestureRightWeight;
            Velocity = AvatarSyncSource.Velocity;
            AngularY = AvatarSyncSource.AngularY;
            Upright = AvatarSyncSource.Upright;
            LocomotionMode = AvatarSyncSource.LocomotionMode;
            GroundProximity = AvatarSyncSource.GroundProximity;
            Grounded = AvatarSyncSource.Grounded;
            Seated = AvatarSyncSource.Seated;
            AFK = AvatarSyncSource.AFK;
            Supine = AvatarSyncSource.Supine;
            FootstepDisable = AvatarSyncSource.FootstepDisable;
        }
        for (int i = 0; i < Floats.Count; i++) {
            if (StageParamterToBuiltin.ContainsKey(Floats[i].stageName)) {
                Floats[i].value = ClampFloat(Floats[i].value);
            }
        }
        for (int i = 0; i < Ints.Count; i++) {
            if (StageParamterToBuiltin.ContainsKey(Ints[i].stageName)) {
                Ints[i].value = ClampByte(Ints[i].value);
            }
        }
        if (Seated != PrevSeated && sittingIndex >= 0)
        {
            playableBlendingStates[sittingIndex].StartBlend(playableMixer.GetInputWeight(sittingIndex), Seated ? 1f : 0f, 0.25f);
            PrevSeated = Seated;
        }
        if (VisemeI != Viseme) {
            Viseme = VisemeI;
            VisemeDD = (VisemeIndex)Viseme;
        }
        if ((int)VisemeDD != Viseme) {
            Viseme = (int)VisemeDD;
            VisemeI = Viseme;
        }
        if (GestureLeftIdx != GestureLeftIdxInt) {
            GestureLeft = (GestureIndex)GestureLeftIdx;
            GestureLeftIdx = (int)GestureLeft;
            GestureLeftIdxInt = (char)GestureLeftIdx;
        }
        if ((int)GestureLeft != (int)GestureLeftIdxInt) {
            GestureLeftIdx = (int)GestureLeft;
            GestureLeftIdxInt = (char)GestureLeftIdx;
        }
        if (GestureRightIdx != GestureRightIdxInt) {
            GestureRight = (GestureIndex)GestureRightIdx;
            GestureRightIdx = (int)GestureRight;
            GestureRightIdxInt = (char)GestureRightIdx;
        }
        if ((int)GestureRight != (int)GestureRightIdxInt) {
            GestureRightIdx = (int)GestureRight;
            GestureRightIdxInt = (char)GestureRightIdx;
        }
        IsLocal = AvatarSyncSource == this;

        int whichcontroller;
        whichcontroller = 0;
        foreach (AnimatorControllerPlayable playable in playables)
        {
            // Debug.Log("Index " + whichcontroller + " len " + playables.Count);
            Dictionary<string, int> parameterIndices = playableParamterIds[whichcontroller];
            int paramid;
            foreach (FloatParam param in Floats)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (param.value != param.lastValue) {
                        playable.SetFloat(paramid, param.value);
                    }
                }
            }
            foreach (IntParam param in Ints)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (param.value != param.lastValue) {
                        playable.SetInteger(paramid, param.value);
                    }
                }
            }
            foreach (BoolParam param in Bools)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (param.value != param.lastValue) {
                        playable.SetBool(paramid, param.value);
                    }
                }
            }
            whichcontroller++;
        }
        foreach (FloatParam param in Floats) {
            param.lastValue = param.value;
        }
        foreach (IntParam param in Ints) {
            param.lastValue = param.value;
        }
        foreach (BoolParam param in Bools) {
            param.lastValue = param.value;
        }

        whichcontroller = 0;
        foreach (AnimatorControllerPlayable playable in playables)
        {
            // Debug.Log("Index " + whichcontroller + " len " + playables.Count);
            Dictionary<string, int> parameterIndices = playableParamterIds[whichcontroller];
            Dictionary<int, int> paramterInts = playableParamterInts[whichcontroller];
            Dictionary<int, float> paramterFloats = playableParamterFloats[whichcontroller];
            int paramid;
            float fparam;
            int iparam;
            foreach (FloatParam param in Floats)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (paramterFloats.TryGetValue(paramid, out fparam)) {
                        if (fparam != playable.GetFloat(paramid)) {
                            param.value = param.lastValue = playable.GetFloat(paramid);
                        }
                    }
                    paramterFloats[paramid] = param.value;
                }
            }
            foreach (IntParam param in Ints)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (paramterInts.TryGetValue(paramid, out iparam)) {
                        if (iparam != playable.GetInteger(paramid)) {
                            param.value = param.lastValue = playable.GetInteger(paramid);
                        }
                    }
                    paramterInts[paramid] = param.value;
                }
            }
            foreach (BoolParam param in Bools)
            {
                if (parameterIndices.TryGetValue(param.name, out paramid))
                {
                    if (paramterInts.TryGetValue(paramid, out iparam)) {
                        if (iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                            param.value = param.lastValue = playable.GetBool(paramid);
                        }
                    }
                    paramterInts[paramid] = param.value ? 1 : 0;
                }
            }
            if (parameterIndices.TryGetValue("Viseme", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != playable.GetInteger(paramid)) {
                    Viseme = VisemeI = playable.GetInteger(paramid);
                    VisemeDD = (VisemeIndex)Viseme;
                }
                playable.SetInteger(paramid, Viseme);
                paramterInts[paramid] = Viseme;
            }
            if (parameterIndices.TryGetValue("GestureLeft", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != playable.GetInteger(paramid)) {
                    GestureLeftIdx = playable.GetInteger(paramid);
                    GestureLeftIdxInt = (char)GestureLeftIdx;
                    GestureLeft = (GestureIndex)GestureLeftIdx;
                }
                playable.SetInteger(paramid, (int)GestureLeft);
                paramterInts[paramid] = (int)GestureLeft;
            }
            if (parameterIndices.TryGetValue("GestureLeftWeight", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    GestureLeftWeight = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, GestureLeftWeight);
                paramterFloats[paramid] = GestureLeftWeight;
            }
            if (parameterIndices.TryGetValue("GestureRight", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != playable.GetInteger(paramid)) {
                    GestureRightIdx = playable.GetInteger(paramid);
                    GestureRightIdxInt = (char)GestureRightIdx;
                    GestureRight = (GestureIndex)GestureRightIdx;
                }
                playable.SetInteger(paramid, (int)GestureRight);
                paramterInts[paramid] = (int)GestureRight;
            }
            if (parameterIndices.TryGetValue("GestureRightWeight", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    GestureRightWeight = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, GestureRightWeight);
                paramterFloats[paramid] = GestureRightWeight;
            }
            if (parameterIndices.TryGetValue("VelocityX", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    Velocity.x = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, Velocity.x);
                paramterFloats[paramid] = Velocity.x;
            }
            if (parameterIndices.TryGetValue("VelocityY", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    Velocity.y = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, Velocity.y);
                paramterFloats[paramid] = Velocity.y;
            }
            if (parameterIndices.TryGetValue("VelocityZ", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    Velocity.z = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, Velocity.z);
                paramterFloats[paramid] = Velocity.z;
            }
            if (parameterIndices.TryGetValue("AngularY", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    AngularY = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, AngularY);
                paramterFloats[paramid] = AngularY;
            }
            if (parameterIndices.TryGetValue("Upright", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    Upright = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, Upright);
                paramterFloats[paramid] = Upright;
            }
            if (parameterIndices.TryGetValue("GroundProximity", out paramid))
            {
                if (paramterFloats.TryGetValue(paramid, out fparam) && fparam != playable.GetFloat(paramid)) {
                    GroundProximity = playable.GetFloat(paramid);
                }
                playable.SetFloat(paramid, GroundProximity);
                paramterFloats[paramid] = GroundProximity;
            }
            if (parameterIndices.TryGetValue("LocomotionMode", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != playable.GetInteger(paramid)) {
                    LocomotionMode = playable.GetInteger(paramid);
                }
                playable.SetInteger(paramid, LocomotionMode);
                paramterInts[paramid] = LocomotionMode;
            }
            if (parameterIndices.TryGetValue("IsLocal", out paramid))
            {
                playable.SetBool(paramid, IsLocal);
            }
            if (parameterIndices.TryGetValue("Grounded", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                    Grounded = playable.GetBool(paramid);
                }
                playable.SetBool(paramid, Grounded);
                paramterInts[paramid] = Grounded ? 1 : 0;
            }
            if (parameterIndices.TryGetValue("Seated", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                    Seated = playable.GetBool(paramid);
                }
                playable.SetBool(paramid, Seated);
                paramterInts[paramid] = Seated ? 1 : 0;
            }
            if (parameterIndices.TryGetValue("AFK", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                    AFK = playable.GetBool(paramid);
                }
                playable.SetBool(paramid, AFK);
                paramterInts[paramid] = AFK ? 1 : 0;
            }
            if (parameterIndices.TryGetValue("Supine", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                    Supine = playable.GetBool(paramid);
                }
                playable.SetBool(paramid, Supine);
                paramterInts[paramid] = Supine ? 1 : 0;
            }
            if (parameterIndices.TryGetValue("FootstepDisable", out paramid))
            {
                if (paramterInts.TryGetValue(paramid, out iparam) && iparam != (playable.GetBool(paramid) ? 1 : 0)) {
                    FootstepDisable = playable.GetBool(paramid);
                }
                playable.SetBool(paramid, FootstepDisable);
                paramterInts[paramid] = FootstepDisable ? 1 : 0;
            }
            whichcontroller++;
        }
        for (int i = 0; i < playableBlendingStates.Count; i++) {
            var pbs = playableBlendingStates[i];
            if (pbs.blending) {
                float newWeight = pbs.UpdateBlending();
                playableMixer.SetInputWeight(i, newWeight);
            }
            for (int j = 0; j < pbs.layerBlends.Count; j++) {
                if (pbs.layerBlends[j].blending) {
                    float newWeight = pbs.layerBlends[j].UpdateBlending();
                    playables[i].SetLayerWeight(j, newWeight);
                }
            }
        }
    }
}
