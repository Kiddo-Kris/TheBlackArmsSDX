using System;
using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    
    [CustomEditor(typeof(ABI.CCK.Components.CVRAssetInfo))]
    public class CCK_CVRAssetInfoEditor : UnityEditor.Editor
    {
        private CVRAssetInfo _info;
        private string _newGuid;

        public override void OnInspectorGUI()
        {
            if (_info == null) _info = (CVRAssetInfo)target;

            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_HEADER_INFORMATION"), MessageType.Info);

            if (!string.IsNullOrEmpty(_info.guid))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_GUID_LABEL") + _info.guid, MessageType.Info);
                if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_DETACH_BUTTON")))
                {
                    bool detach = EditorUtility.DisplayDialog(
                        CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_TITLE"),
                        CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_BODY"),
                        CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_ACCEPT"), 
                        CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_DETACH_BUTTON_DIALOG_DENY"));
                    if (detach) DetachGuid();
                }
            }
            else
            {
                _newGuid = EditorGUILayout.TextField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_ATTACH_LABEL"), _newGuid);
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_ATTACH_INFO"), MessageType.Warning);
                if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ASSET_INFO_ATTACH_BUTTON"))) ReattachGuid(_newGuid);
            }
            
        }

        private void DetachGuid()
        {
            if (!string.IsNullOrEmpty(_info.guid)) _info.guid = string.Empty;
        }
        private void ReattachGuid(string Guid)
        {
            _info.guid = Guid;
        }

    }
}
