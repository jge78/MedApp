﻿using AppointmentsConsumer.Data.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AppointmentsConsumer.Data
{
    internal class AppointmentRepository : IAppointmentRepository
    {

        private IDbConnection db;
        public AppointmentRepository(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }
        public Appointment Add(Appointment appointment)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dbo.appointments (Patient,Medic,Date,Time)");
            sb.Append($" VALUES({(int)appointment.Patient},{(int)appointment.Medic},'{appointment.Date.ToString("yyyyMMdd")}','{appointment.Time.ToString("HH:mm:ss")}');");
            sb.Append(" SELECT CAST(SCOPE_IDENTITY() AS int);");

            var id = db.Query<int>(sb.ToString(), appointment).Single();
            appointment.Id = id;
            return appointment;

        }

        public void Delete(int id)
        {
            this.db.Execute("DELETE FROM dbo.Appointments WHERE Id = @Id", new { id });
        }

        public Appointment Get(int id)
        {
            return this.db.Query<Appointment>("SELECT Id,Patient,Medic,[Date],CONVERT(SMALLDATETIME,[Time]) AS Time FROM dbo.Appointments WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Appointment> GetAll()
        {
            return db.Query<Appointment>("SELECT Id,Patient,Medic,[Date],CONVERT(SMALLDATETIME,[Time]) AS Time FROM dbo.Appointments").ToList();
        }

        public Appointment Update(Appointment appointment)
        {

            var sql =
            "UPDATE dbo.Appointments " +
            "SET Patient = @Patient," +
            " Medic = @Medic," +
            " Date = @Date," +
            " Time = @Time" +
            " WHERE Id = @Id";

            this.db.Execute(sql, appointment);
            return appointment;
        }
    }
}
