
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using ClienteAPI.Models;

namespace ClienteAPI.Data
{
    public class ClienteData
    {

        private readonly Conexion conexiones;


        public ClienteData(IOptions<Conexion> options)
        {
            conexiones = options.Value;
        }


        public async Task<List<Cliente>> Listar()
        {

            List<Cliente> lista= new List<Cliente>();


            try
            {


                using (var conexion = new SqlConnection(conexiones.CadenaSqlServer))
                {

                    await conexion.OpenAsync();
                    SqlCommand cmd = new SqlCommand("sp_listarClientes", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                FechaNacimiento = (DateTime)reader["FechaNac"],
                                Edad = Convert.ToInt32(reader["Edad"])
                            });
                        }
                    }

                  

                }
            }
            catch (Exception ex)
            {

            }

            return lista;

        }


        public async Task<bool> CrearCliente(Cliente cliente)
        {

            bool rpta = true;

            using (var conexion = new SqlConnection(conexiones.CadenaSqlServer))
            {
                SqlCommand cmd = new SqlCommand("sp_crearCliente", conexion);
                cmd.Parameters.AddWithValue("@nombres",cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidos",cliente.Apellidos);
                cmd.Parameters.AddWithValue("@fechanac",cliente.FechaNacimiento);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conexion.OpenAsync();
                    rpta = await cmd.ExecuteNonQueryAsync()>0?true:false;
                }
                catch
                {
                    rpta=false;
                }
             
            }
            return rpta;         
        }



        public async Task<List<Cliente>> BuscarCliente(int idcliente)
        {

            List<Cliente> lista = new List<Cliente>();

            using (var conexion = new SqlConnection(conexiones.CadenaSqlServer))
            {

                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_buscarCliente", conexion);
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cliente
                        {
                            IdCliente = Convert.ToInt32(reader["IdCliente"]),
                            Nombres = reader["Nombres"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNac"]),
                            Edad = Convert.ToInt32(reader["Edad"])
                        });
                    }
                }



            }

            return lista;
        }


    }
}
