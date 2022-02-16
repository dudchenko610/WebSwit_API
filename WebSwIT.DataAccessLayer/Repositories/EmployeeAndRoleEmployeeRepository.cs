using WebSwIT.DataAccessLayer.AppContext;
using WebSwIT.DataAccessLayer.Interfaces.Repository;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.Repositories
{
    public class EmployeeAndRoleEmployeeRepository : BaseRepository<EmployeeInRoleEmployee>, IEmployeeAndRoleEmployeeRepository
    {
        public EmployeeAndRoleEmployeeRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
