using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Saresp.Models;
using Saresp.Repository.Contract;
using System.Data;

namespace Saresp.Repository
{
    public class AlunoRepository : IAlunoRepository
    {

        private readonly string _conexaoMySQL;

        public AlunoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Aluno aluno)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Update Aluno set Nome=@Nome, Email=@Email, Telefone=@Telefone, Serie=@Serie, Turma=@Turma " + "DataNascimento=@DataNascimento where IdAluno = @Id", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = aluno.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@Telefone", MySqlDbType.Decimal).Value = aluno.Telefone;
                cmd.Parameters.Add("@Serie", MySqlDbType.VarChar).Value = aluno.Serie;
                cmd.Parameters.Add("@Turma", MySqlDbType.VarChar).Value = aluno.Turma;
                cmd.Parameters.Add("@Turma", MySqlDbType.VarChar).Value = aluno.DataNascimento.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = aluno.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Aluno aluno)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Aluno (Nome, Email, Telefone, Serie, Turma, DataNascimento)" + " values (@Nome, @Email, @Telefone, @Serie, @Turma, @DataNascimento)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = aluno.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@Telefone", MySqlDbType.Decimal).Value = aluno.Telefone;
                cmd.Parameters.Add("@Serie", MySqlDbType.VarChar).Value = aluno.Serie;
                cmd.Parameters.Add("@Turma", MySqlDbType.VarChar).Value = aluno.Turma;
                cmd.Parameters.Add("@DataNascimento", MySqlDbType.VarChar).Value = aluno.DataNascimento.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Delete from Aluno where IdAluno=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Aluno obterAluno(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Aluno " + " Where IdAluno=@Id ", conexao);

                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Aluno aluno = new Aluno();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while(dr.Read())
                {
                    aluno.Id = Convert.ToInt32(dr["IdAluno"]);
                    aluno.Nome = (string)(dr["Nome"]);
                    aluno.DataNascimento = Convert.ToDateTime(dr["DataNascimento"]);
                    aluno.Email = (string)(dr["Email"]);
                    aluno.Turma = (string)(dr["Turma"]);
                    aluno.Telefone = Convert.ToDecimal(dr["Telefone"]);
                    aluno.Serie = Convert.ToInt32(dr["Serie"]);

                }
                return aluno;
            }
        }

        public IEnumerable<Aluno> ObterTodosAlunos()
        {
            List<Aluno> AlunoList = new List<Aluno>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Select * from Aluno", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    AlunoList.Add(
                        new Aluno
                        {
                            Id = Convert.ToInt32(dr["idAluno"]),
                            Nome = (string)dr["Nome"],
                            Email = (string)dr["Email"],
                            Telefone = Convert.ToInt64(dr["Telefone"]),
                            Serie = Convert.ToInt32(dr["Serie"]),
                            Turma = (string)(dr["Turma"]),
                            DataNascimento = Convert.ToDateTime(dr["DataNascimento"])
                        });
                }
                return AlunoList;
            }
        }
    }
}
