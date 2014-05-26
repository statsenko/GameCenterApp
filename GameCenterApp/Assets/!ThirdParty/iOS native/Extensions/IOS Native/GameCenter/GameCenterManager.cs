////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Native Plugin for Unity3D 
// @author Osipov Stanislav (Stan's Assets) 
// @support stans.assets@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GameCenterManager : MonoBehaviour {


	public const string GAME_CENTER_PLAYER_AUTHENTICATED       					= "game_center_player_authenticated";
	public const string GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED  			= "game_center_player_authentification_failed";
	public const string GAME_CENTER_ACHIEVEMENTS_RESET        	 				= "game_center_achievements_reset";
	public const string GAME_CENTER_ACHIEVEMENTS_LOADED        					= "game_center_achievements_loaded";
	public const string GAME_CENTER_LEADER_BOARD_SCORE_LOADED  					= "game_center_leader_board_score_loaded";
	public const string GAME_CENTER_LEADER_BOARD_SCORE_LIST_LOADED  			= "game_center_leader_board_score_list_loaded";
	public const string GAME_CENTER_LEADER_BOARD_SCORE_LIST_FAILED_TO_LOADED  	= "game_center_leader_board_score_list_failed_to_loaded";
	public const string GAME_CENTER_ACHIEVEMENT_PROGRESS  						= "game_center_achievement_progress";

	public const string GAME_CENTER_USER_INFO_LOADED  							= "game_center_user_info_loaded";
	public const string GAME_CENTER_USER_INFO_FAILED_TO_LOAD  					= "game_center_user_info_failed_to_load";

	
	[DllImport ("__Internal")]
	private static extern void _initGamaCenter();
	
	[DllImport ("__Internal")]
	private static extern void _showLeaderBoard(string leaderBoradrId, int scope);

	[DllImport ("__Internal")]
	private static extern void _reportScore (int score, string leaderBoradrId);



	[DllImport ("__Internal")]
	private static extern void _showLeaderBoards ();


	[DllImport ("__Internal")]
	private static extern void _getLeadrBoardScore (string leaderBoradrId, int scope);

	[DllImport ("__Internal")]
	private static extern void _loadLeadrBoardScore (string leaderBoradrId, int scope, int collection, int from, int to);
	
	[DllImport ("__Internal")]
	private static extern void _showAchievements();

	[DllImport ("__Internal")]
	private static extern void _resetAchievements();
	

	[DllImport ("__Internal")]
	private static extern void _submitAchievement(float percent, string achievementId, bool isCompleteNotification);

	[DllImport ("__Internal")]
	private static extern void _loadGCUserData(string uid);

	[DllImport ("__Internal")]
	private static extern void _sendChallenge(string leaderBoradrId, string message, string playerId);


	



	private  static bool _IsInited = false;
	private  static bool _IsPlayerAuthed = false;


	private static List<AchievementTemplate> _achievements = new List<AchievementTemplate> ();
	private static EventDispatcherBase _dispatcher  = new EventDispatcherBase ();

	private static Dictionary<string, GCLeaderBoard> _leaderboards =  new Dictionary<string, GCLeaderBoard>();
	private static Dictionary<string, GameCenterPlayerTemplate> _players =  new Dictionary<string, GameCenterPlayerTemplate>();


	private static GameCenterPlayerTemplate _player = null;

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public static void init() {

		if(_IsInited) {
			return;
		}

		_IsInited = true;


		GameObject go =  new GameObject("GameCenterManager");
		go.AddComponent<GameCenterManager>();
		DontDestroyOnLoad(go);


		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_initGamaCenter();
		}
			
	}
	



	public static void registerAchievement(string achievemenId) {
		bool isContains = false;

		foreach(AchievementTemplate t in _achievements) {
			if (t.id.Equals (achievemenId)) {
				isContains = true;
			}
		}


		if(!isContains) {
			AchievementTemplate tpl = new AchievementTemplate ();
			tpl.id = achievemenId;
			tpl.progress = 0;
			_achievements.Add (tpl);
		}
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	

	public static void showLeaderBoard(string leaderBoradrId) {
		showLeaderBoard(leaderBoradrId, GCBoardTimeSpan.ALL_TIME);
	}


	public static void showLeaderBoard(string leaderBoradrId, GCBoardTimeSpan scope) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showLeaderBoard(leaderBoradrId, (int) scope);
		}
	}

	public static void showLeaderBoards() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showLeaderBoards ();
		}
	}
	

	public static void reportScore(int score, string leaderBoradrId) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_reportScore(score, leaderBoradrId);
		}
	}

	public static void getScore(string leaderBoradrId, GCBoardTimeSpan scope = GCBoardTimeSpan.ALL_TIME) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_getLeadrBoardScore(leaderBoradrId, (int) scope);
		}
	}

	
	public static void loadScore(string leaderBoradrId, int from, int to, GCBoardTimeSpan scope = GCBoardTimeSpan.ALL_TIME, GCCollectionType collection = GCCollectionType.FRIENDS) {

		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_loadLeadrBoardScore(leaderBoradrId, (int) scope, (int) collection, from, to);
		}

	}


	public static void sendChallenge(string leaderBoradrId, string message, string playerId) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_sendChallenge(leaderBoradrId, message, playerId);
		}
	}


	public static void showAchievements() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_showAchievements();
		}
	}

	public static void resetAchievements() {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_resetAchievements();

			foreach(AchievementTemplate tpl in _achievements) {
				tpl.progress = 0f;
			}
		}
	}


	public static void submitAchievement(float percent, string achievementId) {
		submitAchievement (percent, achievementId, true);
	}

	public static void submitAchievement(float percent, string achievementId, bool isCompleteNotification) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			_submitAchievement(percent, achievementId, isCompleteNotification);
		}
	}

	public static void loadUsersData(string[] UIDs) {
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			//_loadGCUserData(UID);
		}
	}


	public static float getAchievementProgress(string id) {
		float progress = 0f;
		foreach(AchievementTemplate tpl in _achievements) {
			if(tpl.id == id) {
				return tpl.progress;
			}
		}

		return progress;
	}

	public static GCLeaderBoard GetLeaderBoard(string id) {
		if(_leaderboards.ContainsKey(id)) {
			return _leaderboards[id];
		} else {
			return null;
		}
	}


	public static GameCenterPlayerTemplate GetPlayerById(string playerID) {
		if(_players.ContainsKey(playerID)) {
			return _players[playerID];
		} else {
			return null;
		}
	}
	

	//--------------------------------------
	//  GET/SET
	//--------------------------------------

	public static List<AchievementTemplate> achievements {
		get {
			return _achievements;
		}
	}


	public static Dictionary<string, GameCenterPlayerTemplate> players {
		get {
			return _players;
		}
	}

	public static EventDispatcherBase dispatcher {
		get {
			return _dispatcher;
		}
	}

	public static GameCenterPlayerTemplate player {
		get {
			return _player;
		}
	}


	private static bool IsInited {
		get {
			return _IsInited;
		}
	}


	private static bool IsPlayerAuthed {
		get {
			return _IsPlayerAuthed;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------


	private void onLeaderBoardScore(string array) {

		string[] data;
		data = array.Split("," [0]);
		LeaderBoardScoreData lData = new LeaderBoardScoreData ();
		lData.leaderBoardId = data [0];
		lData.leaderBoardScore = data [1];
		lData.rank = data [2];

		dispatcher.dispatch (GAME_CENTER_LEADER_BOARD_SCORE_LOADED, lData);
	}


	private void onLeaderBoardScoreListLoaded(string array) {


		
		string[] data;
		data = array.Split("," [0]);

		string lbId = data[0];
		GCBoardTimeSpan scope = (GCBoardTimeSpan) System.Convert.ToInt32(data[1]);
		GCCollectionType collection = (GCCollectionType) System.Convert.ToInt32(data[2]);

		GCLeaderBoard board;
		if(_leaderboards.ContainsKey(lbId)) {
			board = _leaderboards[lbId];
		} else {
			board =  new GCLeaderBoard(lbId);
			_leaderboards.Add(lbId, board);
		}


	
		
		
		for(int i = 3; i < data.Length; i+=3) {
			board.UpdateScore(data[i + 2], data[i + 1], data[i], scope, collection);
		}
		


		
		dispatcher.dispatch (GAME_CENTER_LEADER_BOARD_SCORE_LIST_LOADED);
	}

	private void onLeaderBoardScoreListLoadFailed(string array) {

		dispatcher.dispatch (GAME_CENTER_LEADER_BOARD_SCORE_LIST_FAILED_TO_LOADED);
	}



	private void onAchievementsReset(string array) {
		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_RESET);
	}


	private void onAchievementProgressChanged(string array) {
		string[] data;
		data = array.Split("," [0]);

		AchievementTemplate tpl =  new AchievementTemplate();
		tpl.id = data [0];
		tpl.progress = System.Convert.ToSingle(data [1]) ;


		submitAchievement (tpl);

		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENT_PROGRESS, tpl);

	}


	private void onAchievementsLoaded(string array) {

		if(array.Equals(string.Empty)) {
			dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_LOADED);
			return;
		}

		string[] data;
		data = array.Split("," [0]);


		for(int i = 0; i < data.Length; i+=2) {
			AchievementTemplate tpl =  new AchievementTemplate();
			tpl.id 				= data[i];
			tpl.progress 		= System.Convert.ToSingle(data[i + 1]);
			submitAchievement (tpl);
		}

		dispatcher.dispatch (GAME_CENTER_ACHIEVEMENTS_LOADED);
	}

	private void onAuthenticateLocalPlayer(string  array) {
		string[] data;
		data = array.Split("," [0]);

		_player = new GameCenterPlayerTemplate (data[0], data [1], data [2]);

		_IsPlayerAuthed = true;
		dispatcher.dispatch (GAME_CENTER_PLAYER_AUTHENTICATED);
	}
	
	
	private void onAuthenticationFailed(string  array) {
		_IsPlayerAuthed = false;
		dispatcher.dispatch(GAME_CENTER_PLAYER_AUTHENTIFICATION_FAILED);
	}


	private void onUserInfoLoaded(string array) {


		string[] data;
		data = array.Split("," [0]);

		string playerId = data[0];
		string displayName = data[3];
		string alias = data[2];
		string avatar = data[1];

		GameCenterPlayerTemplate player =  new GameCenterPlayerTemplate(playerId, displayName, alias);
		player.SetAvatar(avatar);

		_players.Add(playerId, player);

		
		
		dispatcher.dispatch (GAME_CENTER_USER_INFO_LOADED);
	}    
	
	private void onUserInfoLoadFailed(string playerId) {
		
		dispatcher.dispatch (GAME_CENTER_USER_INFO_FAILED_TO_LOAD, playerId);
	}
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	private void submitAchievement(AchievementTemplate tpl) {
		bool isContains = false;
		foreach(AchievementTemplate t in _achievements) {
			if (t.id.Equals (tpl.id)) {
				isContains = true;
				t.progress = tpl.progress;
			}
		}

		if(!isContains) {
			_achievements.Add (tpl);
		}
	}
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
