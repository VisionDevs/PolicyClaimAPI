using AutoMapper;
using PolicyClaimAPI.Models;
using PolicyClaimAPI.DTOs;

namespace PolicyClaimAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Claim, ClaimResponseDto>()
                .ForMember(dest => dest.DocumentCount, 
                    opt => opt.MapFrom(src => src.Documents.Count))
                .ForMember(dest => dest.NoteCount, 
                    opt => opt.MapFrom(src => src.Notes.Count));
            
            CreateMap<CreateClaimDto, Claim>()
                .ForMember(dest => dest.ClaimNumber, 
                    opt => opt.MapFrom(src => GenerateClaimNumber()))
                .ForMember(dest => dest.Status, 
                    opt => opt.MapFrom(src => ClaimStatus.Submitted))
                .ForMember(dest => dest.DateFiled, 
                    opt => opt.MapFrom(src => DateTime.UtcNow));
        }
        
        private string GenerateClaimNumber()
        {
            return $"CLM-{DateTime.UtcNow.Year}-{new Random().Next(1000, 9999)}";
        }
    }
}