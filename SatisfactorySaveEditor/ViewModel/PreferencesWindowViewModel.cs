﻿using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace SatisfactorySaveEditor.ViewModel
{
    public class PreferencesWindowViewModel : ViewModelBase
    {
        private bool canApply;
        private bool autoUpdate;
        private int backupCount;

        public bool AutoUpdate
        {
            get => autoUpdate;
            set
            {
                Set(() => AutoUpdate, ref autoUpdate, value);
                Set(() => CanApply, ref canApply, true);
            }
        }

        public int BackupCount
        {
            get => backupCount;
            set
            {
                Set(() => BackupCount, ref backupCount, value);
                Set(() => CanApply, ref canApply, true);
            }
        }

        public bool CanApply => canApply;

        public RelayCommand<Window> AcceptCommand { get; }
        public RelayCommand ApplyCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }

        public PreferencesWindowViewModel()
        {
            AcceptCommand = new RelayCommand<Window>(Accept);
            ApplyCommand = new RelayCommand(Apply);
            CancelCommand = new RelayCommand<Window>(Cancel);

            autoUpdate = Properties.Settings.Default.AutoUpdate;
        }

        private void Accept(Window window)
        {
            Apply();
            window.Close();
        }

        private void Apply()
        {
            Properties.Settings.Default.AutoUpdate = autoUpdate;

            Properties.Settings.Default.Save();
            Set(() => CanApply, ref canApply, false);
        }

        private void Cancel(Window window)
        {
            window.Close();
        }
    }
}
