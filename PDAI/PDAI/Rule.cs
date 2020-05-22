using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDAI
{
    static class Rule
    {
        public static string GetAdminUsername { get { return "Admin"; } }
        public static string GetAdminPassword { get { return "a"; } }
        public static string TargetPath { get { return @"C:\EyesOpen\"; } }

        public static List<string> GetRoles()
        {
            List<string> roles = new List<string>();
            roles.Add("Administrador.0");
            roles.Add("Prisioneiro.0");
            roles.Add("Diretor.1");
            roles.Add("Secretária.1");
            roles.Add("Guarda.1");

            return roles;
        }


        public static List<string> GetMaritalStatus()
        {
            List<string> maritalStatus = new List<string>();
            maritalStatus.Add("Solteiro(a)");
            maritalStatus.Add("Casado(a)");
            maritalStatus.Add("Divorciado(a)");
            maritalStatus.Add("Viúvo(a)");
            maritalStatus.Add("Separado(a)");

            return maritalStatus;
        }


        public static List<string> GetPrivilegesRoles()
        {
            List<string> privilegesRoles = new List<string>();
            privilegesRoles.Add("Diretor");
            privilegesRoles.Add("Gestor R.H.");
            privilegesRoles.Add("Secretária");
            privilegesRoles.Add("Guarda-Chefe");
            privilegesRoles.Add("Guarda");

            return privilegesRoles;
        }


        public static List<string> GetPrivileges_Diretor()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Estatística-Consultar");
            privileges.Add("Privilégio Câmara-Registar");
            privileges.Add("Privilégio Câmara-Editar");
            privileges.Add("Privilégio Câmara-Consultar");
            privileges.Add("Privilégio Câmara-Consultar Gravação");
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Funcionário-Consultar");
            privileges.Add("Privilégio Guarda");
            privileges.Add("Privilégio Guarda-Consultar");
            privileges.Add("Privilégio Ocorrência-Consultar");
            privileges.Add("Privilégio Conta-Consultar");
            privileges.Add("Privilégio Alerta-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");

            return privileges;
        }

        public static List<string> GetPrivileges_GestorRH()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Funcionário-Registar");
            privileges.Add("Privilégio Funcionário-Editar");
            privileges.Add("Privilégio Funcionário-Apagar");
            privileges.Add("Privilégio Funcionário-Consultar");
            privileges.Add("Privilégio Recluso-Registar");
            privileges.Add("Privilégio Recluso-Editar");
            privileges.Add("Privilégio Recluso-Apagar");
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");

            return privileges;
        }


        public static List<string> GetPrivileges_Secretaria()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Visita-Registar");
            privileges.Add("Privilégio Visita-Editar");
            privileges.Add("Privilégio Visita-Apagar");
            privileges.Add("Privilégio Visita-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");
            return privileges;
        }



        public static List<string> GetPrivileges_GuardaChefe()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Estatística-Consultar");
            privileges.Add("Privilégio Câmara-Registar");
            privileges.Add("Privilégio Câmara-Editar");
            privileges.Add("Privilégio Câmara-Consultar");
            privileges.Add("Privilégio Câmara-Consultar Gravação");
            privileges.Add("Privilégio Câmara-Apagar Gravação");
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Ocorrência-Registar");
            privileges.Add("Privilégio Ocorrência-Editar");
            privileges.Add("Privilégio Ocorrência-Apagar");
            privileges.Add("Privilégio Ocorrência-Consultar");
            privileges.Add("Privilégio Alerta-Consultar");
            privileges.Add("Privilégio Guarda-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");
            return privileges;
        }



        public static List<string> GetPrivileges_Guarda()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Câmara-Consultar");
            privileges.Add("Privilégio Câmara-Consultar Gravação");
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Ocorrência-Registar");
            privileges.Add("Privilégio Ocorrência-Editar");
            privileges.Add("Privilégio Ocorrência-Apagar");
            privileges.Add("Privilégio Ocorrência-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");
            return privileges;
        }


        public static List<string> GetPrivileges()
        {
            List<string> privileges = new List<string>();
            privileges.Add("Privilégio Estatística");
            privileges.Add("Privilégio Estatística-Consultar");
            privileges.Add("Privilégio Câmara");
            privileges.Add("Privilégio Câmara-Registar");
            privileges.Add("Privilégio Câmara-Editar");
            privileges.Add("Privilégio Câmara-Apagar");
            privileges.Add("Privilégio Câmara-Consultar");
            privileges.Add("Privilégio Câmara-Consultar Deteção");
            privileges.Add("Privilégio Câmara-Consultar Gravação");
            privileges.Add("Privilégio Câmara-Apagar Gravação");
            privileges.Add("Privilégio Recluso");
            privileges.Add("Privilégio Recluso-Registar");
            privileges.Add("Privilégio Recluso-Editar");
            privileges.Add("Privilégio Recluso-Apagar");
            privileges.Add("Privilégio Recluso-Consultar");
            privileges.Add("Privilégio Funcionário");
            privileges.Add("Privilégio Funcionário-Registar");
            privileges.Add("Privilégio Funcionário-Editar");
            privileges.Add("Privilégio Funcionário-Apagar");
            privileges.Add("Privilégio Funcionário-Consultar");
            privileges.Add("Privilégio Guarda");
            privileges.Add("Privilégio Guarda-Consultar");
            privileges.Add("Privilégio Visita");
            privileges.Add("Privilégio Visita-Registar");
            privileges.Add("Privilégio Visita-Editar");
            privileges.Add("Privilégio Visita-Apagar");
            privileges.Add("Privilégio Visita-Consultar");
            privileges.Add("Privilégio Ocorrência");
            privileges.Add("Privilégio Ocorrência-Registar");
            privileges.Add("Privilégio Ocorrência-Editar");
            privileges.Add("Privilégio Ocorrência-Apagar");
            privileges.Add("Privilégio Ocorrência-Consultar");
            privileges.Add("Privilégio Conta");
            privileges.Add("Privilégio Conta-Registar");
            privileges.Add("Privilégio Conta-Editar");
            privileges.Add("Privilégio Conta-Apagar");
            privileges.Add("Privilégio Conta-Consultar");
            privileges.Add("Privilégio Conta-Alterar Credenciais");
            privileges.Add("Privilégio Alerta");
            privileges.Add("Privilégio Alerta-Consultar");
            

            return privileges;
        }






    }
}
