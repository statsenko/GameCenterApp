﻿////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LbCustomGUIExample : MonoBehaviour {

	private string leaderBoardId =  "your.leaderbord2.id.here";
	public int hiScore = 100;


	public GUIStyle headerStyle;
	public GUIStyle boardStyle;


	private GCLeaderBoard loadedLeaderBoard = null;

	private GCCollectionType diplayCollection = GCCollectionType.FRIENDS;

	void Awake() {
		
		IOSNative.instance.Init();


		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_PLAYER_AUTHENTICATED, OnAuth);
		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED, OnAuthFailed);

		GameCenterManager.dispatcher.addEventListener (GameCenterManager.GAME_CENTER_LEADER_BOARD_SCORE_LIST_LOADED, OnScoreListLoaded);


		//Initializing Game Cneter class. This action will triger authentication flow
		GameCenterManager.init();
	}
	

	void OnGUI() {

		GUI.Label(new Rect(10, 20, 400, 40), "Custom Leader Board GUI Example", headerStyle);

		if(GUI.Button(new Rect(400, 10, 150, 50), "Load Friends Scores")) {
			GameCenterManager.loadScore(leaderBoardId, 1, 10, GCBoardTimeSpan.ALL_TIME, GCCollectionType.FRIENDS);
		}

		if(GUI.Button(new Rect(600, 10, 150, 50), "Load Global Scores")) {
			GameCenterManager.loadScore(leaderBoardId, 1, 10, GCBoardTimeSpan.ALL_TIME, GCCollectionType.GLOBAL);
		}

		Color defaultColor = GUI.color;

		if(diplayCollection == GCCollectionType.GLOBAL) {
			GUI.color = Color.green;
		}
		if(GUI.Button(new Rect(800, 10, 170, 50), "Displying Global Scores")) {
			diplayCollection = GCCollectionType.GLOBAL;
		}
		GUI.color = defaultColor;



		if(diplayCollection == GCCollectionType.FRIENDS) {
			GUI.color = Color.green;
		}
		if(GUI.Button(new Rect(800, 70, 170, 50), "Displying Friends Scores")) {
			diplayCollection = GCCollectionType.FRIENDS;
		}
		GUI.color = defaultColor;

		GUI.Label(new Rect(10,  90, 100, 40), "rank", boardStyle);
		GUI.Label(new Rect(100, 90, 100, 40), "score", boardStyle);
		GUI.Label(new Rect(200, 90, 100, 40), "playerId", boardStyle);
		GUI.Label(new Rect(400, 90, 100, 40), "name ", boardStyle);
		GUI.Label(new Rect(550, 90, 100, 40), "avatar ", boardStyle);

		if(loadedLeaderBoard != null) {
			for(int i = 1; i < 10; i++) {
				GCScore score = loadedLeaderBoard.GetScore(i, GCBoardTimeSpan.ALL_TIME, diplayCollection);
				if(score != null) {
					GUI.Label(new Rect(10,  90 + 70 * i, 100, 40), i.ToString(), boardStyle);
					GUI.Label(new Rect(100, 90 + 70 * i, 100, 40), score.GetIntScore().ToString() , boardStyle);
					GUI.Label(new Rect(200, 90 + 70 * i, 100, 40), score.PlayerID, boardStyle);


					GameCenterPlayerTemplate player = GameCenterManager.GetPlayerById(score.PlayerID);
					if(player != null) {
						GUI.Label(new Rect(400, 90 + 70 * i , 100, 40), player.alias, boardStyle);
						if(player.avatar != null) {
							GUI.DrawTexture(new Rect(550, 75 + 70 * i, 50, 50), player.avatar);
						} else  {
							GUI.Label(new Rect(550, 90 + 70 * i, 100, 40), "no photo ", boardStyle);
						}
					}

					if(GUI.Button(new Rect(650, 90 + 70 * i, 100, 30), "Chalange")) {
						GameCenterManager.sendChallenge(leaderBoardId, "Your message here", score.PlayerID);
					}

				}

			}
		}



	}




	private void OnScoreListLoaded() {

		Debug.Log("Scores loaded");

		loadedLeaderBoard = GameCenterManager.GetLeaderBoard(leaderBoardId);

	}




	private void OnAuth() {
		IOSNative.showMessage("Player Authed ", "ID: " + GameCenterManager.player.playerId + "\n" + "Name: " + GameCenterManager.player.displayName);
	}
	
	private void OnAuthFailed() {
		IOSNative.showMessage("Game Cneter ", "Player auntification failed");
		
		//if you got this event it means that player canseled auntification flow. With probably mean that playr do not whant to use gamcenter in your game
	}
}
