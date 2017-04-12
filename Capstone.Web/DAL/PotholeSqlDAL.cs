using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class PotholeSqlDAL : IPotholeDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["potholeDB"].ConnectionString;

        public bool ReportPothole(PotholeModel newPothole)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand addPothole = new SqlCommand($"INSERT INTO pothole(longitude, latitude, whoReported, reportDate) VALUES(@longitude, @latitude, @whoReported, @reportDate);", conn);

                    addPothole.Parameters.AddWithValue("@longitude", newPothole.Longitude);
                    addPothole.Parameters.AddWithValue("@latitude", newPothole.Latitude);
                    addPothole.Parameters.AddWithValue("@whoReported", newPothole.WhoReported);
                    addPothole.Parameters.AddWithValue("@reportDate", newPothole.ReportDate);


                    int result = addPothole.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public List<PotholeModel> GetAllPotholes()
        {
            List<PotholeModel> potholeList = new List<PotholeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE repairEndDate > GETDATE()- 180 OR repairEndDate IS NULL;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PotholeModel ph = new PotholeModel();

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = (reader["whoInspected"] != DBNull.Value) ? Convert.ToInt32(reader["whoInspected"]) : -1;
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);

                        DateTime? inspectDate = null;
                        if (reader["inspectDate"] != DBNull.Value)
                        {
                            inspectDate = Convert.ToDateTime(reader["inspectDate"]);
                        }

                        DateTime? repairStartDate = null;
                        if (reader["repairStartDate"] != DBNull.Value)
                        {
                            repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        }

                        DateTime? repairEndDate = null;
                        if (reader["repairEndDate"] != DBNull.Value)
                        {
                            repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        }

                        int severity = (reader["severity"] != DBNull.Value) ? Convert.ToInt32(reader["severity"]) : -1;
                        string comment = Convert.ToString(reader["comment"]);

                        ph.PotholeID = potholeID;
                        ph.Longitude = longitude;
                        ph.Latitude = latitude;
                        ph.WhoReported = whoReported;
                        ph.WhoInspected = whoInspected;
                        ph.Picture = picture;
                        ph.ReportDate = reportDate;
                        ph.InspectDate = inspectDate;
                        ph.RepairStartDate = repairStartDate;
                        ph.RepairEndDate = repairEndDate;
                        ph.Severity = severity;
                        ph.Comment = comment;

                        potholeList.Add(ph);
                    }
                    return potholeList;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public PotholeModel GetOnePothole(string id)
        {
            PotholeModel pothole = new PotholeModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE potholeId=@pothId", conn);
                    cmd.Parameters.AddWithValue("@pothId", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = Convert.ToInt32(reader["whoInspected"]);
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);
                        DateTime inspectDate = Convert.ToDateTime(reader["inspectDate"]);
                        DateTime repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        DateTime repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        int severity = Convert.ToInt32(reader["severity"]);
                        string comment = Convert.ToString(reader["comment"]);

                        pothole.PotholeID = potholeID;
                        pothole.Longitude = longitude;
                        pothole.Latitude = latitude;
                        pothole.WhoReported = whoReported;
                        pothole.WhoInspected = whoInspected;
                        pothole.Picture = picture;
                        pothole.ReportDate = reportDate;
                        pothole.InspectDate = inspectDate;
                        pothole.RepairStartDate = repairStartDate;
                        pothole.RepairEndDate = repairEndDate;
                        pothole.Severity = severity;
                        pothole.Comment = comment;

                    }
                    return pothole;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdatePothole(PotholeModel existingPothole, int whoInspected)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand updatePothole = new SqlCommand($"UPDATE pothole SET whoInspected = @whoInspected, inspectDate = @inspectDate, repairStartDate = @repairStartDate, repairEndDate = @repairEndDate, severity = @severity, comment = @comment WHERE potholeID = @potholeID;", conn);

                    updatePothole.Parameters.AddWithValue("@whoInspected", whoInspected);
                    updatePothole.Parameters.AddWithValue("@inspectDate", existingPothole.InspectDate);
                    if (existingPothole.RepairStartDate == null)
                    {
                        updatePothole.Parameters.AddWithValue("@repairStartDate", DBNull.Value);
                    }
                    else
                    {
                        updatePothole.Parameters.AddWithValue("@repairStartDate", existingPothole.RepairStartDate);
                    }
                    updatePothole.Parameters.AddWithValue("@repairEndDate", existingPothole.RepairEndDate);
                    updatePothole.Parameters.AddWithValue("@severity", existingPothole.Severity);
                    updatePothole.Parameters.AddWithValue("@comment", existingPothole.Comment);
                    updatePothole.Parameters.AddWithValue("@potholeID", existingPothole.PotholeID);

                    int result = updatePothole.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public List<PotholeModel> GetPotholesUninspected()
        {
            List<PotholeModel> potholeListUninspected = new List<PotholeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE inspectDate IS NULL;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PotholeModel ph = new PotholeModel();

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = (reader["whoInspected"] != DBNull.Value) ? Convert.ToInt32(reader["whoInspected"]) : -1;
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);

                        DateTime? inspectDate = null;
                        if (reader["inspectDate"] != DBNull.Value)
                        {
                            inspectDate = Convert.ToDateTime(reader["inspectDate"]);
                        }

                        DateTime? repairStartDate = null;
                        if (reader["repairStartDate"] != DBNull.Value)
                        {
                            repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        }

                        DateTime? repairEndDate = null;
                        if (reader["repairEndDate"] != DBNull.Value)
                        {
                            repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        }

                        int severity = (reader["severity"] != DBNull.Value) ? Convert.ToInt32(reader["severity"]) : -1;
                        string comment = Convert.ToString(reader["comment"]);

                        ph.PotholeID = potholeID;
                        ph.Longitude = longitude;
                        ph.Latitude = latitude;
                        ph.WhoReported = whoReported;
                        ph.WhoInspected = whoInspected;
                        ph.Picture = picture;
                        ph.ReportDate = reportDate;
                        ph.InspectDate = inspectDate;
                        ph.RepairStartDate = repairStartDate;
                        ph.RepairEndDate = repairEndDate;
                        ph.Severity = severity;
                        ph.Comment = comment;

                        potholeListUninspected.Add(ph);
                    }
                    return potholeListUninspected;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<PotholeModel> GetInspectedOnly()
        {
            List<PotholeModel> potholeListInspected = new List<PotholeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE inspectDate IS NOT NULL AND repairStartDate IS NULL;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PotholeModel ph = new PotholeModel();

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = (reader["whoInspected"] != DBNull.Value) ? Convert.ToInt32(reader["whoInspected"]) : -1;
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);
                        DateTime inspectDate = Convert.ToDateTime(reader["inspectDate"]);
                        
                        DateTime? repairStartDate = null;
                        if (reader["repairStartDate"] != DBNull.Value)
                        {
                            repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        }

                        DateTime? repairEndDate = null;
                        if (reader["repairEndDate"] != DBNull.Value)
                        {
                            repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        }

                        int severity = (reader["severity"] != DBNull.Value) ? Convert.ToInt32(reader["severity"]) : -1;
                        string comment = Convert.ToString(reader["comment"]);

                        ph.PotholeID = potholeID;
                        ph.Longitude = longitude;
                        ph.Latitude = latitude;
                        ph.WhoReported = whoReported;
                        ph.WhoInspected = whoInspected;
                        ph.Picture = picture;
                        ph.ReportDate = reportDate;
                        ph.InspectDate = inspectDate;
                        ph.RepairStartDate = repairStartDate;
                        ph.RepairEndDate = repairEndDate;
                        ph.Severity = severity;
                        ph.Comment = comment;

                        potholeListInspected.Add(ph);
                    }
                    return potholeListInspected;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<PotholeModel> GetRepairsInProgress()
        {
            List<PotholeModel> potholeListInspected = new List<PotholeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE inspectDate IS NOT NULL AND repairStartDate IS NOT NULL AND repairEndDate IS NULL;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PotholeModel ph = new PotholeModel();

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = (reader["whoInspected"] != DBNull.Value) ? Convert.ToInt32(reader["whoInspected"]) : -1;
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);
                        DateTime inspectDate = Convert.ToDateTime(reader["inspectDate"]);

                        DateTime? repairStartDate = null;
                        if (reader["repairStartDate"] != DBNull.Value)
                        {
                            repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        }

                        DateTime? repairEndDate = null;
                        if (reader["repairEndDate"] != DBNull.Value)
                        {
                            repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        }

                        int severity = (reader["severity"] != DBNull.Value) ? Convert.ToInt32(reader["severity"]) : -1;
                        string comment = Convert.ToString(reader["comment"]);

                        ph.PotholeID = potholeID;
                        ph.Longitude = longitude;
                        ph.Latitude = latitude;
                        ph.WhoReported = whoReported;
                        ph.WhoInspected = whoInspected;
                        ph.Picture = picture;
                        ph.ReportDate = reportDate;
                        ph.InspectDate = inspectDate;
                        ph.RepairStartDate = repairStartDate;
                        ph.RepairEndDate = repairEndDate;
                        ph.Severity = severity;
                        ph.Comment = comment;

                        potholeListInspected.Add(ph);
                    }
                    return potholeListInspected;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<PotholeModel> GetRepairedPotholes()
        {
            List<PotholeModel> potholeListInspected = new List<PotholeModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // !!!!!!!!!!!!
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM pothole WHERE repairEndDate IS NOT NULL AND repairEndDate > GETDATE()- 180;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PotholeModel ph = new PotholeModel();

                        int potholeID = Convert.ToInt32(reader["potholeID"]);
                        double longitude = Convert.ToDouble(reader["longitude"]);
                        double latitude = Convert.ToDouble(reader["latitude"]);
                        int whoReported = Convert.ToInt32(reader["whoReported"]);
                        int whoInspected = (reader["whoInspected"] != DBNull.Value) ? Convert.ToInt32(reader["whoInspected"]) : -1;
                        string picture = Convert.ToString(reader["picture"]);
                        DateTime reportDate = Convert.ToDateTime(reader["reportDate"]);
                        DateTime inspectDate = Convert.ToDateTime(reader["inspectDate"]);

                        DateTime? repairStartDate = null;
                        if (reader["repairStartDate"] != DBNull.Value)
                        {
                            repairStartDate = Convert.ToDateTime(reader["repairStartDate"]);
                        }

                        DateTime? repairEndDate = null;
                        if (reader["repairEndDate"] != DBNull.Value)
                        {
                            repairEndDate = Convert.ToDateTime(reader["repairEndDate"]);
                        }

                        int severity = (reader["severity"] != DBNull.Value) ? Convert.ToInt32(reader["severity"]) : -1;
                        string comment = Convert.ToString(reader["comment"]);

                        ph.PotholeID = potholeID;
                        ph.Longitude = longitude;
                        ph.Latitude = latitude;
                        ph.WhoReported = whoReported;
                        ph.WhoInspected = whoInspected;
                        ph.Picture = picture;
                        ph.ReportDate = reportDate;
                        ph.InspectDate = inspectDate;
                        ph.RepairStartDate = repairStartDate;
                        ph.RepairEndDate = repairEndDate;
                        ph.Severity = severity;
                        ph.Comment = comment;

                        potholeListInspected.Add(ph);
                    }
                    return potholeListInspected;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool DeletePothole(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand deletePothole = new SqlCommand($"DELETE FROM pothole WHERE potholeId=@potholeId;", conn);
                        
                    deletePothole.Parameters.AddWithValue("@potholeId", id);
                    
                    int result = deletePothole.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UpdateInspectDate(int employeeId, int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand updateInspectDate = new SqlCommand($"UPDATE pothole SET whoInspected = @whoInspected, inspectDate = @inspectDate WHERE potholeId=@potholeId", conn);

                    updateInspectDate.Parameters.AddWithValue("@whoInspected", employeeId);
                    updateInspectDate.Parameters.AddWithValue("@inspectDate", DateTime.Now);
                    updateInspectDate.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = updateInspectDate.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UpdateStartRepairDate(int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand updateStartRepairDate = new SqlCommand($"UPDATE pothole SET repairStartDate = @repairStartDate WHERE potholeId=@potholeId", conn);

                    updateStartRepairDate.Parameters.AddWithValue("@repairStartDate", DateTime.Now);
                    updateStartRepairDate.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = updateStartRepairDate.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UpdateEndRepairDate(int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand updateEndRepairDate = new SqlCommand($"UPDATE pothole SET repairEndDate = @repairEndDate WHERE potholeId=@potholeId", conn);

                    updateEndRepairDate.Parameters.AddWithValue("@repairEndDate", DateTime.Now);
                    updateEndRepairDate.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = updateEndRepairDate.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UpdateSeverity(int potholeId, int severity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand updateSeverity = new SqlCommand($"UPDATE pothole SET severity = @severity WHERE potholeId=@potholeId", conn);

                    updateSeverity.Parameters.AddWithValue("@severity", severity);
                    updateSeverity.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = updateSeverity.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UndoInspect(int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand UndoInspect = new SqlCommand($"UPDATE pothole SET whoInspected = @whoInspected, inspectDate = @inspectDate, severity=@severity WHERE potholeId=@potholeId", conn);

                    UndoInspect.Parameters.AddWithValue("@whoInspected", DBNull.Value);
                    UndoInspect.Parameters.AddWithValue("@inspectDate", DBNull.Value);
                    UndoInspect.Parameters.AddWithValue("@severity", DBNull.Value);
                    UndoInspect.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = UndoInspect.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UndoStartRepair(int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand UndoStartRepair = new SqlCommand($"UPDATE pothole SET repairStartDate = @repairStartDate WHERE potholeId=@potholeId", conn);

                    UndoStartRepair.Parameters.AddWithValue("@repairStartDate", DBNull.Value);
                    UndoStartRepair.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = UndoStartRepair.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public bool UndoRepairComplete(int potholeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand UndoRepairComplete = new SqlCommand($"UPDATE pothole SET repairEndDate = @repairEndDate WHERE potholeId=@potholeId", conn);

                    UndoRepairComplete.Parameters.AddWithValue("@repairEndDate", DBNull.Value);
                    UndoRepairComplete.Parameters.AddWithValue("@potholeId", potholeId);

                    int result = UndoRepairComplete.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

    }
}


