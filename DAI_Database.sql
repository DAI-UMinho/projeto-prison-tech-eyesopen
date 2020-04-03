if DB_ID('DAI_Database') is null begin create database DAI_Database end 
go 
use [DAI_Database]
create table Tipo (tipo nvarchar(20), primary key (tipo))
create table EstadoCivil (estadoCivil nvarchar(20), primary key (estadoCivil))
create table Pessoa (id int identity(1,1), nomeCompleto nvarchar(50), dataNascimento datetime, cc nvarchar(15), estadoCivil nvarchar(20) ,tipo nvarchar(20), ativo bit default 1, pastaRegistos nvarchar(max), dataRegisto datetime default getdate(), primary key (id),  foreign key (tipo) references Tipo (tipo),  foreign key (estadoCivil) references EstadoCivil (estadoCivil))
create table Camara (id int identity(1,1), indexCamara int, pastaRegistos nvarchar(max), local nvarchar(50), primary key (id))
create table PessoaDetetada (id int identity(1,1), idPessoa int, idCamara int, dataRegisto datetime default getdate(), coordenadaX int, coordenadaY int, primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idCamara) references Camara (id))
create table Visita (id int identity(1,1), idPessoa int, nome nvarchar(50),dataRegisto datetime default getdate(), dataVisita datetime, primary key (id), foreign key (idPessoa) references Pessoa (id))
create table Ocorrencia (id int identity(1,1), idPessoa int, dataRegisto datetime default getdate(), dataOcorrencia datetime, motivo nvarchar(30), descricao nvarchar(100), codigoOcorrencia int, primary key (id), foreign key (idPessoa) references Pessoa (id) on delete cascade on update cascade)
create table Reconhecimento (id int identity(1,1), idOcorrencia int, idPessoa int, culpada bit, primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idOcorrencia) references Ocorrencia (id) on delete cascade on update cascade)
create table Alerta (id int identity(1,1), alerta int, primary key (id))
create table AssocAlerta (id int, idPessoa int, idAlerta int, dataRegisto datetime,  primary key (id), foreign key (idPessoa) references Pessoa (id), foreign key (idAlerta) references Alerta (id) on delete cascade on update cascade)
create table Login (id int, username nvarchar(30), password varBinary(256), accessLevel nvarchar(30), primary key (id), foreign key (id) references Pessoa (id))
create table Grupo (id int identity(1,1), idPessoa int, grupo int,  primary key (id), foreign key (idPessoa) references Pessoa (id))
create table Precedente (id int identity(1,1), dado nvarchar(50), primary key (id))
create table FichaRecluso (id int identity(1,1), idPessoa int, idPrecedente int, descricao nvarchar(100), dataRegisto datetime,  primary key (id), foreign key (idPessoa) references Pessoa (id),  foreign key (idPrecedente) references Precedente (id) on delete cascade on update cascade)
create table Papel (id int identity(1,1), privilegio varBinary(256), primary key(id))
create table AssocPapel (id int identity(1,1), idLogin int, idPapel int,  primary key(id), foreign key (idLogin) references Login (id), foreign key (idPapel) references Papel (id))


