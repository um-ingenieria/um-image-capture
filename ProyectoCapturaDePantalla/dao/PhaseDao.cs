using ProyectoCapturaDePantalla.Domain.Phase;
using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class PhaseDao
    {
        private static PhaseDao instance = null;

        private PhaseDao()
        {
        }

        public static PhaseDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhaseDao();
                }
                return instance;
            }
        }

        public static PhaseBase GetPhase(int phaseId)
        {
            return Instance.getPhase(phaseId);
        }

        private PhaseBase getPhase(int phaseId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            int id = 0;
            string description = "";
            string name = "";
            string phaseType = "";
            string valenceArousalQuadrant = "";
            List<int> stimulusIds;

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [ID], [DESCRIPTION], [NAME], [PHASE_TYPE], [VALENCE_AROUSAL_QUARDRANT], [STIMULI_ID] FROM PHASE WHERE ID = {phaseId}", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                stimulusIds = new List<int>();

                while (dr.Read())
                {
                    id = int.Parse(Convert.ToString(dr["ID"]));
                    description = Convert.ToString(dr["DESCRIPTION"]);
                    name = Convert.ToString(dr["NAME"]);
                    phaseType = Convert.ToString(dr["PHASE_TYPE"]);
                    valenceArousalQuadrant = Convert.ToString(dr["VALENCE_AROUSAL_QUARDRANT"]);
                    stimulusIds.Add(int.Parse(Convert.ToString(dr["STIMULI_ID"])));
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando phase de la base de datos";
                Console.WriteLine(message + e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }

            PhaseBase phaseBase;
            switch (phaseType)
            {
                case ImagePhase.IAP_TYPE:
                    List<IAP> iapsList = IAPSDao.GetIAPS(stimulusIds, IAP.ALL_SUBJECTS);
                    phaseBase = new ImagePhase(id, name, description, valenceArousalQuadrant, iapsList);
                    break;
                default:
                    throw new TypeUnloadedException("Tipo de fase no definido");
            }

            return phaseBase;
        }
    }
}
