using UnityEngine;
using System.Collections.Generic;
using VRC.SDK3.Avatars.Components;

[RequireComponent(typeof(Animator))]
public class SDKAv3Emulator : MonoBehaviour
{
    public bool RestartEmulator;
    private bool RestartingEmulator;
    public bool CreateNonLocalClone;

    static public SDKAv3Emulator emulatorInstance;
    static public RuntimeAnimatorController EmptyController;

    public List<SDKAv3Runtime> runtimes = new List<SDKAv3Runtime>();

    private void Awake()
    {
        Animator animator = gameObject.GetOrAddComponent<Animator>();
        animator.enabled = false;
        animator.runtimeAnimatorController = EmptyController;
        emulatorInstance = this;
    }

    private void Start()
    {
        VRCAvatarDescriptor[] avatars = FindObjectsOfType<VRCAvatarDescriptor>();
        Debug.Log("drv len "+avatars.Length);
        foreach (var avadesc in avatars)
        {
            // Creates the playable director, and initializes animator.
            runtimes.Add(avadesc.gameObject.GetOrAddComponent<SDKAv3Runtime>());
        }
    }
    private void OnDisable() {
        foreach (var runtime in runtimes) {
            runtime.enabled = false;
        }
    }
    private void OnEnable() {
        foreach (var runtime in runtimes) {
            runtime.enabled = true;
        }
    }
    private void OnDestroy() {
        foreach (var runtime in runtimes) {
            Destroy(runtime);
        }
        runtimes.Clear();
    }

    private void Update() {
        if (RestartingEmulator) {
            RestartingEmulator = false;
            Start();
        } else if (RestartEmulator) {
            RestartEmulator = false;
            OnDestroy();
            RestartingEmulator = true;
        }
        if (CreateNonLocalClone) {
            CreateNonLocalClone = false;
            foreach (var runtime in runtimes)
            {
                if (runtime.AvatarSyncSource == runtime)
                {
                    runtime.CreateNonLocalClone = true;
                }
            }
        }
    }

}
