using AutoMapper;
using Lykke.Service.Reporting.Client;
using Lykke.Service.Reporting.Domain;
using Lykke.Service.Reporting.MsSqlRepositories.Entities;

namespace Lykke.Service.Reporting
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
