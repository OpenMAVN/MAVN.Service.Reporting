﻿using Autofac;
using JetBrains.Annotations;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.MsSqlRepositories;
using MAVN.Service.Reporting.MsSqlRepositories.Repositories;
using MAVN.Service.Reporting.Settings;
using Lykke.SettingsReader;
using MAVN.Persistence.PostgreSQL.Legacy;

namespace MAVN.Service.Reporting.Modules
{
    [UsedImplicitly]
    public class DbModule : Module
    {
        private readonly string _connectionString;

        public DbModule(IReloadingManager<AppSettings> appSettings)
        {
            _connectionString = appSettings.CurrentValue.ReportService.Db.SqlDbConnString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(
                _connectionString,
                connString => new ReportContext(connString, false),
                dbConn => new ReportContext(dbConn));
            
            builder.RegisterType<TransactionReportRepository>()
                .As<ITransactionReportRepository>()
                .SingleInstance();
        }
    }
}
