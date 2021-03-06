﻿using System.Threading.Tasks;

namespace Naru.WPF.Dialog
{
    public interface IDialogBuilder<T>
    {
        IDialogBuilder<T> WithDialogType(DialogType dialogType);

        IDialogBuilder<T> WithAnswers(params T[] answers);

        IDialogBuilder<T> WithTitle(string title);

        IDialogBuilder<T> WithMessage(string message);

        T Show();

        Task<T> ShowAsync();
    }
}