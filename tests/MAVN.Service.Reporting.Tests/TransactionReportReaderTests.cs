using System;
using System.Threading.Tasks;
using MAVN.Service.Reporting.Domain.Repositories;
using MAVN.Service.Reporting.Domain.Services;
using MAVN.Service.Reporting.DomainServices.Services;
using Moq;
using Xunit;

namespace MAVN.Service.Reporting.Tests
{
    public class TransactionReportReaderTests
    {
        private readonly Mock<ITransactionReportRepository>  _transactionReportRepositoryMock = 
            new Mock<ITransactionReportRepository>();

        [Theory]
        [InlineData(0, 1, "11/1/2019", "12/1/2019")]
        [InlineData(0, 0, "11/1/2019", "12/1/2019")]
        [InlineData(1, 0, "11/1/2019", "12/1/2019")]
        [InlineData(-1, 1, "11/1/2019", "12/1/2019")]
        [InlineData(-1, -1, "11/1/2019", "12/1/2019")]
        [InlineData(1, -1, "11/1/2019", "12/1/2019")]
        public async Task GetAsync_InvalidInputParameters_RaisesException(
            int currentPage, int pageSize,
            DateTime from, DateTime to)
        {
            var sut = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(
                () => sut.GetPaginatedAsync(currentPage, pageSize, from, to, null)
                );
        }
        
        [Theory]
        [InlineData("11/1/2019", "12/1/2019", 0)]
        [InlineData("11/1/2019", "12/1/2019", -1)]
        public async Task GetLimitedAsync_InvalidInputParameters_RaisesException(
            DateTime from, DateTime to, int limit)
        {
            var sut = CreateSutInstance();
            
            await Assert.ThrowsAsync<ArgumentException>(
                () => sut.GetLimitedAsync(from, to, limit, null)
            );
            
        }
        
        [Theory]
        [InlineData("1/1/2000", "1/1/2000", 1)]
        [InlineData("1/1/2000", "1/1/2001", 1)]
        public async Task GetLimitedAsync_ReturnNull_RaisesException(
            DateTime from, DateTime to, int limit)
        {
            var sut = CreateSutInstance();

            var reports = await sut.GetLimitedAsync(from, to, limit, null);
            
            Assert.Null(reports);
        }
        
        private ITransactionReportReader CreateSutInstance()
        {
            return new TransactionReportReader(_transactionReportRepositoryMock.Object);
        }
    }
}
