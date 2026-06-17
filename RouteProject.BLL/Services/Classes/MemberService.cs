using RouteProject.BLL.Services.Interfaces;
using RouteProject.BLL.Services.ViewModels.MemberViewModels;
using RouteProject.DAL.Data.Models;
using RouteProject.DAL.Repositories.Interfaces;

namespace RouteProject.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> memberRepository;
        private readonly IGenericRepository<HealthRecord> healthRecordRepository;
        private readonly IGenericRepository<MemberShip> memberShips;
        private readonly IGenericRepository<Plan> planRepository;
        private readonly IGenericRepository<Booking> bookingRepository;

        public MemberService(IGenericRepository<Member> memberRepository,
            IGenericRepository<HealthRecord> healthRecordRepository,
            IGenericRepository<MemberShip> memberShips,
            IGenericRepository<Plan> planRepository,
            IGenericRepository<Booking> bookingRepository)
        {
            this.memberRepository = memberRepository;
            this.healthRecordRepository = healthRecordRepository;
            this.memberShips = memberShips;
            this.planRepository = planRepository;
            this.bookingRepository = bookingRepository;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var EmailExists = await memberRepository.AnyAsync(m => m.Email == model.Email, ct);
            var PhoneExists = await memberRepository.AnyAsync(m => m.Phone == model.Phone, ct);
            if (EmailExists || PhoneExists)
                return false;

            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                BirthOfDate = model.DateOfBirth,
                Address = new Address
                {
                    Street = model.Street,
                    City = model.City,
                    BuildingNumber = model.BuildingNumber
                },
                HealthRecord = new HealthRecord
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    Note = model.HealthRecordViewModel.Note
                }
            };

            var result = await memberRepository.AddAsync(member);
            return result > 0;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await memberRepository.GetAllAsync(ct: ct);
            if (!members.Any())
                return [];
            var memberViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString(),
            });
            return memberViewModels;
        }

        public async Task<HealthRecordViewModel?> GetHealthRecordAsync(int id, CancellationToken ct = default)
        {
            var record = await healthRecordRepository.GetByIdAsync(id, ct);
            if (record == null)
                return null;
            var healthRecordViewModel = new HealthRecordViewModel
            {
                Height = record.Height,
                Weight = record.Weight,
                BloodType = record.BloodType,
                Note = record.Note
            };
            return healthRecordViewModel;
        }

        public async Task<MemberViewModel?> GetMemberDetails(int id, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(id, ct);
            if (member is null)
                return null;
            var memberviewModel = new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.BirthOfDate.ToShortDateString(),
                Address = $"{member.Address.Street} - {member.Address.BuildingNumber} - {member.Address.City}",
            };
            var Activemembership = await memberShips.FirstorDefaultAsync(m => m.MemberId == id && m.EndDate > DateTime.Now, ct);
            if (Activemembership is not null)
            {
                var Plan = await planRepository.GetByIdAsync(Activemembership.PlanId, ct);
                memberviewModel.PlanName = Plan?.Name;
                memberviewModel.MembershipStartDate = Plan?.CreateAt.ToShortDateString();
                memberviewModel.MembershipEndDate = Plan?.CreateAt.AddDays(Plan.DurationDays).ToShortDateString();
            }
            return memberviewModel;
        }

        public async Task<MemberToUpdateViewModel?> GetMemberToUpdateViewModel(int id, CancellationToken ct = default)
        {
            var Member = await memberRepository.GetByIdAsync(id, ct);
            if (Member is null)
                return null;
            else
            {
                return new MemberToUpdateViewModel
                {
                    Name = Member.Name,
                    Email = Member.Email,
                    Phone = Member.Phone,
                    City = Member.Address.City,
                    Street = Member.Address.Street,
                    BuildingNumber = Member.Address.BuildingNumber
                };
            }

        }

        public async Task<bool> RemoveMemberAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(id, ct);
            if (member is null)
                return false;
            var HasFutureBookings = await bookingRepository.AnyAsync(b => b.MemberId == id && b.Session.EndDate > DateTime.Now, ct: ct);
            if (HasFutureBookings)
                return false;
            var Result = await memberRepository.DeleteAsync(member);
            return Result > 0;

        }

        public async Task<bool?> UpdateMemberAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepository.GetByIdAsync(id, ct);
            if (member is null)
                return null;
            if (await memberRepository.AnyAsync(m => m.Email == model.Email && m.Id != id, ct))
                return false;
            if (await memberRepository.AnyAsync(m => m.Phone == model.Phone && m.Id != id, ct))
                return false;
            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.City = model.City;
            member.Address.Street = model.Street;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.UpdateAt = DateTime.Now;

            var result = await memberRepository.UpdateAsync(member);
            return result > 0;

        }


    }
}
