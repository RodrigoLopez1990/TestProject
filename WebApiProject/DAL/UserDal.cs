using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDal
    {
        TestProyectEntities _context;
        public UserDal()
        {
            this._context = new TestProyectEntities();
        }
        public IQueryable<Usuario> GetUsers()
        {
            return this._context.Usuarios;
        }

        public Usuario Get(int id)
        {
            return (from users in this._context.Usuarios where users.Id == id select users).FirstOrDefault();
        }

        public void Update(int id, string name, string lastName, string eMail, string password)
        {
            var newUser = this.Get(id);
            newUser.Nombre = lastName;
            newUser.Apellido = name;
            newUser.Email = eMail;
            newUser.Password = password;

            this._context.SaveChanges();
        }

        public bool UserExists(int userId)
        {
            return (from users in this._context.Usuarios where users.Id == userId select users).Any();
        }

        public void Add(Usuario user)
        {
            this._context.Usuarios.Add(user);
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = this.Get(id);
            this._context.Usuarios.Remove(user);
            this._context.SaveChanges();
        }
    }
}
