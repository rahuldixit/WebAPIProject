using APICRUD.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace APICRUD.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IRepository<User> _employeeRepository;

        public EmployeesController(IRepository<User> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("api/get/{id:int}")]
        public async Task<User> Get(int id)
        {
            return await _employeeRepository.GetAsync(id);
        }
        
        [Route("api/add")]
        [HttpPost]
        public async Task AddEmployee(User employee)
        {
            await _employeeRepository.AddAsync(employee);
        }

        [Route("api/delete/{id:int}")]
        [HttpDelete]
        public async Task DeleteEmployeeByID(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        [Route("api/update")]
        [HttpPost]
        public async Task updateEmployee(User employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }


    }
   



}
