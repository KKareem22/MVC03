using RouteProject.BLL.Services.Interfaces;
using RouteProject.BLL.Services.ViewModels.TrainerViewModels;
using RouteProject.DAL.Data.Models;
using RouteProject.DAL.Repositories.Interfaces;

namespace RouteProject.BLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IGenericRepository<Trainer> trainerRepository;
        private readonly IGenericRepository<Session> sessionRepository;

        public TrainerService(IGenericRepository<Trainer> trainerRepository
            , IGenericRepository<Session> sessionRepository)
        {
            this.trainerRepository = trainerRepository;
            this.sessionRepository = sessionRepository;
        }
        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel trainer, CancellationToken ct = default)
        {
            var EmailExist = await trainerRepository.AnyAsync(t => t.Email == trainer.Email, ct);
            var PhoneExist = await trainerRepository.AnyAsync(t => t.Phone == trainer.Phone, ct);
            if (EmailExist || PhoneExist)
                return false;
            var Trainer = new Trainer
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender,
                BirthOfDate = trainer.DateOfBirth,
                Address = new Address
                {
                    Street = trainer.Street,
                    City = trainer.City,
                    BuildingNumber = trainer.BuildingNumber
                },
                Specialties = trainer.Specialties

            };
            var Result = await trainerRepository.AddAsync(Trainer);
            return Result > 0;

        }

        public async Task<IEnumerable<TrainerViewModel>> GetAllTrainersAsync(CancellationToken ct)
        {
            var trainers = await trainerRepository.GetAllAsync(ct: ct);
            if (!trainers.Any())
                return [];
            var trainersViewModel = trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Specialization = t.Specialties.ToString()

            });
            return trainersViewModel;

        }

        public async Task<TrainerViewModel?> GetTrainerDetailsAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetByIdAsync(id, ct);
            if (trainer is null)
                return null;
            var trainerViewModel = new TrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.BirthOfDate.ToShortDateString(),
                Specialization = trainer.Specialties.ToString() + " Trainer",
                Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}"
            };
            return trainerViewModel;
        }

        public async Task<TrainerToUpdateViewModel?> GetTrainerToUpdateViewModel(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetByIdAsync(id, ct);
            if (trainer is null)
                return null;
            var TrainerToUpdate = new TrainerToUpdateViewModel
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                Specialty = trainer.Specialties
            };
            return TrainerToUpdate;
        }

        public async Task<bool> RemoveTrainerAsync(int id, CancellationToken ct)
        {
            var trainer = await trainerRepository.GetByIdAsync(id, ct);
            if (trainer is null)
                return false;
            var hasFutureSessions = await sessionRepository.AnyAsync(s => s.TrainerId == id && s.EndDate > DateTime.Now, ct);
            if (hasFutureSessions)
                return false;
            var Result = await trainerRepository.DeleteAsync(trainer);
            return Result > 0;
        }

        public async Task<bool> UpdateTrainerAsync(int id, TrainerToUpdateViewModel model, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetByIdAsync(id, ct);
            if (trainer is null)
                return false;
            if (await trainerRepository.AnyAsync(t => t.Email == model.Email && t.Id != id, ct))
                return false;
            if (await trainerRepository.AnyAsync(t => t.Phone == model.Phone && t.Id != id, ct))
                return false;
            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.Street = model.Street;
            trainer.Address.City = model.City;
            trainer.UpdateAt = DateTime.Now;
            trainer.Specialties = model.Specialty;
            var Result = await trainerRepository.UpdateAsync(trainer);
            return Result > 0;
        }



    }
}
