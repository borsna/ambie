﻿using System;
using Windows.Media;

#nullable enable

namespace AmbientSounds.Tools.Uwp;

public class WindowsSystemMediaControls : ISystemMediaControls
{
    private readonly SystemMediaTransportControls _smtc;

    public event EventHandler<SystemMediaControlsButton>? ButtonPressed;

    public WindowsSystemMediaControls()
    {
        _smtc = SystemMediaTransportControls.GetForCurrentView();
        _smtc.ButtonPressed += SmtcButtonPressed;
    }

    public bool IsPlayEnabled
    {
        get => _smtc.IsPlayEnabled;
        set => _smtc.IsPlayEnabled = value;
    }

    public bool IsPauseEnabled
    {
        get => _smtc.IsPauseEnabled;
        set => _smtc.IsPauseEnabled = value;
    }

    public bool IsNextEnabled
    {
        get => _smtc.IsNextEnabled;
        set => _smtc.IsNextEnabled = value;
    }

    public bool IsPreviousEnabled
    {
        get => _smtc.IsPreviousEnabled;
        set => _smtc.IsPreviousEnabled = value;
    }

    public SystemMediaState PlaybackStatus
    {
        get => (SystemMediaState)(int)_smtc.PlaybackStatus;
        set => _smtc.PlaybackStatus = (MediaPlaybackStatus)(int)value;
    }

    public void UpdateDisplay(string title, string artist)
    {
        _smtc.DisplayUpdater.Type = MediaPlaybackType.Music;
        _smtc.DisplayUpdater.MusicProperties.Title = title;
        _smtc.DisplayUpdater.MusicProperties.Artist = artist;
        _smtc.DisplayUpdater.Update();
    }

    private void SmtcButtonPressed(
        SystemMediaTransportControls sender,
        SystemMediaTransportControlsButtonPressedEventArgs args)
    {
        SystemMediaControlsButton pressed = (SystemMediaControlsButton)(int)args.Button;
        ButtonPressed?.Invoke(this, pressed);
    }
}
