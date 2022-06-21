using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDefender : MonoBehaviour, Selectable
{
	public SlotManager Manager;
	public string Name;

	[SerializeField] private GameObject _highlight;
	[SerializeField] private Button _button;

	private void Awake()
	{
		_highlight.SetActive(false);
		_button.onClick.AddListener(OnSelected);
	}

	public void Init(SlotManager manager)
	{
		Manager = manager;
	}

	public void OnSelected()
	{
		//TODO: Implement!
		//Show highlight effects!
		Manager.OnSelectableClicked(this);
		_highlight.SetActive(true);
	}

	public void OnDeselect()
	{
		//TODO: Implement!
		_highlight.SetActive(false);
	}
}
