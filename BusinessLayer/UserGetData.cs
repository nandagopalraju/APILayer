using Microsoft.EntityFrameworkCore.Query.Internal;
using ModelLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using DataLayer.Services;

namespace BusinessLayer
{
    public class UserGetData :IEmployeeServices
    {
        private readonly DataContext _employeeContext;
        public UserGetData(DataContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public Employee Get(string username)
        {
            return _employeeContext.Employees.FirstOrDefault(i => i.username == username);
        }

        public List<Employee> Get()
        {
            return _employeeContext.Employees.ToList();
        }
        public void delete(string username)
        {
            Employee emp = _employeeContext.Employees.FirstOrDefault(i => i.username == username);
            if (emp != null)
            {
                _employeeContext.Remove(emp);
                _employeeContext.SaveChanges();
            }
        }
        public void create(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
        }
    }
}







//using BusinessLayer.Interfaces;
//using DataLayer.Services;
//using ModelLayer;

//namespace BusinessLayer
//{
//    public class UserGetData : IUserServices
//    {
//        private readonly DataContext _context;
//        public UserGetData(DataContext context)
//        {
//            _context = context;
//        }

//        public List<User> Get()
//        {
//            return null;

//        }

//    }
//}