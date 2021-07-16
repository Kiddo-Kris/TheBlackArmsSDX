using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
#endif

using System;
using System.Linq;
using VRC.Core;
using VRCSDK2;

#if UNITY_EDITOR
public class GetPropertiesOfObject : EditorWindow
{
	public GameObject SelectedObject;
	public string CustomMessage;
	Vector2 MainScroll=Vector2.zero;
	public List<bool> GUIList_Bool=new List<bool>();

	[MenuItem("Phoenix/Utilities/GetPropertiesOfObject")]
	static void Init()
	{
		GetPropertiesOfObject window=(GetPropertiesOfObject)EditorWindow.GetWindow(typeof(GetPropertiesOfObject));
		window.Show();
	}

	IEnumerator Sleep(double f)
	{
		while(f>0)
		{
			f-=Time.deltaTime;
			yield return null;
		}
	}

	string Eval(bool a, string b, string c)
	{
		if(a)
		{
			return b;
		}

		return c;
	}

	int Min(int i, int l)
	{
		if(i>l)
		{
			return l;
		}

		return i;
	}

	int Max(int i, int l)
	{
		if(i>l)
		{
			return i;
		}

		return l;
	}

	int Len(string S)
	{
		return S.Length;
	}

	public string Left(string S, int i)
	{
		if (S.Length <= i)
		{
			return S;
		}

		return S.Substring(0, i);
	}

	public string Right(string S, int i)
	{
		if (i >= S.Length)
		{
			return S;
		}

		return S.Substring(S.Length - i);
	}

	public string Mid(string S, int i, int l = 0)
	{
		if (l > 0)
		{
			return S.Substring(i, l);
		}

		return S.Substring(i);
	}

	int InStr(string Text, string TextB)
	{
		if(Text=="" || TextB=="")
		{
			return -1;
		}

		int i=0;
		int l=0;

		while(i<Len(Text))
		{
			if(Text[i]==TextB[l])
			{
				if(l==Len(TextB)-1)
				{
					return i-l;
				}

				l++;
			}
			else
			{
				l=0;
			}

			i++;
		}

		return -1;
	}

	List<int> InStrList(string Text, string TextB)
	{
		List<int> m=new List<int>();

		if(Text=="" || TextB=="")
		{
			return m;
		}

		int i=0;
		int l=0;

		while(i<Len(Text))
		{
			if(Text[i]==TextB[l])
			{
				l++;

				if(l==Len(TextB))
				{
					m.Add(i-l);
					l=0;
				}
			}
			else
			{
				l=0;
			}

			i++;
		}

		return m;
	}

	string Repl(string Text, string Replace, string With)
	{
		int i;
		string Input;

		if(Text=="" || Replace=="")
		{
			return Text;
		}

		Input=Text;
		Text="";
		i=InStr(Input, Replace);

		while(i!=-1)
		{
			Text=Text+Left(Input, i)+With;
			Input=Mid(Input, i+Len(Replace));
			i=InStr(Input, Replace);
		}

		return Text=Text+Input;
	}

	public void GetListOfMessages(string S, string DividerSymbol, out List<string> ListS)
	{
		List<int> i;
		string SS;
		int n,m=0;

		ListS=new List<string>();
		ListS.Clear();

		if(S!="")
		{
			i=InStrList(S,DividerSymbol);

			if(i==null || i.Count==0)
			{
				ListS.Add(S);
				return;
			}

			for(n=0;n<i.Count;n++)
			{
				SS="";

				while(m<=i[n])
				{
					SS+=S[m];
					m++;
				}

				if(SS!="")
				{
					ListS.Add(SS);
				}

				m=i[n]+Len(DividerSymbol)+1;
			}

			SS="";

			while(m<Len(S))
			{
				SS+=S[m];
				m++;
			}

			if(SS!="")
			{
				ListS.Add(SS);
			}
		}
	}

	void AddInfo(ref string S, string SS, string SSS)
	{
		if(S==null)
		{
			S="";
		}

		S=Eval(S=="",SS,S+SSS+SS);
	}

	void ShowLayersOfMessage(string S, string SS)
	{
		List<string> ListS;
		int i,l=0,bi=0;
		List<bool> b=new List<bool>();

		GetListOfMessages(S,SS,out ListS);

		for(i=0;i<ListS.Count;i++)
		{
			if(Left(ListS[i],Len("Foldout_Start("))=="Foldout_Start(" && Right(ListS[i],1)==")")
			{
				if(bi>=GUIList_Bool.Count)
				{
					GUIList_Bool.Add(true);
				}

				if(l==0)
				{
					GUIList_Bool[bi]=EditorGUILayout.Foldout(GUIList_Bool[bi], Mid(ListS[i],Len("Foldout_Start("),Len(ListS[i])-Len("Foldout_Start(")-1));
					b.Add(GUIList_Bool[bi]);
				}

				bi++;

				if(l==0 && b.Count>0 && b[b.Count-1])
				{
				}
				else
				{
					l++;
				}

				continue;
			}
			else if(ListS[i]=="Foldout_End")
			{
				if(!b[b.Count-1])
				{
					l--;
				}

				if(l==0)
				{
					b.RemoveAt(b.Count-1);
				}

				continue;
			}

			if(l==0 && (b.Count<=0 || b[b.Count-1]))
			{
				EditorGUILayout.LabelField(ListS[i]);
			}
		}
	}

	public void OnGUI()
	{
		MainScroll=EditorGUILayout.BeginScrollView(MainScroll);
		SelectedObject=(GameObject)EditorGUILayout.ObjectField("Object", SelectedObject, typeof(GameObject), true);

		if(GUILayout.Button("Get Properties"))
		{
			GetProperties(SelectedObject);
		}

		if(CustomMessage!="")
		{
			ShowLayersOfMessage(CustomMessage,"|");
		}

		EditorGUILayout.EndScrollView();
	}

	public bool GetProperties(GameObject GO)
	{
		int i;
		List<Component> ListOfComponents=new List<Component>();
		Transform MyTransform;
		ParticleSystem MyParticleSystem;
		ParticleSystemRenderer MyParticleSystemRenderer;
		ParticleSystem.MainModule MyMainModule;
		ParticleSystem.MinMaxCurve MyMinMaxCurve;
		AnimationCurve MyAnimationCurve;

		if(GO==null)
		{
			CustomMessage="You've To Select Object";
			return false;
		}

		CustomMessage="";
		GUIList_Bool.Clear();
		AddInfo(ref CustomMessage,"Tag: "+GO.tag,"|");
		AddInfo(ref CustomMessage,"Layer: "+GO.layer,"; ");
		GO.GetComponents(ListOfComponents);
		AddInfo(ref CustomMessage,"Count Of Components: "+ListOfComponents.Count,"|");

		for(i=0;i<ListOfComponents.Count;i++)
		{
			AddInfo(ref CustomMessage,"Foldout_Start("+i+". ","| |");

			if(ListOfComponents[i] is Transform)
			{
				MyTransform=(Transform)ListOfComponents[i];

				if(MyTransform!=null)
				{
					AddInfo(ref CustomMessage,"Transform ("+ListOfComponents[i].ToString()+"))","");
					AddInfo(ref CustomMessage,"Position: X="+MyTransform.localPosition.x+"; Y="+MyTransform.localPosition.y+"; Z="+MyTransform.localPosition.z,"|");
					AddInfo(ref CustomMessage,"Global Position: X="+MyTransform.position.x+"; Y="+MyTransform.position.y+"; Z="+MyTransform.position.z,"|");
					AddInfo(ref CustomMessage,"Rotation: X="+MyTransform.localRotation.x+"; Y="+MyTransform.localRotation.y+"; Z="+MyTransform.localRotation.z,"|");
					AddInfo(ref CustomMessage,"Global Rotation: X="+MyTransform.rotation.x+"; Y="+MyTransform.rotation.y+"; Z="+MyTransform.rotation.z,"|");
					AddInfo(ref CustomMessage,"Scale: X="+MyTransform.localScale.x+"; Y="+MyTransform.localScale.y+"; Z="+MyTransform.localScale.z,"|");
					AddInfo(ref CustomMessage,"Global Scale: X="+MyTransform.lossyScale.x+"; Y="+MyTransform.lossyScale.y+"; Z="+MyTransform.lossyScale.z,"|");
				}
			}
			else if(ListOfComponents[i] is ParticleSystem)
			{
				MyParticleSystem=(ParticleSystem)ListOfComponents[i];

				if(MyParticleSystem!=null)
				{
					AddInfo(ref CustomMessage,"ParticleSystem ("+ListOfComponents[i].ToString()+"))","");
					MyMainModule=MyParticleSystem.main;
					AddInfo(ref CustomMessage,"Foldout_Start(Main)","|");
					AddInfo(ref CustomMessage,"Duration: "+MyMainModule.duration,"|");
					AddInfo(ref CustomMessage,"Looping: "+MyMainModule.loop,"|");
					AddInfo(ref CustomMessage,"Prewarm: "+MyMainModule.prewarm,"|");
					MyMinMaxCurve=MyParticleSystem.startDelay;
					AddInfo(ref CustomMessage,"Foldout_Start(MinMaxCurve)","|");
					AddInfo(ref CustomMessage,"Mode: "+MyMinMaxCurve.mode,"|");

					if(MyMinMaxCurve.mode==ParticleSystemCurveMode.Constant)
					{
						AddInfo(ref CustomMessage,"Value: "+MyMinMaxCurve.constant,"|");
					}
					else if(MyMinMaxCurve.mode==ParticleSystemCurveMode.Curve)
					{
						MyAnimationCurve=MyMinMaxCurve.curve;

						if(MyAnimationCurve!=null)
						{
							AddInfo(ref CustomMessage,"Length: "+MyAnimationCurve.length,"|");
							AddInfo(ref CustomMessage,"Foldout_Start(Curve)","|");

							for(i=0;i<MyAnimationCurve.length;i++)
							{
								AddInfo(ref CustomMessage,"Key["+i+"]: Time: "+MyAnimationCurve.keys[i].time,"|");
								AddInfo(ref CustomMessage,"Value: "+MyAnimationCurve.keys[i].value,";");
							}

							AddInfo(ref CustomMessage,"Foldout_End","|");
						}
					}
					else if(MyMinMaxCurve.mode==ParticleSystemCurveMode.TwoCurves)
					{
					}
					else if(MyMinMaxCurve.mode==ParticleSystemCurveMode.TwoConstants)
					{
					}

					AddInfo(ref CustomMessage,"Foldout_End","|");
					AddInfo(ref CustomMessage,"Foldout_End","|");
				}
			}
			else if(ListOfComponents[i] is ParticleSystemRenderer)
			{
				MyParticleSystemRenderer=(ParticleSystemRenderer)ListOfComponents[i];

				if(MyParticleSystemRenderer!=null)
				{
					AddInfo(ref CustomMessage,"ParticleSystemRenderer ("+ListOfComponents[i].ToString()+"))","");
					AddInfo(ref CustomMessage,"Render Mode: "+MyParticleSystemRenderer.renderMode,"|");
					AddInfo(ref CustomMessage,"Material: "+MyParticleSystemRenderer.sharedMaterial,"|");
					AddInfo(ref CustomMessage,"Sort Mode: "+MyParticleSystemRenderer.sortMode,"|");
					AddInfo(ref CustomMessage,"Sorting Fudge: "+MyParticleSystemRenderer.sortingFudge,"|");
					AddInfo(ref CustomMessage,"Alignment: "+MyParticleSystemRenderer.alignment,"|");
					AddInfo(ref CustomMessage,"Pivot: X="+MyParticleSystemRenderer.pivot.x+"; Y="+MyParticleSystemRenderer.pivot.y+"; Z="+MyParticleSystemRenderer.pivot.z,"|");
					AddInfo(ref CustomMessage,"Shadow Casting Mode: "+MyParticleSystemRenderer.shadowCastingMode,"|");
					AddInfo(ref CustomMessage,"Receive Shadows: "+MyParticleSystemRenderer.receiveShadows,"|");
					AddInfo(ref CustomMessage,"Sorting Layer ID: "+MyParticleSystemRenderer.sortingLayerID,"|");
					AddInfo(ref CustomMessage,"Sorting Layer Name: "+MyParticleSystemRenderer.sortingLayerName,"|");
					AddInfo(ref CustomMessage,"Sorting Order: "+MyParticleSystemRenderer.sortingOrder,"|");
					AddInfo(ref CustomMessage,"Use Light Probes: "+MyParticleSystemRenderer.useLightProbes,"|");
					AddInfo(ref CustomMessage,"Reflection Probe Usage: "+MyParticleSystemRenderer.reflectionProbeUsage,"|");
				}
			}
			else if(ListOfComponents[i] is VRC_Trigger)
			{
				VRC_Trigger MyVRC_Trigger=(VRC_Trigger)ListOfComponents[i];

				if(MyVRC_Trigger!=null)
				{
					AddInfo(ref CustomMessage,"VRC_Trigger ("+ListOfComponents[i].ToString()+"))","");

					foreach (VRC.SDKBase.VRC_Trigger.TriggerEvent TE in MyVRC_Trigger.Triggers)
					{
						AddInfo(ref CustomMessage,"Foldout_Start(TriggerType: "+Eval(TE.TriggerType==VRC_Trigger.TriggerType.Custom,TE.TriggerType+" ("+TE.Name+")",TE.TriggerType.ToString())+"; BroadcastType: "+TE.BroadcastType+")","|");
						AddInfo(ref CustomMessage,"AfterSeconds: "+TE.AfterSeconds,"|");

						foreach (VRC.SDKBase.VRC_EventHandler.VrcEvent VRCE in TE.Events)
						{
							AddInfo(ref CustomMessage,"Foldout_Start(EventType: "+VRCE.EventType+")","|");

							for(int l=0;l<VRCE.ParameterObjects.Length;l++)
							{
								GameObject GO2=VRCE.ParameterObjects[l];

								if(!GO2)
								{
									continue;
								}

								AddInfo(ref CustomMessage,"ParameterObjects["+l+"]: "+GO,"|");
							}

							AddInfo(ref CustomMessage,"ParameterBoolOp: "+VRCE.ParameterBoolOp,"|");
							AddInfo(ref CustomMessage,"ParameterString: "+VRCE.ParameterString,"|");
							AddInfo(ref CustomMessage,"ParameterBool: "+VRCE.ParameterBool,"|");
							AddInfo(ref CustomMessage,"ParameterFloat: "+VRCE.ParameterFloat,"|");
							AddInfo(ref CustomMessage,"ParameterInt: "+VRCE.ParameterInt,"|");
							AddInfo(ref CustomMessage,"ParameterObject: "+VRCE.ParameterObject,"|");
							AddInfo(ref CustomMessage,"Foldout_End","|");
						}

						AddInfo(ref CustomMessage,"Foldout_End","|");
					}
				}
			}
			else
			{
				AddInfo(ref CustomMessage,"Error ("+ListOfComponents[i].ToString()+"))","");
			}

			AddInfo(ref CustomMessage,"Foldout_End","|");
		}

		return true;
	}
}
#endif
