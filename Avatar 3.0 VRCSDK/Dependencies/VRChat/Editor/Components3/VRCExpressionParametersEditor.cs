using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ExpressionParameters = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;
using ExpressionParameter = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters.Parameter;
using VRC.SDK3.Avatars.ScriptableObjects;

[CustomEditor(typeof(VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters))]
public class VRCExpressionParametersEditor : Editor
{
	public void OnEnable()
	{
		//Init parameters
		var expressionParameters = target as ExpressionParameters;
		if (expressionParameters.parameters == null || expressionParameters.parameters.Length < ExpressionParameters.MAX_PARAMETERS)
			InitExpressionParameters(true);

	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		{
			EditorGUILayout.LabelField("Parameters");
			EditorGUI.indentLevel += 1;
			{
				//Draw parameters
				var parameters = serializedObject.FindProperty("parameters");
				for (int i = 0; i < ExpressionParameters.MAX_PARAMETERS; i++)
					DrawExpressionParameter(parameters, i);

				//Info
				EditorGUILayout.HelpBox("Only parameters defined here can be used by expression menus, sync between all playable layers and sync across the network to remote clients.", MessageType.Info);
				EditorGUILayout.HelpBox("The parameter name and type should match a parameter defined on one or more of your animation controllers.", MessageType.Info);
				EditorGUILayout.HelpBox("Parameters used by the default animation controllers (Optional)\nVRCEmote, Int\nVRCFaceBlendH, Float\nVRCFaceBlendV, Float", MessageType.Info);

				//Clear
				if (GUILayout.Button("Clear Parameters"))
				{
					if (EditorUtility.DisplayDialogComplex("Warning", "Are you sure you want to clear all expression parameters?", "Clear", "Cancel", "") == 0)
					{
						InitExpressionParameters(false);
					}
				}
				if (GUILayout.Button("Default Parameters"))
				{
					if (EditorUtility.DisplayDialogComplex("Warning", "Are you sure you want to reset all expression parameters to default?", "Reset", "Cancel", "") == 0)
					{
						InitExpressionParameters(true);
					}
				}
			}
			EditorGUI.indentLevel -= 1;
		}
		serializedObject.ApplyModifiedProperties();
	}
	void DrawExpressionParameter(SerializedProperty parameters, int index)
	{
		if (parameters.arraySize < index + 1)
			parameters.InsertArrayElementAtIndex(index);
		var item = parameters.GetArrayElementAtIndex(index);

		var name = item.FindPropertyRelative("name");
		var valueType = item.FindPropertyRelative("valueType");

		EditorGUI.indentLevel += 1;
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField(string.Format("Parameter {0}", index + 1), GUILayout.MaxWidth(128));
			EditorGUILayout.PropertyField(name, new GUIContent(""));
			EditorGUILayout.PropertyField(valueType, new GUIContent(""));
		}
		EditorGUILayout.EndHorizontal();
		EditorGUI.indentLevel -= 1;
	}
	void InitExpressionParameters(bool populateWithDefault)
	{
		var expressionParameters = target as ExpressionParameters;
		serializedObject.Update();
		{
			expressionParameters.parameters = new ExpressionParameter[ExpressionParameters.MAX_PARAMETERS];
			for (int i = 0; i < ExpressionParameters.MAX_PARAMETERS; i++)
			{
				expressionParameters.parameters[i] = new ExpressionParameter();
				expressionParameters.parameters[i].name = "";
				expressionParameters.parameters[i].valueType = ExpressionParameters.ValueType.Int;
			}

			if (populateWithDefault)
			{
				expressionParameters.parameters[0].name = "VRCEmote";
				expressionParameters.parameters[0].valueType = ExpressionParameters.ValueType.Int;

				expressionParameters.parameters[1].name = "VRCFaceBlendH";
				expressionParameters.parameters[1].valueType = ExpressionParameters.ValueType.Float;

				expressionParameters.parameters[2].name = "VRCFaceBlendV";
				expressionParameters.parameters[2].valueType = ExpressionParameters.ValueType.Float;
			}
		}
		serializedObject.ApplyModifiedProperties();
	}
}