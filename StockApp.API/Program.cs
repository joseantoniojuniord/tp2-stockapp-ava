using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockApp.Infra.IoC;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddInfrastructureAPI(builder.Configuration); 

      
        if (string.IsNullOrEmpty(builder.Configuration["Jwt:Issuer"]) ||
            string.IsNullOrEmpty(builder.Configuration["Jwt:Audience"]) ||
            string.IsNullOrEmpty(builder.Configuration["Jwt:SecretKey"]))
        {
            throw new InvalidOperationException("As configurações de JWT não estão corretamente definidas.");
        }

        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;  
                options.SaveToken = true; 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true,  
                    ValidateLifetime = true,  
                    ValidateIssuerSigningKey = true,  
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],  
                    ValidAudience = builder.Configuration["Jwt:Audience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))  
                };
            });

        builder.Services.AddControllers();  

        builder.Services.AddEndpointsApiExplorer();  
        builder.Services.AddSwaggerGen();

       
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();  
            app.UseSwaggerUI(); 
        }

        app.UseHttpsRedirection();  

      
        app.UseAuthentication();

        
        app.UseAuthorization();

       
        app.UseCors("AllowAll");

        app.MapControllers(); 

        app.Run();  
    }
}
