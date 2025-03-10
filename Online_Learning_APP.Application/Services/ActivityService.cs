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
            var activity = _mapper.Map<Activity>(createActivityDto);
            activity.ActivityId = Guid.NewGuid(); // Generate a new ID
            activity.Id = activity.ActivityId; //If Id is needed.
            activity.ActivityName=activity.ActivityName;
          //  activity.Validate(); // Domain validation

            await _activityRepository.AddAsync(activity);
            return _mapper.Map<ActivityDto>(activity);
        }
    }
}