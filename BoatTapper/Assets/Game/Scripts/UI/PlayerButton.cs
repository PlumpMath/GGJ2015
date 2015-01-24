using UnityEngine;
using System.Collections;

public enum Player
{
	Player1,
	Player2,
};

public class PlayerButton : MonoBehaviour 
{
	[SerializeField]
	private Side m_player;
	[SerializeField]
	private TapType m_type;
	[SerializeField]
	private bool m_isEnabled;
	private tk2dSprite m_sprite;

	public Signal OnTriggerAction = new Signal(typeof(PlayerButton));

	private void Awake ()
	{
		m_sprite = this.GetComponent<tk2dSprite>();
	}

	private void OnClicked ()
	{
		this.OnTriggerAction.Invoke(this);
	}

	public Side Player 
	{
		get { return m_player; }
		private set { m_player = value; }
	}

	public TapType TapType 
	{
		get { return m_type; }
		private set { m_type = value; }
	}

	public bool IsEnabled 
	{
		get { return m_isEnabled; }
		set 
		{ 
			m_isEnabled = value;

			if (!m_isEnabled)
			{
				m_sprite.color = Color.gray;
			}
			else
			{
				m_sprite.color = Color.white;
			}
		}
	}
}
