#if VRC_SDK_VRCSDK3
using UnityEngine;
using UnityEditor;
using VRC.SDK3.Avatars.Components;
using static VRC.SDKBase.VRC_AvatarParameterDriver;
using Boo.Lang;
using System;
using System.Linq;

[CustomEditor(typeof(VRCAvatarParameterDriver))]
public class AvatarParameterDriverEditor : Editor
{
	VRCAvatarParameterDriver driver;
	string[] parameterNames;
	AnimatorControllerParameterType[] parameterTypes;

	public void OnEnable()
	{
		var driver = target as VRCAvatarParameterDriver;

		//Build parameter names
		var controller = GetCurrentController();
		if (controller != null)
		{
			//Standard
			List<string> names = new List<string>();
			List<AnimatorControllerParameterType> types = new List<AnimatorControllerParameterType>();
			foreach (var item in controller.parameters)
			{
				names.Add(item.name);
				types.Add(item.type);
			}
			parameterNames = names.ToArray();
			parameterTypes = types.ToArray();
		}
	}

	static UnityEditor.Animations.AnimatorController GetCurrentController()
	{
		UnityEditor.Animations.AnimatorController controller = null;
		var toolType = Type.GetType("UnityEditor.Graphs.AnimatorControllerTool, UnityEditor.Graphs");
		var tool = EditorWindow.GetWindow(toolType);
		var controllerProperty = toolType.GetProperty("animatorController", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
		if (controllerProperty != null)
		{
			controller = controllerProperty.GetValue(tool, null) as UnityEditor.Animations.AnimatorController;
		}
		else
			Debug.LogError("Unable to find animator window.", tool);
		return controller;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		var driver = target as VRCAvatarParameterDriver;

		//Info
		EditorGUILayout.HelpBox("Use this behaviour to drive any parameter defined on this animation controller.  This change affects all animation controllers defined on the avatar descriptor.", MessageType.Info);
		EditorGUILayout.HelpBox("You should primarily be driving expression parameters as they are the only variables that sync across the network. Changes to any other parameter will not be synced across the network.", MessageType.Info);

		//Parameters
		for (int i = 0; i < driver.parameters.Count; i++)
		{
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				var param = driver.parameters[i];
				var index = IndexOf(parameterNames, param.name);

				//Name
				if (parameterNames != null)
				{
					EditorGUI.BeginChangeCheck();
					index = EditorGUILayout.Popup("Name", index, parameterNames);
					if (EditorGUI.EndChangeCheck() && index >= 0)
						param.name = parameterNames[index];
				}
				else
					EditorGUILayout.LabelField("Name");
				param.name = EditorGUILayout.TextField(" ", param.name);

				//Value
				if (index >= 0)
				{
					var type = parameterTypes[index];
					switch(type)
					{
						case AnimatorControllerParameterType.Int:
							param.value = Mathf.Clamp(EditorGUILayout.IntField("Value", (int)param.value), 0, 255);
							break;
						case AnimatorControllerParameterType.Float:
							param.value = Mathf.Clamp(EditorGUILayout.FloatField("Value", param.value), -1f, 1);
							break;
						case AnimatorControllerParameterType.Bool:
							param.value = EditorGUILayout.Toggle("Value", param.value != 0) ? 1f : 0f;
							break;
						case AnimatorControllerParameterType.Trigger:
							//Nothing
							break;
					}
				}
				else
				{
					EditorGUI.BeginDisabledGroup(true);
					param.value = EditorGUILayout.FloatField("Value", param.value);
					EditorGUI.EndDisabledGroup();
					DrawInfoBox("WARNING: Parameter not found. Make sure you defined it on the animation controller.");
				}

				//Delete
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Delete"))
				{
					driver.parameters.RemoveAt(i);
					i--;
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndHorizontal();
		}

		//Add
		if (GUILayout.Button("Add Parameter"))
		{
			var parameter = new Parameter();
			if (parameterNames != null && parameterNames.Length > 0)
				parameter.name = parameterNames[0];
			driver.parameters.Add(parameter);
		}

		int IndexOf(string[] array, string value)
		{
			if (array == null)
				return -1;
			for(int i=0; i<array.Length; i++)
			{
				if (array[i] == value)
					return i;
			}
			return -1;
		}

		void DrawInfoBox(string text)
		{
			EditorGUI.indentLevel += 2;
			EditorGUILayout.LabelField(text, EditorStyles.textArea);
			EditorGUI.indentLevel -= 2;
		}

		serializedObject.ApplyModifiedProperties();
	}

	/*public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.indentLevel++;

        bool dirtyi = false;
        bool dirtyf = false;
        var oldParam = driver.driveParam;
        driver.driveParam = (DriveableAvatarParameter)EditorGUILayout.IntPopup("Parameter", (int)driver.driveParam, parameterNames, parameterValues);

        if (oldParam != driver.driveParam)
        { 
            if (GetDriverType(oldParam) == DriverType.GESTURE)
                dirtyi = true;
            else if (GetDriverType(oldParam) == DriverType.CLAMP01_FLOAT)
                dirtyf = true;
        }

        if (GetDriverType(driver.driveParam) == DriverType.BYTE_OR_SIGNED_FLOAT)
        {
            int oldIVal = driver.iValue;
            driver.iValue = EditorGUILayout.IntField("Int ("+driver.driveParam+")", driver.iValue);
            driver.iValue = Mathf.Clamp(driver.iValue, 0, 255);
            if (dirtyi || oldIVal != driver.iValue)
                driver.fValue = ((float)(sbyte)driver.iValue) / 127f;

            float oldFVal = driver.fValue;
            driver.fValue = EditorGUILayout.FloatField("Float ("+driver.driveParam+"f)", driver.fValue);
            driver.fValue = Mathf.Clamp(driver.fValue, -1f, 1f);
            if (dirtyf || oldFVal != driver.fValue)
                driver.iValue = (byte)(sbyte)Mathf.RoundToInt(driver.fValue * 127f);
        }
        else if (GetDriverType(driver.driveParam) == DriverType.CLAMP01_FLOAT)
        {
            driver.fValue = EditorGUILayout.FloatField("Value", Mathf.Clamp01(driver.fValue));
        }
        else if (GetDriverType(driver.driveParam) == DriverType.GESTURE)
        {
            driver.iValue = Mathf.Clamp(EditorGUILayout.IntField("Value", driver.iValue), 0, 7);
        }
        else
        {
            EditorGUILayout.LabelField("-- UNKNOWN PARAMETER --");
        }

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();

        //if (_repaint)
        //    EditorUtility.SetDirty(target);
    }

    DriverType GetDriverType(DriveableAvatarParameter param)
    {
        switch(param)
        {
            case DriveableAvatarParameter.GestureLeft: return DriverType.GESTURE;
            case DriveableAvatarParameter.GestureRight: return DriverType.GESTURE;
            case DriveableAvatarParameter.GestureLeftWeight: return DriverType.CLAMP01_FLOAT;
            case DriveableAvatarParameter.GestureRightWeight: return DriverType.CLAMP01_FLOAT;

            case DriveableAvatarParameter.Stage1:
            case DriveableAvatarParameter.Stage2:
            case DriveableAvatarParameter.Stage3:
            case DriveableAvatarParameter.Stage4:
            case DriveableAvatarParameter.Stage5:
            case DriveableAvatarParameter.Stage6:
            case DriveableAvatarParameter.Stage7:
            case DriveableAvatarParameter.Stage8:
            case DriveableAvatarParameter.Stage9:
            case DriveableAvatarParameter.Stage10:
            case DriveableAvatarParameter.Stage11:
            case DriveableAvatarParameter.Stage12:
            case DriveableAvatarParameter.Stage13:
            case DriveableAvatarParameter.Stage14:
            case DriveableAvatarParameter.Stage15:
            case DriveableAvatarParameter.Stage16: return DriverType.BYTE_OR_SIGNED_FLOAT;
        }

        return DriverType.UNKNOWN;
    }*/
}
#endif
