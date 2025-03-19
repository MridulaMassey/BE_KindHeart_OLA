using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.Repository
{
    public class SubjectRepository: ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext; // Replace with your DbContext
        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Subject subject)
        {
            await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
        }
    }
}
