using Assets.Scripts;
using GameServer.Models;
using System;
using System.Linq;
using UnityEngine;

public class OpponentCommands : MonoBehaviour
{
	private GameObject ScoreBoard;
    private PlayerStateModel Opponent;

	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect()
	{
        //Calculate Damage
		//Make Web call
        //Where Do i update result?
	}

    void OnGameStateUpdate()
    {
        //Will change for multi player games.
        Opponent = CurrentGameState.GetInstance().Players.Where(player => player.Key != CurrentPlayerState.Instance.PlayerId).First().Value;
        if(ScoreBoard != null)
            ScoreBoard.GetComponentsInChildren<TextMesh>()[0].text = Opponent.Health.ToString();
    }
}