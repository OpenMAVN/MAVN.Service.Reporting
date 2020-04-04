using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.Reporting.MsSqlRepositories.Repositories
{
    public class TransactionReportRepository : ITransactionReportRepository
    {
        private readonly MsSqlContextFactory<ReportContext> _contextFactory;
        private readonly IMapper _mapper;

        public TransactionReportRepository(
            MsSqlContextFactory<ReportContext> contextFactory,
            IMapper mapper
        )
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task AddAsync(TransactionReport report)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                await context.TransactionReports.AddAsync(_mapper.Map<TransactionReportEntity>(report));
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(string id, TxStatus status)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.TransactionReports.FindAsync(Guid.Parse(id));
                if (entity == null)
                    throw new InvalidOperationException($"Could find operation with id = {id}");

                entity.Status = status.ToString();
                context.Update(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<TransactionReport>> GetPaginatedAsync(
            int skip, int take,
            DateTime from, DateTime to)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var reports = await context.TransactionReports
                    .Where(t => from <= t.Timestamp && t.Timestamp <= to )
                    .OrderByDescending(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .Select(report => _mapper.Map<TransactionReport>(report))
                    .ToListAsync();

                return reports;
            }
        }

        public async Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from, DateTime to, int limit
        )
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var reports = await context.TransactionReports
                    .Where(t => from <= t.Timestamp && t.Timestamp <= to )
                    .OrderByDescending(t => t.Timestamp)
                    .Take(limit)
                    .Select(report => _mapper.Map<TransactionReport>(report))
                    .ToListAsync();

                return reports;
            }
        }
    }
}
