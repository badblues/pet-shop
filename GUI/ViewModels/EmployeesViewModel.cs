using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GUI.Core;
using Persistence.Models;
using Persistence.Repositories;

namespace GUI.ViewModels;

public class EmployeesViewModel : ViewModel
{
    private readonly EmployeeRepository _employeeRepository;
    private ICollection<Employee> _employees = new List<Employee>();
    private ICollection<Employee> _filteredEmployees = new List<Employee>();
    private Employee? _selectedEmployee;
    private string _searchText;

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
            FilterEmployees();
        }
    }

    public ICollection<Employee> FilteredEmployees
    {
        get => _filteredEmployees;
        set
        {
            _filteredEmployees = value;
            OnPropertyChanged(nameof(FilteredEmployees));
        }
    }

    public string EnteredName { get; set; }
    public string EnteredAddress { get; set; }
    public string EnteredPosition { get; set; }
    public string EnteredSalary { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterEmployees();
            }
        }
    }

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
    public void DeleteEmployee(object? parameter)
    {
        if (parameter is Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee.Id);
                Employees = _employeeRepository.GetAll();
                SelectedEmployee = null;
            }
            catch(Exception e)
            {
                string message = "Could not delete employee. There are applications with that employee.";
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void FilterEmployees()
    {
        ICollection<Employee> filteredEmployees = new List<Employee>();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (Employee employee in Employees)
            {
                filteredEmployees.Add(employee);
            }
        }
        else
        {
            string searchTextLower = SearchText.ToLower();
            foreach (Employee employee in Employees)
            {
                if (employee.Name.ToLower().Contains(searchTextLower)
                    || employee.Position.ToLower().Contains(searchTextLower))
                {
                    filteredEmployees.Add(employee);
                }
            }
        }

        FilteredEmployees = filteredEmployees;
    }
}
