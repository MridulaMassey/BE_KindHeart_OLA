using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Online_Learning_APP.Application.Services
{
    public class ClassGroupService : IClassGroupService
    {
        private readonly ApplicationDbContext _context;

        public ClassGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClassGroup> CreateClassGroupAsync(ClassGroupCreateDto classGroupDto)
        {
            // Validate that the admin exists
            // First, find the admin using AdminId
            var admin = await _context.Admin.FindAsync(classGroupDto.AdminId);
            if (admin == null)
                throw new Exception("Admin not found");

            // Then, create the ClassGroup instance and initialize it
            var classGroup = new ClassGroup
            {
                ClassGroupId = Guid.NewGuid(),
                ClassName = classGroupDto.ClassName,
                AdminId = classGroupDto.AdminId,
                // Do not initialize Activities and ClassGroupSubjects yet
            };

            // Populate Activities and ClassGroupSubjects
            //classGroup.Activities = await _context.Activities
            //                                      .Where(a => classGroupDto.ActivityIds.Contains(a.Id))
            //                                      .ToListAsync();

            //// Populate the many-to-many relationship with Subjects
            //classGroup.ClassGroupSubjects = classGroupDto?.SubjectIds
            //    .Select(subjectId => new ClassGroupSubject
            //    {
            //        ClassGroupId = classGroup.ClassGroupId,
            //        SubjectId = subjectId
            //    }).ToList();
            //  classGroup.ClassGroupSubjects = null;

            // Save the classGroup object in the context (database)
            await _context.ClassGroups.AddAsync(classGroup);
            await _context.SaveChangesAsync();

            return classGroup;
        }
    }
}
