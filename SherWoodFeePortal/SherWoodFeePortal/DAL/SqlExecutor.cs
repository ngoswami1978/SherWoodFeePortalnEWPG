using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SherWoodFeePortal.DAL
{
    public class SqlExecutor
    {
    }

    public sealed class SQLHelper
    {
       
        #region Class Variables

        private static SqlTransaction _sqltrn = null;
        private static SqlConnection _sqlcon = null;
        private static SqlCommand _sqlcom = null;
        private static Boolean _result = false;
        private static Object _value = null;

        private static DataTable _resulttable = null;
        private static String typevalue = String.Empty;
        private static Dictionary<String, SqlDbType> _output = null;
        private static Dictionary<String, String> _paranames = null;

        #endregion

        #region Static Values

        public static string ConnectionValue = string.Empty;

        #endregion

        #region Private Methods

        private static void CreateParameters(List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                _output = null;
                _paranames = null;

                _output = new Dictionary<string, SqlDbType>();
                _paranames = new Dictionary<string, string>();

                if (_sqlcom.Parameters.Count != 0)
                    _sqlcom.Parameters.Clear();

                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.InputOutput)
                    {
                        _output.Add(parameter.SourceColumn, parameter.SqlDbType);
                        _paranames.Add(parameter.SourceColumn, parameter.ParameterName);
                    }

                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = DBNull.Value;
                        _output.Add(parameter.SourceColumn, parameter.SqlDbType);
                        _paranames.Add(parameter.SourceColumn, parameter.ParameterName);
                    }
                    _sqlcom.Parameters.Add(parameter);
                }

                if (_output.Count != 0)
                {
                    _resulttable = new DataTable("Result");
                    DataColumn col = null;

                    SqlDbType dbtype;
                    foreach (String key in _output.Keys)
                    {
                        dbtype = (SqlDbType)_output[key];
                        col = new DataColumn(key, Type.GetType(GetColumnType(dbtype)));
                        _resulttable.Columns.Add(col);
                    }
                }
            }
        }
        private static String GetColumnType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return "System.Int64";

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return "System.Byte[]";

                case SqlDbType.Bit:
                    return "System.Boolean";

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return "System.String";

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return "System.DateTime";

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return "System.Decimal";

                case SqlDbType.Float:
                    return "System.Double";

                case SqlDbType.Int:
                    return "System.Int32";

                case SqlDbType.Real:
                    return "System.Single";

                case SqlDbType.UniqueIdentifier:
                    return "System.Guid";

                case SqlDbType.SmallInt:
                    return "System.Int16";

                case SqlDbType.TinyInt:
                    return "System.Byte";

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return "System.Object";

                case SqlDbType.Structured:
                    return "System.Data.DataTable";

                case SqlDbType.DateTimeOffset:
                    return "System.DateTimeOffset";

                default:
                    throw new ArgumentOutOfRangeException("Invalid SQL Data type");
            }
        }
        private static void FillTable()
        {
            if (_resulttable != null)
            {
                DataRow row = _resulttable.NewRow();

                foreach (String item in _output.Keys)
                    row[item] = _sqlcom.Parameters[_paranames[item].ToString()].Value;

                _resulttable.Rows.Add(row);
            }
        }

        #endregion

        #region Execute SQLHelper

        public static void CreateObjects(Boolean istransaction)
        {
            _sqlcon = new SqlConnection(SQLHelper.ConnectionValue);
            _sqlcon.Open();

            if (istransaction)
                _sqltrn = _sqlcon.BeginTransaction(IsolationLevel.Serializable);

            _sqlcom = new SqlCommand();
            _sqlcom.Connection = _sqlcon;
            _sqlcom.CommandType = CommandType.StoredProcedure;

            if (istransaction)
                _sqlcom.Transaction = _sqltrn;
        }
        public static void CommitTransction()
        {
            if (_sqltrn != null)
                _sqltrn.Commit();
        }
        public static void RollBackTransction()
        {
            if (_sqltrn != null)
                _sqltrn.Rollback();
        }
        public static void ClearObjects()
        {
            if (_sqlcom != null)
            {
                if (_sqlcom.Parameters.Count != 0)
                    _sqlcom.Parameters.Clear();

                _sqlcom.Cancel();
                _sqlcom.Dispose();

                _sqlcom = null;
            }

            if (_sqltrn != null)
            {
                _sqltrn.Dispose();
                _sqltrn = null;
            }

            if (_sqlcon != null)
            {
                if (_sqlcon.State == ConnectionState.Open)
                    _sqlcon.Close();

                _sqlcon.Dispose();
                _sqlcon = null;
            }

            if (_output != null)
                _output = null;

            if (_paranames != null)
                _paranames = null;
        }

        public static Boolean SQLHelper_ExecuteNonQuery(string Procedure_Name)
        {
            try
            {
                _result = false;
                _sqlcom.CommandText = Procedure_Name;
                _sqlcom.ExecuteNonQuery();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }
        public static Boolean SQLHelper_ExecuteNonQuery(string Procedure_Name, List<SqlParameter> parameters)
        {
            try
            {
                _result = false;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                _sqlcom.ExecuteNonQuery();
                SQLHelper.FillTable();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public static Object SQLHelper_ExecuteScalar(string Procedure_Name)
        {
            try
            {
                _value = null;
                _sqlcom.CommandText = Procedure_Name;
                _value = _sqlcom.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _value;
        }
        public static Object SQLHelper_ExecuteScalar(string Procedure_Name, List<SqlParameter> parameters)
        {
            try
            {
                _value = null;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                _value = _sqlcom.ExecuteScalar();
                SQLHelper.FillTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _value;
        }

        public static DataTable SQLHelper_ExecuteReader(string Procedure_Name)
        {
            DataTable _data = null;
            try
            {
                _data = null;
                _sqlcom.CommandText = Procedure_Name;
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
                DataSet dataset = new DataSet("SQLHelper");
                _adapter.Fill(dataset);
                _data = dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _data;
        }
        public static DataTable SQLHelper_ExecuteReader(string Procedure_Name, List<SqlParameter> parameters)
        {
            DataTable _data = null;
            try
            {
                _data = null;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
                DataSet dataset = new DataSet("SQLHelper");
                _adapter.Fill(dataset);
                _data = dataset.Tables[0];
                SQLHelper.FillTable();
                _sqltrn.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _data;
        }

        public static DataTable SQLHelper_OutputValues()
        {
            return _resulttable;
        }

        #endregion
    }
}