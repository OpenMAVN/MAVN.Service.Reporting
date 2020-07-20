using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.Reporting.Domain;
using MAVN.Service.Reporting.Domain.Models;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.Reporting.MsSqlRepositories.Repositories
{
    public class TransactionReportRepository : ITransactionReportRepository
    {
        private readonly PostgreSQLContextFactory<ReportContext> _contextFactory;
        private readonly IMapper _mapper;

        public TransactionReportRepository(
            PostgreSQLContextFactory<ReportContext> contextFactory,
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
            int skip, int take, DateTime from, DateTime to, string[] partnerIds,
            string transactionType, string status, Guid? campaignId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = GetQuery(context, from, to);

                query = AddFiltersToQuery(query, partnerIds, transactionType, status, campaignId);

                var reports = await query
                    .OrderByDescending(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .Select(report => _mapper.Map<TransactionReport>(report))
                    .ToListAsync();

                return reports;
            }
        }

        public async Task<IReadOnlyList<TransactionReport>> GetLimitedAsync(
            DateTime from, DateTime to, int limit, string[] partnerIds,
            string transactionType, string status, Guid? campaignId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = GetQuery(context, from, to);

                query = AddFiltersToQuery(query, partnerIds, transactionType, status, campaignId);

                var reports = await query
                    .OrderByDescending(t => t.Timestamp)
                    .Take(limit)
                    .Select(report => _mapper.Map<TransactionReport>(report))
                    .ToListAsync();

                return reports;
            }
        }

        private IQueryable<TransactionReportEntity> GetQuery(ReportContext context, DateTime from, DateTime to)
        {
            return context.TransactionReports
                .Where(t => from <= t.Timestamp && t.Timestamp <= to);
        }

        private IQueryable<TransactionReportEntity> AddFiltersToQuery(
             IQueryable<TransactionReportEntity> query, string[] partnerIds,
             string transactionType, string status, Guid? campaignId)
        {
            var shouldFilterByPartners = partnerIds != null && partnerIds.Any();

            if (shouldFilterByPartners)
                query = query.Where(t => partnerIds.Contains(t.PartnerId));

            if (!string.IsNullOrEmpty(transactionType))
                query = query.Where(t => t.TransactionType == transactionType);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status == status);

            if (campaignId.HasValue)
                query = query.Where(t => t.CampaignId == campaignId);

            return query;
        }
    }
}
