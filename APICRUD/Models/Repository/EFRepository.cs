using APICRUD.Models.DAL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APICRUD.Models
{
    public class EFRepository : IRepository<User>
    {
        private readonly UserContext employeeContext;

        public EFRepository(UserContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }

        public async Task<User> GetAsync(int userId)
        {
            return await employeeContext.Employees.FindAsync(userId);            
        }

        public async Task AddAsync(User user)
        {
            employeeContext.Employees.Add(user);
            await employeeContext.SaveChangesAsync();
        }
     
        public async Task UpdateAsync(User userId)
        {
            var employeeEntry = await employeeContext.Employees.FindAsync(userId.Id);
            employeeEntry = userId;
            await employeeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            employeeContext.Employees.Remove(employeeContext.Employees.Single(a => a.Id == id));
            await employeeContext.SaveChangesAsync(); 
        }
    }
}