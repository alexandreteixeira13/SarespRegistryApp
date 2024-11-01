using MySql.Data.MySqlClient;
using Saresp.Models;
using Saresp.Repository.Contract;
using System.Data;

namespace Saresp.Repository
{
    public class ProfessorAplicadorRepository : IProfessorAplicadorRepository
    {
        private readonly string _conexaoMySQL;

        public ProfessorAplicadorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(ProfessorAplicador professorAplicador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update ProfessorAplicador set Nome=@Nome, CPF=@CPF, RG=@RG, Telefone=@Telefone, " + " DataNascimento=@DataNascimento where IdProfessor = @Id", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = professorAplicador.Nome;
                cmd.Parameters.Add("@CPF", MySqlDbType.Decimal).Value = professorAplicador.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.Decimal).Value = professorAplicador.telefone;
                cmd.Parameters.Add("@RG", MySqlDbType.VarChar).Value = professorAplicador.RG;
                cmd.Parameters.Add("@DataNascimento", MySqlDbType.VarChar).Value = professorAplicador.DataNascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = professorAplicador.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(ProfessorAplicador professorAplicador)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into ProfessorAplicador (Nome, CPF, RG, Telefone, DataNascimento)" + " values (@Nome, @CPF, @RG, @Telefone, @DataNascimento)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = professorAplicador.Nome;
                cmd.Parameters.Add("@CPF", MySqlDbType.Decimal).Value = professorAplicador.CPF;
                cmd.Parameters.Add("@Telefone", MySqlDbType.Decimal).Value = professorAplicador.telefone;
                cmd.Parameters.Add("@RG", MySqlDbType.VarChar).Value = professorAplicador.RG;
                cmd.Parameters.Add("@DataNascimento", MySqlDbType.VarChar).Value = professorAplicador.DataNascimento.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Delete from ProfessorAplicador where IdProfessor=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public ProfessorAplicador ObterProfessor(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from ProfessorAplicador " + " Where IdProfessor=@Id ", conexao);

                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                ProfessorAplicador professorAplicador = new ProfessorAplicador();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    professorAplicador.Id = Convert.ToInt32(dr["IdProfessor"]);
                    professorAplicador.Nome = (string)(dr["Nome"]);
                    professorAplicador.DataNascimento = Convert.ToDateTime(dr["DataNascimento"]);
                    professorAplicador.telefone = Convert.ToDecimal(dr["Telefone"]);
                    professorAplicador.CPF = Convert.ToDecimal(dr["CPF"]);
                    professorAplicador.RG = (string)(dr["RG"]);

                }
                return professorAplicador;
            }
        }

        public IEnumerable<ProfessorAplicador> ObterTodosProfessores()
        {
            List<ProfessorAplicador> ProfessorList = new List<ProfessorAplicador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Select * from ProfessorAplicador", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    ProfessorList.Add(
                        new ProfessorAplicador
                        {
                            Id = Convert.ToInt32(dr["idProfessor"]),
                            Nome = (string)dr["Nome"],
                            CPF = Convert.ToDecimal(dr["CPF"]),
                            telefone = Convert.ToDecimal(dr["Telefone"]),
                            RG = (string)(dr["RG"]),
                            DataNascimento = Convert.ToDateTime(dr["DataNascimento"])
                        });
                }
                return ProfessorList;
            }
        }
    }
}
