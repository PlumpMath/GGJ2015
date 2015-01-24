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
	private Player m_player;
	[SerializeField]
	private TapType m_type;
	[SerializeField]
	private bool m_isEnabled;

	public Signal OnTriggerAction = new Signal(typeof(PlayerButton));
	
	private void OnClicked ()
	{
		this.OnTriggerAction.Invoke(this);
	}

	public Player Player 
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
		set { m_isEnabled = value; }
	}
}
