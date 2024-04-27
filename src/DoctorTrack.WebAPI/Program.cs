using DoctorTrack.CronJob.DoctorTrack.CronJob.Jobs;
using DoctorTrack.Domain.Interfaces;
using DoctorTrack.WebAPI.Services;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ICsvExportService, CsvExportService>();


//hangfire
var hangfireConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(hangfireConnectionString);
    RecurringJob.AddOrUpdate<DoctorTrack.CronJob.Job>(x => x.CsvCronJob(), Cron.Daily());
    RecurringJob.AddOrUpdate<DoctorsJob>(job => job.PrintDoctorsToConsole(), Cron.Daily);
});
builder.Services.AddHangfireServer();



builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard(); //hangfire dashboard

app.MapControllers();

app.Run();
