using demo.Data;
using demo.Models;
using demo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.DataManager
{
    public class EmployeeManager : IDataRepository<Employee>
    {

        readonly EmployeeContext _employeeContext;
        public EmployeeManager(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public Employee Get(long id)
        {
            return _employeeContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeContext.Employees.ToList();
        }
        public void Add(Employee entity)
        {
            _employeeContext.Employees.Add(entity);
            _employeeContext.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            _employeeContext.Employees.Remove(entity);
            _employeeContext.SaveChanges();
        }

        public void Update(Employee dbEntity, Employee entity)
        {
            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;
            dbEntity.Email = entity.Email;
            dbEntity.DateOfBirth = entity.DateOfBirth;
            dbEntity.PhoneNumber = entity.PhoneNumber;

            _employeeContext.SaveChanges();
        }
    }
}
