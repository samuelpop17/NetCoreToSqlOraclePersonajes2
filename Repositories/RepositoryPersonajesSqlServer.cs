using NetCoreToSqlOraclePersonajes.Models;
using System.Data.SqlClient;
using System.Data;

namespace NetCoreToSqlOraclePersonajes.Repositories
{
    public class RepositoryPersonajesSqlServer : IRepositoryPersonajes
    {

        private DataTable tablaDPersonajes;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryPersonajesSqlServer()
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn= new SqlConnection(connectionString);
            this.com= new SqlCommand();
            this.com.Connection = cn;
            this.tablaDPersonajes = new DataTable();
            string sql = "select * from PERSONAJES";
            SqlDataAdapter ad= new SqlDataAdapter(sql,this.cn);
            ad.Fill(this.tablaDPersonajes);
        }

        public void DeletePersonaje(int id)
        {
            string sql = "delete from PERSONAJES WHERE IDPERSONAJE=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Personaje FindPersonaje(int id)
        {
            var consulta = from datos in this.tablaDPersonajes.AsEnumerable()
                           where datos.Field<int>("IDPERSONAJE")==id select datos;
            var row = consulta.First();
            Personaje personaje = new Personaje
            {

                Id = row.Field<int>("IDPERSONAJE"),
                Nombre = row.Field<string>("PERSONAJE"),
                Imagen = row.Field<string>("IMAGEN")
            };
            return personaje;
        }

        public List<Personaje> GetPersonajes()
        {
            var consulta= from datos in this.tablaDPersonajes.AsEnumerable()
                          select datos;
            List<Personaje> personajes = new List<Personaje>();
            foreach (var row in consulta)
            {

                Personaje personaje = new Personaje { 
                
                Id=row.Field<int>("IDPERSONAJE"),
                Nombre=row.Field<string>("PERSONAJE"),
                Imagen = row.Field<string>("IMAGEN")
                };
                personajes.Add(personaje);
                
            }
            return personajes;
        }

        public void InsertPersonajes(int id, string nombre, string imagen)
        {
            string sql = "insert into Personajes values(@id,@nombre,@imagen)";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@imagen", imagen);


            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public void UpdatePersonaje(int id, string nombre, string imagen)
        {
            string sql = "update personajes set  PERSONAJE=@nombre, IMAGEN=@imagen where IDPERSONAJE=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@imagen", imagen);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.OpenAsync();
            int af = this.com.ExecuteNonQuery();
            this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
