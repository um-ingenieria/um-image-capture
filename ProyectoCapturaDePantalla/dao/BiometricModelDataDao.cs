using ProyectoCapturaDePantalla.Domain;
using ProyectoCapturaDePantalla.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class BiometricModelDataDao
    {
        private static BiometricModelDataDao instance = null;

        private BiometricModelDataDao()
        {
        }

        public static BiometricModelDataDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BiometricModelDataDao();
                }
                return instance;
            }
        }

        public static void SaveBiometricModelDataToCsv(List<BiometricModelData> model)
        {
            Instance.saveBiometricModelDataToCsv(model);
        }

        private void saveBiometricModelDataToCsv(List<BiometricModelData> model)
        {
            CsvExport<BiometricModelData> csv = new CsvExport<BiometricModelData>(model);
            csv.ExportToFile(string.Concat(ConfigurationManager.AppSettings["data-path"],"\\biometrics.csv"));
        }
    }
}
