using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Reductech.EDR.Core;
using Reductech.EDR.Core.Connectors;
using Reductech.EDR.Core.Entities;
using Reductech.EDR.Core.Internal;
using Reductech.EDR.Core.Internal.Errors;

namespace ExampleConnector
{

public sealed class ColorInjector : IConnectorInjection
{
    /// <inheritdoc />
    public Result<IReadOnlyCollection<(string Name, object Context)>, IErrorBuilder>
        TryGetInjectedContexts(SCLSettings settings)
    {
        var colorName =
            settings.Entity.TryGetNestedString(
                "connectors",
                "example",
                ColorSource.KeyName
            );

        if (colorName.HasNoValue)
            return ErrorCode.MissingStepSettingsValue.ToErrorBuilder(
                "example",
                ColorSource.KeyName
            );

        var color = Color.FromName(colorName.Value);

        var list = new List<(string Name, object Context)>()
        {
            { (ColorSource.KeyName, new ColorSource(color)) }
        };

        return list;
    }
}

public sealed record ColorSource(Color Color)
{
    public const string KeyName = nameof(ColorSource);
}

public sealed class GetTestString : CompoundStep<StringStream>
{
    /// <inheritdoc />
    protected override async Task<Result<StringStream, IError>> Run(
        IStateMonad stateMonad,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var context = stateMonad.ExternalContext.TryGetContext<ColorSource>(ColorSource.KeyName);

        if (context.IsFailure)
            return context.ConvertFailure<StringStream>().MapError(x => x.WithLocation(this));

        return new StringStream(TestString + " " + context.Value.Color);
    }

    public const string TestString = "The Color is";

    /// <inheritdoc />
    public override IStepFactory StepFactory { get; } =
        new SimpleStepFactory<GetTestString, StringStream>();
}

}
