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

        public static Phase GetPhase(int phaseId)
        {
            return Instance.getPhase(phaseId);
        }

        private Phase getPhase(int phaseId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            int id = 0;
            string description = "";
            string name = "";
            string phaseType = "";
            string valenceArousalQuadrant = "";
            
            List<float> iapsIds;
            List<float> devoIds;

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [ID], [DESCRIPTION], [NAME], [STIMULI_TYPE], [VALENCE_AROUSAL_QUARDRANT], [STIMULI_ID] FROM PHASE WHERE ID = {phaseId}", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                iapsIds = new List<float>();
                devoIds = new List<float>();

                while (dr.Read())
                {
                    id = int.Parse(Convert.ToString(dr["ID"]));
                    description = Convert.ToString(dr["DESCRIPTION"]);
                    name = Convert.ToString(dr["NAME"]);
                    valenceArousalQuadrant = Convert.ToString(dr["VALENCE_AROUSAL_QUARDRANT"]);
                    
                    if (Convert.ToString(dr["STIMULI_TYPE"]) == IAP.IAP_TYPE)
                    {
                        iapsIds.Add(float.Parse(Convert.ToString(dr["STIMULI_ID"])));
                    } else if (Convert.ToString(dr["STIMULI_TYPE"]) == DEVO.DEVO_TYPE)
                    {
                        devoIds.Add(float.Parse(Convert.ToString(dr["STIMULI_ID"])));
                    }
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

            Phase phaseBase = new Phase(id, name, description, valenceArousalQuadrant);
            if (iapsIds.Count > 0)
            {
                phaseBase.Stimulis.AddRange(IAPSDao.GetIAPS(iapsIds, IAP.ALL_SUBJECTS));
            }
            if (devoIds.Count > 0)
            {
                phaseBase.Stimulis.AddRange(DEVODao.GetVideos(devoIds, DEVO.ALL_SUBJECTS));
            }

            return phaseBase;
        }
    }
}
