using BusinessLayer.Services;
using DataAccess.Services;
using System;

namespace ConsoleApp
{
    public class Program
    {
        //public static object LoggingService { get; private set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Entity Data Model projec starts here.");
            MainMenu();
            Console.ReadLine();
        }
        private static void MainMenu()
        {
            bool run = true;
            do
            {
                Console.WriteLine("Choose a menu.");
                Console.WriteLine("1)Employee menu.");
                Console.WriteLine("2)Customer menu.");
                Console.WriteLine("3)People menu.");
                Console.WriteLine("4) Enter 3 to exit!");
                var input = Console.ReadLine();
                var option = FormatCheckOfInput(input);
                switch (option)
                {
                    case 1:
                        EmployeeMenu();
                        break;
                    case 2:
                        customerMenu();
                        break;
                    case 3:
                        PeopleMenu();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (run);
        }

        private static void EmployeeMenu()
        {
            Console.WriteLine("Choose an option!");
            Console.WriteLine("1)List all employess.");
            Console.WriteLine("2)Find an employee by id.");
            Console.WriteLine("3)Add a new employee.");
            Console.WriteLine("4)Update a employee by id.");
            Console.WriteLine("5)Delete an employee by id.");
            Console.WriteLine("6)Enter 6 to return to main menu!");
            bool run = true;
            do
            {
                var input = Console.ReadLine();
                var option = FormatCheckOfInput(input);
                var employeeDataReader = new EmployeeDataReader();
                var viewModel = new EmployeeReader(employeeDataReader);
                switch (option)
                {
                    case 1:
                        viewModel.PrintEmployees();
                        break;
                    case 2:
                        Console.WriteLine("Enter an employee id.");
                        var entry = Console.ReadLine();
                        var id = FormatCheckOfInput(entry);
                        Console.WriteLine(viewModel.FindEmployeeById(id));
                        break;
                    case 3:
                        Console.WriteLine("Enter an employee id, name, last name and email with comma between entities.");
                        var employee = Console.ReadLine();
                        viewModel.AddEmployee(employee);
                        viewModel.PrintEmployees();
                        break;
                    case 4:
                        Console.WriteLine("Enter an employee with comma between entities to update.");
                        var updateEmployee = Console.ReadLine();
                        viewModel.UpdateEmployee(updateEmployee);
                        break;
                    case 5:
                        Console.WriteLine("Enter employee id to delete.");
                        var employeeId = FormatCheckOfInput(Console.ReadLine());
                        viewModel.DeleteEmployee(employeeId);
                        break;
                    case 6:
                        run = false;
                        MainMenu();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (run);
        }

        private static void customerMenu()
        {
            Console.WriteLine("Choose an option!");
            Console.WriteLine("1)List all customer.");
            Console.WriteLine("2)Find a customer by id.");
            Console.WriteLine("3)Add a new customer.");
            Console.WriteLine("4)Update a customer by id.");
            Console.WriteLine("5)Delete a customer by id.");
            Console.WriteLine("6)Enter 6 to return to main menu!");
            bool run = true;
            do
            {
                var input = Console.ReadLine();
                var customerDataReader = new CustomerDataReader();
                var viewModel = new CustomerReader(customerDataReader);
                var option = FormatCheckOfInput(input);
                switch (option)
                {
                    case 1:
                        viewModel.PrintCustomers();
                        break;
                    case 2:
                        Console.WriteLine("Enter a customerId to find the current customer.");
                        var entry = Console.ReadLine();
                        var id = FormatCheckOfInput(entry);
                        Console.WriteLine(viewModel.FindCustomerById(id).Log());
                        break;
                    case 3:
                        Console.Write("Add a customer to list with name, last name and email with comma separator.");
                        var customer = Console.ReadLine();
                        viewModel.AddCustomer(customer);
                        break;
                    case 4:
                        Console.WriteLine("Enter an customer with comma between entities to update.");
                        var updateCustomer = Console.ReadLine();
                        viewModel.UpdateCustomer(updateCustomer);
                        break;
                    case 5:
                        Console.WriteLine("Enter a customerId to delete.");
                        var customerID = Console.ReadLine();
                        var customId = FormatCheckOfInput(customerID);
                        viewModel.DeleteCustomer(customId);
                        break;
                    case 6:
                        run = false;
                        MainMenu();
                        break;
                    default:
                        break;
                }
            } while (run);
        }

        private static void PeopleMenu()
        {
            Console.WriteLine("Choose an option!");
            Console.WriteLine("1)List people.");
            Console.WriteLine("2)Find an person by id.");
            Console.WriteLine("3)Add a new person.");
            Console.WriteLine("4)Update a person by id.");
            Console.WriteLine("5)Delete an person by id.");
            Console.WriteLine("6)Enter 6 to return to main menu!");
            bool run = true;
            do
            {
                var input = Console.ReadLine();
                var option = FormatCheckOfInput(input);
                var peopleDataReader = new PeopleDataReader();
                var viewModel = new PeopleReader(peopleDataReader);
                switch (option)
                {
                    case 1:
                        viewModel.PrintPeople();
                        break;
                    case 2:
                        Console.WriteLine("Enter an id.");
                        var entry = Console.ReadLine();
                        var id = FormatCheckOfInput(entry);
                        Console.WriteLine(viewModel.FindPersonById(id).Log());
                        break;
                    case 3:
                        Console.WriteLine("Enter a person with comma between entities.");
                        var dataItem = Console.ReadLine();
                        viewModel.AddPerson(dataItem);
                        break;
                    case 4:
                        Console.WriteLine("Enter a person to update.");
                        var data = Console.ReadLine();
                        viewModel.UpdatePerson(data);
                        break;
                    case 5:
                        Console.WriteLine("Enter an id of a person to delete.");
                        var personId = Console.ReadLine();
                        var personID = FormatCheckOfInput(personId);
                        viewModel.DeletePerson(personID);
                        break;
                    case 6:
                        run = false;
                        MainMenu();
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            } while (run);
        }

        private static int FormatCheckOfInput(string input)
        {
            try
            {
                var result = int.Parse(input);
                return result;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Wrong format " + ex.Message);
                Environment.Exit(0);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message + "The value can`nt be null.");
                throw;
            }
            return -1;
        }
    }
}
