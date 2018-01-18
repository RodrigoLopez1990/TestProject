using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace WebApiProject.Controllers
{
    public class UsuariosController : ApiController
    {
        DAL.UserDal _userDal = new DAL.UserDal();
        public IEnumerable Get()
        {
            try
            {
                var users = this._userDal.GetUsers();
                return this.ConvertToOutputUsers(users);
            }
            catch (Exception)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                throw new HttpResponseException(resp);
            }            
        }

        private IEnumerable ConvertToOutputUsers(IQueryable<Usuario> users)
        {
            var list = new List<Models.OutputUser>();
            foreach (var item in users)
            {
                var outputUser = new Models.OutputUser();
                outputUser.Id = item.Id;
                outputUser.Name = item.Nombre;
                outputUser.LastName = item.Apellido;
                outputUser.EMail = item.Email;
                list.Add(outputUser);
            }
            return list;
        }

        // PUT: api/Usuarios/5
        [Route("api/usuarios/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Models.InputUser updatingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!this._userDal.UserExists(id))
            {
                return NotFound();
            }

            try
            {
                var encription = new BLL.Encription();
                updatingUser.Password = encription.Encrypt(updatingUser.Password);
                this._userDal.Update(id, updatingUser.Name, updatingUser.LastName, updatingUser.EMail, updatingUser.Password);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al realizar la petición: " + ex.Message));
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(Models.InputUser))]
        public IHttpActionResult PostUsuario(Models.InputUser userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new DAL.Usuario();
                user.Nombre = userData.Name;
                user.Apellido = userData.LastName;
                user.Email = userData.EMail;

                var encription = new BLL.Encription();
                user.Password = encription.Encrypt(userData.Password);

                this._userDal.Add(user);

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, userData);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al realizar la petición: " + ex.Message));
            }
        }

        // DELETE: api/Usuarios/5
        [Route("api/usuarios/{id}")]
        [ResponseType(typeof(Models.InputUser))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            if (!this._userDal.UserExists(id))
            {
                return NotFound();
            }

            try
            {
                this._userDal.Delete(id);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error al realizar la petición: " + ex.Message));
            }

            return Ok();
        }
    }
}
