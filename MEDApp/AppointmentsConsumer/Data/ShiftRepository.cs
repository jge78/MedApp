using AppointmentsConsumer.Data.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AppointmentsConsumer.Data
{
    internal class ShiftRepository : IShiftRepository
    {
        private IDbConnection db;
        public ShiftRepository(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }
        public Shift Add(Shift Shift)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dbo.Shifts (Medic,DayOfWeek,StartTime,EndTime)");
            sb.Append($" VALUES({(int)Shift.Medic},{(int)Shift.DayOfWeek},'{Shift.StartTime.ToString("HH:mm:ss")}','{Shift.EndTime.ToString("HH:mm:ss")}');");
            sb.Append(" SELECT CAST(SCOPE_IDENTITY() AS int);");

            var id = db.Query<int>(sb.ToString(), Shift).Single();
            Shift.Id = id;
            return Shift;

        }

        public void Delete(int id)
        {
            db.Execute("DELETE FROM dbo.Shifts WHERE Id = @Id", new { id });
        }

        public Shift Get(int id)
        {
            return db.Query<Shift>("SELECT Id,Medic,DayOfWeek,CONVERT(SMALLDATETIME,StartTime) AS StartTime,CONVERT(SMALLDATETIME,EndTime) AS EndTime FROM dbo.Shifts WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Shift> GetAll()
        {
            return db.Query<Shift>("SELECT Id,Medic,DayOfWeek,CONVERT(SMALLDATETIME,StartTime) AS StartTime,CONVERT(SMALLDATETIME,EndTime) AS EndTime FROM dbo.Shifts").ToList(); 
        }

        public Shift Update(Shift Shift)
        {
            var sql =
            "UPDATE dbo.Shifts " +
            " SET Medic = @Medic," +
            " DayOfWeek = @DayOfWeek," +
            " StartTime = @StartTime" +
            " EndTime = @EndTime" +
            " WHERE Id = @Id";

            db.Execute(sql, Shift);
            return Shift;

        }
    }
}
