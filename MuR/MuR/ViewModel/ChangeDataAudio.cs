using System;
using System.Windows.Input;

namespace MuR.ViewModel
{
    /// <summary>
    /// команда на изменение данных объекта базы данных
    /// </summary>
    class ChangeDataAudio : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private AudioEditViewModel viewModel;
        public ChangeDataAudio(AudioEditViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                viewModel.UpdateDBItem();
        }
    }
}
