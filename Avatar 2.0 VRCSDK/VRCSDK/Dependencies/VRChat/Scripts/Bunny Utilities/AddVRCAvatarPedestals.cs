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
public class AddVRCAvatarPedestals : EditorWindow
{
	public class CustomMeterialsByIDClass
	{
		public struct CustomMeterialsByIDStruct
		{
			public string IDs;
			public List<string> ListOfIDs;
			public Material M;
		}

		public List<CustomMeterialsByIDStruct> List=new List<CustomMeterialsByIDStruct>();
		CustomMeterialsByIDStruct Dummy=new CustomMeterialsByIDStruct();
		public int DummyInt=0;

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

		string Left(string S, int i)
		{
			if (S.Length <= i)
			{
				return S;
			}

			return S.Substring(0, i);
		}

		string Right(string S, int i)
		{
			if (i >= S.Length)
			{
				return S;
			}

			return S.Substring(S.Length - i);
		}

		string Mid(string S, int i, int l = 0)
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

		public void SetLength(int i)
		{
			int l;
			List<CustomMeterialsByIDStruct> NewList=new List<CustomMeterialsByIDStruct>();
			CustomMeterialsByIDStruct LocalDummy;

			if(i==List.Count)
			{
				return;
			}

			for(l=0;l<i;l++)
			{
				if(l<List.Count)
				{
					NewList.Add(List[l]);
				}
				else
				{
					LocalDummy=CreateCustomMeterialsByIDStruct();
					NewList.Add(LocalDummy);
				}
			}

			List=NewList;
		}

		public CustomMeterialsByIDStruct CreateCustomMeterialsByIDStruct()
		{
			CustomMeterialsByIDStruct LocalDummy;

			LocalDummy.IDs="";
			LocalDummy.ListOfIDs=new List<string>();
			LocalDummy.ListOfIDs.Clear();
			LocalDummy.M=null;
			return LocalDummy;
		}

		public void ClearDummy()
		{
			Dummy.IDs="";
			Dummy.ListOfIDs=new List<string>();
			Dummy.ListOfIDs.Clear();
			Dummy.M=null;
		}
/*
		public void GetListOfIDs(int l, string DividerSymbol)
		{
			int i;
			string S,SS;

			if(l<0 || l>=List.Count)
			{
				return;
			}

			List[l].ListOfIDs.Clear();
			S=List[l].IDs;

			while(Len(S)>0)
			{
				i=InStr(S,DividerSymbol);

				if(i>=0)
				{
					SS=Left(S,i);
					S=Mid(S,i+1);
				}
				else
				{
					SS=S;
					S="";
				}

				List[l].ListOfIDs.Add(SS);
			}
		}
*/
		public void GetListOfIDs(int l, string DividerSymbol)
		{
			List<int> i;
			string S,SS;
			int n,m=0;

			if(l<0 || l>=List.Count)
			{
				return;
			}

			List[l].ListOfIDs.Clear();
			S=List[l].IDs;

			if(S!="")
			{
				i=InStrList(S,DividerSymbol);

				if(i==null || i.Count==0)
				{
					List[l].ListOfIDs.Add(S);
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
						List[l].ListOfIDs.Add(SS);
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
					List[l].ListOfIDs.Add(SS);
				}
			}
		}

		public void SetListVars(int i, string IDs, Material M)
		{
			if(i<0 || i>=List.Count)
			{
				return;
			}

			ClearDummy();
			Dummy.IDs=IDs;
			Dummy.ListOfIDs=List[i].ListOfIDs;
			Dummy.M=M;
			List[i]=Dummy;
		}
	}

	public bool bHideWarning=false;
	public GameObject SelectedObject,ParentObject;
	public bool bKeepParentObject=true;
	public string AvatarIDs,PreviousAvatarIDs;
	public string DividerSymbol;
	public List<string> ListOfAvatarIDs=new List<string>();
	public Vector3 StartPosition=new Vector3(),OffsetByIndex=new Vector3(-1,0,0),OffsetByRow=new Vector3(0,0,-1),OffsetByReset=new Vector3(-11,0,0),OffsetByReset2=new Vector3(0,1,0);
	public string UsedName="CustomVRCAvatarPedestal_%Index%",UsedTag="CreatedAutomaticalyVRCAvatarPedestal",CustomMessage;
	public int LengthOfRow=10,MaxRows=0,MaxResets=0;
	public bool bDefaultHide,bActivateByTrigger,bLocalActivateByTrigger,bDifferentTriggersByRow,bCreateTriggers;
	public List<GameObject> ObjectsWithTrigger=new List<GameObject>();
	public List<GameObject> PendingAvatarPedestals=new List<GameObject>();
	public CustomMeterialsByIDClass CustomMeterialsByID=new CustomMeterialsByIDClass();
	public bool bWorldPositionStays=true;
	public bool bUseFullSearch;
	Vector2 MainScroll=Vector2.zero;

	[MenuItem("Phoenix/Bunny/AddVRCAvatarPedestals")]
	static void Init()
	{
		AddVRCAvatarPedestals window=(AddVRCAvatarPedestals)EditorWindow.GetWindow(typeof(AddVRCAvatarPedestals));
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

	string Left(string S, int i)
	{
		if (S.Length <= i)
		{
			return S;
		}

		return S.Substring(0, i);
	}

	string Right(string S, int i)
	{
		if (i >= S.Length)
		{
			return S;
		}

		return S.Substring(S.Length - i);
	}

	string Mid(string S, int i, int l = 0)
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

	public void OnGUI()
	{
		string S;
		int i,l;
		Material M;

		MainScroll=EditorGUILayout.BeginScrollView(MainScroll);

		if(!bHideWarning)
		{
			EditorGUILayout.LabelField("Warning! Don't Forget To Create Tag Before Using This Script");
			EditorGUILayout.LabelField("How To Do That:");
			EditorGUILayout.LabelField("1. Press On Any Object");
			EditorGUILayout.LabelField("2. Press On Tag");
			EditorGUILayout.LabelField("3. Press \"Add Tag...\"");
			EditorGUILayout.LabelField("4. Press \"+\"");
			EditorGUILayout.LabelField("5. Enter Tag Name And Press Save");
		}

		bHideWarning=EditorGUILayout.Toggle("Hide Warning", bHideWarning);
		SelectedObject=(GameObject)EditorGUILayout.ObjectField("Avatar Pedestal", SelectedObject, typeof(GameObject), true);
		EditorGUILayout.BeginHorizontal();
		bKeepParentObject=EditorGUILayout.Toggle("Keep Parent Object", bKeepParentObject);

		if(!bKeepParentObject)
		{
			ParentObject=(GameObject)EditorGUILayout.ObjectField("Parent Object", ParentObject, typeof(GameObject), true);
		}

		EditorGUILayout.EndHorizontal();
		AvatarIDs=EditorGUILayout.TextField("Avatar IDs", AvatarIDs);
		CustomMeterialsByID.DummyInt=EditorGUILayout.IntField("Custom Meterials Length", CustomMeterialsByID.DummyInt);
		CustomMeterialsByID.SetLength(CustomMeterialsByID.DummyInt);

		for(i=0;i<CustomMeterialsByID.List.Count;i++)
		{
			EditorGUILayout.BeginHorizontal();
			S=EditorGUILayout.TextField("IDs", CustomMeterialsByID.List[i].IDs);
			M=(Material)EditorGUILayout.ObjectField("Material", CustomMeterialsByID.List[i].M, typeof(Material), true);
			CustomMeterialsByID.SetListVars(i,S,M);

			if(GUILayout.Button("Get List"))
			{
				CustomMeterialsByID.GetListOfIDs(i,DividerSymbol);
			}

			EditorGUILayout.LabelField("IDs Count: "+CustomMeterialsByID.List[i].ListOfIDs.Count);
			EditorGUILayout.EndHorizontal();
		}

		DividerSymbol=EditorGUILayout.TextField("Divider Symbol", DividerSymbol);
		UsedName=EditorGUILayout.TextField("Used Name", UsedName);
		UsedTag=EditorGUILayout.TextField("Used Tag", UsedTag);
		EditorGUILayout.BeginHorizontal();
		StartPosition.x=EditorGUILayout.FloatField("StartPosition.X", StartPosition.x);
		StartPosition.y=EditorGUILayout.FloatField("StartPosition.Y", StartPosition.y);
		StartPosition.z=EditorGUILayout.FloatField("StartPosition.Z", StartPosition.z);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		OffsetByIndex.x=EditorGUILayout.FloatField("OffsetByIndex.X", OffsetByIndex.x);
		OffsetByIndex.y=EditorGUILayout.FloatField("OffsetByIndex.Y", OffsetByIndex.y);
		OffsetByIndex.z=EditorGUILayout.FloatField("OffsetByIndex.Z", OffsetByIndex.z);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		OffsetByRow.x=EditorGUILayout.FloatField("OffsetByRow.X", OffsetByRow.x);
		OffsetByRow.y=EditorGUILayout.FloatField("OffsetByRow.Y", OffsetByRow.y);
		OffsetByRow.z=EditorGUILayout.FloatField("OffsetByRow.Z", OffsetByRow.z);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		OffsetByReset.x=EditorGUILayout.FloatField("OffsetByReset.X", OffsetByReset.x);
		OffsetByReset.y=EditorGUILayout.FloatField("OffsetByReset.Y", OffsetByReset.y);
		OffsetByReset.z=EditorGUILayout.FloatField("OffsetByReset.Z", OffsetByReset.z);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		OffsetByReset2.x=EditorGUILayout.FloatField("OffsetByReset2.X", OffsetByReset2.x);
		OffsetByReset2.y=EditorGUILayout.FloatField("OffsetByReset2.Y", OffsetByReset2.y);
		OffsetByReset2.z=EditorGUILayout.FloatField("OffsetByReset2.Z", OffsetByReset2.z);
		EditorGUILayout.EndHorizontal();
		LengthOfRow=EditorGUILayout.IntField("Length Of Row", LengthOfRow);
		MaxRows=EditorGUILayout.IntField("Max Rows", MaxRows);
		MaxResets=EditorGUILayout.IntField("Max Resets", MaxResets);

		if(bKeepParentObject || (ParentObject!=null && ParentObject.transform!=null))
		{
			bWorldPositionStays=EditorGUILayout.Toggle("World Position Stays", bWorldPositionStays);
		}

		bDefaultHide=EditorGUILayout.Toggle("Default Hide", bDefaultHide);
		EditorGUILayout.BeginHorizontal();
		bActivateByTrigger=EditorGUILayout.Toggle("Activate By Trigger", bActivateByTrigger);
		bLocalActivateByTrigger=EditorGUILayout.Toggle("Local", bLocalActivateByTrigger);
		bDifferentTriggersByRow=EditorGUILayout.Toggle("Different Triggers By Row", bDifferentTriggersByRow);
		bCreateTriggers=EditorGUILayout.Toggle("Create Triggers", bCreateTriggers);
		EditorGUILayout.EndHorizontal();
		l=EditorGUILayout.IntField("Objects With Trigger Length", ObjectsWithTrigger.Count);

		while(l<ObjectsWithTrigger.Count)
		{
			ObjectsWithTrigger.RemoveAt(ObjectsWithTrigger.Count-1);
		}

		while(l>ObjectsWithTrigger.Count)
		{
			ObjectsWithTrigger.Add(null);
		}

		for(i=0;i<ObjectsWithTrigger.Count;i++)
		{
			ObjectsWithTrigger[i]=(GameObject)EditorGUILayout.ObjectField("Object With Trigger", ObjectsWithTrigger[i], typeof(GameObject), true);
		}

		if(GUILayout.Button("Add Trigger"))
		{
			AddTrigger();
		}

		if(GUILayout.Button("Remove Wrong Variables In Trigger"))
		{
			RemoveWrongVariablesInTrigger();
		}

		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Get List Of Avatars"))
		{
			GetListOfAvatarIDs();
		}

		if(GUILayout.Button("Get List Of Avatars And Remove Copies"))
		{
			GetListOfAvatarIDsAndRemoveCopies();
		}

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.LabelField("Avatars Count: "+ListOfAvatarIDs.Count);

		if(GUILayout.Button("Add"))
		{
			AddAvatarPedestals();
		}

		bUseFullSearch=EditorGUILayout.Toggle("Use Full Search", bUseFullSearch);

		if(GUILayout.Button("Destroy Objects By Tag"))
		{
			DestroyObjectsByTag();
		}

		if(CustomMessage!="")
		{
			EditorGUILayout.LabelField(CustomMessage);
		}

		EditorGUILayout.EndScrollView();
	}

	public bool RemoveWrongVariablesInTrigger()
	{
		bool b=true;
		int i;

		for(i=0;i<ObjectsWithTrigger.Count;i++)
		{
			if(!RemoveWrongVariablesInTrigger(ObjectsWithTrigger[i]))
			{
				b=false;
			}
		}

		return b;
	}

	public bool RemoveWrongVariablesInTrigger(GameObject MyObjectWithTrigger)
	{
		VRC_Trigger MyTriggerScript;
		List<VRC_Trigger.TriggerEvent> TriggerEvents=null;
		List<VRC_EventHandler.VrcEvent> MyEvents;
		int i,l,m,n,k;

		if(MyObjectWithTrigger==null)
		{
			CustomMessage="Select \"Objects With Trigger\" To Remove Wrong Variables";
			return false;
		}

		MyTriggerScript=MyObjectWithTrigger.GetComponent<VRC_Trigger>();

		if(MyTriggerScript==null)
		{
			CustomMessage="Object Does Not Contain A Trigger";
			return false;
		}

		TriggerEvents=MyTriggerScript.Triggers;

		if(TriggerEvents!=null)
		{
			for(k=0;k<TriggerEvents.Count;k++)
			{
				MyEvents=TriggerEvents[k].Events;

				if(MyEvents.Count==0)
				{
					TriggerEvents.RemoveAt(k);
					k--;
				}

				if(MyEvents!=null)
				{
					for(i=0;i<MyEvents.Count;i++)
					{
						n=0;

						for(l=MyEvents[i].ParameterObjects.Length-1;l>=0;l--)
						{
							if(MyEvents[i].ParameterObjects[l]==null)
							{
								for(m=l;m<MyEvents[i].ParameterObjects.Length-1;m++)
								{
									MyEvents[i].ParameterObjects[m]=MyEvents[i].ParameterObjects[m+1];
								}

								n++;
							}
						}

						if(n>0)
						{
							n=MyEvents[i].ParameterObjects.Length-n;
							Array.Resize(ref MyEvents[i].ParameterObjects,n);

							if(MyEvents[i].ParameterObjects.Length==0)
							{
								MyEvents.RemoveAt(i);
								i--;

								if(MyEvents.Count==0)
								{
									TriggerEvents.RemoveAt(k);
									k--;
								}
							}
						}

						if(MyEvents==null)
						{
							break;
						}
					}
				}
			}
		}

		return true;
	}

	public void AddAvatarPedestals()
	{
		GameObject MyObject,MyParentObject=null;
		VRC_AvatarPedestal MyAvatarScript;
		Vector3 V=new Vector3();
		int i,l,m,n,k,CurrentRow=0,CurrentReset=0,CurrentReset2=0;
		MeshRenderer[] MyMeshRenderers;
		Material[] MyMaterials;

		if(SelectedObject==null)
		{
			CustomMessage="You've To Select Avatar Pedestal";
			return;
		}

		MyAvatarScript=SelectedObject.GetComponent<VRC_AvatarPedestal>();

		if(MyAvatarScript==null)
		{
			CustomMessage="VRC_AvatarPedestal Script Not Found. Select Correct Avatar Pedestal";
			return;
		}

		if(UsedName=="" || UsedTag=="")
		{
			CustomMessage="You've To Set Used Name And Used Tag";
			return;
		}

		CustomMessage="";
		PendingAvatarPedestals.Clear();

		for(i=0;i<ListOfAvatarIDs.Count;i++)
		{
			if(i>0 && i%LengthOfRow==0)
			{
				CurrentRow++;

				if(CurrentRow==MaxRows)
				{
					CurrentRow=0;
					CurrentReset++;

					if(CurrentReset==MaxResets)
					{
						CurrentReset=0;
						CurrentReset2++;
					}
				}
			}

			V=StartPosition+(i%LengthOfRow)*OffsetByIndex+OffsetByRow*CurrentRow+OffsetByReset*CurrentReset+OffsetByReset2*CurrentReset2;
			MyObject=Instantiate(SelectedObject, V, SelectedObject.transform.rotation);
			MyObject.name=Repl(UsedName,"%Index%",i.ToString());
			MyObject.tag=UsedTag;
			MyObject.SetActive(!bDefaultHide);

			if(bKeepParentObject)
			{
				if(SelectedObject.transform.parent!=null)
				{
					MyParentObject=SelectedObject.transform.parent.gameObject;
				}
			}
			else
			{
				MyParentObject=ParentObject;
			}

			if(MyParentObject!=null && MyParentObject.transform!=null)
			{
				MyObject.transform.SetParent(MyParentObject.transform,bWorldPositionStays);
			}

			if(CustomMeterialsByID.List.Count>0)
			{
				for(m=0;m<CustomMeterialsByID.List.Count;m++)
				{
					for(n=0;n<CustomMeterialsByID.List[m].ListOfIDs.Count;n++)
					{
						if(CustomMeterialsByID.List[m].ListOfIDs[n]==ListOfAvatarIDs[i])
						{
							MyMeshRenderers=MyObject.GetComponentsInChildren<MeshRenderer>(true);

							for(k=0;k<MyMeshRenderers.Length;k++)
							{
								MyMaterials=MyMeshRenderers[k].materials;

								for(l=0;l<MyMaterials.Length;l++)
								{
									MyMaterials[l]=CustomMeterialsByID.List[m].M;
								}

								MyMeshRenderers[k].materials=MyMaterials;
							}
						}
					}
				}
			}

			MyAvatarScript=MyObject.GetComponent<VRC_AvatarPedestal>();
			MyAvatarScript.blueprintId=ListOfAvatarIDs[i];

			if(bActivateByTrigger)
			{
				PendingAvatarPedestals.Add(MyObject);
			}
		}

		if(bActivateByTrigger)
		{
			AddAvatarPedestalsToTrigger();
		}
	}

	public bool AddTrigger()
	{
		bool b=true;
		int i;
		string S="";

		for(i=0;i<ObjectsWithTrigger.Count;i++)
		{
			if(!AddTrigger(ObjectsWithTrigger[i]))
			{
				b=false;
			}

			if(CustomMessage!="")
			{
				S=CustomMessage;
			}
		}

		if(S!="")
		{
			CustomMessage=S;
		}

		return b;
	}

	public bool AddTrigger(GameObject MyObjectWithTrigger)
	{
		VRC_Trigger MyTriggerScript;

		if(MyObjectWithTrigger==null)
		{
			CustomMessage="Select \"Objects With Trigger\" To Add Trigger To This Object";
			return false;
		}

		MyTriggerScript=MyObjectWithTrigger.GetComponent<VRC_Trigger>();

		if(MyTriggerScript!=null)
		{
			CustomMessage="Object Already Has A Trigger";
			return false;
		}

		MyObjectWithTrigger.AddComponent<VRC_Trigger>();
		CustomMessage="";
		return true;
	}

	public bool AddAvatarPedestalsToTrigger()
	{
		bool b=true;
		string S="";

		if(bCreateTriggers)
		{
			if(bDifferentTriggersByRow)
			{
				if(!AddAvatarPedestalsToTrigger_CreateTriggers(LengthOfRow*MaxRows, LengthOfRow*MaxRows*MaxResets))
				{
					b=false;
				}

				if(CustomMessage!="")
				{
					S=CustomMessage;
				}
			}
			else
			{
				GameObject MyObject=CreateObjectWithTrigger(0,0);

				if(!AddAvatarPedestalsToTrigger(MyObject))
				{
					b=false;
				}

				if(CustomMessage!="")
				{
					S=CustomMessage;
				}
			}
		}
		else
		{
			if(bDifferentTriggersByRow)
			{
				if(!AddAvatarPedestalsToTrigger(LengthOfRow*MaxRows))
				{
					b=false;
				}

				if(CustomMessage!="")
				{
					S=CustomMessage;
				}
			}
			else
			{
				for(int i=0;i<ObjectsWithTrigger.Count;i++)
				{
					if(!AddAvatarPedestalsToTrigger(ObjectsWithTrigger[i]))
					{
						b=false;
					}

					if(CustomMessage!="")
					{
						S=CustomMessage;
					}
				}
			}
		}

		if(S!="")
		{
			CustomMessage=S;
		}

		PendingAvatarPedestals.Clear();
		return b;
	}

	public GameObject CreateObjectWithTrigger(int i, int l)
	{
		GameObject MyParentObject=null;

		if(ObjectsWithTrigger.Count<1)
		{
			CustomMessage="Select First Object With Trigger To Create Copy";
			return null;
		}

		GameObject MySelectedObject=ObjectsWithTrigger[0];

		if(MySelectedObject==null)
		{
			CustomMessage="Select First Object With Trigger To Create Copy";
			return null;
		}

		Vector3 V=StartPosition+OffsetByReset*i+OffsetByReset2*l-OffsetByRow;
		V.y+=MySelectedObject.transform.localScale.y/2;
		GameObject MyObject=Instantiate(MySelectedObject, V, MySelectedObject.transform.rotation);
		MyObject.name=MySelectedObject.name+"_"+i.ToString();
		MyObject.tag=UsedTag;
		MyObject.SetActive(true);

		if(bKeepParentObject)
		{
			if(MySelectedObject.transform.parent!=null)
			{
				MyParentObject=MySelectedObject.transform.parent.gameObject;
			}
		}
		else
		{
			MyParentObject=ParentObject;
		}

		if(MyParentObject!=null && MyParentObject.transform!=null)
		{
			MyObject.transform.SetParent(MyParentObject.transform,bWorldPositionStays);
		}

		CustomMessage="";
		return MyObject;
	}

	public bool AddAvatarPedestalsToTrigger_CreateTriggers(int i, int n)
	{
		List<GameObject> MyAvatarPedestals=new List<GameObject>();
		int m=0, o=0;
		bool b=true;
		string S="";
		GameObject MyObjectWithTrigger;

		for(int l=0;l<PendingAvatarPedestals.Count;l++)
		{
			if(l>0 && l%i==0)
			{
				if(l%n==0)
				{
					m=0;
					o++;
				}

				MyObjectWithTrigger=CreateObjectWithTrigger(m, o);

				if(CustomMessage!="")
				{
					S=CustomMessage;
				}

				if(MyObjectWithTrigger!=null)
				{
					if(!AddAvatarPedestalsToTrigger(MyObjectWithTrigger, MyAvatarPedestals))
					{
						b=false;
					}

					if(CustomMessage!="")
					{
						S=CustomMessage;
					}

					MyAvatarPedestals=new List<GameObject>();
					m++;
				}
				else
				{
					b=false;
					MyAvatarPedestals=new List<GameObject>();
					break;
				}
			}

			MyAvatarPedestals.Add(PendingAvatarPedestals[l]);
		}

		if(MyAvatarPedestals.Count>0)
		{
			MyObjectWithTrigger=CreateObjectWithTrigger(m, o);

			if(CustomMessage!="")
			{
				S=CustomMessage;
			}

			if(MyObjectWithTrigger!=null)
			{
				if(!AddAvatarPedestalsToTrigger(MyObjectWithTrigger, MyAvatarPedestals))
				{
					b=false;
				}

				if(CustomMessage!="")
				{
					S=CustomMessage;
				}
			}
			else
			{
				b=false;
			}
		}

		if(S!="")
		{
			CustomMessage=S;
		}

		return b;
	}

	public bool AddAvatarPedestalsToTrigger(int i)
	{
		if(ObjectsWithTrigger.Count==0)
		{
			return true;
		}

		if(ObjectsWithTrigger.Count==1)
		{
			return AddAvatarPedestalsToTrigger(ObjectsWithTrigger[0]);
		}

		List<GameObject> MyAvatarPedestals=new List<GameObject>();
		int m=0;
		bool b=true;
		string S="";

		for(int l=0;l<PendingAvatarPedestals.Count;l++)
		{
			if(l>0 && l%i==0)
			{
				if(m<ObjectsWithTrigger.Count-1)
				{
					if(!AddAvatarPedestalsToTrigger(ObjectsWithTrigger[m], MyAvatarPedestals))
					{
						b=false;
					}

					if(CustomMessage!="")
					{
						S=CustomMessage;
					}

					MyAvatarPedestals=new List<GameObject>();
					m++;
				}
			}

			MyAvatarPedestals.Add(PendingAvatarPedestals[l]);
		}

		if(MyAvatarPedestals.Count>0 && ObjectsWithTrigger.Count>=m)
		{
			if(!AddAvatarPedestalsToTrigger(ObjectsWithTrigger[m], MyAvatarPedestals))
			{
				b=false;
			}

			if(CustomMessage!="")
			{
				S=CustomMessage;
			}
		}

		if(S!="")
		{
			CustomMessage=S;
		}

		return b;
	}

	public bool AddAvatarPedestalsToTrigger(GameObject MyObjectWithTrigger)
	{
		return AddAvatarPedestalsToTrigger(MyObjectWithTrigger, PendingAvatarPedestals);
	}

	public bool AddAvatarPedestalsToTrigger(GameObject MyObjectWithTrigger, List<GameObject> MyAvatarPedestals)
	{
		VRC_Trigger MyTriggerScript;
		List<VRC_Trigger.TriggerEvent> TriggerEvents;
		VRC_Trigger.TriggerEvent MyTriggerEvent;
		List<VRC_EventHandler.VrcEvent> MyEvents;
		VRC_EventHandler.VrcEvent MyEvent;

		if(MyObjectWithTrigger==null)
		{
			CustomMessage="Select Objects With Trigger If You Want To Toggle Avatar Pedestals By Pressing Button";
			return false;
		}

		MyTriggerScript=MyObjectWithTrigger.GetComponent<VRC_Trigger>();

		if(MyTriggerScript==null)
		{
			CustomMessage="Object Does Not Contain A Trigger";
			return false;
		}

		MyTriggerScript.interactText="Avatars";
		TriggerEvents=MyTriggerScript.Triggers;
		MyTriggerEvent=new VRC_Trigger.TriggerEvent();
		MyTriggerEvent.TriggerType=VRC_Trigger.TriggerType.OnInteract;

		if(bLocalActivateByTrigger)
		{
			MyTriggerEvent.BroadcastType=VRC_EventHandler.VrcBroadcastType.Local;
		}

		MyEvents=MyTriggerEvent.Events;
		MyEvent=new VRC_EventHandler.VrcEvent();
		MyEvent.EventType=VRC_EventHandler.VrcEventType.SetGameObjectActive;
		MyEvent.ParameterObjects=MyAvatarPedestals.ToArray();
		MyEvent.ParameterBoolOp=VRC_EventHandler.VrcBooleanOp.Toggle;
		MyEvents.Add(MyEvent);
		TriggerEvents.Add(MyTriggerEvent);
		return true;
	}
/*
	public void GetListOfAvatarIDs()
	{
		int i;
		string S,SS;

		ListOfAvatarIDs.Clear();
		S=AvatarIDs;

		while(Len(S)>0)
		{
			i=InStr(S,DividerSymbol);

			if(i>=0)
			{
				SS=Left(S,i);
				S=Mid(S,i+Len(DividerSymbol));
			}
			else
			{
				SS=S;
				S="";
			}

			ListOfAvatarIDs.Add(SS);
		}
	}
*/
	public void GetListOfAvatarIDs()
	{
		List<int> i;
		string S,SS;
		int l,m=0;

		ListOfAvatarIDs.Clear();
		S=AvatarIDs;

		if(S!="")
		{
			i=InStrList(S,DividerSymbol);

			if(i==null || i.Count==0)
			{
				ListOfAvatarIDs.Add(S);
				return;
			}

			for(l=0;l<i.Count;l++)
			{
				SS="";

				while(m<=i[l])
				{
					SS+=S[m];
					m++;
				}

				if(SS!="")
				{
					ListOfAvatarIDs.Add(SS);
				}

				m=i[l]+Len(DividerSymbol)+1;
			}

			SS="";

			while(m<Len(S))
			{
				SS+=S[m];
				m++;
			}

			if(SS!="")
			{
				ListOfAvatarIDs.Add(SS);
			}
		}
	}

	public void UpdateListOfAvatars()
	{
		string S="";

		for(int i=0;i<ListOfAvatarIDs.Count;i++)
		{
			if(S!="")
			{
				S+="|";
			}

			S+=ListOfAvatarIDs[i];
		}

		AvatarIDs=S;
	}

	public void GetListOfAvatarIDsAndRemoveCopies()
	{
		GetListOfAvatarIDs();

		for(int i=0;i<ListOfAvatarIDs.Count;i++)
		{
			bool bRemoved=false;

			for(int l=0;l<i;l++)
			{
				if(ListOfAvatarIDs[i]==ListOfAvatarIDs[l])
				{
					bRemoved=true;
					ListOfAvatarIDs.RemoveAt(i);
					break;
				}
			}

			if(bRemoved)
			{
				i--;
			}
		}

		UpdateListOfAvatars();
	}

	public GameObject[] FindChildrenObjectsByTag(GameObject GO, string S)
	{
		List<GameObject> Result=new List<GameObject>();
		GameObject[] FoundChildrenObjects;
		GameObject CurrentObject;

		for(int i=0; i<GO.transform.childCount; i++)
		{
			CurrentObject=GO.transform.GetChild(i).gameObject;

			if(CurrentObject.tag==S)
			{
				Result.Add(CurrentObject);
			}
			else
			{
				FoundChildrenObjects=FindChildrenObjectsByTag(CurrentObject,S);

				foreach(GameObject FoundChildrenObject in FoundChildrenObjects)
				{
					Result.Add(FoundChildrenObject);
				}
			}
		}

		return Result.ToArray();
	}

	public GameObject[] FindSceneGameObjectsByTag(string S, bool bFullSearch)
	{
		List<GameObject> Result=new List<GameObject>();
		Scene MyScene=SceneManager.GetActiveScene();
		List<GameObject> FoundObjects=new List<GameObject>();
		GameObject[] FoundChildrenObjects;
		MyScene.GetRootGameObjects(FoundObjects);

		foreach(GameObject FoundObject in FoundObjects)
		{
			if(FoundObject.tag==S)
			{
				Result.Add(FoundObject);
			}
			else if(bFullSearch)
			{
				FoundChildrenObjects=FindChildrenObjectsByTag(FoundObject,S);

				foreach(GameObject FoundChildrenObject in FoundChildrenObjects)
				{
					Result.Add(FoundChildrenObject);
				}
			}
		}

		return Result.ToArray();
	}

	public void DestroyObjectsByTag()
	{
		GameObject[] FoundObjects;

		if(UsedTag=="")
		{
			CustomMessage="You've To Set Used Tag";
			return;
		}

		CustomMessage="";
		FoundObjects=FindSceneGameObjectsByTag(UsedTag,bUseFullSearch);

		foreach(GameObject FoundObject in FoundObjects)
		{
			if(FoundObject!=SelectedObject)
			{
				DestroyObject(FoundObject);
			}
		}
	}

	public bool DestroyObject(GameObject MyGameObject)
	{
		if(MyGameObject==null)
		{
			return false;
		}

		RemoveObjectsInTrigger(MyGameObject);
		DestroyImmediate(MyGameObject);
		return true;
	}

	public bool RemoveObjectsInTrigger(GameObject MyGameObject)
	{
		bool b=true;
		int i;

		for(i=0;i<ObjectsWithTrigger.Count;i++)
		{
			if(!RemoveObjectsInTrigger(MyGameObject,ObjectsWithTrigger[i]))
			{
				b=false;
			}
		}

		return b;
	}

	public bool RemoveObjectsInTrigger(GameObject MyGameObject, GameObject MyObjectWithTrigger)
	{
		VRC_Trigger MyTriggerScript;
		List<VRC_Trigger.TriggerEvent> TriggerEvents=null;
		List<VRC_EventHandler.VrcEvent> MyEvents;
		int i,l,m,n,k;

		if(MyObjectWithTrigger!=null)
		{
			MyTriggerScript=MyObjectWithTrigger.GetComponent<VRC_Trigger>();

			if(MyTriggerScript!=null)
			{
				TriggerEvents=MyTriggerScript.Triggers;
			}
		}

		if(TriggerEvents!=null)
		{
			for(k=0;k<TriggerEvents.Count;k++)
			{
				MyEvents=TriggerEvents[k].Events;

				if(MyEvents==null || MyEvents.Count==0)
				{
					TriggerEvents.RemoveAt(k);
					k--;
				}

				if(MyEvents!=null)
				{
					for(i=0;i<MyEvents.Count;i++)
					{
						n=0;

						for(l=MyEvents[i].ParameterObjects.Length-1;l>=0;l--)
						{
							if(MyEvents[i].ParameterObjects[l]==MyGameObject)
							{
								for(m=l;m<MyEvents[i].ParameterObjects.Length-1;m++)
								{
									MyEvents[i].ParameterObjects[m]=MyEvents[i].ParameterObjects[m+1];
								}

								n++;
							}
						}

						if(n>0)
						{
							n=MyEvents[i].ParameterObjects.Length-n;
							Array.Resize(ref MyEvents[i].ParameterObjects,n);

							if(MyEvents[i].ParameterObjects.Length==0)
							{
								MyEvents.RemoveAt(i);
								i--;

								if(MyEvents.Count==0)
								{
									TriggerEvents.RemoveAt(k);
									k--;
								}
							}
						}

						if(MyEvents==null)
						{
							break;
						}
					}
				}
			}
		}

		return true;
	}
}
#endif
