using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private List<PlayerButton> m_buttons;

	private void Start ()
	{
	}

	[Signal]
	private void OnPressedButton (PlayerButton p_button)
	{
		this.Log("UIManager::OnPressedButton", "Player:{0} Action:{1}", p_button.Player, p_button.TapType);
	}
}
