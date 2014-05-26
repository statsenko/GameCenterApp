#import <Foundation/Foundation.h>
#import <GameKit/GameKit.h>
#import "GameCenterManager.h"
#import "GCHelper.h"



@interface IOSGameCenterManager : NSObject <GKGameCenterControllerDelegate, GKAchievementViewControllerDelegate> {
    BOOL isAchievementsWasLoaded;
    UIView* curentView;
    NSString* requestedLeaderBordId;
    int lbscope;
    GameCenterManager *gameCenterManager;
    
    
}



- (void) reportScore: (int) score forCategory: (NSString*) category;

- (void) authenticateLocalPlayer;
- (void) showLeaderBoard: (NSString*)leaderBoradrId scope: (int) scope;
- (void) retrieveScoreForLocalPlayerWithCategory:(NSString*)category scope: (int) scope;
- (void) retriveScores:(NSString*)category scope: (int) scope collection: (int) collection from: (int) from to: (int) to;
- (void) loadUserData:(NSString *)uid;
- (void) sendChallenge:(NSString*) leaderBoradrId message:(NSString*) message playerId: (NSString*) playerId;


- (void) showAchievements;
- (void) resetAchievements; 
- (void) submitAchievement: (double) percentComplete identifier: (NSString*) identifier notifayComplete: (BOOL) notifayComplete;

- (void)findMatchWithMinPlayers:(int)minPlayers maxPlayers:(int)maxPlayers;

- (BOOL)isGameCenterAvailable;





@end