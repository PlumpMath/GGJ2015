using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private List<PlayerButton> m_buttons;
	private Dictionary<TapType, Player> m_abilities = new Dictionary<TapType, Player>()
	{
		{ TapType.Hammer, Player.Player1 },
		{ TapType.Move, Player.Player1 },
		{ TapType.Pail, Player.Player1 },
		{ TapType.Stitch, Player.Player1 },
	};

	private void Start ()
	{
		this.Assert<List<PlayerButton>>(m_buttons, "ERROR: m_buttons is null!");
		this.UpdateButton(this.Buttons(Player.Player1), true);
		this.UpdateButton(this.Buttons(Player.Player2), false);
	}

	public Player HasAbility (TapType p_type)
	{
		return m_abilities[p_type];
	}

	public bool HasAbility (TapType p_type, Player p_player)
	{
		return this.HasAbility(p_type) == p_player;
	}

	[Signal]
	private void OnPressedButton (PlayerButton p_button)
	{
		this.Log("UIManager::OnPressedButton", "Player:{0} Action:{1}", p_button.Player, p_button.TapType);

		if (!p_button.IsEnabled) { return; }

		p_button.IsEnabled = false;

		Player otherPlayerId = p_button.Player == Player.Player1 ? Player.Player2 : Player.Player1;
		PlayerButton otherPlayer = this.Button(otherPlayerId, p_button.TapType);
		otherPlayer.IsEnabled = true;

		m_abilities[p_button.TapType] = otherPlayerId;
	}

	private List<PlayerButton> Buttons (Player p_player)
	{
		Predicate<PlayerButton> playerButtons = new Predicate<PlayerButton>(b => b.Player == p_player);
		return m_buttons.FindAll(playerButtons);
	}

	private List<PlayerButton> Buttons (TapType p_type)
	{
		Predicate<PlayerButton> typeButtons = new Predicate<PlayerButton>(b => b.TapType == p_type);
		return m_buttons.FindAll(typeButtons);
	}

	private PlayerButton Button (Player p_player, TapType p_type)
	{
		Predicate<PlayerButton> buttons = new Predicate<PlayerButton>(b => b.TapType == p_type && b.Player == p_player);
		return m_buttons.FindAll(buttons)[0];
	}

	private void UpdateButton (List<PlayerButton> p_buttons, bool p_isEnabled)
	{
		foreach (PlayerButton button in p_buttons) 
		{
			button.IsEnabled = p_isEnabled;
		}
	}
}
