using CBO.Core.Orders.DataAccess.Csc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CBO.Core.Orders.DataAccess.Csc
{
    public class CscOrdersDataAccess : IOrdersDataAccess
    {
        private readonly GVDBContext _cscDb;
        private readonly int _dbTimeout;

        public CscOrdersDataAccess(IConfiguration configuration)
        {
            Int32.TryParse(configuration["DbOptions:TimeoutInSeconds"], out _dbTimeout);

            DbContextOptionsBuilder<GVDBContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:ITS_CSC"],
                sqlServerOptions => sqlServerOptions.CommandTimeout(_dbTimeout));

            _cscDb = new GVDBContext(optionsBuilder.Options);

        }

        public async Task<string> Login(string clientId, string password)
        {
            var login = await _cscDb.Agents
                .FirstOrDefaultAsync
                (
                    g =>
                        g.AgentName == clientId
                        &&
                        g.AgentSecret == password
                );

            return login?.AgentName!;
        }

        public async Task<List<string>> GetMatchingRoles(string featureCode, string clientId)
        {

            var roles = _cscDb.Agents
                .Where
                (
                    g =>
                        g.AgentName == clientId
                        &&
                        g.AgentSecret == featureCode
                )
                .Select
                (
                    g => g.AgentCode
                );

            return await roles.ToListAsync();
        }
    }
}