using My_Book.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace My_Book.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;
        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLosFromDB() => _context.Logs.ToList();
    }
}
