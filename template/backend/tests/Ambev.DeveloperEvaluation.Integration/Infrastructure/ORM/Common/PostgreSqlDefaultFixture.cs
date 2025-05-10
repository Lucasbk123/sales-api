using DotNet.Testcontainers.Builders;
using Npgsql;
using System.Data.Common;
using Testcontainers.PostgreSql;
using Testcontainers.Xunit;

using Xunit.Abstractions;


public class PostgreSqlDefaultFixture(IMessageSink messageSink)
    : DbContainerFixture<PostgreSqlBuilder, PostgreSqlContainer>((IMessageSink)messageSink)
{
    public override DbProviderFactory DbProviderFactory
        => NpgsqlFactory.Instance;
}

public class PostgreSqlWaitForDatabaseFixture(IMessageSink messageSink)
    : PostgreSqlDefaultFixture(messageSink)
{
    protected override PostgreSqlBuilder Configure(PostgreSqlBuilder builder)
    {
        return builder
            .WithImage("postgres:17.5")
            .WithWaitStrategy(Wait.ForUnixContainer()
            .UntilCommandIsCompleted("pg_isready"));
    }
}