using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
	public bool showWinScreen = false;
	public string labelText = "Coolect all 3 items wand win yr freedom!";
	public int maxitems = 3;
	
	private int _itemsCollected = 0;

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

	private int _playerHp = 10;

	public int HP
	{
		get => _playerHp;
		set
		{
			_playerHp = value;
			Debug.Log($"HP: {_playerHp}");
		}
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
				SceneManager.LoadScene(0);
				Time.timeScale = 1f;
			}
		}
	}

}
