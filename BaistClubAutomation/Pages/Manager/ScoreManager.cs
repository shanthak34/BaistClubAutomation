using Microsoft.Data.SqlClient;
using BaistClubAutomation.Pages.Models;

namespace BaistClubAutomation.Pages.Manager
{
    public interface IScoreManager
    {
        bool AddScore(Score score);
        bool CheckMemberExists(int memberNumber);
        List<Score> GetRecentScores(int memberNumber, int count);
        
    }

    public class ScoreManager : IScoreManager
    {
        private readonly string _connectionString = "Server=your_server;Database=sarumugam3;User Id=sarumugam3;Password=your_password;TrustServerCertificate=True;";

        public bool CheckMemberExists(int memberNumber)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Members WHERE MemberNumber = @mn", conn);
            cmd.Parameters.AddWithValue("@mn", memberNumber);
            return (int)cmd.ExecuteScalar() > 0;
        }

        public bool AddScore(Score score)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            string sql = @"INSERT INTO Scores (MemberNumber, DatePlayed, GolfCourse, TeeColor, CourseRating, SlopeRating, TotalGrossScore, HandicapDifferential) 
                           VALUES (@mn, @dp, @gc, @tc, @cr, @sr, @tgs, @hd)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mn", score.MemberNumber);
            cmd.Parameters.AddWithValue("@dp", score.DatePlayed);
            cmd.Parameters.AddWithValue("@gc", score.GolfCourse);
            cmd.Parameters.AddWithValue("@tc", score.TeeColor ?? "White");
            cmd.Parameters.AddWithValue("@cr", score.CourseRating);
            cmd.Parameters.AddWithValue("@sr", score.SlopeRating);
            cmd.Parameters.AddWithValue("@tgs", score.TotalGrossScore);
            cmd.Parameters.AddWithValue("@hd", score.HandicapDifferential);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<Score> GetRecentScores(int memberNumber, int count)
        {
            var scores = new List<Score>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT TOP (@c) HandicapDifferential, DatePlayed, GolfCourse FROM Scores WHERE MemberNumber = @mn ORDER BY DatePlayed DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@c", count);
                cmd.Parameters.AddWithValue("@mn", memberNumber);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    scores.Add(new Score
                    {
                        HandicapDifferential = reader.GetDouble(0),
                        DatePlayed = reader.GetDateTime(1),
                        GolfCourse = reader.GetString(2)
                    });
                }
            }
            return scores;
        }
    }
}