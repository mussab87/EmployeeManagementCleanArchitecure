using Employee.Application.Commands;
using Employee.Application.Mappers;
using Employee.Application.Responses;
using Employee.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.Application.Handlers.CommandHandlers
{

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepo;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }
        public async Task<EmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            //var employeeEntitiy = EmployeeMapper.Mapper.Map<Employee.Core.Entities.Employee>(request);
            if(request is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var emp = await  _employeeRepo.GetAllAsync();
            var selectedEmp = emp.Where(e => e.FirstName == request.FirstName).FirstOrDefault();
            var DeleteEmployee = _employeeRepo.DeleteAsync(selectedEmp);
            var employeeResponse = EmployeeMapper.Mapper.Map<EmployeeResponse>(request);
            return employeeResponse;
        }
    }
}
