using System.Threading;
using System.Threading.Tasks;
using Reductech.EDR.ConnectorManagement.Base;

namespace EDR.Tests;

public class FakeConnectorManager : IConnectorManager
{
    public Task Add(
        string id,
        string? name = null,
        string? version = null,
        bool prerelease = false,
        bool force = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public Task Update(
        string name,
        string? version = null,
        bool prerelease = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public Task Remove(
        string name,
        bool configurationOnly = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public IEnumerable<(string name, ConnectorData data)> List(string? nameFilter = null) =>
        Array.Empty<(string name, ConnectorData data)>();

    public Task<ICollection<ConnectorMetadata>> Find(
        string? search = null,
        bool prerelease = false,
        CancellationToken ct = default) => throw new NotImplementedException();

    public Task<bool> Verify(CancellationToken ct = default) => Task.FromResult(true);
}
