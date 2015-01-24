using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private List<PlayerButton> m_buttons;
	private Dictionary<TapType, Side> m_abilities = new Dictionary<TapType, Side>()
	{
		{ TapType.Hammer, Side.Left },
		{ TapType.Move, Side.Left },
		{ TapType.Pail, Side.Left },
		{ TapType.Stitch, Side.Left },
	};

	private bool m_muted = false, m_paused = false;

	public static UIManager Instance { get; private set; }
	
	private void Awake () 
	{
		if(UIManager.Instance == null)
			UIManager.Instance = this;
	}


	private void Start ()
	{
		this.Assert<List<PlayerButton>>(m_buttons, "ERROR: m_buttons is null!");
		this.UpdateButton(this.Buttons(Side.Left), true);
		this.UpdateButton(this.Buttons(Side.Right), false);
	}

	public Side HasAbility (TapType p_type)
	{
		return m_abilities[p_type];
	}

	public bool HasAbility (TapType p_type, Side p_player)
	{
		return this.HasAbility(p_type) == p_player;
	}

	[Signal]
	private void OnPressedButton (PlayerButton p_button)
	{
		this.Log("UIManager::OnPressedButton", "Player:{0} Action:{1}", p_button.Player, p_button.TapType);

		if (!p_button.IsEnabled) { return; }

		p_button.IsEnabled = false;

		Side otherPlayerId = p_button.Player == Side.Left ? Side.Right : Side.Left;
		PlayerButton otherPlayer = this.Button(otherPlayerId, p_button.TapType);
		otherPlayer.IsEnabled = true;

		m_abilities[p_button.TapType] = otherPlayerId;
	}

	private List<PlayerButton> Buttons (Side p_player)
	{
		Predicate<PlayerButton> playerButtons = new Predicate<PlayerButton>(b => b.Player == p_player);
		return m_buttons.FindAll(playerButtons);
	}

	private List<PlayerButton> Buttons (TapType p_type)
	{
		Predicate<PlayerButton> typeButtons = new Predicate<PlayerButton>(b => b.TapType == p_type);
		return m_buttons.FindAll(typeButtons);
	}

	private PlayerButton Button (Side p_player, TapType p_type)
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

	public bool Paused { get { return m_paused; } }

	public void TogglePaused()
	{
		if(GameLogic.Instance && GameLogic.Instance != null && GameLogic.Instance.GameHasStarted)
		{
			m_paused = !m_paused;
			GameLogic.Instance.UpdatePaused(m_paused);
		}
	}

	public void ToggleMuted ()
	{
		m_muted = !m_muted;
		AudioListener listener = FindObjectOfType<AudioListener>();
		if(listener)
			listener.enabled = !m_muted;
	}
}

