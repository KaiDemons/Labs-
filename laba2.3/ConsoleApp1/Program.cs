namespace DailyPlanner;

class Program
{
    static void Main(string[] args)
    { 
            while(true)
            {
                Console.WriteLine("1. Add task: ");
                Console.WriteLine("2. Edit task: ");
                Console.WriteLine("3. Delete task: ");
                Console.WriteLine("4. View tasks for today: ");
                Console.WriteLine("5. View tasks for tomorrow: ");
                Console.WriteLine("6. View tasks for this week: ");
                Console.WriteLine("7. View all tasks: ");
                Console.WriteLine("8. View completed tasks: ");
                Console.WriteLine("0. Exit: ");

                Console.Write("\nEnter your choice: ");
                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter task title: ");
                        var title = Console.ReadLine();

                        Console.WriteLine("Enter task description: ");
                        var description = Console.ReadLine();

                        Console.WriteLine("Enter due date (yyyy-mm-dd): ");
                        var dueDate = DateTime.Parse(Console.ReadLine());
                        
                        DatabaseRequests.AddTask( title, description, dueDate);
                        Console.WriteLine("Task added!");
                        break;
                    case "2":
                        Console.Write("Enter task id to edit: ");
                        int taskId = int.Parse(Console.ReadLine());

                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();

                        Console.Write("Enter new description: ");
                        string newDescription = Console.ReadLine();

                        Console.Write("Enter new due date (yyyy-MM-dd HH:mm:ss): ");
                        DateTime newDueDate = DateTime.Parse(Console.ReadLine());

                        DatabaseRequests.EditTask(taskId, newTitle, newDescription, newDueDate);
                        break;
                    case "3":
                        Console.Write("Enter task id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int taskIdToDelete))
                        {
                            DatabaseRequests.DeleteTask(taskIdToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Invalid task id. Please enter a valid integer.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Tasks for today: ");
                        DatabaseRequests.ViewTasksForToday();
                        break;
                    case "5":
                        Console.WriteLine("Tasks for tomorrow: ");
                        DatabaseRequests.ViewTasksForTomorrow();
                        break;
                    case "6":
                        Console.WriteLine("Tasks for this week: ");
                        DatabaseRequests.ViewTasksForThisWeek();
                        break;
                    case "7":
                        Console.WriteLine("All tasks: ");
                        DatabaseRequests.ViewAllTasks();
                        break;
                    case "8":
                        Console.WriteLine("Completed tasks: ");
                        DatabaseRequests.ViewCompletedTasks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nWrong choice. Try again.");
                        break;
                }
            }
        }
    }
