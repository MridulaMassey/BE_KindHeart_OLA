using System;
using System.Threading.Tasks;
using AutoMapper; // Assuming you're using AutoMapper
//using Online_Learning_App.Application.DTOs;
//using Online_Learning_App.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
//using Online_Learning_App.Infrastructure.Persistence.Interfaces; // Assuming a repository

namespace Online_Learning_App.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
        {
            // Fetch existing activities for the subject and class
            var existingActivities = await _activityRepository.GetBySubjectAndClassAsync(createActivityDto.SubjectId, createActivityDto.ClassGroupId.Value);

            // Calculate total weightage including the new activity
            double totalWeightage = existingActivities.Sum(a => a.WeightagePercent) + createActivityDto.WeightagePercent;

            if (totalWeightage > 100)
            {
                throw new InvalidOperationException("Total weightage percent cannot exceed 100 percent.");
            }

            var activity = _mapper.Map<Activity>(createActivityDto);
            activity.ActivityId = Guid.NewGuid(); // Generate a new ID
            activity.Id = activity.ActivityId; // If Id is needed.

            await _activityRepository.AddAsync(activity);
            return _mapper.Map<ActivityDto>(activity);
        }




        public async Task<ActivityDto> GetActivityByIdAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            return activity == null ? null : _mapper.Map<ActivityDto>(activity);
        }

        // ✅ Read All Activities
        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            var activities = await _activityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        // ✅ Update Activity
        public async Task<ActivityDto> UpdateActivityAsync(Guid activityId, UpdateActivityDto updateActivityDto)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return null;
            }

            // Update properties
            activity.ActivityName = updateActivityDto.ActivityName ?? activity.ActivityName;
            activity.Description = updateActivityDto.Description ?? activity.Description;

            await _activityRepository.UpdateAsync(activity);
            return _mapper.Map<ActivityDto>(activity);
        }

        // ✅ Delete Activity
        public async Task<bool> DeleteActivityAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return false;
            }

            await _activityRepository.DeleteAsync(activity.Id);
            return true;
        }

    }
}