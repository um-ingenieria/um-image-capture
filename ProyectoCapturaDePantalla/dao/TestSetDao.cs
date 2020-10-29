using ProyectoCapturaDePantalla.Domain.Phase;
using ProyectoCapturaDePantalla.Domain.TestSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class TestSetDao
    {
        private static TestSetDao instance = null;

        private TestSetDao()
        {
        }

        public static TestSetDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TestSetDao();
                }
                return instance;
            }
        }

        public static TestSet GetTestSet(int testSetId)
        {
            return Instance.getSet(testSetId);
        }

        private TestSet getSet(int testSetId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            int id = 0;
            string description = "";
            List<int> phasesIds = new List<int>();

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [ID], [DESCRIPTION], [PHASE_ID] FROM TEST_SET WHERE ID = {testSetId} order by [PHASE_ID]", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    id = int.Parse(Convert.ToString(dr["ID"]));
                    description = Convert.ToString(dr["DESCRIPTION"]);
                    phasesIds.Add(int.Parse(Convert.ToString(dr["PHASE_ID"])));
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando test set de la base de datos";
                Console.WriteLine(message + e.Message);
                throw e;
            } finally
            {
                dbConnection.Close();
            }

            List<Phase> phases = new List<Phase>();
            foreach (int phaseId in phasesIds)
            {
                phases.Add(PhaseDao.GetPhase(phaseId));
            }
            
            return new TestSet(id, description, phases);
        }
    }
}
