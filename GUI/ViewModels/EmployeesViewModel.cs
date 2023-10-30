using System.Collections.Generic;
using System.Linq;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

class EmployeesViewModel : ViewModel
{
    private readonly EmployeeRepository _employeeRepository;
    private IEnumerable<Employee> _employees = new List<Employee>();
    
    public IEnumerable<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged(nameof(Employees));
        }
    }

    public EmployeesViewModel(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        Employees = _employeeRepository.GetAll();
    }
}
