using FillGaps.SoundHub.WebApp.Services.Interfaces;

namespace FillGaps.SoundHub.WebApp.BackgroundServices
{
    public class ApiHealthCheckService : BackgroundService
    {
        private readonly ILogger<ApiHealthCheckService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public ApiHealthCheckService(ILogger<ApiHealthCheckService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de Health Check da API iniciado.");

            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var apiClient = scope.ServiceProvider.GetRequiredService<IApiClientService>();

                    _logger.LogInformation("Executando health check para a API... Hora: {time}", DateTimeOffset.Now);

                    await apiClient.ObterPlanosAtivosAsync();

                    _logger.LogInformation("Health check da API concluído com sucesso.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Falha ao executar o health check da API.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de Health Check da API finalizado.");
            await base.StopAsync(stoppingToken);
        }
    }
}
