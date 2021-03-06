﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    class Program
    {
        static Hashtable dataAdapters = new Hashtable();
        static SqlDataAdapter dataAdapter;

        static void Main(string[] args)
        {
            DataSet esquema = new DataSet();

            rellenar(esquema);

            Contrato contrato = new Contrato(esquema);
            Producto producto = new Producto(esquema);

            imprimirTabla(esquema, "Contrato");
            contrato.insertar(4, 2, 350m, DateTime.Today);
            imprimirTabla(esquema, "Contrato");

            imprimirTabla(esquema, "ReconocimientoIngreso");
            contrato.calcularReconocimiento(4);            
            imprimirTabla(esquema, "ReconocimientoIngreso");

            actualizarBD(esquema);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void actualizarBD(DataSet esquema)
        {
            actualizarTabla(esquema, "Contrato");
            actualizarTabla(esquema, "Producto");
            actualizarTabla(esquema, "ReconocimientoIngreso");
        }

        private static void actualizarTabla(DataSet esquema, string tabla)
        {
            SqlCommandBuilder cb;
            SqlConnection conexion = conectarBD();

            dataAdapter = (SqlDataAdapter)dataAdapters[tabla];
            cb = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(esquema, tabla);
        }

        private static void imprimirTabla(DataSet esquema, string tabla)
        {
            foreach (DataRow fila in esquema.Tables[tabla].Rows)
            {
                foreach (var campo in fila.ItemArray)
                {
                    Console.Write("{0}\t", campo);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private static void rellenar(DataSet esquema)
        {
            rellenarTabla(esquema, "Contrato");
            rellenarTabla(esquema, "Producto");
            rellenarTabla(esquema, "ReconocimientoIngreso");
        }

        private static void rellenarTabla(DataSet esquema, string tabla)
        {
            SqlConnection conexion = conectarBD();

            conexion.Open();
            dataAdapter = new SqlDataAdapter(String.Format("SELECT * FROM {0}", tabla), conexion);
            dataAdapter.FillSchema(esquema, SchemaType.Source, tabla);
            dataAdapter.Fill(esquema, tabla);
            dataAdapters.Add(tabla, dataAdapter);
            conexion.Close();
        }

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
