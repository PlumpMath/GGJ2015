using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class UIManager : MonoBehaviour 
{
	//public readonly float HIDDEN_Y = 0.10f;
	//public readonly float SHOWN_Y = -0.15f;

	[SerializeField] private List<PlayerButton> m_buttons;
	private Dictionary<TapType, Side> m_abilities = new Dictionary<TapType, Side>()
	{
		{ TapType.Hammer, Side.Left },
		{ TapType.Move, Side.Left },
		{ TapType.Pail, Side.Left },
		{ TapType.Stitch, Side.Left },
	};

	private bool m_muted = false, m_paused = false;
	private bool m_uiIsShown = true;
	private float m_hiddenY;
	private float m_shownY;

	public static UIManager Instance { get; private set; }
	
	private void Awake () 
	{
		if (UIManager.Instance == null) 
		{
			UIManager.Instance = this;
		}
		
		m_shownY = m_buttons[0].transform.position.y;
		m_hiddenY = m_shownY + 0.25f;

		GameLogic.Instance.OnStartGame += StartGame;
		this.SnapHideUI(Side.Left);
		this.SnapHideUI(Side.Right);
		m_uiIsShown = false;
	}
	
	private void Start ()
	{
		this.Assert<List<PlayerButton>>(m_buttons, "ERROR: m_buttons is null!");
		this.UpdateButton(this.Buttons(Side.Left), true);
		this.UpdateButton(this.Buttons(Side.Right), false);
	}

	private void StartGame()
	{
		// animate ui
		this.ShowUI();
	}

	// test
	private IEnumerator AnimateUI ()
	{
		yield return new WaitForEndOfFrame();

		this.Log("UIManager::AnimateUI", "UI:{0}", m_uiIsShown);

		while (true) 
		{
			this.ToggleUI();
			yield return new WaitForSeconds(2.5f);
		}
	}

	public Side HasAbility (TapType p_type)
	{
		return m_abilities[p_type];
	}

	public bool HasAbility (TapType p_type, Side p_player)
	{
		return this.HasAbility(p_type) == p_player;
	}

	public void ToggleUI ()
	{
		this.Log("UIManager::ToggleUI", "UI:{0}", m_uiIsShown);

		m_uiIsShown = !m_uiIsShown;

		if (m_uiIsShown) 
		{
			this.ShowUI();
		} 
		else 
		{
			this.HideUI();
		}
	}

	public void ShowUI ()
	{
		this.StartCoroutine(this.ShowUI(Side.Left));
		this.StartCoroutine(this.ShowUI(Side.Right));
	}

	public void HideUI ()
	{
		this.StartCoroutine(this.HideUI(Side.Left));
		this.StartCoroutine(this.HideUI(Side.Right));
	}

	public IEnumerator ShowUI (Side p_player)
	{
		List<PlayerButton> buttons = this.Buttons(p_player);

//		Debug.LogError("ShowUI ButtonsCount:" + buttons.Count);

		if (p_player == Side.Left) 
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				PlayerButton button = buttons[i];
				Vector3 showmPos = new Vector3(button.transform.position.x, m_shownY);
				iTween.MoveTo(button.gameObject, showmPos, 0.75f);
				yield return new WaitForSeconds(0.10f);
			}
		}
		else 
		{
			for (int i = buttons.Count-1; i >= 0; i--)
			{
				PlayerButton button = buttons[i];
				Vector3 showmPos = new Vector3(button.transform.position.x, m_shownY);
				iTween.MoveTo(button.gameObject, showmPos, 0.75f);
				yield return new WaitForSeconds(0.10f);
			}
		}
	}

	public IEnumerator HideUI (Side p_player)
	{
		List<PlayerButton> buttons = this.Buttons(p_player);

		Debug.LogError("HideUI ButtonsCount:" + buttons.Count);

		if (p_player == Side.Left) 
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				PlayerButton button = buttons[i];
				Vector3 hidePos = new Vector3(button.transform.position.x, m_hiddenY, 0);
				iTween.MoveTo(button.gameObject, hidePos, 0.75f);
				yield return new WaitForSeconds(0.10f);
			}
		}
		else 
		{
			for (int i = buttons.Count-1; i >= 0; i--)
			{
				PlayerButton button = buttons[i];
				Vector3 hidePos = new Vector3(button.transform.position.x, m_hiddenY, 0);
				iTween.MoveTo(button.gameObject, hidePos, 0.75f);
				yield return new WaitForSeconds(0.10f);
			}
		}
	}

	private void SnapHideUI(Side p_player)
	{
		List<PlayerButton> buttons = this.Buttons(p_player);
		
		//Debug.LogError("HideUI ButtonsCount:" + buttons.Count);
		
		for (int i = 0; i < buttons.Count; i++)
		{
				PlayerButton button = buttons[i];
				Vector3 hidePos = new Vector3(button.transform.position.x, m_hiddenY, 0);
				button.transform.position = hidePos;
		}

	}

	public IEnumerator PrepareForBattle()
	{

		yield return null;
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

