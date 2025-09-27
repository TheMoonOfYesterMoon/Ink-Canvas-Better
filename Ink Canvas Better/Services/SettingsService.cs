using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services.Interfaces;
using Newtonsoft.Json;
using File = System.IO.File;

namespace Ink_Canvas_Better.Services
{
    internal class SettingsService : INotifyPropertyChanged
    {
        private Settings _settings = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public Settings Settings
        {
            get => _settings;
            set => SetField(ref _settings, value);
        }

        public async void LoadData()
        {
            try
            {
                if (File.Exists(App.RootPath + App.SettingsFileName))
                {
                    string raw = await File.ReadAllTextAsync(App.RootPath + App.SettingsFileName);
                    Settings = JsonConvert.DeserializeObject<Settings>(raw) ?? new();
                }
                else
                {
                    Settings = new();
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                // TODO
            }
        }

        public void SaveData()
        {
            try
            {
                string text = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                File.WriteAllText(App.RootPath + App.SettingsFileName, text);
            }
            catch { }
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public void ResetData()
        {
            Settings = new();
            SaveData();
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
