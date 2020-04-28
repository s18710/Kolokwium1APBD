using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwiums18710.models;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwiums18710.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PrescriptionController : ControllerBase
    {
        [HttpGet("/prescriptions/{name}")]
        public IActionResult getPrescriptions(string name)
        {
            Boolean isNameGiven = false;
            ArrayList prescriptions = new ArrayList();
            if(name != null)
            {
                isNameGiven = true;
            }
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                if (isNameGiven)
                {
                    com.CommandText = "select IdPrescription, Date, DueDate, pr.IdPatient, pr.IdDoctor from Prescription pr, Patient pa"+
                    " where pr.IdPatient = pa.IdPatient and pa.LastName like @name";
                    com.Parameters.AddWithValue("name", name);

                    try
                    {
                        client.Open();
                        var dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            var pr = new Prescription();
                            pr.date = dr["Date"].ToString();
                            pr.dueDate = dr["DueDate"].ToString();
                            pr.IdPrescription = Int32.Parse(dr["IdPrescription"].ToString());
                            pr.IdPatient = Int32.Parse(dr["IdPatient"].ToString());
                            pr.IdDoctor = Int32.Parse(dr["IdDoctor"].ToString());
                            prescriptions.Add(pr);
                        }

                        if(prescriptions.Capacity == 0)
                        {
                            return NotFound();
                        }

                        return Ok(prescriptions);

                    }
                    catch (SqlException)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    com.CommandText = "select * from Prescription order by Date";
                    try
                    {
                        client.Open();
                        var dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            var pr = new Prescription();
                            pr.date = dr["Date"].ToString();
                            pr.dueDate = dr["DueDate"].ToString();
                            pr.IdPrescription = Int32.Parse(dr["IdPrescription"].ToString());
                            pr.IdPatient = Int32.Parse(dr["IdPatient"].ToString());
                            pr.IdDoctor = Int32.Parse(dr["IdDoctor"].ToString());
                            prescriptions.Add(pr);
                        }

                        if (prescriptions.Capacity == 0)
                        {
                            return NotFound();
                        }

                        return Ok(prescriptions);

                    }
                    catch (SqlException)
                    {
                        return NotFound();
                    }
                }
               

          
            }
        }

        [HttpPost("/prescription/{pid}/medicaments")]
        public IActionResult addMedicineToPrescription(String pid, MedicamentsCollection collection)
        {
            
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                SqlTransaction transaction;
                transaction = client.BeginTransaction("MedicamentTransaction");
                com.Transaction = transaction;
                foreach (Medicament medicament in collection.medicaments)
                {


                    com.CommandText = "select IdMedicament as id from Prescription_Medicament where IdMedicament like @id ";
                    com.Parameters.AddWithValue("id", medicament.IdMedicament);

                    client.Open();
                    var dr = com.ExecuteReader();
                    var checkId = -1;
                    while (dr.Read())
                    {
                        checkId = Int32.Parse(dr["id"].ToString());
                    }

                    if (checkId == -1)
                    {
                        transaction.Rollback();
                        return BadRequest();
                    }
                    try
                    {
                        com.CommandText = "insert into Prescription_Medicament values (@mid,@pid,@dose,@details)";
                        com.Parameters.AddWithValue("mid", medicament.IdMedicament);
                        com.Parameters.AddWithValue("pid", pid);
                        com.Parameters.AddWithValue("mid", medicament.dose);
                        com.Parameters.AddWithValue("mid", medicament.details);
                        com.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        return BadRequest();
                    }
                }
            }
            return Ok();
        }
    }
}