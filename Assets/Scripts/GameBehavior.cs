using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class GameBehavior : MonoBehaviour, IManager
{
	public bool showWinScreen = false;
	public string labelText = "Coolect all 3 items wand win yr freedom!";
	public int maxitems = 3;
	public bool showLossScreen = false;

	private int _itemsCollected = 0;
	private string _state;

	public string State
	{
		get { return _state; }
		set { _state = value; }
	}

	public int Items
	{
		get => _itemsCollected;
		set
		{
			_itemsCollected = value;
			if (_itemsCollected >= maxitems)
			{
				labelText = "You`ve found all items";
				showWinScreen = true;
				Time.timeScale = 0;
			}
			else
			{
				labelText = $"You found only {(maxitems - _itemsCollected)}";
			}
				Debug.Log($"Items: {_itemsCollected} collected");
		}
	}

	private int _playerHp = 2;

	public int HP
	{
		get => _playerHp;
		set
		{
			_playerHp = value;
			if (_playerHp <= 0)
			{
				labelText = "You want another life with that?";
				showLossScreen = true;
				Time.timeScale = 0;
			}
			else
			{
				Debug.Log($"Ene,y hits u \tHP: {_playerHp}");
			}
				
		}
	}

	private void Start()
	{
		Initialize();
	}


	public void Initialize()
	{
		_state = "Manager initializide.";
		_state.FancyDebug();
		Debug.Log("State");
	}
	private void OnGUI()
	{
		GUI.Box( new Rect(20,20,150,25), "Player Health: " + _playerHp);
		GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " +  _itemsCollected);
		GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

		if (showWinScreen)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100),
				"You won!"))
			{
				Utilities.RestartLevel(0);
			}
		}
		if (showLossScreen)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100),
				"You lose..."))
			{
				Utilities.RestartLevel();
			}
		}
	}

}
