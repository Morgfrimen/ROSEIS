using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkersDataParserLib;

namespace Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SettingApp _settingApp;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _settingApp = serviceProvider.GetRequiredService<SettingApp>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(message: "Проверка настроек приложения");
            _settingApp.CheckSettings();
            Workers workers = new Workers(checkValueDateTime: _settingApp.DateTimeCheck,_logger);
            _logger.LogInformation("Начало работы");
            while (!stoppingToken.IsCancellationRequested)
            {
                //await Task.Run(action: () => { workers.RunWork(); }).ConfigureAwait(true);
                workers.RunWork();
                if (workers.IsParse)
                    Thread.Sleep(new TimeSpan(_settingApp.PeriodTimeSpanDay,0,_settingApp.PeriodTimeSpanSecond));//вырубаем поток нахер, чтоб не грузил проц
                workers.IsParse = false;
            }
        }

     
    }
}