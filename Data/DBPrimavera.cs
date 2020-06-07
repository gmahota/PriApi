using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PriApi.Data
{
    public class DBPrimavera
    {
        
        public string connectionString { get; set; }

        public DBPrimavera(string conn)
        {
            connectionString = conn;
        }

        public int ExecutaQuery(string querySQL)
        {
            
            try
            {
                DataTable dt = new DataTable();

                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.CommandText = querySQL;

                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public DataTable ConsultaSQLDatatable(string querySql)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlConnection con = new SqlConnection(connectionString);

                SqlDataAdapter da = new SqlDataAdapter(querySql, con);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddLinhaVazia(DataTable dt)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable daListaTabela(string tabela, int maximo = 0, string campos = "", string filtros = "", string juncoes = "", string ordenacao = "")
        {
            try
            {
                string strSql = "select ";

                DataTable dt;

                if (maximo > 0) strSql = strSql + string.Format("Top {0} ", maximo);

                if ((campos.Length > 0))
                {
                    strSql = (strSql + campos);
                }
                else
                {
                    strSql = (strSql + " * ");
                }

                strSql = (strSql + (" from " + tabela));
                if ((juncoes.Length > 0))
                {
                    strSql = (strSql + (" " + juncoes));
                }

                if ((filtros.Length > 0))
                {
                    strSql = (strSql + (" where " + filtros));
                }

                if ((ordenacao.Length > 0))
                {
                    strSql = (strSql + (" order by " + ordenacao));
                }

                dt = this.ConsultaSQLDatatable(strSql);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
