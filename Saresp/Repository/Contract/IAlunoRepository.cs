using Saresp.Models;

namespace Saresp.Repository.Contract
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> ObterTodosAlunos();

        void Cadastrar(Aluno aluno);

        void Atualizar(Aluno aluno);

        Aluno obterAluno(int id);

        void Excluir(int id);
    }
}
