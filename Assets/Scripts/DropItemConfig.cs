using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public struct DropItemConfig
{
	public Entry[] entries;

	[Serializable]
	public struct Entry
	{
		public DropType dropType;
		public GameObject prefab;
	}

	[CanBeNull]
	public GameObject GetDropByType(DropType dropType)
	{
		for (int i = 0, length = entries.Length; i < length; i++)
		{
			if (entries[i].dropType == dropType)
				return entries[i].prefab;
		}

		return null;
	}
}