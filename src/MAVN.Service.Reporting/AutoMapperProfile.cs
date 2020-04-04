using AutoMapper;
using MAVN.Service.Reporting.Client;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.MsSqlRepositories.Entities;

namespace MAVN.Service.Reporting
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionReportEntity, Domain.Models.TransactionReport>().ReverseMap();
            CreateMap<Domain.Models.TransactionReport, Client.Models.TransactionReport>();
            CreateMap<Domain.Models.TransactionReportResult, Client.Models.PaginatedReportResult>();
        }
    }
}
