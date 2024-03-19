using sistemadecompras.Models;
using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Infraestructura;
using System;
using System.Data.Entity;
using System.Linq;

namespace sistemadecompras.Services.Account
{
    public class UsuariosServicios : IUsuariosServicios
    {
        private readonly SistemasComprasDBEntities entities;
        public UsuariosServicios()
        {
            entities = new SistemasComprasDBEntities();
        }

        public bool ValidarLogin(LoginDomainModel loginDomain)
        {
            try
            {

                return entities.Usuarios.Any(P => P.UserName == loginDomain.UserName.Trim() && P.Password == loginDomain.Password);
            }
            catch (Exception )
            {
                return false;
            }
        }

        public  LoginDomainModel Login(LoginDomainModel loginDomain)
        {
            LoginDomainModel LoginDM = new LoginDomainModel();
            try
            {
                    Usuarios usuario= entities.Usuarios.Include("Roles").Where(p=>p.UserName.Equals(loginDomain.UserName)&& p.Password.Equals(loginDomain.Password)).FirstOrDefault();
                if(usuario != null)
                {
                    LoginDM.Id= usuario.id;
                    LoginDM.UserName=usuario.UserName.Trim();
                    RolesDomainModel rolesDomain = new RolesDomainModel();
                    rolesDomain.StrValor= usuario.Roles.strValor.Trim();
                    LoginDM.Roles = rolesDomain;


                }
                return LoginDM;
            }
            catch(Exception )
            {

                return LoginDM;
            }

            }
        public bool Insert(LoginDomainModel loginDomain)
        {
            var transaccion = entities.Database.BeginTransaction();
            try
            {
                var usuarios = new Usuarios();
                usuarios.UserName = loginDomain.UserName.Trim();
                usuarios.Password = loginDomain .Password.Trim();
                entities.SaveChanges();
                entities.Usuarios.Add(usuarios);
                transaccion.Commit();
                return true;
               
            }
            catch (Exception ) 
            {
                transaccion.Rollback();
                return false;

            }
        }
          
    }
}
