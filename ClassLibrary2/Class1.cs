using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace ClassLibrary2;

[Generator]
public class Class2 : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            static (node, _) => node is ClassDeclarationSyntax,
            static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
            ).Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, Execute);
    }

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) tuple)
    {
        string theCode = """
            namespace ClassLibrary1;
            
            public partial class Class1
            {
                public string Prop2 { get; set; }
                public string Prop3 { get; set; }
            }

            """;

        context.AddSource("tourclass.g.cs", theCode);
    }
}
