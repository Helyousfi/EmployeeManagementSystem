using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_COURSE.Commands;
using WPF_COURSE.Models;

namespace WPF_COURSE.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        EmployeeService ObjEmployeesService;
        public EmployeeViewModel()
        {
            ObjEmployeesService = new EmployeeService();
            LoadData();
            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save); 
            updateCommand = new RelayCommand(Update);
            searchCommand = new RelayCommand(Search);
            deleteCommand = new RelayCommand(Delete);
        }


        #region DisplayEmployee_Operation
        private List<Employee>? employeesList;
        public List<Employee>? EmployeesList 
        {
            get { return employeesList; }
            set { 
                employeesList = value; 
                OnPropertyChanged("EmployeesList"); 
            }
        }

        private void LoadData()
        {
            EmployeesList = ObjEmployeesService.GetAll();
        }
        #endregion


        // A full property for current employee (This will be gotten from the UI)
        private Employee currentEmployee;
        
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { 
                currentEmployee = value;
                OnPropertyChanged("CurrentEmployee");
            }
        }

        #region AddEmployee_Operation
        // This is the message that will be displayed when something is added / updated
        private string message;
        public string Message 
        { 
            get { return message; } 
            set { 
                message = value; 
                OnPropertyChanged("Message"); 
            } 
        }


        // Create a command for add 
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjEmployeesService.AddEmployee(CurrentEmployee);
                LoadData();
                if(IsSaved)
                {
                    Message = "Employee Saved";
                }
                else
                {
                    Message = "Saved operation failed!";
                }
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion


        #region UpdateEmployee_Operation
        // // Create a command for update 
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjEmployeesService.UpdateEmployee(currentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed!";
                }
            }
            catch(Exception ex )
            {
                Message = ex.Message;
            }
        }
        #endregion


        #region SearchEmployee_Operation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }

        private void Search()
        {
            Message = "";
            try
            {
                var objEmployee = 
                    ObjEmployeesService.SearchEmployee(CurrentEmployee.Id);

                if(objEmployee != null)
                {
                    CurrentEmployee = new Employee
                    {
                        Id = objEmployee.Id,
                        Name = objEmployee.Name,
                        Age = objEmployee.Age,
                        Email = objEmployee.Email,
                        Phone = objEmployee.Phone,
                        Job = objEmployee.Job
                    };
                }
                else
                {
                    Message = "Employee Not Found!";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion


        #region DeleteEmployee_operation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        private void Delete()
        {
            try
            {
                var IsDeleted = ObjEmployeesService.DeleteEmployee(CurrentEmployee.Id);
                if (IsDeleted)
                {
                    Message = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Message = "Employee Not Found!";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion



    }
}
