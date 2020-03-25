using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Service.Reporting.Domain.Repositories;
using Lykke.Service.Reporting.MsSqlRepositories;
using Lykke.Service.Reporting.MsSqlRepositories.Repositories;
using Lykke.Service.Reporting.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.Reporting.Modules
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
            builder.RegisterMsSql(
                _connectionString,
                connString => new ReportContext(connString, false),
                dbConn => new ReportContext(dbConn));
            
            builder.RegisterType<TransactionReportRepository>()
                .As<ITransactionReportRepository>()
                .SingleInstance();
        }
    }
}
