using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroServiceSetup.Models;

namespace MicroServiceSetup.Services
{
    public class TodoService : ITodoService
    {
        public void GenMessage(Todo item)
        {
             Console.WriteLine($"In Service. Item: {item.Name}. Completed: {item.IsComplete}");
        }
    }
}