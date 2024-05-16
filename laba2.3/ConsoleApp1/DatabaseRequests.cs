using Npgsql;

namespace DailyPlanner;

public static class DatabaseRequests
{
    public static void AddTask(string title, string description, DateTime dueDate)
    {
        var querySql = $"INSERT INTO tasks (title, description, dueDate) VALUES ('{title}', '{description}', '{dueDate.ToString("yyyy-MM-dd")}') RETURNING id";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        var id = (int)cmd.ExecuteScalar();
        Console.WriteLine($"Task added with id: {id}");
    }

    public static void EditTask(int id, string title, string description, DateTime dueDate)
    {
        // Выполняем запрос для выборки задачи по id
        var selectSql = $"SELECT * FROM tasks WHERE id={id};";
        using var selectCmd = new NpgsqlCommand(selectSql, DatabaseService.GetSqlConnection());

        using var reader = selectCmd.ExecuteReader();

        // Если задача с таким id найдена, обновляем её поля
        if (reader.Read())
        {
            reader.Close(); // Закрываем ридер перед выполнением запроса на обновление

            // Выполняем запрос для обновления полей задачи
            var updateSql = $"UPDATE tasks SET title='{title}', description='{description}', due_date='{dueDate}' WHERE id={id};";
            using var updateCmd = new NpgsqlCommand(updateSql, DatabaseService.GetSqlConnection());

            updateCmd.ExecuteNonQuery();
            Console.WriteLine("The task has been successfully updated.");
        }
        else
        {
            Console.WriteLine("The task with the specified id was not found.");
        }
    }


    public static void DeleteTask(int id)
    {
        var querySql = $"DELETE FROM tasks WHERE id='{id}';";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    public static void ViewTasksForToday()
    {
        var querySql = "SELECT * FROM tasks WHERE dueDate = CURRENT_DATE";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} Название: {reader[1]} Описание: {reader[2]} Дата {reader[3]}");
        }
    }

    public static void ViewTasksForTomorrow()
    {
        var querySql = "SELECT * FROM tasks WHERE dueDate = CURRENT_DATE + INTERVAL '1 day'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} Название: {reader[1]} Описание: {reader[2]} Дата {reader[3]}");
        }
    }

    public static void ViewTasksForThisWeek()
    {
        var querySql = "SELECT * FROM tasks WHERE dueDate BETWEEN CURRENT_DATE AND CURRENT_DATE + INTERVAL '7 days'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} Название: {reader[1]} Описание: {reader[2]} Дата {reader[3]}");
        }
    }

    public static void ViewAllTasks()
    {
        var querySql = "SELECT * FROM tasks";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} Название: {reader[1]} Описание: {reader[2]} Дата {reader[3]}");
        }
    }

    public static void ViewCompletedTasks()
    {
        var querySql = "SELECT * FROM tasks WHERE dueDate < CURRENT_DATE";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader[0]} Название: {reader[1]} Описание: {reader[2]} Дата {reader[3]}");
        }
    }
}
    