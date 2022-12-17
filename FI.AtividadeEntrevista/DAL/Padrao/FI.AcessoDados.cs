﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    internal class AcessoDados
    {
        private string stringDeConexao => @"Data Source=THIAGO;AttachDbFilename=|DataDirectory|\BancoDeDados.mdf;Integrated Security=True;Initial Catalog=BancoDeDados;Password=mssqlserver;User ID=sa;";
        // {
        //     get
        //     {
        //         var conn = ConfigurationManager.ConnectionStrings["BancoDeDados"];
        //         if (conn != null)
        //             return conn.ConnectionString;
        //         else
        //             return string.Empty;
        //     }
        // }

        internal void Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand();
            var conexao = new SqlConnection(stringDeConexao);
            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            conexao.Open();
            try
            {
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexao.Close();
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand();
            var conexao = new SqlConnection(stringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            var adapter = new SqlDataAdapter(comando);
            var ds = new DataSet();
            conexao.Open();

            try
            {               
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }

    }
}
