﻿CREATE TABLE "main"."army"(
  "id" INT NOT NULL DEFAULT 0,
  "time0" INT NOT NULL DEFAULT 0,
  "win0" INT NOT NULL DEFAULT 0,
  "loss0" INT NOT NULL DEFAULT 0,
  "score0" INT NOT NULL DEFAULT 0,
  "best0" INT NOT NULL DEFAULT 0,
  "worst0" INT NOT NULL DEFAULT 0,
  "brnd0" INT NOT NULL DEFAULT 0,
  "time1" INT NOT NULL DEFAULT 0,
  "win1" INT NOT NULL DEFAULT 0,
  "loss1" INT NOT NULL DEFAULT 0,
  "score1" INT NOT NULL DEFAULT 0,
  "best1" INT NOT NULL DEFAULT 0,
  "worst1" INT NOT NULL DEFAULT 0,
  "brnd1" INT NOT NULL DEFAULT 0,
  "time2" INT NOT NULL DEFAULT 0,
  "win2" INT NOT NULL DEFAULT 0,
  "loss2" INT NOT NULL DEFAULT 0,
  "score2" INT NOT NULL DEFAULT 0,
  "best2" INT NOT NULL DEFAULT 0,
  "worst2" INT NOT NULL DEFAULT 0,
  "brnd2" INT NOT NULL DEFAULT 0,
  "time3" INT NOT NULL DEFAULT 0,
  "win3" INT NOT NULL DEFAULT 0,
  "loss3" INT NOT NULL DEFAULT 0,
  "score3" INT NOT NULL DEFAULT 0,
  "best3" INT NOT NULL DEFAULT 0,
  "worst3" INT NOT NULL DEFAULT 0,
  "brnd3" INT NOT NULL DEFAULT 0,
  "time4" INT NOT NULL DEFAULT 0,
  "win4" INT NOT NULL DEFAULT 0,
  "loss4" INT NOT NULL DEFAULT 0,
  "score4" INT NOT NULL DEFAULT 0,
  "best4" INT NOT NULL DEFAULT 0,
  "worst4" INT NOT NULL DEFAULT 0,
  "brnd4" INT NOT NULL DEFAULT 0,
  "time5" INT NOT NULL DEFAULT 0,
  "win5" INT NOT NULL DEFAULT 0,
  "loss5" INT NOT NULL DEFAULT 0,
  "score5" INT NOT NULL DEFAULT 0,
  "best5" INT NOT NULL DEFAULT 0,
  "worst5" INT NOT NULL DEFAULT 0,
  "brnd5" INT NOT NULL DEFAULT 0,
  "time6" INT NOT NULL DEFAULT 0,
  "win6" INT NOT NULL DEFAULT 0,
  "loss6" INT NOT NULL DEFAULT 0,
  "score6" INT NOT NULL DEFAULT 0,
  "best6" INT NOT NULL DEFAULT 0,
  "worst6" INT NOT NULL DEFAULT 0,
  "brnd6" INT NOT NULL DEFAULT 0,
  "time7" INT NOT NULL DEFAULT 0,
  "win7" INT NOT NULL DEFAULT 0,
  "loss7" INT NOT NULL DEFAULT 0,
  "score7" INT NOT NULL DEFAULT 0,
  "best7" INT NOT NULL DEFAULT 0,
  "worst7" INT NOT NULL DEFAULT 0,
  "brnd7" INT NOT NULL DEFAULT 0,
  "time8" INT NOT NULL DEFAULT 0,
  "win8" INT NOT NULL DEFAULT 0,
  "loss8" INT NOT NULL DEFAULT 0,
  "score8" INT NOT NULL DEFAULT 0,
  "best8" INT NOT NULL DEFAULT 0,
  "worst8" INT NOT NULL DEFAULT 0,
  "brnd8" INT NOT NULL DEFAULT 0,
  "time9" INT NOT NULL DEFAULT 0,
  "win9" INT NOT NULL DEFAULT 0,
  "loss9" INT NOT NULL DEFAULT 0,
  "score9" INT NOT NULL DEFAULT 0,
  "best9" INT NOT NULL DEFAULT 0,
  "worst9" INT NOT NULL DEFAULT 0,
  "brnd9" INT NOT NULL DEFAULT 0,
  "time10" INT NOT NULL DEFAULT 0,
  "win10" INT NOT NULL DEFAULT 0,
  "loss10" INT NOT NULL DEFAULT 0,
  "score10" INT NOT NULL DEFAULT 0,
  "best10" INT NOT NULL DEFAULT 0,
  "worst10" INT NOT NULL DEFAULT 0,
  "brnd10" INT NOT NULL DEFAULT 0,
  "time11" INT NOT NULL DEFAULT 0,
  "win11" INT NOT NULL DEFAULT 0,
  "loss11" INT NOT NULL DEFAULT 0,
  "score11" INT NOT NULL DEFAULT 0,
  "best11" INT NOT NULL DEFAULT 0,
  "worst11" INT NOT NULL DEFAULT 0,
  "brnd11" INT NOT NULL DEFAULT 0,
  "time12" INT NOT NULL DEFAULT 0,
  "win12" INT NOT NULL DEFAULT 0,
  "loss12" INT NOT NULL DEFAULT 0,
  "score12" INT NOT NULL DEFAULT 0,
  "best12" INT NOT NULL DEFAULT 0,
  "worst12" INT NOT NULL DEFAULT 0,
  "brnd12" INT NOT NULL DEFAULT 0,
  "time13" INT NOT NULL DEFAULT 0,
  "win13" INT NOT NULL DEFAULT 0,
  "loss13" INT NOT NULL DEFAULT 0,
  "score13" INT NOT NULL DEFAULT 0,
  "best13" INT NOT NULL DEFAULT 0,
  "worst13" INT NOT NULL DEFAULT 0,
  "brnd13" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id")
);

CREATE TABLE "main"."awards"(
  "id" INT NOT NULL DEFAULT 0,
  "awd" MEDIUMINT NOT NULL DEFAULT 0,
  "level" INT NOT NULL DEFAULT 0,
  "earned" INT NOT NULL DEFAULT 0,
  "first" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id","awd","level")
);

CREATE TABLE "main"."ip2nation"(
  "rowid" INT PRIMARY KEY,
  "ip" INT NOT NULL DEFAULT 0,
  "country" TEXT NOT NULL DEFAULT ''
);

CREATE TABLE "main"."ip2nationcountries"(
  "code" TEXT NOT NULL DEFAULT '',
  "iso_code_2" TEXT NOT NULL DEFAULT '',
  "iso_code_3" TEXT DEFAULT '',
  "iso_country" TEXT NOT NULL DEFAULT '',
  "country" TEXT NOT NULL DEFAULT '',
  "lat" REAL NOT NULL DEFAULT 0,
  "lon" REAL NOT NULL DEFAULT 0,
  PRIMARY KEY ("code")
);

CREATE TABLE "main"."kills"(
  "attacker" INT  NOT NULL DEFAULT 0,
  "victim" INT  NOT NULL DEFAULT 0,
  "count" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("attacker","victim")
);

CREATE TABLE "main"."kits"(
  "id" INT NOT NULL DEFAULT 0,
  "time0" INT NOT NULL DEFAULT 0,
  "kills0" INT NOT NULL DEFAULT 0,
  "deaths0" INT NOT NULL DEFAULT 0,
  "time1" INT NOT NULL DEFAULT 0,
  "kills1" INT NOT NULL DEFAULT 0,
  "deaths1" INT NOT NULL DEFAULT 0,
  "time2" INT NOT NULL DEFAULT 0,
  "kills2" INT NOT NULL DEFAULT 0,
  "deaths2" INT NOT NULL DEFAULT 0,
  "time3" INT NOT NULL DEFAULT 0,
  "kills3" INT NOT NULL DEFAULT 0,
  "deaths3" INT NOT NULL DEFAULT 0,
  "time4" INT NOT NULL DEFAULT 0,
  "kills4" INT NOT NULL DEFAULT 0,
  "deaths4" INT NOT NULL DEFAULT 0,
  "time5" INT NOT NULL DEFAULT 0,
  "kills5" INT NOT NULL DEFAULT 0,
  "deaths5" INT NOT NULL DEFAULT 0,
  "time6" INT NOT NULL DEFAULT 0,
  "kills6" INT NOT NULL DEFAULT 0,
  "deaths6" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id")
);

CREATE TABLE "main"."mapinfo"(
  "id" INT NOT NULL DEFAULT 0,
  "name" TEXT NOT NULL DEFAULT '',
  "score" INT NOT NULL DEFAULT 0,
  "time" INT NOT NULL DEFAULT 0,
  "times" INT NOT NULL DEFAULT 0,
  "kills" INT NOT NULL DEFAULT 0,
  "deaths" INT NOT NULL DEFAULT 0,
  "custom" tinyint(2) NOT NULL DEFAULT 0,
  PRIMARY KEY ("id","name")
);

CREATE TABLE "main"."maps"(
  "id" INT NOT NULL DEFAULT 0,
  "mapid" INT NOT NULL DEFAULT 0,
  "time" INT NOT NULL DEFAULT 0,
  "win" INT NOT NULL DEFAULT 0,
  "loss" INT NOT NULL DEFAULT 0,
  "best" INT NOT NULL DEFAULT 0,
  "worst" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id","mapid")
);

CREATE TABLE "main"."player"(
  "id" INT NOT NULL DEFAULT 0,
  "name" TEXT NOT NULL DEFAULT '',
  "country" TEXT NOT NULL DEFAULT '',
  "time" INT NOT NULL DEFAULT 0,
  "rounds" INT NOT NULL DEFAULT 0,
  "ip" TEXT NOT NULL DEFAULT '',
  "score" INT NOT NULL DEFAULT 0,
  "cmdscore" INT NOT NULL DEFAULT 0,
  "skillscore" INT NOT NULL DEFAULT 0,
  "teamscore" INT NOT NULL DEFAULT 0,
  "kills" INT NOT NULL DEFAULT 0,
  "deaths" INT NOT NULL DEFAULT 0,
  "captures" INT NOT NULL DEFAULT 0,
  "neutralizes" INT NOT NULL DEFAULT 0,
  "captureassists" INT NOT NULL DEFAULT 0,
  "neutralizeassists" INT NOT NULL DEFAULT 0,
  "defends" INT NOT NULL DEFAULT 0,
  "damageassists" INT NOT NULL DEFAULT 0,
  "heals" INT NOT NULL DEFAULT 0,
  "revives" INT NOT NULL DEFAULT 0,
  "ammos" INT NOT NULL DEFAULT 0,
  "repairs" INT NOT NULL DEFAULT 0,
  "targetassists" INT NOT NULL DEFAULT 0,
  "driverspecials" INT NOT NULL DEFAULT 0,
  "driverassists" INT NOT NULL DEFAULT 0,
  "passengerassists" INT NOT NULL DEFAULT 0,
  "teamkills" INT NOT NULL DEFAULT 0,
  "teamdamage" INT NOT NULL DEFAULT 0,
  "teamvehicledamage" INT NOT NULL DEFAULT 0,
  "suicides" INT NOT NULL DEFAULT 0,
  "killstreak" INT NOT NULL DEFAULT 0,
  "deathstreak" INT NOT NULL DEFAULT 0,
  "rank" INT NOT NULL DEFAULT 0,
  "banned" INT NOT NULL DEFAULT 0,
  "kicked" INT NOT NULL DEFAULT 0,
  "cmdtime" INT NOT NULL DEFAULT 0,
  "sqltime" INT NOT NULL DEFAULT 0,
  "sqmtime" INT NOT NULL DEFAULT 0,
  "lwtime" INT NOT NULL DEFAULT 0,
  "wins" INT NOT NULL DEFAULT 0,
  "losses" INT NOT NULL DEFAULT 0,
  "availunlocks" INT NOT NULL DEFAULT 0,
  "usedunlocks" INT NOT NULL DEFAULT 0,
  "joined" INT NOT NULL DEFAULT 0,
  "rndscore" INT NOT NULL DEFAULT 0,
  "lastonline" INT NOT NULL DEFAULT 0,
  "chng" INT NOT NULL DEFAULT 0,
  "decr" INT NOT NULL DEFAULT 0,
  "mode0" INT NOT NULL DEFAULT 0,
  "mode1" INT NOT NULL DEFAULT 0,
  "mode2" INT NOT NULL DEFAULT 0,
  "permban" INT NOT NULL DEFAULT 0,
  "clantag" TEXT NOT NULL DEFAULT '',
  "isbot" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id","name")
);

CREATE TABLE "main"."player_history"(
  "id" INT NOT NULL DEFAULT 0,
  "timestamp" INT NOT NULL DEFAULT 0,
  "time" INT NOT NULL DEFAULT 0,
  "score" INT NOT NULL DEFAULT 0,
  "cmdscore" INT NOT NULL DEFAULT 0,
  "skillscore" INT NOT NULL DEFAULT 0,
  "teamscore" INT NOT NULL DEFAULT 0,
  "kills" INT NOT NULL DEFAULT 0,
  "deaths" INT NOT NULL DEFAULT 0,
  "rank" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id","timestamp")
);

CREATE TABLE "main"."round_history"(
  "id" INTEGER PRIMARY KEY,
  "timestamp" INT NOT NULL DEFAULT 0,
  "mapid" INT NOT NULL DEFAULT 0,
  "time" INT NOT NULL DEFAULT 0,
  "team1" INT NOT NULL DEFAULT 0,
  "team2" INT NOT NULL DEFAULT 0,
  "tickets1" INT NOT NULL DEFAULT 0,
  "tickets2" INT NOT NULL DEFAULT 0,
  "pids1" INT NOT NULL DEFAULT 0,
  "pids1_end" INT NOT NULL DEFAULT 0,
  "pids2" INT NOT NULL DEFAULT 0,
  "pids2_end" INT NOT NULL DEFAULT 0
);

CREATE TABLE "main"."servers"(
  "id" INTEGER PRIMARY KEY,
  "ip" TEXT NOT NULL DEFAULT '',
  "prefix" TEXT NOT NULL DEFAULT '',
  "name" TEXT DEFAULT NULL,
  "port" INT DEFAULT 0,
  "queryport" INT NOT NULL DEFAULT 0,
  "rcon_port" INT NOT NULL DEFAULT 4711,
  "rcon_password" TEXT DEFAULT NULL,
  "lastupdate" TEXT NOT NULL DEFAULT '0000-00-00 00:00:00'
);

CREATE TABLE "main"."unlocks"(
  "id" INT NOT NULL DEFAULT 0,
  "kit" SMALLINT NOT NULL DEFAULT 0,
  "state" TEXT NOT NULL DEFAULT 'n',
  PRIMARY KEY ("id","kit")
);

CREATE TABLE "main"."vehicles"(
  "id" INT NOT NULL DEFAULT 0,
  "time0" INT NOT NULL DEFAULT 0,
  "time1" INT NOT NULL DEFAULT 0,
  "time2" INT NOT NULL DEFAULT 0,
  "time3" INT NOT NULL DEFAULT 0,
  "time4" INT NOT NULL DEFAULT 0,
  "time5" INT NOT NULL DEFAULT 0,
  "time6" INT NOT NULL DEFAULT 0,
  "timepara" INT NOT NULL DEFAULT 0,
  "kills0" INT NOT NULL DEFAULT 0,
  "kills1" INT NOT NULL DEFAULT 0,
  "kills2" INT NOT NULL DEFAULT 0,
  "kills3" INT NOT NULL DEFAULT 0,
  "kills4" INT NOT NULL DEFAULT 0,
  "kills5" INT NOT NULL DEFAULT 0,
  "kills6" INT NOT NULL DEFAULT 0,
  "deaths0" INT NOT NULL DEFAULT 0,
  "deaths1" INT NOT NULL DEFAULT 0,
  "deaths2" INT NOT NULL DEFAULT 0,
  "deaths3" INT NOT NULL DEFAULT 0,
  "deaths4" INT NOT NULL DEFAULT 0,
  "deaths5" INT NOT NULL DEFAULT 0,
  "deaths6" INT NOT NULL DEFAULT 0,
  "rk0" INT NOT NULL DEFAULT 0,
  "rk1" INT NOT NULL DEFAULT 0,
  "rk2" INT NOT NULL DEFAULT 0,
  "rk3" INT NOT NULL DEFAULT 0,
  "rk4" INT NOT NULL DEFAULT 0,
  "rk5" INT NOT NULL DEFAULT 0,
  "rk6" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id")
);

CREATE TABLE "main"."weapons"(
  "id" INT NOT NULL DEFAULT 0,
  "time0" INT NOT NULL DEFAULT 0,
  "time1" INT NOT NULL DEFAULT 0,
  "time2" INT NOT NULL DEFAULT 0,
  "time3" INT NOT NULL DEFAULT 0,
  "time4" INT NOT NULL DEFAULT 0,
  "time5" INT NOT NULL DEFAULT 0,
  "time6" INT NOT NULL DEFAULT 0,
  "time7" INT NOT NULL DEFAULT 0,
  "time8" INT NOT NULL DEFAULT 0,
  "knifetime" INT NOT NULL DEFAULT 0,
  "c4time" INT NOT NULL DEFAULT 0,
  "handgrenadetime" INT NOT NULL DEFAULT 0,
  "claymoretime" INT NOT NULL DEFAULT 0,
  "shockpadtime" INT NOT NULL DEFAULT 0,
  "atminetime" INT NOT NULL DEFAULT 0,
  "tacticaltime" INT NOT NULL DEFAULT 0,
  "grapplinghooktime" INT NOT NULL DEFAULT 0,
  "ziplinetime" INT NOT NULL DEFAULT 0,
  "kills0" INT NOT NULL DEFAULT 0,
  "kills1" INT NOT NULL DEFAULT 0,
  "kills2" INT NOT NULL DEFAULT 0,
  "kills3" INT NOT NULL DEFAULT 0,
  "kills4" INT NOT NULL DEFAULT 0,
  "kills5" INT NOT NULL DEFAULT 0,
  "kills6" INT NOT NULL DEFAULT 0,
  "kills7" INT NOT NULL DEFAULT 0,
  "kills8" INT NOT NULL DEFAULT 0,
  "knifekills" INT NOT NULL DEFAULT 0,
  "c4kills" INT NOT NULL DEFAULT 0,
  "handgrenadekills" INT NOT NULL DEFAULT 0,
  "claymorekills" INT NOT NULL DEFAULT 0,
  "shockpadkills" INT NOT NULL DEFAULT 0,
  "atminekills" INT NOT NULL DEFAULT 0,
  "deaths0" INT NOT NULL DEFAULT 0,
  "deaths1" INT NOT NULL DEFAULT 0,
  "deaths2" INT NOT NULL DEFAULT 0,
  "deaths3" INT NOT NULL DEFAULT 0,
  "deaths4" INT NOT NULL DEFAULT 0,
  "deaths5" INT NOT NULL DEFAULT 0,
  "deaths6" INT NOT NULL DEFAULT 0,
  "deaths7" INT NOT NULL DEFAULT 0,
  "deaths8" INT NOT NULL DEFAULT 0,
  "knifedeaths" INT NOT NULL DEFAULT 0,
  "c4deaths" INT NOT NULL DEFAULT 0,
  "handgrenadedeaths" INT NOT NULL DEFAULT 0,
  "claymoredeaths" INT NOT NULL DEFAULT 0,
  "shockpaddeaths" INT NOT NULL DEFAULT 0,
  "atminedeaths" INT NOT NULL DEFAULT 0,
  "ziplinedeaths" INT NOT NULL DEFAULT 0,
  "grapplinghookdeaths" INT NOT NULL DEFAULT 0,
  "tacticaldeployed" INT NOT NULL DEFAULT 0,
  "grapplinghookdeployed" INT NOT NULL DEFAULT 0,
  "ziplinedeployed" INT NOT NULL DEFAULT 0,
  "fired0" INT NOT NULL DEFAULT 0,
  "fired1" INT NOT NULL DEFAULT 0,
  "fired2" INT NOT NULL DEFAULT 0,
  "fired3" INT NOT NULL DEFAULT 0,
  "fired4" INT NOT NULL DEFAULT 0,
  "fired5" INT NOT NULL DEFAULT 0,
  "fired6" INT NOT NULL DEFAULT 0,
  "fired7" INT NOT NULL DEFAULT 0,
  "fired8" INT NOT NULL DEFAULT 0,
  "knifefired" INT NOT NULL DEFAULT 0,
  "c4fired" INT NOT NULL DEFAULT 0,
  "claymorefired" INT NOT NULL DEFAULT 0,
  "handgrenadefired" INT NOT NULL DEFAULT 0,
  "shockpadfired" INT NOT NULL DEFAULT 0,
  "atminefired" INT NOT NULL DEFAULT 0,
  "hit0" INT NOT NULL DEFAULT 0,
  "hit1" INT NOT NULL DEFAULT 0,
  "hit2" INT NOT NULL DEFAULT 0,
  "hit3" INT NOT NULL DEFAULT 0,
  "hit4" INT NOT NULL DEFAULT 0,
  "hit5" INT NOT NULL DEFAULT 0,
  "hit6" INT NOT NULL DEFAULT 0,
  "hit7" INT NOT NULL DEFAULT 0,
  "hit8" INT NOT NULL DEFAULT 0,
  "knifehit" INT NOT NULL DEFAULT 0,
  "c4hit" INT NOT NULL DEFAULT 0,
  "claymorehit" INT NOT NULL DEFAULT 0,
  "handgrenadehit" INT NOT NULL DEFAULT 0,
  "shockpadhit" INT NOT NULL DEFAULT 0,
  "atminehit" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("id")
);

CREATE TABLE "main"."_version"(
  "dbver" TEXT NOT NULL DEFAULT '',
  "dbdate" INT NOT NULL DEFAULT 0,
  PRIMARY KEY ("dbver")
);

/* Temp 
INSERT INTO player VALUES (101249154, ' wilson212', 'us', 19118, 16, '174.49.21.221', 2489, 0, 1819, 670, 808, 103, 67, 0, 45, 0, 24, 66, 34, 87, 0, 2, 0, 41, 0, 0, 17, 16, 4, 3, 32, 4, 3, 0, 0, 0, 5873, 11779, 1464, 15, 1, 0, 2, 1361127107, 309, 1361682993, 0, 0, 0, 0, 16, 0, 'w212', 0);
INSERT INTO army ("id", "time0", "win0", "loss0", "score0", "best0", "worst0", "brnd0", "time1", "win1", "loss1", "score1", "best1", "worst1", "brnd1", "time2", "win2", "loss2", "score2", "best2", "worst2", "brnd2", "time3", "win3", "loss3", "score3", "best3", "worst3", "brnd3", "time4", "win4", "loss4", "score4", "best4", "worst4", "brnd4", "time5", "win5", "loss5", "score5", "best5", "worst5", "brnd5", "time6", "win6", "loss6", "score6", "best6", "worst6", "brnd6", "time7", "win7", "loss7", "score7", "best7", "worst7", "brnd7", "time8", "win8", "loss8", "score8", "best8", "worst8", "brnd8", "time9", "win9", "loss9", "score9", "best9", "worst9", "brnd9", "time10", "win10", "loss10", "score10", "best10", "worst10", "brnd10", "time11", "win11", "loss11", "score11", "best11", "worst11", "brnd11", "time12", "win12", "loss12", "score12", "best12", "worst12", "brnd12", "time13", "win13", "loss13", "score13", "best13", "worst13", "brnd13") VALUES
(101249154, 16951, 13, 1, 2240, 309, 111, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 767, 1, 0, 96, 96, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1400, 1, 0, 153, 153, 0, 0, 0, 0, 0, 0, 0, 0, 0);
INSERT INTO awards ("id", "awd", "level", "earned", "first") VALUES
(101249154, 1031105, 1, 1361127107, 0),
(101249154, 1031109, 1, 1361680338, 0),
(101249154, 1031113, 1, 1361133601, 0),
(101249154, 1031115, 1, 1361129274, 0),
(101249154, 1031119, 1, 1361680338, 0),
(101249154, 1032415, 1, 1361598276, 0),
(101249154, 1190601, 1, 1361133601, 0),
(101249154, 1220118, 1, 1361144883, 0),
(101249154, 1220803, 1, 1361127107, 0),
(101249154, 1261115, 1, 1361508116, 0),
(101249154, 2051902, 2, 1361508116, 1361156848),
(101249154, 2051907, 8, 1361682994, 1361127107),
(101249154, 2051919, 5, 1361680338, 1361144883),
(101249154, 3150914, 1, 1361505724, 0),
(101249154, 3190803, 1, 1361127107, 0),
(101249154, 3211305, 1, 1361127107, 0),
(101249154, 3240301, 1, 1361129274, 0);
INSERT INTO kits ("id", "time0", "kills0", "deaths0", "time1", "kills1", "deaths1", "time2", "kills2", "deaths2", "time3", "kills3", "deaths3", "time4", "kills4", "deaths4", "time5", "kills5", "deaths5", "time6", "kills6", "deaths6") VALUES
(101249154, 8, 0, 0, 800, 22, 7, 6532, 260, 25, 8489, 377, 51, 1897, 122, 14, 9, 1, 1, 745, 26, 5);
INSERT INTO weapons ("id", "time0", "time1", "time2", "time3", "time4", "time5", "time6", "time7", "time8", "knifetime", "c4time", "handgrenadetime", "claymoretime", "shockpadtime", "atminetime", "tacticaltime", "grapplinghooktime", "ziplinetime", "kills0", "kills1", "kills2", "kills3", "kills4", "kills5", "kills6", "kills7", "kills8", "knifekills", "c4kills", "handgrenadekills", "claymorekills", "shockpadkills", "atminekills", "deaths0", "deaths1", "deaths2", "deaths3", "deaths4", "deaths5", "deaths6", "deaths7", "deaths8", "knifedeaths", "c4deaths", "handgrenadedeaths", "claymoredeaths", "shockpaddeaths", "atminedeaths", "ziplinedeaths", "grapplinghookdeaths", "tacticaldeployed", "grapplinghookdeployed", "ziplinedeployed", "fired0", "fired1", "fired2", "fired3", "fired4", "fired5", "fired6", "fired7", "fired8", "knifefired", "c4fired", "claymorefired", "handgrenadefired", "shockpadfired", "atminefired", "hit0", "hit1", "hit2", "hit3", "hit4", "hit5", "hit6", "hit7", "hit8", "knifehit", "c4hit", "claymorehit", "handgrenadehit", "shockpadhit", "atminehit") VALUES
(101249154, 2334, 15, 774, 9, 648, 523, 2, 145, 677, 42, 258, 130, 0, 966, 81, 0, 0, 1, 53, 0, 18, 1, 25, 2, 0, 3, 8, 2, 4, 0, 0, 0, 13, 24, 0, 11, 1, 4, 2, 0, 2, 4, 1, 0, 2, 0, 8, 0, 0, 0, 0, 0, 0, 897, 3, 394, 31, 58, 108, 0, 138, 12, 13, 13, 0, 27, 102, 18, 224, 1, 98, 6, 34, 27, 0, 20, 4, 2, 7, 0, 12, 87, 6);
INSERT INTO vehicles ("id", "time0", "time1", "time2", "time3", "time4", "time5", "time6", "timepara", "kills0", "kills1", "kills2", "kills3", "kills4", "kills5", "kills6", "deaths0", "deaths1", "deaths2", "deaths3", "deaths4", "deaths5", "deaths6", "rk0", "rk1", "rk2", "rk3", "rk4", "rk5", "rk6") VALUES
(101249154, 5367, 0, 134, 3933, 1835, 0, 56, 2, 415, 0, 24, 152, 83, 0, 7, 18, 0, 1, 12, 11, 0, 0, 24, 0, 1, 6, 15, 0, 0);
*/
