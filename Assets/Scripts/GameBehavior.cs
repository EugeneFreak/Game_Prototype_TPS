using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Collections.Generic;

public class GameBehavior : MonoBehaviour, IManager
{
	public bool showWinScreen = false;
	public string labelText = "Coolect all 3 items wand win yr freedom!";
	public int maxitems = 3;
	public bool showLossScreen = false;
	public Stack<string> lootStack = new Stack<string>();
	public delegate void DebugDelegate(string newText);
	public DebugDelegate debug = Print;
	

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
		InventoryList<string> inventoryList = new InventoryList<string>();
		inventoryList.SetItem("Potion");
		Debug.LogFormat(inventoryList.item);
	}


	public void Initialize()
	{
		_state = "Manager initializide.";
		_state.FancyDebug();
		
		lootStack.Push("Sword of the Doom");
		lootStack.Push("HP+");
		lootStack.Push("Golden Key");
		lootStack.Push("Winged Boot");
		lootStack.Push("Mythril Bracers");
		debug(_state);
		LogWithDelegate(debug);

		GameObject player = GameObject.Find("Player");
		PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
		playerBehavior.playerJump += HandlePlayerJump;
	}

	public void HandlePlayerJump()
	{
		debug("Player has jumped...");
	}

	public static void Print(string newText)
	{
		Debug.Log(newText);
	}

	public void LogWithDelegate(DebugDelegate del)
	{
		del("Delegating the debug rask...");
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
				try
				{
					Utilities.RestartLevel(0);
					debug("Level restarted succesfully");
				}
				catch ( System.ArgumentException e)
				{
					Utilities.RestartLevel(0);
					debug("reverting to scene 0 " + e.ToString());
				}
				finally
				{
					debug("Restart handled...");
				}




				
			}
		}
	}

	public void PrintLootReport()
	{
		var currentItem = lootStack.Pop();
		var nextItem = lootStack.Peek();
		Debug.LogFormat($"You take a {currentItem}, next item is {nextItem}");

		Debug.LogFormat($"There are {lootStack.Count} random items waiting for u");
	}
}
