using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace BaistClubAutomation.Pages.Manager
{
   

    public class ScoreManager : IScoreManager
    {
        private readonly string _connectionString = "Your_sarumugam3_Connection_String";

        public List<Score> GetLast20Scores(int memberId)
        {
            List<Score> scores = new List<Score>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // SQL query to get only the top 20 most recent rounds for this member
                string query = @"SELECT TOP 20 TotalScore, CourseRating, SlopeRating, RoundDate 
                             FROM PlayerScores 
                             WHERE MemberId = @MemberId 
                             ORDER BY RoundDate DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MemberId", memberId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    scores.Add(new Score
                    {
                        TotalScore = reader.GetDouble(0),
                        CourseRating = reader.GetDouble(1),
                        SlopeRating = reader.GetDouble(2),
                        RoundDate = reader.GetDateTime(3)
                    });
                }
            }
            return scores;
        }
    }
}

