using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteMapping", order = 1)]
public class SpriteMapping : ScriptableObject
{
	public NPCDialogueData[] data;
	
	[System.Serializable]
	public class NPCDialogueData
	{
		public NPCName Name;
		public string DisplayName = "???";
		public Sprite Sprite;

	}

	public NPCDialogueData GetNPCData(NPCName name)
	{
		foreach(NPCDialogueData npc in data)
		{
			if(npc.Name.Equals(name))
			{
				return npc;
			}
		}

		Debug.LogError("[NPCData] - Cannot find Sprite for NPC with id: " + name);

		return null;
	}
}
