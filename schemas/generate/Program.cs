using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace generate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // var input = Environment.GetEnvironmentVariable("INPUT");
            // var output = Environment.GetEnvironmentVariable("OUTPUT");

            var input = "/home/jack/projects/xero/invoice-worker/schemas/dist/schema.json";
            var output = "/home/jack/projects/xero/invoice-worker/schemas/dist/Schema.cs";
            var schema = await JsonSchema.FromFileAsync(input);
            var generator = new CSharpGenerator(schema, new CSharpGeneratorSettings()
            {
                Namespace = "InvoiceEvents",
                ClassStyle = CSharpClassStyle.Record,
                SchemaType = SchemaType.JsonSchema,
            });
            var file = generator.GenerateFile();
            await File.WriteAllTextAsync(output!, file);
        }
    }
}
