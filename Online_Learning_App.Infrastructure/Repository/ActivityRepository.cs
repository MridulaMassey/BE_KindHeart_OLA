using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_App.Infrastructure.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _dbContext; // Replace with your DbContext

        public ActivityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Activities.FindAsync(id);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _dbContext.Activities.ToListAsync();
        }

        public async Task AddAsync(Activity activity)
        {
            await _dbContext.Activities.AddAsync(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Activity activity)
        {
            _dbContext.Activities.Update(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _dbContext.Activities.FindAsync(id);
            if (activity != null)
            {
                _dbContext.Activities.Remove(activity);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Activity>> GetBySubjectAndClassAsync(Guid subjectId, Guid classId)
        {
            return await _dbContext.Activities
                .Where(a => a.SubjectId == subjectId && a.ClassGroupId == classId)
                .ToListAsync();
        }
    }
}
