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

public class GCLeaderBoard  {


	public ScoreCollection SocsialCollection =  new ScoreCollection();
	public ScoreCollection GlobalCollection =  new ScoreCollection();

	

	private string _id;

	public GCLeaderBoard(string leaderBoardId) {
		_id = leaderBoardId;
	}




	public string id {
		get {
			return _id;
		}
	}


	public GCScore GetScore(int rank, GCBoardTimeSpan scope, GCCollectionType collection) {

		ScoreCollection col = GlobalCollection;
		
		switch(collection) {
		case GCCollectionType.GLOBAL:
			col = GlobalCollection;
			break;
		case GCCollectionType.FRIENDS:
			col = SocsialCollection;
			break;
		}
		



		Dictionary<int, GCScore> scoreDict = col.AllTimeScores;
		
		switch(scope) {
		case GCBoardTimeSpan.ALL_TIME:
			scoreDict = col.AllTimeScores;
			break;
		case GCBoardTimeSpan.TODAY:
			scoreDict = col.TodayScores;
			break;
		case GCBoardTimeSpan.WEEK:
			scoreDict = col.WeekScores;
			break;
		}



		if(scoreDict.ContainsKey(rank)) {
			return scoreDict[rank];
		} else {
			return null;
		}

	}

	public void UpdateScore(string rank, string score, string playerID, GCBoardTimeSpan scope, GCCollectionType collection) {
		GCScore s =  new GCScore();
		s.PlayerID = playerID;
		s.rank = rank;
		s.leaderBoardScore = score;


		ScoreCollection col = GlobalCollection;

		switch(collection) {
		case GCCollectionType.GLOBAL:
			col = GlobalCollection;
			break;
		case GCCollectionType.FRIENDS:
			col = SocsialCollection;
			break;
		}




		Dictionary<int, GCScore> scoreDict = col.AllTimeScores;

		switch(scope) {
		case GCBoardTimeSpan.ALL_TIME:
			scoreDict = col.AllTimeScores;
			break;
		case GCBoardTimeSpan.TODAY:
			scoreDict = col.TodayScores;
			break;
		case GCBoardTimeSpan.WEEK:
			scoreDict = col.WeekScores;
			break;
		}


		if(scoreDict.ContainsKey(s.GetRank())) {
			scoreDict[s.GetRank()] = s;
		} else {
			scoreDict.Add(s.GetRank(), s);
		}
	}

}

