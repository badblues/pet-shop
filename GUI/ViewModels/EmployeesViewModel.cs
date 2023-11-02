using System.Collections.Generic;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class EmployeesViewModel : ViewModel
{
    private readonly EmployeeRepository _employeeRepository;
    private ICollection<Employee> _employees = new List<Employee>();
    private Employee? _selectedEmployee;

    public ICommand AddEmployeeCommand { get; set; }
    public ICommand UpdateEmployeeCommand { get; set; }
    public ICommand DeleteEmployeeCommand { get; set; }

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            _selectedEmployee = value;
            OnPropertyChanged(nameof(SelectedEmployee));
        }
    }

    public ICollection<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged(nameof(Employees));
        }
    }

    public string EnteredName { get; set; }
    public string EnteredAddress { get; set; }
    public string EnteredPosition { get; set; }
    public string EnteredSalary { get; set; }

    public EmployeesViewModel(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        AddEmployeeCommand = new RelayCommand(AddEmployee, o => true);
        UpdateEmployeeCommand = new RelayCommand(UpdateEmployee, o => true);
        DeleteEmployeeCommand = new RelayCommand(DeleteEmployee, o => true);

        Employees = _employeeRepository.GetAll();
    }

    public void SelectEmployee(Employee employee)
    {
        SelectedEmployee = employee;
    }

    public void AddEmployee(object? unused)
    {
        if (string.IsNullOrWhiteSpace(EnteredName)
            || string.IsNullOrWhiteSpace(EnteredAddress)
            || string.IsNullOrWhiteSpace(EnteredPosition)
            || string.IsNullOrWhiteSpace(EnteredSalary))
        {
            return;
        }
        Employee newEmployee = new()
        {
            Name = EnteredName,
            Address = EnteredAddress,
            Position = EnteredPosition,
            Salary = double.Parse(EnteredSalary)
        };

        _employeeRepository.Add(newEmployee);
        Employees = _employeeRepository.GetAll();
    }

    public void DeleteEmployee(object? parameter)
    {
        if (parameter is Employee employee)
        {
            _employeeRepository.Delete(employee.Id);
            Employees = _employeeRepository.GetAll();
            SelectedEmployee = null;
        }
    }

    public void UpdateEmployee(object? parameter)
    {
        if (parameter is Employee employee)
        {
            _employeeRepository.Update(employee);
            Employees = _employeeRepository.GetAll();
            SelectedEmployee = null;
            SelectedEmployee = _employeeRepository.Get(employee.Id);
        }
    }
}
