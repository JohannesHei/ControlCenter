﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using BF2Statistics.Database;

namespace BF2Statistics.ASP.Requests
{
    class GetLeaderBoard
    {
        private ASPResponse Response;
        private Dictionary<string, string> QueryString;
        private DatabaseDriver Driver;

        // Needed Params
        private string Id = "";
        private int Pid = 0;

        // Optional Params
        private int Before = 0;
        private int After = 0;
        private int Pos = 1;
        private int Min;
        private int Max;

        public GetLeaderBoard(HttpClient Client)
        {
            // Set internal variables
            this.Response = Client.Response;
            this.QueryString = Client.Request.QueryString;
            this.Driver = ASPServer.Database.Driver;

            // We need a type!
            if (!QueryString.ContainsKey("type"))
            {
                Response.WriteResponseStart(false);
                Response.WriteHeaderLine("asof", "err");
                Response.WriteDataLine(DateTime.UtcNow.ToUnixTimestamp(), "Invalid Syntax!");
                Response.Send();
                return;
            }

            // Setup Params
            if(QueryString.ContainsKey("pid"))
                Int32.TryParse(QueryString["pid"], out Pid);
            if (QueryString.ContainsKey("id"))
                Id = QueryString["id"];
            if (QueryString.ContainsKey("before"))
                Int32.TryParse(QueryString["before"], out Before);
            if(QueryString.ContainsKey("after"))
                Int32.TryParse(QueryString["after"], out After);
            if (QueryString.ContainsKey("pos"))
                Int32.TryParse(QueryString["pos"], out Pos);

            Min = (Pos - 1) - Before;
            Max = After + 1;

            // Do our requested Task
            switch (QueryString["type"])
            {
                case "score":
                    DoScore();
                    break;
                case "risingstar":
                    DoRisingStar();
                    break;
                case "kit":
                    DoKit();
                    break;
                case "vehicle":
                    DoVehicles();
                    break;
                case "weapon":
                    DoWeapons();
                    break;
                default:
                    //Response.HTTPStatusCode = ASPResponse.HTTPStatus.BadRequest;
                    Response.Send();
                    break;
            }
        }

        private void DoScore()
        {
            // Make sure we have a score sub type
            if (String.IsNullOrWhiteSpace(Id))
                return;

            // Prepare Output
            Response.WriteResponseStart();
            Response.WriteHeaderLine("size", "asof");
            List<Dictionary<string, object>> Rows;
            int Count;

            if (Id == "overall")
            {
                // Get Player count with a score
                Rows = Driver.Query("SELECT COUNT(id) AS count FROM player WHERE score > 0");
                Count = Int32.Parse(Rows[0]["count"].ToString());
                Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

                // Build New Header Output
                Response.WriteHeaderLine("n", "pid", "nick", "score", "totaltime", "playerrank", "countrycode");
                if (Count == 0)
                {
                    Response.Send();
                    return;
                }

                if (Pid == 0)
                {
                    string Query = "SELECT id, name, rank, country, time, score FROM player WHERE score > 0 ORDER BY score DESC, name DESC LIMIT @P0, @P1";
                    Rows = Driver.Query(Query, Min, Max);
                    foreach (Dictionary<string, object> Player in Rows)
                    {
                        Response.WriteDataLine(
                            Pos++,
                            Player["id"],
                            Player["name"].ToString().Trim(),
                            Player["score"],
                            Player["time"],
                            Player["rank"],
                            Player["country"].ToString().ToUpper()
                        );
                    }
                }
                else
                {
                    // Get Player Position
                    string Query = "SELECT id, name, rank, country, time, score FROM player WHERE id = @P0 ORDER BY score DESC, name DESC";
                    Rows = Driver.Query(Query, Pid);
                    if(Rows.Count > 0)
                    {
                        Response.WriteDataLine(
                            Int32.Parse(
                                Driver.Query("SELECT COUNT(id) as count FROM player WHERE score > @P0", Rows[0]["score"])[0]["count"].ToString()
                            ) + 1,
                            Rows[0]["id"],
                            Rows[0]["name"].ToString().Trim(),
                            Rows[0]["score"],
                            Rows[0]["time"],
                            Rows[0]["rank"],
                            Rows[0]["country"].ToString().ToUpper()
                        );
                    }
                }

                // Send Response
                Response.Send();
            }
            else if (Id == "commander")
            {
                Rows = Driver.Query("SELECT COUNT(id) AS count FROM player WHERE cmdscore > 0");
                Count = Int32.Parse(Rows[0]["count"].ToString());
                Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

                // Build New Header Output
                Response.WriteHeaderLine("n", "pid", "nick", "coscore", "cotime", "playerrank", "countrycode");
                if (Count == 0)
                {
                    Response.Send();
                    return;
                }

                if (Pid == 0)
                {
                    string Query = "SELECT id, name, rank, country, cmdtime, cmdscore FROM player "
                        + "WHERE cmdscore > 0 ORDER BY cmdscore DESC, name DESC LIMIT @P0, @P1";
                    Rows = Driver.Query(Query, Min, Max);
                    foreach (Dictionary<string, object> Player in Rows)
                    {
                        Response.WriteDataLine(
                            Pos++,
                            Player["id"],
                            Player["name"].ToString().Trim(),
                            Player["cmdscore"],
                            Player["cmdtime"],
                            Player["rank"],
                            Player["country"].ToString().ToUpper()
                        );
                    }
                }
                else
                {
                    // Get Player Position
                    string Query = "SELECT id, name, rank, country, cmdtime, cmdscore FROM player WHERE id = @P0";
                    Rows = Driver.Query(Query, Pid);
                    if(Rows.Count > 0)
                    {
                        Response.WriteDataLine(
                            Int32.Parse(
                                Driver.Query("SELECT COUNT(id) as count FROM player WHERE cmdscore > @P0", Rows[0]["cmdscore"])[0]["count"].ToString()
                            ) + 1,
                            Rows[0]["id"],
                            Rows[0]["name"].ToString().Trim(),
                            Rows[0]["cmdscore"],
                            Rows[0]["cmdtime"],
                            Rows[0]["rank"],
                            Rows[0]["country"].ToString().ToUpper()
                        );
                    }
                }

                // Send Response
                Response.Send();
            }
            else if (Id == "team")
            {
                Rows = Driver.Query("SELECT COUNT(id) AS count FROM player WHERE teamscore > 0");
                Count = Int32.Parse(Rows[0]["count"].ToString());
                Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

                // Build New Header Output
                Response.WriteHeaderLine("n", "pid", "nick", "teamscore", "totaltime", "playerrank", "countrycode");
                if (Count == 0)
                {
                    Response.Send();
                    return;
                }

                if (Pid == 0)
                {
                    string Query = "SELECT id, name, rank, country, time, teamscore FROM player "
                        + "WHERE teamscore > 0 ORDER BY teamscore DESC, name DESC LIMIT @P0, @P1";
                    Rows = Driver.Query(Query, Min, Max);
                    foreach (Dictionary<string, object> Player in Rows)
                    {
                        Response.WriteDataLine(
                            Pos++,
                            Player["id"],
                            Player["name"].ToString().Trim(),
                            Player["teamscore"],
                            Player["time"],
                            Player["rank"],
                            Player["country"].ToString().ToUpper()
                        );
                    }
                }
                else
                {
                    // Get Player Position
                    string Query = "SELECT id, name, rank, country, time, teamscore FROM player WHERE id = @P0";
                    Rows = Driver.Query(Query, Pid);
                    if(Rows.Count > 0)
                    {
                        Response.WriteDataLine(
                            Int32.Parse(
                                Driver.Query("SELECT COUNT(id) as count FROM player WHERE teamscore > @P0", Rows[0]["teamscore"])[0]["count"].ToString()
                            ) + 1,
                            Rows[0]["id"],
                            Rows[0]["name"].ToString().Trim(),
                            Rows[0]["teamscore"],
                            Rows[0]["time"],
                            Rows[0]["rank"],
                            Rows[0]["country"].ToString().ToUpper()
                        );
                    }
                }

                // Send Response
                Response.Send();
            }
            else if (Id == "combat")
            {
                Rows = Driver.Query("SELECT COUNT(id) AS count FROM player WHERE skillscore > 0");
                Count = Int32.Parse(Rows[0]["count"].ToString());
                Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

                // Build New Header Output
                Response.WriteHeaderLine("n", "pid", "nick", "score", "totalkills", "totaltime", "playerrank", "countrycode");
                if (Count == 0)
                {
                    Response.Send();
                    return;
                }

                if (Pid == 0)
                {
                    string Query = "SELECT id, name, rank, country, time, kills, skillscore FROM player "
                        + "WHERE skillscore > 0 ORDER BY skillscore DESC, name DESC LIMIT @P0, @P1";
                    Rows = Driver.Query(Query, Min, Max);
                    foreach (Dictionary<string, object> Player in Rows)
                    {
                        Response.WriteDataLine(
                            Pos++,
                            Player["id"],
                            Player["name"].ToString().Trim(),
                            Player["skillscore"],
                            Player["kills"],
                            Player["time"],
                            Player["rank"],
                            Player["country"].ToString().ToUpper()
                        );
                    }
                }
                else
                {
                    // Get Player Position
                    string Query = "SELECT id, name, rank, country, time, kills, skillscore FROM player WHERE id = @P0";
                    Rows = Driver.Query(Query, Pid);
                    if (Rows.Count > 0)
                    {
                        Response.WriteDataLine(
                            Int32.Parse(
                                Driver.Query("SELECT COUNT(id) as count FROM player WHERE skillscore > @P0", Rows[0]["skillscore"])[0]["count"].ToString()
                            ) + 1,
                            Rows[0]["id"],
                            Rows[0]["name"].ToString().Trim(),
                            Rows[0]["skillscore"],
                            Rows[0]["kills"],
                            Rows[0]["time"],
                            Rows[0]["rank"],
                            Rows[0]["country"].ToString().ToUpper()
                        );
                    }
                }

                // Send Response
                Response.Send();
            }
            else
            {
                //Response.HTTPStatusCode = ASPResponse.HTTPStatus.BadRequest;
                Response.Send();
            }
        }

        private void DoRisingStar()
        {
            // Fetch all players that made the rising star board
            int Timeframe = DateTime.UtcNow.ToUnixTimestamp() - (60 * 60 * 24 * 7);
            string Query = "SELECT COUNT(DISTINCT(id)) AS count FROM player_history WHERE score > 0 AND timestamp >= @P0";
            List<Dictionary<string, object>> Rows = Driver.Query(Query, Timeframe);

            // Write initial Headers
            int Count = Int32.Parse(Rows[0]["count"].ToString());
            Response.WriteResponseStart();
            Response.WriteHeaderLine("size", "asof");
            Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

            // Start a new header
            Response.WriteHeaderLine("n", "pid", "nick", "weeklyscore", "totaltime", "date", "playerrank", "countrycode");
            if (Count == 0)
            {
                Response.Send();
                return;
            }

            // Are we finding players position, or are we fetching the list?
            if (Pid == 0)
            {
                Query = "SELECT p.id, p.name, p.rank, p.country, p.time, sum(h.score) as weeklyscore, p.joined"
				    + " FROM player AS p JOIN player_history AS h ON p.id = h.id"
				    + " WHERE (h.score > 0 AND h.timestamp >= @P0)"
				    + " GROUP BY p.id"
				    + " ORDER BY weeklyscore DESC, name DESC LIMIT @P1, @P2";
                Rows = Driver.Query(Query, Timeframe, Min, Max);
                foreach (Dictionary<string, object> Player in Rows)
                {
                    DateTime FromUnix = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Int32.Parse(Player["joined"].ToString())).ToLocalTime();
                    string DateString = FromUnix.ToString( "MM/dd/yy hh:mm:00 tt" );
                    Response.WriteDataLine(
                        Pos++,
                        Player["id"],
                        Player["name"].ToString().Trim(),
                        Player["weeklyscore"],
                        Player["time"],
                        DateString,
                        Player["rank"],
                        Player["country"].ToString().ToUpper()
                    );
                }

                // Send Response
                Response.Send();
            }
            else
            {
                // Find players position
                Query = @"SELECT p.id, p.name, p.rank, p.country, p.time, sum(h.score) as weeklyscore, p.joined"
                    + " FROM player AS p JOIN player_history AS h ON p.id = h.id"
                    + " WHERE (h.score > 0 AND h.timestamp >= @P0)"
                    + " GROUP BY p.id"
                    + " ORDER BY weeklyscore DESC, name DESC";
                Rows = Driver.Query(Query, Timeframe);
                foreach (Dictionary<string, object> Player in Rows)
                {
                    if (Int32.Parse(Player["id"].ToString()) == Pid)
                    {
                        DateTime FromUnix = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Int32.Parse(Player["joined"].ToString())).ToLocalTime();
                        string DateString = FromUnix.ToString("MM/dd/yy hh:mm:00 tt");
                        Response.WriteDataLine(
                            Pos,
                            Player["id"],
                            Player["name"].ToString().Trim(),
                            Player["weeklyscore"],
                            Player["time"],
                            DateString,
                            Player["rank"],
                            Player["country"].ToString().ToUpper()
                        );
                        break;
                    }
                    Pos++;
                }

                // Send Response
                Response.Send();
            }
        }

        private void DoKit()
        {
            // Make sure we have a score sub type
            int KitId = 0;
            if (String.IsNullOrWhiteSpace(Id) || !Int32.TryParse(Id, out KitId))
            {
                Response.WriteResponseStart(false);
                Response.WriteHeaderLine("asof", "err");
                Response.WriteDataLine(DateTime.UtcNow.ToUnixTimestamp(), "Invalid Syntax!");
                Response.Send();
                return;
            }

            // Prepare variables
            String Query;
            List<Dictionary<string, object>> Rows;
            int Count;

            // Get total number of players who have at least 1 kill in kit
            Rows = Driver.Query(String.Format("SELECT COUNT(id) AS count FROM kits WHERE kills{0} > 0", Id));
            Count = Int32.Parse(Rows[0]["count"].ToString());
            Response.WriteResponseStart();
            Response.WriteHeaderLine("size", "asof");
            Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

            // Build New Header Output
            Response.WriteHeaderLine("n", "pid", "nick", "killswith", "deathsby", "timeplayed", "playerrank", "countrycode");

            // Get Leaderboard Positions
            Query = String.Format("SELECT player.id AS plid, name, rank, country, kills{0} AS kills, deaths{0} AS deaths, time{0} AS time"
                + " FROM player NATURAL JOIN kits WHERE kills{0} > 0 ORDER BY kills{0} DESC, name DESC", KitId);

            if (Pid == 0)
                Query += String.Format(" LIMIT {0}, {1}", Min, Max);

            Rows = Driver.Query(Query);
            foreach (Dictionary<string, object> Player in Rows)
            {
                if (Pid == 0 || Int32.Parse(Player["plid"].ToString()) == Pid)
                {
                    Response.WriteDataLine(
                        Pos,
                        Player["plid"],
                        Player["name"].ToString().Trim(),
                        Player["kills"],
                        Player["deaths"],
                        Player["time"],
                        Player["rank"],
                        Player["country"].ToString().ToUpper()
                    );

                    if (Pid != 0)
                        break;
                }
                Pos++;
            }

            // Send Response
            Response.Send();
        }

        private void DoVehicles()
        {
            // Make sure we have a score sub type
            int KitId = 0;
            if (String.IsNullOrWhiteSpace(Id) || !Int32.TryParse(Id, out KitId))
            {
                Response.WriteResponseStart(false);
                Response.WriteHeaderLine("asof", "err");
                Response.WriteDataLine(DateTime.UtcNow.ToUnixTimestamp(), "Invalid Syntax!");
                Response.Send();
                return;
            }

            // Prepare Variables
            String Query;
            List<Dictionary<string, object>> Rows;
            int Count;

            // Get total number of players who have at least 1 kill in kit
            Rows = Driver.Query(String.Format("SELECT COUNT(id) AS count FROM vehicles WHERE kills{0} > 0", Id));
            Count = Int32.Parse(Rows[0]["count"].ToString());
            Response.WriteResponseStart();
            Response.WriteHeaderLine("size", "asof");
            Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

            // Build New Header Output
            Response.WriteHeaderLine("n", "pid", "nick", "killswith", "detahsby", "timeused", "playerrank", "countrycode");

            // Get Leaderboard Positions
            Query = String.Format("SELECT player.id AS plid, name, rank, country, kills{0} AS kills, deaths{0} AS deaths, time{0} AS time"
                + " FROM player NATURAL JOIN vehicles WHERE kills{0} > 0 ORDER BY kills{0} DESC, name DESC", KitId);
            if (Pid == 0)
                Query += String.Format(" LIMIT {0}, {1}", Min, Max);

            Rows = Driver.Query(Query);
            foreach (Dictionary<string, object> Player in Rows)
            {
                if (Pid == 0 || Int32.Parse(Player["plid"].ToString()) == Pid)
                {
                    Response.WriteDataLine(
                        Pos,
                        Player["plid"],
                        Player["name"].ToString().Trim(),
                        Player["kills"],
                        Player["deaths"],
                        Player["time"],
                        Player["rank"],
                        Player["country"].ToString().ToUpper()
                    );

                    if (Pid != 0)
                        break;
                }
                Pos++;
            }

            // Send Response
            Response.Send();
        }

        private void DoWeapons()
        {
            // Make sure we have a score sub type
            int KitId = 0;
            if (String.IsNullOrWhiteSpace(Id) || !Int32.TryParse(Id, out KitId))
            {
                Response.WriteResponseStart(false);
                Response.WriteHeaderLine("asof", "err");
                Response.WriteDataLine(DateTime.UtcNow.ToUnixTimestamp(), "Invalid Syntax!");
                Response.Send();
                return;
            }

            // Prepare variables
            String Query;
            List<Dictionary<string, object>> Rows;
            int Count;

            // Get total number of players who have at least 1 kill in kit
            Rows = Driver.Query(String.Format("SELECT COUNT(id) AS count FROM weapons WHERE kills{0} > 0", Id));
            Count = Int32.Parse(Rows[0]["count"].ToString());
            Response.WriteResponseStart();
            Response.WriteHeaderLine("size", "asof");
            Response.WriteDataLine(Count, DateTime.UtcNow.ToUnixTimestamp());

            // Build New Header Output
            Response.WriteHeaderLine("n", "pid", "nick", "killswith", "detahsby", "timeused", "accuracy", "playerrank", "countrycode");

            // Get Leaderboard Positions
            Query = String.Format("SELECT player.id AS plid, name, rank, country, kills{0} AS kills, deaths{0} AS deaths, time{0} AS time, "
                + "hit{0} AS hit, fired{0} AS fired FROM player NATURAL JOIN weapons WHERE kills{0} > 0 ORDER BY kills{0} DESC, name DESC", KitId);
            if (Pid == 0)
                Query += String.Format(" LIMIT {0}, {1}", Min, Max);

            Rows = Driver.Query(Query);
            foreach (Dictionary<string, object> Player in Rows)
            {
                if (Pid == 0 || Int32.Parse(Player["plid"].ToString()) == Pid)
                {
                    float Hit = float.Parse(Player["hit"].ToString());
                    float Fired = float.Parse(Player["fired"].ToString());
                    float Acc = (Hit / Fired) * 100;
                    Response.WriteDataLine(
                        Pos,
                        Player["plid"],
                        Player["name"].ToString().Trim(),
                        Player["kills"],
                        Player["deaths"],
                        Player["time"],
                        Math.Round(Acc, 0),
                        Player["rank"],
                        Player["country"].ToString().ToUpper()
                    );

                    if (Pid != 0)
                        break;
                }
                Pos++;
            }

            // Send response
            Response.Send();
        }
    }
}
