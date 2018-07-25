using UnityEngine;

public class OpponentCommands : MonoBehaviour
{
	public GameObject scoreBoard;

	private void Awake()
	{
		scoreBoard.GetComponentsInChildren<TextMesh>()[0].text = "100";
	}
	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect()
	{
		string oldScoreStr = scoreBoard.GetComponentsInChildren<TextMesh>()[0].text;
		int oldScore = int.Parse(oldScoreStr);
		int newScore = oldScore - 10;
		string newScoreStr = newScore.ToString();
		scoreBoard.GetComponentsInChildren<TextMesh>()[0].text = newScoreStr;
	}
}