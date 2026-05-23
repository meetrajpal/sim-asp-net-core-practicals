namespace Practical18.Infrastructure.Mappers;

public class StudentMapper : Profile
{
    public StudentMapper()
    {
        CreateMap<CreateStudentViewModel, Student>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName)
            )
            .ForMember(
                dest => dest.GRNumber,
                opt => opt.MapFrom(src => src.GRNumber)
            )
            .ForMember(
                dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth)
            );

        CreateMap<UpdateStudentViewModel, Student>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName)
            )
            .ForMember(
                dest => dest.GRNumber,
                opt => opt.MapFrom(src => src.GRNumber)
            )
            .ForMember(
                dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth)
            )
            .ForMember(
                dest => dest.IsActive,
                opt => opt.MapFrom(src => src.IsActive)
            );
    }
}
