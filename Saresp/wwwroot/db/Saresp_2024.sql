drop database saresp_2024;
create database Saresp_2024;

use Saresp_2024;

create table ProfessorAplicador(
IdProfessor int primary key auto_increment,
Nome varchar(200),
CPF decimal (11,0),
RG varchar(9),
Telefone decimal(11,0),
DataNascimento datetime
);

create table Aluno(
IdAluno int primary key auto_increment,
Nome varchar(200),
Email varchar(250),
Telefone decimal(11,0),
Serie tinyint,
Turma varchar(50),
DataNascimento datetime
);

select * from Aluno;