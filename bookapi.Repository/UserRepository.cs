using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;

namespace bookapi.Repository
{
    public class UserRepository
    {
        BookStoreContext _context = new BookStoreContext();

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User Login(LoginModel model)
        {
            return  _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));

        }

        public User Register(RegisterModel registerModel)
        {
            User user = new User()
            {
                Firstname = registerModel.Firstname,
                Lastname = registerModel.Lastname,
                Email = registerModel.Email,
                Password = registerModel.Password,
                Roleid = registerModel.Roleid,
            };
            var entry= _context.Users.Add( user);
            _context.SaveChanges();
            return entry.Entity;
        }



        public List<User> GetUsersPaging(int pageIndex, int pageSize, string keyword)
        {
            var users = _context.Users.AsQueryable();

            if (pageIndex > 0)
            {
                if (string.IsNullOrEmpty(keyword) == false)
                {
                    users = users.Where(w => w.Firstname.ToLower().Contains(keyword) || w.Lastname.ToLower().Contains(keyword));
                }

                var userList = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return userList;
            }

            return null;
        }




        public User GetUser(int id)
        {
            if (id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }

            return null;
        }

        public bool UpdateUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Update(model);
                _context.SaveChanges();

                return true;
            }

            return false;
        }



        public bool DeleteUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Remove(model);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

    }


}
