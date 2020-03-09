if DB_ID('DAI_Database') is null begin create database DAI_Database end 
go 
use [DAI_Database]
create table Pessoa (id int identity(1,1), nomeCompleto nvarchar(50), dataNascimento datetime, cc nvarchar(15), estadoCivil nvarchar(20) ,tipo nvarchar(11), ativo bit default 1, pastaRegistos nvarchar(max), dataRegisto datetime default getdate(), primary key (id))
create table Camara (id int identity(1,1), indexCamara int, local nvarchar(50), primary key (id))
create table PessoaDetetada (id int identity(1,1), idPessoa int, idCamara int, dataRegisto datetime default getdate(), coordenadaX int, coordenadaY int, primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idCamara) references Camara (id))
create table Visita (id int identity(1,1), idPessoa int, nome nvarchar(50), dataVisita datetime, primary key (id), foreign key (idPessoa) references Pessoa (id))
create table Ocorrencia (id int identity(1,1), idPessoa int, dataRegisto datetime default getdate(), dataOcorrencia datetime, motivo nvarchar(30), descricao nvarchar(100), codigoOcorrencia int, primary key (id), foreign key (idPessoa) references Pessoa (id) on delete cascade on update cascade)
create table Reconhecimento (id int identity(1,1), idOcorrencia int, idPessoa int, primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idOcorrencia) references Ocorrencia (id) on delete cascade on update cascade)
create table Alerta (id int identity(1,1), alerta int, primary key (id))
create table AssocAlerta (id int, idPessoa int, idAlerta int, dataRegisto datetime,  primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idAlerta) references Alerta (id) on delete cascade on update cascade)
create table Login (idPessoa int, username nvarchar(20), password nvarchar(20),  primary key (idPessoa), foreign key (idPessoa) references Pessoa (id))
create table Grupo (id int identity(1,1), idPessoa int, grupo int,  primary key (id), foreign key (idPessoa) references Pessoa (id))
create table Historico (id int identity(1,1), dado nvarchar(50), primary key (id))
create table Informacao (id int, idPessoa int, idHistorico int, descricao nvarchar(100), dataRegisto datetime,  primary key (id), foreign key (idPessoa) references Pessoa (id),  foreign key (idHistorico) references Historico (id) on delete cascade on update cascade)


