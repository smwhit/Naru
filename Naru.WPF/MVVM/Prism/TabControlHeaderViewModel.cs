﻿using Microsoft.Practices.Prism.Commands;

namespace Naru.WPF.MVVM.Prism
{
    public class TabControlHeaderViewModel : IViewModel
    {
        public IViewModel Header { get; private set; }

        public bool CanClose { get; private set; }

        public DelegateCommand CloseCommand { get; private set; }

        public TabControlHeaderViewModel(ISupportHeader viewModel)
        {
            Header = viewModel.Header;

            var supportClose = viewModel as ISupportClosing;
            if (supportClose == null) return;

            CloseCommand = new DelegateCommand(() => supportClose.Close());
            CanClose = supportClose.CanClose();
        }
    }
}