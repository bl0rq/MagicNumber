var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.

builder.Services.AddControllers ( );

builder.Services.AddSingleton (
    sp =>
    {
        var server = new MagicNumber.Core.Model.Server ( "redis-17337.c8.us-east-1-4.ec2.cloud.redislabs.com:17337", "m6G1NrbkhzWsjUo1zzSKDgHYKFIN4aCT", new MagicNumber.Core.Model.AvroSerializer ( ) );
        server.Load ( );
        return server;
    } );

var app = builder.Build ( );

// Configure the HTTP request pipeline.

app.UseHttpsRedirection ( );

app.UseAuthorization ( );

app.MapControllers ( );

app.Run ( );
