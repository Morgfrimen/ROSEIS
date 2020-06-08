using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class SettingApp
    {
        private readonly IConfiguration _configuration;
        private int HoutsDay = 24*5;

        public SettingApp(IConfiguration configuration)
        {
            _configuration = configuration;
            this.DateTimeCheck = _configuration.GetValue<DateTime>(key: nameof(this.DateTimeCheck));
            this.PeriodTimeSpanDay =
                _configuration.GetValue<int>(key: nameof(this.PeriodTimeSpanDay));
            this.PeriodTimeSpanSecond =
                _configuration.GetValue<int>(key: nameof(this.PeriodTimeSpanSecond));
        }

        public SettingApp()
        {
        }

        public SettingApp( DateTime time, int periodTimeSpanDay,int periodTimeSpanSecond)
        {
            this.DateTimeCheck = time;
            this.PeriodTimeSpanDay = periodTimeSpanDay;
            this.PeriodTimeSpanSecond = periodTimeSpanSecond;
        }


        public DateTime DateTimeCheck { get; set; }


        public int PeriodTimeSpanDay { get; set; }

        public int PeriodTimeSpanSecond { get; set; }

        //Если вдруг записи нет
        public async void CheckSettings()
        {
            string patch = Program.NameJSONSettings;
            using (FileStream fs = new FileStream(path: patch, mode: FileMode.OpenOrCreate,
                access: FileAccess.ReadWrite))
            {
                if ( this.DateTimeCheck == default)
                {
                    SettingApp settingApp = new SettingApp(time: DateTime.Now,
                        periodTimeSpanDay: HoutsDay,default);
                    this.DateTimeCheck = settingApp.DateTimeCheck;
                    this.PeriodTimeSpanDay = settingApp.PeriodTimeSpanDay;
                    await JsonSerializer.SerializeAsync<SettingApp>(utf8Json: fs, value: settingApp,
                        options: new JsonSerializerOptions()
                        {
                            WriteIndented = true, AllowTrailingCommas = true
                        });
                }
            }
        }
    }
}